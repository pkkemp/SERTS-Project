

#include "cmsis_os.h"  // CMSIS RTOS header file
#include "Board_LED.h"
#include "UART_driver.h"
#include "stdint.h"                     // data type definitions
#include "stdio.h"                      // file I/O functions
#include "rl_usb.h"                     // Keil.MDK-Pro::USB:CORE
#include "rl_fs.h"                      // Keil.MDK-Pro::File System:CORE
#include "stm32f4xx_hal.h"
#include "stm32f4_discovery.h"
#include "stm32f4_discovery_audio.h"
#include <stdio.h>
#include <stdlib.h>

/*----------------------------------------------------------------------------
 *      Thread 1 'Thread_Name': Sample thread
 *---------------------------------------------------------------------------*/
 #define NUM_CHAN	2 // number of audio channels
 #define NUM_POINTS 1024 // number of points per channel
 #define BUF_LEN NUM_CHAN*NUM_POINTS // length of the audio buffer

/* Private variables ---------------------------------------------------------*/

// pointer to file type for files on USB device
FILE *f;

void ControlThread (void const *argument);   // thread function
void PlayThread (void const *argument);	// Thread for play/pause

int16_t Audio_Buffer1[BUF_LEN];
int16_t Audio_Buffer2[BUF_LEN];
int16_t Buff[NUM_POINTS];

// Thread Stuff -----------------------------------------
osThreadId tid_ControlThread;                                          // thread id
osThreadId tid_PlayThread;

osThreadDef (ControlThread, osPriorityNormal, 1, 0);                   // thread object
osThreadDef (PlayThread, osPriorityNormal, 1, 0);

//Message Queue 1 Things -----------------------------
osMessageQId mid_MsqQueue;
osMessageQDef (MsgQueue, 1, uint32_t);

//Message Queue for control object Things -----------------------------
osMessageQId mid_ControlMsqQueue;
osMessageQDef (ControlMsgQueue, 1, char[15]);


osSemaphoreDef(Sem);
osSemaphoreId(Sem_id);

uint8_t buf2use = 2;

char names[3][30];

// Action Definitions
#define getFiles 'R'
#define play 'P'
#define pause 'S'

// WAVE file header format
typedef struct WAVHEADER {
	unsigned char riff[4];						// RIFF string
	uint32_t overall_size;				// overall size of file in bytes
	unsigned char wave[4];						// WAVE string
	unsigned char fmt_chunk_marker[4];		// fmt string with trailing null char
	uint32_t length_of_fmt;					// length of the format data
	uint16_t format_type;					// format type. 1-PCM, 3- IEEE float, 6 - 8bit A law, 7 - 8bit mu law
	uint16_t channels;						// no.of channels
	uint32_t sample_rate;					// sampling rate (blocks per second)
	uint32_t byterate;						// SampleRate * NumChannels * BitsPerSample/8
	uint16_t block_align;					// NumChannels * BitsPerSample/8
	uint16_t bits_per_sample;				// bits per sample, 8- 8bits, 16- 16 bits etc
	unsigned char data_chunk_header [4];		// DATA string or FLLR string
	uint32_t data_size;						// NumSamples * NumChannels * BitsPerSample/8 - size of the next chunk that will be read
} WAVHEADER;

void Init_Thread (void) {
	
	// Init LED and UART
	LED_Initialize();
	UART_Init();
	
	// Init threads
	tid_ControlThread = osThreadCreate (osThread(ControlThread), NULL);
	tid_PlayThread = osThreadCreate (osThread(PlayThread), NULL);
	
	// Init Message queues and semaphore
	Sem_id = osSemaphoreCreate(osSemaphore(Sem), 0);
	mid_MsqQueue = osMessageCreate(osMessageQ(MsgQueue), NULL);
	mid_ControlMsqQueue = osMessageCreate(osMessageQ(ControlMsgQueue), NULL);
	
  if (!tid_ControlThread) return;
	if (!tid_PlayThread) return;
	
	LED_On(0);
}

