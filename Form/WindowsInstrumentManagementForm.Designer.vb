<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class WindowsInstrumentManagementForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(WindowsInstrumentManagementForm))
        Me.DomainTxt = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.UsernameTxt = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.PasswordTxt = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.SubmitBtn = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ComputerNameCmb = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.ConnectionLbl = New System.Windows.Forms.Label()
        Me.WmiLbl = New System.Windows.Forms.Label()
        Me.RemoteRegistryLbl = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'DomainTxt
        '
        Me.DomainTxt.Font = New System.Drawing.Font("Source Sans Pro", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DomainTxt.Location = New System.Drawing.Point(15, 32)
        Me.DomainTxt.Name = "DomainTxt"
        Me.DomainTxt.Size = New System.Drawing.Size(374, 28)
        Me.DomainTxt.TabIndex = 3
        Me.DomainTxt.Text = "YMBYKGW"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Source Sans Pro", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(144, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(117, 20)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Remote Domain"
        '
        'UsernameTxt
        '
        Me.UsernameTxt.Enabled = False
        Me.UsernameTxt.Font = New System.Drawing.Font("Source Sans Pro", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsernameTxt.Location = New System.Drawing.Point(15, 155)
        Me.UsernameTxt.Name = "UsernameTxt"
        Me.UsernameTxt.Size = New System.Drawing.Size(187, 28)
        Me.UsernameTxt.TabIndex = 5
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Source Sans Pro", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(18, 132)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(133, 20)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Remote Username"
        '
        'PasswordTxt
        '
        Me.PasswordTxt.Enabled = False
        Me.PasswordTxt.Font = New System.Drawing.Font("Source Sans Pro", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PasswordTxt.Location = New System.Drawing.Point(217, 155)
        Me.PasswordTxt.Name = "PasswordTxt"
        Me.PasswordTxt.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.PasswordTxt.Size = New System.Drawing.Size(172, 28)
        Me.PasswordTxt.TabIndex = 7
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Source Sans Pro", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(220, 132)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(129, 20)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Remote Password"
        '
        'SubmitBtn
        '
        Me.SubmitBtn.Enabled = False
        Me.SubmitBtn.Font = New System.Drawing.Font("Source Sans Pro", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SubmitBtn.Location = New System.Drawing.Point(137, 200)
        Me.SubmitBtn.Name = "SubmitBtn"
        Me.SubmitBtn.Size = New System.Drawing.Size(143, 29)
        Me.SubmitBtn.TabIndex = 8
        Me.SubmitBtn.Text = "Submit"
        Me.SubmitBtn.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Source Sans Pro", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(142, 69)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(119, 20)
        Me.Label1.TabIndex = 10
        Me.Label1.Text = "Computer Name"
        '
        'ComputerNameCmb
        '
        Me.ComputerNameCmb.Enabled = False
        Me.ComputerNameCmb.Font = New System.Drawing.Font("Source Sans Pro", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.ComputerNameCmb.FormattingEnabled = True
        Me.ComputerNameCmb.Location = New System.Drawing.Point(15, 92)
        Me.ComputerNameCmb.Name = "ComputerNameCmb"
        Me.ComputerNameCmb.Size = New System.Drawing.Size(374, 27)
        Me.ComputerNameCmb.TabIndex = 11
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Source Sans Pro", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(18, 264)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(87, 20)
        Me.Label5.TabIndex = 12
        Me.Label5.Text = "Connection"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Source Sans Pro", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(18, 294)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(38, 20)
        Me.Label6.TabIndex = 13
        Me.Label6.Text = "WMI"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Source Sans Pro", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(18, 324)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(118, 20)
        Me.Label7.TabIndex = 14
        Me.Label7.Text = "Remote Registry"
        '
        'ConnectionLbl
        '
        Me.ConnectionLbl.Font = New System.Drawing.Font("Source Sans Pro", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ConnectionLbl.Location = New System.Drawing.Point(174, 264)
        Me.ConnectionLbl.Name = "ConnectionLbl"
        Me.ConnectionLbl.Size = New System.Drawing.Size(87, 20)
        Me.ConnectionLbl.TabIndex = 15
        '
        'WmiLbl
        '
        Me.WmiLbl.Font = New System.Drawing.Font("Source Sans Pro", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.WmiLbl.Location = New System.Drawing.Point(174, 294)
        Me.WmiLbl.Name = "WmiLbl"
        Me.WmiLbl.Size = New System.Drawing.Size(87, 20)
        Me.WmiLbl.TabIndex = 16
        '
        'RemoteRegistryLbl
        '
        Me.RemoteRegistryLbl.Font = New System.Drawing.Font("Source Sans Pro", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RemoteRegistryLbl.Location = New System.Drawing.Point(174, 324)
        Me.RemoteRegistryLbl.Name = "RemoteRegistryLbl"
        Me.RemoteRegistryLbl.Size = New System.Drawing.Size(87, 20)
        Me.RemoteRegistryLbl.TabIndex = 17
        '
        'WindowsInstrumentManagementForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(409, 366)
        Me.Controls.Add(Me.RemoteRegistryLbl)
        Me.Controls.Add(Me.WmiLbl)
        Me.Controls.Add(Me.ConnectionLbl)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.ComputerNameCmb)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.SubmitBtn)
        Me.Controls.Add(Me.PasswordTxt)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.UsernameTxt)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.DomainTxt)
        Me.Controls.Add(Me.Label2)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "WindowsInstrumentManagementForm"
        Me.Text = "Windows Intrument Management"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DomainTxt As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents UsernameTxt As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents PasswordTxt As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents SubmitBtn As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents ComputerNameCmb As ComboBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents ConnectionLbl As Label
    Friend WithEvents WmiLbl As Label
    Friend WithEvents RemoteRegistryLbl As Label
End Class
