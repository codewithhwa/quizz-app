Imports MySql.Data.MySqlClient

Public Class home
    Private conn As MySqlConnection
    Private connStr As String = "server=localhost;userid=root;password=;database=db_quiz"

    Private Sub koneksi()
        conn = New MySqlConnection(connStr)
        If conn.State = ConnectionState.Closed Then conn.Open()
    End Sub

    Private Sub home_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Tampilkan nama user jika sudah login
        Try
            If String.IsNullOrEmpty(currentUsername) Then
                Label2.Text = "Welcome, Guest"
            Else
                Label1.Text = "Welcome, " & currentUsername
            End If
        Catch ex As Exception
            Label1.Text = "Welcome"
        End Try
    End Sub

    ' =============================
    ' Tombol: pilih sebagai Student
    ' =============================
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If String.IsNullOrEmpty(currentUsername) Then
            MessageBox.Show("Kamu belum login!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Try
            koneksi()
            Dim cmd As New MySqlCommand("UPDATE user SET role='student' WHERE username=@user", conn)
            cmd.Parameters.AddWithValue("@user", currentUsername)
            cmd.ExecuteNonQuery()
            conn.Close()

            currentRole = "student"
            MessageBox.Show("Role kamu diset sebagai Student.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)

            Beranda.Show()
            Me.Hide()
        Catch ex As Exception
            MessageBox.Show("Gagal menyimpan role ke database: " & ex.Message)
        End Try
    End Sub

    ' =============================
    ' Tombol: pilih sebagai Teacher
    ' =============================
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If String.IsNullOrEmpty(currentUsername) Then
            MessageBox.Show("Kamu belum login!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Try
            koneksi()
            Dim cmd As New MySqlCommand("UPDATE user SET role='teacher' WHERE username=@user", conn)
            cmd.Parameters.AddWithValue("@user", currentUsername)
            cmd.ExecuteNonQuery()
            conn.Close()

            currentRole = "teacher"
            MessageBox.Show("Role kamu diset sebagai Teacher.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)

            Teacher.Show()
            Me.Hide()
        Catch ex As Exception
            MessageBox.Show("Gagal menyimpan role ke database: " & ex.Message)
        End Try
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        ' optional: klik logo
    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click
        ' optional: klik label
    End Sub
End Class
