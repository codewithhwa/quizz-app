Imports MySql.Data.MySqlClient

Public Class formDoquiz
    ' Gunakan koneksi dari Module1, tidak perlu buat ulang.
    ' Jadi hapus: Private conn As New MySqlConnection(...)

    Private quizId As Integer
    Private questions As New List(Of DataRow)
    Private currentIndex As Integer = 0
    Private answers As New List(Of String)

    ' Konstruktor, dipanggil dari Beranda.vb
    Public Sub New(qId As Integer, qTitle As String)
        InitializeComponent()
        quizId = qId
        lblQuizTitle.Text = qTitle
    End Sub

    ' === Saat form pertama kali dibuka ===
    Private Sub formDoquiz_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            bukaKoneksi()
            Dim da As New MySqlDataAdapter("SELECT * FROM questions WHERE id_quiz=@id", conn)
            da.SelectCommand.Parameters.AddWithValue("@id", quizId)
            Dim dt As New DataTable
            da.Fill(dt)

            If dt.Rows.Count = 0 Then
                MessageBox.Show("Soal untuk kuis ini belum tersedia.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.Close()
                Exit Sub
            End If

            questions = dt.AsEnumerable().ToList()
            answers = Enumerable.Repeat("", questions.Count).ToList()
            ShowQuestion(0)
        Catch ex As Exception
            MessageBox.Show("Gagal memuat soal: " & ex.Message)
        End Try
    End Sub

    ' === Menampilkan soal ke layar ===
    Private Sub ShowQuestion(i As Integer)
        Dim q = questions(i)
        lblQuestionNumber.Text = $"Soal {i + 1} dari {questions.Count}"
        lblQuestionText.Text = q("question_text").ToString()
        RadioButton1.Text = "A. " & q("option_a").ToString()
        RadioButton2.Text = "B. " & q("option_b").ToString()
        RadioButton3.Text = "C. " & q("option_c").ToString()
        RadioButton4.Text = "D. " & q("option_d").ToString()

        ' Reset pilihan sebelumnya
        RadioButton1.Checked = False
        RadioButton2.Checked = False
        RadioButton3.Checked = False
        RadioButton4.Checked = False

        ' Jika sudah pernah menjawab, tampilkan jawabannya kembali
        Select Case answers(i)
            Case "A" : RadioButton1.Checked = True
            Case "B" : RadioButton2.Checked = True
            Case "C" : RadioButton3.Checked = True
            Case "D" : RadioButton4.Checked = True
        End Select
    End Sub

    ' === Ambil pilihan jawaban siswa ===
    Private Function GetSelectedAnswer() As String
        If RadioButton1.Checked Then Return "A"
        If RadioButton2.Checked Then Return "B"
        If RadioButton3.Checked Then Return "C"
        If RadioButton4.Checked Then Return "D"
        Return ""
    End Function

    ' === Tombol SUBMIT (kumpulkan jawaban) ===
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        answers(currentIndex) = GetSelectedAnswer()

        Dim score As Integer = 0
        For i = 0 To questions.Count - 1
            If answers(i) = questions(i)("correct_answer").ToString() Then
                score += 1
            End If
        Next

        Try
            conn.Open()

            ' === Simpan ke tabel userquiz ===
            Dim cmdQuiz As New MySqlCommand("INSERT INTO userquiz (id_user, id_quiz, score, date_taken) VALUES (@user,@quiz,@score, NOW())", conn)
            cmdQuiz.Parameters.AddWithValue("@user", currentUserID)
            cmdQuiz.Parameters.AddWithValue("@quiz", quizId)
            cmdQuiz.Parameters.AddWithValue("@score", score)
            cmdQuiz.ExecuteNonQuery()

            ' Ambil ID userquiz terakhir
            Dim userQuizID As Integer = CInt(cmdQuiz.LastInsertedId)

            ' === Simpan semua jawaban siswa ===
            For i = 0 To questions.Count - 1
                Dim cmdAnswer As New MySqlCommand("INSERT INTO user_answers (id_userquiz, id_question, selected_option) VALUES (@uq, @q, @ans)", conn)
                cmdAnswer.Parameters.AddWithValue("@uq", userQuizID)
                cmdAnswer.Parameters.AddWithValue("@q", questions(i)("id_question"))
                cmdAnswer.Parameters.AddWithValue("@ans", answers(i))
                cmdAnswer.ExecuteNonQuery()
            Next

            MessageBox.Show($"Kuis selesai! Skor kamu: {score}/{questions.Count}", "Hasil", MessageBoxButtons.OK, MessageBoxIcon.Information)

            Dim home As New Beranda(currentUserID, currentUserName)
            home.Show()
            Me.Close()

        Catch ex As Exception
            MessageBox.Show("Gagal menyimpan hasil: " & ex.Message)
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub


    ' === Tombol Next Soal ===
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        answers(currentIndex) = GetSelectedAnswer()

        If currentIndex < questions.Count - 1 Then
            currentIndex += 1
            ShowQuestion(currentIndex)
        Else
            MessageBox.Show("Ini soal terakhir. Klik Submit untuk menyelesaikan kuis.")
        End If
    End Sub
End Class