void ControlThread (void const *argument) {
	usbStatus ustatus; // USB driver status variable
	uint8_t drivenum = 0; // Using U0: drive number
	char *drive_name = "U0:"; // USB drive name
	fsStatus fstatus; // file system status variable
	size_t rd;
	uint32_t i;
	static uint8_t rtrn = 0;
	uint8_t rdnum = 1; // read buffer number
	ustatus = USBH_Initialize (drivenum); // initialize the USB Host
	
	fsFileInfo info;
	static FILE *f;
	info.fileID = 0;
	
	char action;

	if (ustatus == usbOK){
		// loop until the device is OK, may be delay from Initialize
		ustatus = USBH_Device_GetStatus (drivenum); // get the status of the USB device
		while(ustatus != usbOK){
			ustatus = USBH_Device_GetStatus (drivenum); // get the status of the USB device
		}
		// initialize the drive
		fstatus = finit (drive_name);
		if (fstatus != fsOK){
			// handle the error, finit didn't work
		} // end if
		// Mount the drive
		fstatus = fmount (drive_name);
		if (fstatus != fsOK){
			// handle the error, fmount didn't work
		} // end if
		// file system and drive are good to go
	} // end if USBH_Initialize

	// initialize the audio output
	rtrn = BSP_AUDIO_OUT_Init(OUTPUT_DEVICE_AUTO, 0x46, 44100);
	if (rtrn != AUDIO_OK)return;
	
	while(1)
	{	
		//Receive Action
		char r_data[2] = {0,0};
		UART_receive(r_data, 1);
		
		action = getFiles;
		
		if(action == getFiles)
		{
			int index = 0;
			if(!strcmp(r_data,"R")){
				while (ffind ("U0:*.*", &info) == fsOK) 
				{ 
					UART_send(info.name,strlen(info.name));
					strcpy(names[index], info.name);
					UART_send("\n\r",2);
					index++;
				}
				UART_send("E",1);
			}
		}
		else if(action == play)
		{
			UART_send("P/n",2);
			char songName[15];
			UART_receive(songName, 1);
			osMessagePut(mid_ControlMsqQueue, (uint32_t)songName, osWaitForever);
		}
		else if(action == pause)
		{
			osMessagePut(mid_ControlMsqQueue, (uint32_t)'S', osWaitForever);
		}
	}
}

void PlayThread (void const *argument) 
{
	WAVHEADER header;
	
	while(1)
	{
		osEvent event;
		event = osMessageGet(mid_ControlMsqQueue, 0);
		
		if(event.status == osEventMessage)
		{
			if(event.value.v != (uint32_t)'S')
			{
				f = fopen (names[event.value.v],"r"); // open a file on the USB device
				if (f != NULL) {
					fread((void *)&header, sizeof(header), 1, f);
				} // end if file opened
					
				fread((void *)Audio_Buffer1, BUF_LEN, 1, f);
				BSP_AUDIO_OUT_Play((uint16_t *)Audio_Buffer1, 2*BUF_LEN*2);

				while(!feof(f))
				{
					if(buf2use == 1)
					{
						fread((void *)Audio_Buffer1, 2*BUF_LEN, 1, f);
						osMessagePut(mid_MsqQueue, buf2use, osWaitForever);
						buf2use = 2;
					}
					else
					{
						fread((void *)Audio_Buffer2, 2*BUF_LEN, 1, f);
						osMessagePut(mid_MsqQueue, buf2use, osWaitForever);
						buf2use = 1;
					}
					osSemaphoreWait(Sem_id, osWaitForever);
				}
				BSP_AUDIO_OUT_SetMute(AUDIO_MUTE_ON);
			}
		}
		else
		{
			osThreadYield();
		}
	}
}

/* User Callbacks: user has to implement these functions in his code if they are needed. */

/* This function is called when the requested data has been completely transferred. */
void  BSP_AUDIO_OUT_TransferComplete_CallBack(void){
	osEvent event;
	event = osMessageGet(mid_MsqQueue, 0);

	if(event.status == osEventMessage)
	{
		if(event.value.v == 1)
		{
			BSP_AUDIO_OUT_ChangeBuffer((uint16_t*)Audio_Buffer1, BUF_LEN);
		}
		else
		{
			BSP_AUDIO_OUT_ChangeBuffer((uint16_t*)Audio_Buffer2, BUF_LEN);
		}
	}
	osSemaphoreRelease(Sem_id);
}

/* This function is called when half of the requested buffer has been transferred. */
void    BSP_AUDIO_OUT_HalfTransfer_CallBack(void){
}

/* This function is called when an Interrupt due to transfer error on or peripheral
   error occurs. */
void    BSP_AUDIO_OUT_Error_CallBack(void){
		while(1){
		}
}

// end Thread
