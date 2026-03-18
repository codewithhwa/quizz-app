Imports MySql.Data.MySqlClient

Module Module1
    ' === Variabel koneksi global ===
    Public conn As MySqlConnection

    ' === Variabel global user yang sedang login ===
    Public currentUserID As Integer
    Public currentUserName As String
    Public currentRole As String

    ' === Fungsi koneksi ke database ===
    Public Sub bukaKoneksi()
        Try

            If conn Is Nothing Then
                conn = New MySqlConnection("server=localhost;user id=root;password=;database=db_quiz;")
            End If
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If

        Catch ex As Exception
            MessageBox.Show("Koneksi gagal: " & ex.Message)
        End Try
    End Sub
End Module
