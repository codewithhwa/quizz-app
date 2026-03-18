Imports MySql.Data.MySqlClient
Imports System.Data

Public Class editquiz
    Private quizId As Integer
    Private quizTitle As String
    Private dt As New DataTable()
    Private da As MySqlDataAdapter
    Private connStr As String = "server=localhost;user id=root;password=;database=db_quiz;"

    ' Konstruktor menerima id_quiz dan quiz title
    Public Sub New(qId As Integer, qTitle As String)
        InitializeComponent()
        quizId = qId
        quizTitle = qTitle
    End Sub

    Private Sub editquiz_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = $"Edit Quiz - {quizTitle}"
        TextBox1.ReadOnly = True ' TextBox1 menampilkan PIN (tidak diubah di sini)
        LoadQuizData()
    End Sub

    Private Sub LoadQuizData()
        Try
            ' Ambil PIN / info kuis
            bukaKoneksi()
            Dim cmdInfo As New MySqlCommand("SELECT quiz_code, quiz_title FROM quizcode WHERE id_quiz=@id", conn)
            cmdInfo.Parameters.AddWithValue("@id", quizId)
            Dim rd As MySqlDataReader = cmdInfo.ExecuteReader()
            If rd.Read() Then
                TextBox1.Text = rd("quiz_code").ToString()
                ' jika ingin menampilkan title di form: lblTitle.Text = rd("quiz_title").ToString()
            End If
            rd.Close()

            ' Ambil data questions ke DataTable dan bind ke DataGridView1
            Dim sql As String = "SELECT id_question, question_text, option_a, option_b, option_c, option_d, correct_answer FROM questions WHERE id_quiz=@id"
            da = New MySqlDataAdapter(sql, conn)
            da.SelectCommand.Parameters.AddWithValue("@id", quizId)

            ' Build commands (Insert/Update/Delete) otomatis
            Dim cb As New MySqlCommandBuilder(da)
            da.MissingSchemaAction = MissingSchemaAction.AddWithKey

            dt.Clear()
            da.Fill(dt)

            ' Bind ke DataGridView
            DataGridView1.DataSource = dt

            ' Sesuaikan header dan kolom
            With DataGridView1
                .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                .ReadOnly = False
                .AllowUserToAddRows = True  ' mengizinkan menambah soal baru langsung di grid
                .AllowUserToDeleteRows = True
                ' Sembunyikan id_question kalau mau
                If .Columns.Contains("id_question") Then
                    .Columns("id_question").ReadOnly = True
                    .Columns("id_question").HeaderText = "ID"
                End If
                If .Columns.Contains("question_text") Then .Columns("question_text").HeaderText = "Soal"
                If .Columns.Contains("option_a") Then .Columns("option_a").HeaderText = "Pil A"
                If .Columns.Contains("option_b") Then .Columns("option_b").HeaderText = "Pil B"
                If .Columns.Contains("option_c") Then .Columns("option_c").HeaderText = "Pil C"
                If .Columns.Contains("option_d") Then .Columns("option_d").HeaderText = "Pil D"
                If .Columns.Contains("correct_answer") Then .Columns("correct_answer").HeaderText = "Kunci (A/B/C/D)"
            End With

        Catch ex As Exception
            MessageBox.Show("Gagal memuat data kuis: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    ' Tombol SIMPAN PERUBAHAN (misal Button5)
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            ' Validasi sederhana: cek data kosong / kunci valid sebelum update
            For Each row As DataRow In dt.Rows
                If row.RowState = DataRowState.Deleted Then Continue For
                If row("question_text") Is DBNull.Value OrElse String.IsNullOrWhiteSpace(row("question_text").ToString()) Then
                    MessageBox.Show("Terdapat soal kosong. Isi semua kolom 'Soal' atau hapus baris yang kosong terlebih dahulu.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If
                If row("correct_answer") Is DBNull.Value OrElse String.IsNullOrWhiteSpace(row("correct_answer").ToString()) Then
                    MessageBox.Show("Pastikan setiap soal memiliki kunci jawaban (A/B/C/D).", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If
                Dim k As String = row("correct_answer").ToString().Trim().ToUpper()
                If Not {"A", "B", "C", "D"}.Contains(k) Then
                    MessageBox.Show("Kunci jawaban harus A, B, C, atau D.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If
            Next

            ' Simpan perubahan ke DB
            bukaKoneksi()
            da.Update(dt) ' CommandBuilder sudah membuat Insert/Update/Delete
            MessageBox.Show("Perubahan soal berhasil disimpan.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' Reload untuk memastikan id_question yang baru terisi
            LoadQuizData()
        Catch ex As Exception
            MessageBox.Show("Gagal menyimpan perubahan: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    ' Tombol HAPUS SELECTED ROW (opsional, jika kamu punya tombol delete)
    Private Sub btnDeleteSelected_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If DataGridView1.SelectedRows.Count = 0 Then
            MessageBox.Show("Pilih baris yang ingin dihapus.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        If MessageBox.Show("Yakin ingin menghapus baris terpilih?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            For Each r As DataGridViewRow In DataGridView1.SelectedRows
                If Not r.IsNewRow Then
                    DataGridView1.Rows.Remove(r)
                End If
            Next
        End If
    End Sub

    ' Tombol HOME / Kembali (Button4)
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Teacher.Show()
        Me.Close()
    End Sub
End Class
