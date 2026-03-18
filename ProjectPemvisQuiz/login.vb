Imports MySql.Data.MySqlClient

Public Class login
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.Text.Trim() = "" Or TextBox2.Text.Trim() = "" Then
            MessageBox.Show("Username dan Password wajib diisi!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Try
            bukaKoneksi()
            Dim cmd As New MySqlCommand("SELECT * FROM `user` WHERE username=@user AND password=@pass", conn)
            cmd.Parameters.AddWithValue("@user", TextBox1.Text.Trim())
            cmd.Parameters.AddWithValue("@pass", TextBox2.Text.Trim())

            Dim reader As MySqlDataReader = cmd.ExecuteReader()

            If reader.Read() Then
                currentUserID = reader("id_user")
                currentUsername = reader("username").ToString()
                currentRole = If(IsDBNull(reader("role")), "", reader("role").ToString().ToLower())

                reader.Close()
                conn.Close()

                MessageBox.Show($"Halo {currentUsername} (Role: {currentRole})", "Login Berhasil", MessageBoxButtons.OK, MessageBoxIcon.Information)

                Select Case currentRole
                    Case "teacher"
                        Teacher.Show()
                    Case "student"
                        Dim f As New Beranda(currentUserID, currentUsername)
                        f.Show()
                        Me.Hide()
                    Case Else
                        home.Show()
                End Select

                Me.Hide()
            Else
                MessageBox.Show("Username atau Password salah!", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error)
                reader.Close()
                conn.Close()
            End If

        Catch ex As Exception
            MessageBox.Show("Terjadi kesalahan: " & ex.Message)
            If conn IsNot Nothing AndAlso conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub
End Class
