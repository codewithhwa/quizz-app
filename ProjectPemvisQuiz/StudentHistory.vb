Imports MySql.Data.MySqlClient

Public Class StudentHistory
    Private studentID As Integer
    Private studentName As String
    Private quizHistory As New DataTable()
    Private questionList As New DataTable()
    Private currentQuizIndex As Integer = 0
    Private currentQuestionIndex As Integer = 0

    Public Sub New(sID As Integer, sName As String)
        InitializeComponent()
        studentID = sID
        studentName = sName
    End Sub


    Private Sub StudentHistory_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadQuizHistory()
    End Sub

    Private Sub LoadQuizHistory()
        Try
            bukaKoneksi()
            Dim query As String = "
                SELECT uq.id_userquiz, q.quiz_title, uq.score, uq.date_taken
                FROM userquiz uq
                JOIN quizcode q ON uq.id_quiz = q.id_quiz
                WHERE uq.id_user = @uid
                ORDER BY uq.date_taken DESC"
            Dim cmd As New MySqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@uid", studentID)
            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(quizHistory)

            If quizHistory.Rows.Count > 0 Then
                ShowQuiz(0)
            Else
                MessageBox.Show("Belum ada riwayat kuis.", "Info")
                Me.Close()
            End If
        Catch ex As Exception
            MessageBox.Show("Gagal memuat riwayat kuis: " & ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub

    Private Sub ShowQuiz(index As Integer)
        Dim row = quizHistory.Rows(index)
        lblQuizTitle.Text = row("quiz_title").ToString()
        Nilai.Text = "Score: " & row("score").ToString()
        Tanggal.Text = "Tanggal: " & Convert.ToDateTime(row("date_taken")).ToString("dd MMM yyyy")

        LoadQuestions(CInt(row("id_userquiz")))
    End Sub

    Private Sub LoadQuestions(userQuizID As Integer)
        Try
            bukaKoneksi()
            Dim query As String = "
                SELECT q.question_text, q.option_a, q.option_b, q.option_c, q.option_d,
                       q.correct_answer, ua.selected_option
                FROM user_answers ua
                JOIN questions q ON ua.id_question = q.id_question
                WHERE ua.id_userquiz = @uq"
            Dim cmd As New MySqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@uq", userQuizID)

            Dim da As New MySqlDataAdapter(cmd)
            questionList.Clear()
            da.Fill(questionList)

            If questionList.Rows.Count > 0 Then
                currentQuestionIndex = 0
                ShowQuestion(currentQuestionIndex)
            End If

        Catch ex As Exception
            MessageBox.Show("Gagal memuat soal: " & ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub

    Private Sub ShowQuestion(i As Integer)
        Dim q = questionList.Rows(i)
        lblQuestionText.Text = q("question_text").ToString()
        RadioButton1.Text = "A. " & q("option_a").ToString()
        RadioButton2.Text = "B. " & q("option_b").ToString()
        RadioButton3.Text = "C. " & q("option_c").ToString()
        RadioButton4.Text = "D. " & q("option_d").ToString()

        RadioButton1.Checked = (q("selected_option").ToString() = "A")
        RadioButton2.Checked = (q("selected_option").ToString() = "B")
        RadioButton3.Checked = (q("selected_option").ToString() = "C")
        RadioButton4.Checked = (q("selected_option").ToString() = "D")
    End Sub

    ' Tombol Next soal
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If currentQuestionIndex < questionList.Rows.Count - 1 Then
            currentQuestionIndex += 1
            ShowQuestion(currentQuestionIndex)
        Else
            MessageBox.Show("Ini soal terakhir.")
        End If
    End Sub

    ' Tombol Back soal
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If currentQuestionIndex > 0 Then
            currentQuestionIndex -= 1
            ShowQuestion(currentQuestionIndex)
        Else
            MessageBox.Show("Ini soal pertama.")
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Beranda.Show()
        Me.Hide()
    End Sub
End Class
