Imports System.Runtime.InteropServices
Imports System.Security.Permissions
Imports System.Security.Principal
Public Class Impersonation
    Implements IDisposable

    Private ReadOnly tokenHandle As New IntPtr(0)
    Private ReadOnly impersonatedUser As WindowsImpersonationContext

    ' Constants for Win32 API calls
    Private Const LOGON32_LOGON_INTERACTIVE As Integer = 2
    Private Const LOGON32_PROVIDER_DEFAULT As Integer = 0

    ' Import the LogonUser function from the Advapi32 DLL
    <DllImport("advapi32.dll", SetLastError:=True, CharSet:=CharSet.Unicode)>
    Private Shared Function LogonUser(lpszUsername As String, lpszDomain As String, lpszPassword As String, dwLogonType As Integer, dwLogonProvider As Integer, ByRef phToken As IntPtr) As Boolean
    End Function

    <PermissionSet(SecurityAction.Demand, Name:="FullTrust")>
    Public Sub New(username As String, password As String, domain As String)
        Dim logonSuccess As Boolean = LogonUser(username, domain, password, LOGON32_LOGON_INTERACTIVE, LOGON32_PROVIDER_DEFAULT, tokenHandle)
        If Not logonSuccess Then
            Throw New ComponentModel.Win32Exception(Marshal.GetLastWin32Error())
        End If

        Dim newId As New WindowsIdentity(tokenHandle)
        impersonatedUser = newId.Impersonate()
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        impersonatedUser?.Dispose()

        If tokenHandle <> IntPtr.Zero Then
            CloseHandle(tokenHandle)
        End If
    End Sub

    ' Import the CloseHandle function from the Kernel32 DLL
    <DllImport("kernel32.dll", SetLastError:=True)>
    Private Shared Function CloseHandle(hObject As IntPtr) As Boolean
    End Function
End Class
