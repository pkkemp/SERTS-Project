<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.playBtn = New System.Windows.Forms.Button()
        Me.songProgressBar = New System.Windows.Forms.ProgressBar()
        Me.backBtn = New System.Windows.Forms.Button()
        Me.nextBtn = New System.Windows.Forms.Button()
        Me.currentSongTime = New System.Windows.Forms.Label()
        Me.totalSongLength = New System.Windows.Forms.Label()
        Me.TrackLabel = New System.Windows.Forms.Label()
        Me.TrackNames = New System.Windows.Forms.ListBox()
        Me.SuspendLayout()
        '
        'Label2
        '
        Me.Label2.AccessibleName = "currentTrack"
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(39, 38)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(0, 13)
        Me.Label2.TabIndex = 1
        '
        'playBtn
        '
        Me.playBtn.AccessibleName = "playBtn"
        Me.playBtn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom), System.Windows.Forms.AnchorStyles)
        Me.playBtn.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.playBtn.Location = New System.Drawing.Point(189, 269)
        Me.playBtn.Name = "playBtn"
        Me.playBtn.Size = New System.Drawing.Size(75, 33)
        Me.playBtn.TabIndex = 2
        Me.playBtn.Text = "Play"
        Me.playBtn.UseVisualStyleBackColor = True
        '
        'songProgressBar
        '
        Me.songProgressBar.ForeColor = System.Drawing.Color.Lime
        Me.songProgressBar.Location = New System.Drawing.Point(12, 251)
        Me.songProgressBar.Name = "songProgressBar"
        Me.songProgressBar.Size = New System.Drawing.Size(436, 12)
        Me.songProgressBar.TabIndex = 3
        '
        'backBtn
        '
        Me.backBtn.AccessibleName = "backBtn"
        Me.backBtn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom), System.Windows.Forms.AnchorStyles)
        Me.backBtn.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.backBtn.Location = New System.Drawing.Point(145, 269)
        Me.backBtn.Name = "backBtn"
        Me.backBtn.Size = New System.Drawing.Size(38, 33)
        Me.backBtn.TabIndex = 5
        Me.backBtn.Text = "<"
        Me.backBtn.UseVisualStyleBackColor = True
        '
        'nextBtn
        '
        Me.nextBtn.AccessibleName = "forwardBtn"
        Me.nextBtn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom), System.Windows.Forms.AnchorStyles)
        Me.nextBtn.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nextBtn.Location = New System.Drawing.Point(270, 269)
        Me.nextBtn.Name = "nextBtn"
        Me.nextBtn.Size = New System.Drawing.Size(38, 33)
        Me.nextBtn.TabIndex = 4
        Me.nextBtn.Text = ">"
        Me.nextBtn.UseVisualStyleBackColor = True
        '
        'currentSongTime
        '
        Me.currentSongTime.AutoSize = True
        Me.currentSongTime.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.currentSongTime.ForeColor = System.Drawing.Color.White
        Me.currentSongTime.Location = New System.Drawing.Point(13, 235)
        Me.currentSongTime.Name = "currentSongTime"
        Me.currentSongTime.Size = New System.Drawing.Size(32, 13)
        Me.currentSongTime.TabIndex = 6
        Me.currentSongTime.Text = "0:00"
        '
        'totalSongLength
        '
        Me.totalSongLength.AutoSize = True
        Me.totalSongLength.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.totalSongLength.ForeColor = System.Drawing.Color.White
        Me.totalSongLength.Location = New System.Drawing.Point(416, 235)
        Me.totalSongLength.Name = "totalSongLength"
        Me.totalSongLength.Size = New System.Drawing.Size(32, 13)
        Me.totalSongLength.TabIndex = 7
        Me.totalSongLength.Text = "0:00"
        '
        'TrackLabel
        '
        Me.TrackLabel.AutoSize = True
        Me.TrackLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TrackLabel.ForeColor = System.Drawing.Color.White
        Me.TrackLabel.Location = New System.Drawing.Point(13, 9)
        Me.TrackLabel.Name = "TrackLabel"
        Me.TrackLabel.Size = New System.Drawing.Size(76, 13)
        Me.TrackLabel.TabIndex = 0
        Me.TrackLabel.Text = "Track Name"
        '
        'TrackNames
        '
        Me.TrackNames.FormattingEnabled = True
        Me.TrackNames.Location = New System.Drawing.Point(16, 30)
        Me.TrackNames.Name = "TrackNames"
        Me.TrackNames.Size = New System.Drawing.Size(432, 199)
        Me.TrackNames.TabIndex = 8
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(460, 322)
        Me.Controls.Add(Me.TrackNames)
        Me.Controls.Add(Me.totalSongLength)
        Me.Controls.Add(Me.currentSongTime)
        Me.Controls.Add(Me.backBtn)
        Me.Controls.Add(Me.nextBtn)
        Me.Controls.Add(Me.songProgressBar)
        Me.Controls.Add(Me.playBtn)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TrackLabel)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label2 As Label
    Friend WithEvents playBtn As Button
    Friend WithEvents songProgressBar As ProgressBar
    Friend WithEvents backBtn As Button
    Friend WithEvents nextBtn As Button
    Friend WithEvents currentSongTime As Label
    Friend WithEvents totalSongLength As Label
    Friend WithEvents TrackLabel As Label
    Friend WithEvents TrackNames As ListBox
End Class
