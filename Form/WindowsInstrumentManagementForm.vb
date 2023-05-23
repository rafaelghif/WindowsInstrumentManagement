Imports System.DirectoryServices
Imports System.Management
Imports System.Net
Imports Microsoft.Win32

Public Class WindowsInstrumentManagementForm
    Private Sub DomainTxt_KeyUp(sender As Object, e As KeyEventArgs) Handles DomainTxt.KeyUp
        If e.KeyCode = Keys.Enter Then
            ComputerNameCmb.Enabled = False
            UsernameTxt.Enabled = False
            PasswordTxt.Enabled = False
            ComputerNameCmb.Items.Clear()
            ComputerNameCmb.Text = ""
            UsernameTxt.Text = ""
            PasswordTxt.Text = ""
            SubmitBtn.Enabled = False
            FindComputerNameInDomain()
        End If
    End Sub
    Private Sub FindComputerNameInDomain()
        Dim domainName As String = DomainTxt.Text
        Try
            Using directoryEntry As New DirectoryEntry("LDAP://" + domainName)
                Using directorySearcher As New DirectorySearcher(directoryEntry)
                    directorySearcher.Filter = "(objectClass=computer)"
                    directorySearcher.PropertiesToLoad.Add("name")
                    directorySearcher.Sort.PropertyName = "name"

                    Dim resultCollection As SearchResultCollection = directorySearcher.FindAll()

                    If resultCollection.Count > 0 Then
                        ComputerNameCmb.Enabled = True
                        UsernameTxt.Enabled = True
                        PasswordTxt.Enabled = True
                        ComputerNameCmb.Items.Clear()
                        SubmitBtn.Enabled = True

                        Dim sortedResults = resultCollection.Cast(Of SearchResult)().OrderBy(Function(result) result.Properties("name")(0).ToString())

                        For Each result As SearchResult In sortedResults
                            Dim computerName As String = result.Properties("name")(0).ToString()
                            ComputerNameCmb.Items.Add(computerName)
                        Next
                    Else
                    End If
                End Using
            End Using
        Catch ex As Exception
            ErrorAlert("Error: The server is not found or not operational.", "Server Not Found")
        End Try
    End Sub
    Private Sub SubmitBtn_Click(sender As Object, e As EventArgs) Handles SubmitBtn.Click
        Dim hostComputerName As String = ComputerNameCmb.Text
        Dim domain As String = DomainTxt.Text
        Dim username As String = UsernameTxt.Text
        Dim password As String = PasswordTxt.Text
        Dim isFailHost As Boolean = False
        Dim isFailWmi As Boolean = False
        Dim isFailRemoteRegistry As Boolean = False

        ConnectionLbl.Text = ""
        WmiLbl.Text = ""
        RemoteRegistryLbl.Text = ""

        isFailHost = ConnectHost(hostComputerName)

        If isFailHost Then
            Exit Sub
        End If

        Dim scope As ManagementScope

        Try
            scope = ConnectToWMI(hostComputerName, domain, username, password)
        Catch ex As Exception
            scope = Nothing
            isFailWmi = True
            WmiLbl.Text = "FAIL"
            WmiLbl.ForeColor = Color.Red
            ErrorAlert("Please enable Windows Management Instrumentation (WMI) in Firewall configuration or check your authentication account has permission to access this resource.", "Permission Error")
        End Try

        isFailRemoteRegistry = ConnectToRemoteRegistry(hostComputerName, username, password, domain)

        If isFailHost Or isFailWmi Or isFailRemoteRegistry Then
            Exit Sub
        End If

        Dim query As ObjectQuery
        Dim key As String
        Dim informations As ManagementObjectSearcher

        ' Computer System Information
        Dim computerName As String = Nothing
        Dim computerUsername As String = Nothing
        Dim computerType As String = Nothing
        Dim computerTotalRam As String = Nothing

        key = "Win32_ComputerSystem"
        query = New ObjectQuery($"SELECT * FROM {key}")
        informations = New ManagementObjectSearcher(scope, query)
        For Each information As ManagementObject In informations.Get()
            computerName = information("Name")
            computerType = information("ChassisSKUNumber")
            computerUsername = information("UserName")
            computerTotalRam = information("TotalPhysicalMemory")
            computerTotalRam = $"{CInt(computerTotalRam / 1024 / 1024 / 1024)}"
        Next

        ' Computer System Product Information
        Dim computerProductNumber As String = Nothing
        Dim computerProductVersion As String = Nothing

        key = "Win32_ComputerSystemProduct"
        query = New ObjectQuery($"SELECT * FROM {key}")
        informations = New ManagementObjectSearcher(scope, query)
        For Each information As ManagementObject In informations.Get()
            computerProductNumber = information("Name")
            computerProductVersion = information("Version")
        Next

        ' Operating System Information
        Dim osName As String = Nothing
        Dim osVersion As String = Nothing
        Dim osArchitecture As String = Nothing
        Dim osManufacturer As String = Nothing
        Dim osInstalDate As String = Nothing
        Dim osSerialNumber As String = Nothing

        key = "Win32_OperatingSystem"
        query = New ObjectQuery($"SELECT * FROM {key}")
        informations = New ManagementObjectSearcher(scope, query)
        For Each information As ManagementObject In informations.Get()
            osName = information("Caption")
            osVersion = information("Version")
            osArchitecture = information("OSArchitecture")
            osManufacturer = information("Manufacturer")
            osInstalDate = information("InstallDate").ToString().Substring(0, 8)
            Dim instalYear As String = osInstalDate.Substring(0, 4)
            Dim instalMonth As String = osInstalDate.Substring(4, 2)
            Dim instalDay As String = osInstalDate.Substring(6, 2)
            osInstalDate = $"{instalYear}-{instalMonth}-{instalDay}"
            osSerialNumber = information("SerialNumber")
        Next

        ' Network Adapter Information
        Dim networkCaption As New List(Of String)
        Dim networkName As New List(Of String)
        Dim networkProductName As New List(Of String)
        Dim networkDescription As New List(Of String)
        Dim networkMacAddress As New List(Of String)
        Dim networkManufacturer As New List(Of String)

        key = "Win32_NetworkAdapter"
        query = New ObjectQuery($"SELECT * FROM {key}")
        informations = New ManagementObjectSearcher(scope, query)
        For Each information As ManagementObject In informations.Get()
            If Not information("MACAddress") = "" Then
                networkName.Add(information("Name"))
                networkProductName.Add(information("ProductName"))
                networkCaption.Add(information("Caption"))
                networkDescription.Add(information("Description"))
                networkMacAddress.Add(information("MACAddress"))
                networkManufacturer.Add(information("Manufacturer"))
            End If
        Next

        ' Product Information
        Dim productName As New List(Of String)
        Dim productVersion As New List(Of String)
        Dim productInstallDate As New List(Of String)
        Dim productPublisher As New List(Of String)

        Using impersonationContext As New Impersonation(username, password, domain)
            Dim entry As IPHostEntry = Dns.GetHostEntry(hostComputerName)
            Dim hostName = entry.HostName
            Using registryKey As RegistryKey = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, hostName).OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall")
                If registryKey IsNot Nothing Then
                    Dim subKeyNames As String() = registryKey.GetSubKeyNames()

                    For Each subKeyName As String In subKeyNames
                        Dim subKey As RegistryKey = registryKey.OpenSubKey(subKeyName)

                        Dim displayName As String = subKey.GetValue("DisplayName")?.ToString()
                        If Not String.IsNullOrEmpty(displayName) Then
                            Dim installDate As String = subKey.GetValue("InstallDate")?.ToString()
                            Dim instalDateFormatted As String = If(Not String.IsNullOrEmpty(installDate), $"{installDate.Substring(0, 4)}-{installDate.Substring(4, 2)}-{installDate.Substring(6, 2)}", "")

                            productName.Add(displayName)
                            productVersion.Add(subKey.GetValue("DisplayVersion")?.ToString())
                            productPublisher.Add(subKey.GetValue("Publisher")?.ToString())
                            productInstallDate.Add(instalDateFormatted)
                        End If
                        subKey.Close()
                    Next

                    ' Sort the lists by display name
                    Dim sortedIndices As Integer() = Enumerable.Range(0, productName.Count).OrderBy(Function(i) productName(i)).ToArray()

                    productName = sortedIndices.Select(Function(i) productName(i)).ToList()
                    productVersion = sortedIndices.Select(Function(i) productVersion(i)).ToList()
                    productPublisher = sortedIndices.Select(Function(i) productPublisher(i)).ToList()
                    productInstallDate = sortedIndices.Select(Function(i) productInstallDate(i)).ToList()
                End If
                registryKey.Close()
            End Using
        End Using

        InformationForm.TreeView1.Nodes.Clear()

        Dim rootNode As New TreeNode("Computer Specification")
        InformationForm.TreeView1.Nodes.Add(rootNode)
        rootNode.Expand()

        Dim computerSystemInformationNode As TreeNode = CreateTreeNode("System Information")
        Dim computerSystemProductNode As TreeNode = CreateTreeNode("System Product Information")
        Dim operatingSystemNode As TreeNode = CreateTreeNode("Operating System Information")
        Dim networkAdapterNode As TreeNode = CreateTreeNode("Network Adapter Information")
        Dim productInformationNode As TreeNode = CreateTreeNode("Product Information")

        AddChildNode(rootNode, computerSystemInformationNode)
        AddChildNode(rootNode, computerSystemProductNode)
        AddChildNode(rootNode, operatingSystemNode)
        AddChildNode(rootNode, networkAdapterNode)
        AddChildNode(rootNode, productInformationNode)

        AddChildNode(computerSystemInformationNode, CreateTreeNodeWithValue("Name", computerName))
        AddChildNode(computerSystemInformationNode, CreateTreeNodeWithValue("Username", computerUsername))
        AddChildNode(computerSystemInformationNode, CreateTreeNodeWithValue("Type", computerType))
        AddChildNode(computerSystemInformationNode, CreateTreeNodeWithValue("Total RAM", $"{computerTotalRam}GB"))

        AddChildNode(computerSystemProductNode, CreateTreeNodeWithValue("Product Number", computerProductNumber))
        AddChildNode(computerSystemProductNode, CreateTreeNodeWithValue("Product Version", computerProductVersion))

        AddChildNode(operatingSystemNode, CreateTreeNodeWithValue("Name", osName))
        AddChildNode(operatingSystemNode, CreateTreeNodeWithValue("Version", osVersion))
        AddChildNode(operatingSystemNode, CreateTreeNodeWithValue("Architecture", osArchitecture))
        AddChildNode(operatingSystemNode, CreateTreeNodeWithValue("Manufacturer", osManufacturer))
        AddChildNode(operatingSystemNode, CreateTreeNodeWithValue("Install Date", osInstalDate))
        AddChildNode(operatingSystemNode, CreateTreeNodeWithValue("Serial Number", osSerialNumber))

        For i As Integer = 0 To networkProductName.Count - 1 Step 1
            Dim networkNameNode As TreeNode = CreateTreeNode(networkProductName(i))
            AddChildNode(networkAdapterNode, networkNameNode)
            AddChildNode(networkNameNode, CreateTreeNodeWithValue("Name", networkName(i)))
            AddChildNode(networkNameNode, CreateTreeNodeWithValue("Mac Address", networkMacAddress(i)))
            AddChildNode(networkNameNode, CreateTreeNodeWithValue("Manufacturer", networkManufacturer(i)))
        Next

        For i As Integer = 0 To productName.Count - 1 Step 1
            Dim productNameNode As TreeNode = CreateTreeNode(productName(i))
            AddChildNode(productInformationNode, productNameNode)
            AddChildNode(productNameNode, CreateTreeNodeWithValue("Version", productVersion(i)))
            AddChildNode(productNameNode, CreateTreeNodeWithValue("Publisher", productPublisher(i)))
            AddChildNode(productNameNode, CreateTreeNodeWithValue("Install Date", productInstallDate(i)))
        Next

        InformationForm.ShowDialog()
    End Sub
    Private Function ConnectHost(hostComputerName As String) As Boolean
        If Not My.Computer.Network.Ping(hostComputerName) Then
            ConnectionLbl.Text = "FAIL"
            ConnectionLbl.ForeColor = Color.Red
            ErrorAlert("Connection Failed!", "Network Error")
            Return True
        Else
            ConnectionLbl.Text = "PASS"
            ConnectionLbl.ForeColor = Color.Green
            Return False
        End If
    End Function
    Private Function ConnectToWMI(computerName As String, domain As String, username As String, password As String) As ManagementScope
        Dim remoteUser = $"{domain}\{username}"
        Dim connectionOptions As New ConnectionOptions With {
            .Username = remoteUser,
            .Password = password,
            .Impersonation = ImpersonationLevel.Impersonate,
            .EnablePrivileges = True
        }

        Dim scope As New ManagementScope($"\\{computerName}\root\cimv2", connectionOptions)
        scope.Connect()

        WmiLbl.Text = "PASS"
        WmiLbl.ForeColor = Color.Green

        Return scope
    End Function
    Private Function ConnectToRemoteRegistry(hostComputerName As String, username As String, password As String, domain As String) As Boolean
        Try
            Using impersonationContext As New Impersonation(username, password, domain)
                Dim entry As IPHostEntry = Dns.GetHostEntry(hostComputerName)
                Dim hostName = entry.HostName
                Using registryKey As RegistryKey = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, hostName).OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall")
                    registryKey.Close()
                End Using
                RemoteRegistryLbl.Text = "PASS"
                RemoteRegistryLbl.ForeColor = Color.Green
            End Using
            Return False
        Catch ex As Exception
            RemoteRegistryLbl.Text = "FAIL"
            RemoteRegistryLbl.ForeColor = Color.Red

            If ex.ToString().Contains("The network path was not found") Then
                ErrorAlert("Please enable Remote Registry in services", "Permission Error")
            Else
                ErrorAlert(ex.ToString)
            End If

            Return True
        End Try
    End Function
    Private Function CreateTreeNode(text As String) As TreeNode
        Return New TreeNode(text)
    End Function

    Private Function CreateTreeNodeWithValue(text As String, value As String) As TreeNode
        Return New TreeNode($"{text} : {value}")
    End Function

    Private Sub AddChildNode(parentNode As TreeNode, childNode As TreeNode)
        parentNode.Nodes.Add(childNode)
    End Sub
End Class