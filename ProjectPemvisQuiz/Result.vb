Imports MySql.Data.MySqlClient

Public Class Result
    Private Sub Result_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadResults()
    End Sub

    ' === Fungsi untuk menampilkan hasil kuis ===
    Private Sub LoadResults()
        Try
            bukaKoneksi()
            Dim query As String =
                "SELECT user.username AS 'Nama Siswa', quizcode.quiz_title AS 'Judul Kuis', userquiz.score AS 'Nilai', userquiz.date_taken AS 'Tanggal' 
                 FROM userquiz 
                 INNER JOIN user ON userquiz.id_user = user.id_user 
                 INNER JOIN quizcode ON userquiz.id_quiz = quizcode.id_quiz 
                 WHERE quizcode.created_by = @guru 
                 ORDER BY userquiz.date_taken DESC"

            Dim cmd As New MySqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@guru", currentUserID)
            Dim da As New MySqlDataAdapter(cmd)
            Dim dt As New DataTable
            da.Fill(dt)
            DataGridView1.DataSource = dt

            ' Set tampilan tabel biar lebih rapi
            DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            DataGridView1.ReadOnly = True
            DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect

            conn.Close()
        Catch ex As Exception
            MessageBox.Show("Gagal memuat hasil: " & ex.Message)
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    ' === Tombol Refresh ===
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        LoadResults()
    End Sub

    ' === Tombol Kembali ===
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Teacher.Show()
        Me.Hide()
    End Sub
End Class
