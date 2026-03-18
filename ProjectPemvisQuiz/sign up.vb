Imports MySql.Data.MySqlClient

Public Class Form1
    Private conn As MySqlConnection
    Private connStr As String = "server=localhost;userid=root;password=;database=db_quiz"

    Private Sub koneksi()
        Try
            conn = New MySqlConnection(connStr)
            If conn.State = ConnectionState.Closed Then conn.Open()
        Catch ex As Exception
            MessageBox.Show("Gagal membuka koneksi: " & ex.Message)
        End Try
    End Sub

    ' Tombol Sign Up
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Or TextBox3.Text = "" Then
            MessageBox.Show("Username dan Password wajib diisi!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        koneksi()

        ' Cek apakah username sudah ada
        Dim checkCmd As New MySqlCommand("SELECT * FROM user WHERE username=@user", conn)
        checkCmd.Parameters.AddWithValue("@user", TextBox1.Text)
        Dim reader As MySqlDataReader = checkCmd.ExecuteReader()

        If reader.HasRows Then
            MessageBox.Show("Username sudah digunakan!", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error)
            reader.Close()
            conn.Close()
            Exit Sub
        End If
        reader.Close()

        ' Role default = none (belum pilih)
        Dim role As String = "none"

        ' Insert data user baru
        Dim insertCmd As New MySqlCommand("INSERT INTO user (username, email, password, role) VALUES (@user, @mail, @pass, @role)", conn)
        insertCmd.Parameters.AddWithValue("@user", TextBox1.Text)
        insertCmd.Parameters.AddWithValue("@mail", TextBox2.Text)
        insertCmd.Parameters.AddWithValue("@pass", TextBox3.Text)
        insertCmd.Parameters.AddWithValue("@role", role)
        insertCmd.ExecuteNonQuery()

        MessageBox.Show("Akun berhasil dibuat! Silakan login dan pilih role kamu di halaman berikutnya.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)
        conn.Close()

        ' Setelah daftar, arahkan ke form login
        login.Show()
        Me.Hide()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        login.Show()
        Me.Hide()
    End Sub
End Class
