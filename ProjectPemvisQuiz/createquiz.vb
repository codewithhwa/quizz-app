Imports MySql.Data.MySqlClient

Public Class createquiz
    Private questions As New List(Of String)
    Private currentIndex As Integer = 0
    Private conn As New MySqlConnection("server=localhost;user id=root;password=;database=db_quiz;")

    ' === Membuat PIN acak untuk kuis ===
    Private Function GeneratePin() As String
        Dim rand As New Random()
        Return rand.Next(100000, 999999).ToString()
    End Function

    Private Sub createquiz_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UpdateForm()
    End Sub

    ' === Tombol NEXT (Button6) ===
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        SaveCurrentQuestion()
        currentIndex += 1
        If currentIndex >= questions.Count Then
            questions.Add("") ' tambah slot soal baru
        End If
        UpdateForm()
    End Sub

    ' === Tombol BACK (Button7) ===
    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        SaveCurrentQuestion()
        If currentIndex > 0 Then
            currentIndex -= 1
            UpdateForm()
        Else
            MessageBox.Show("Ini soal pertama!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    ' === Tombol SAVE ALL (Button5) ===
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        SaveCurrentQuestion()

        ' Validasi
        If TextBox6.Text.Trim() = "" Then
            MessageBox.Show("Masukkan nama kuis terlebih dahulu!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If questions.Count = 0 Then
            MessageBox.Show("Belum ada soal ditambahkan!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Try
            conn.Open()

            ' === Simpan data kuis baru ke tabel quizcode ===
            Dim quizPin As String = GeneratePin()
            Dim cmdQuiz As New MySqlCommand("INSERT INTO quizcode (quiz_code, quiz_title, created_by) VALUES (@code,@title,@creator)", conn)
            cmdQuiz.Parameters.AddWithValue("@code", quizPin)
            cmdQuiz.Parameters.AddWithValue("@title", TextBox6.Text)
            cmdQuiz.Parameters.AddWithValue("@creator", currentUserID) ' pastikan currentUserID diambil dari login
            cmdQuiz.ExecuteNonQuery()

            Dim quizId As Integer = cmdQuiz.LastInsertedId

            ' === Simpan semua soal ke tabel questions ===
            Dim cmdQ As New MySqlCommand("INSERT INTO questions (id_quiz, question_text, option_a, option_b, option_c, option_d, correct_answer)
                                          VALUES (@id,@q,@a,@b,@c,@d,@ans)", conn)

            For Each q As String In questions
                If q.Trim() = "" Then Continue For
                Dim parts() As String = q.Split("|"c)
                If parts.Length < 6 Then Continue For

                cmdQ.Parameters.Clear()
                cmdQ.Parameters.AddWithValue("@id", quizId)
                cmdQ.Parameters.AddWithValue("@q", parts(0))
                cmdQ.Parameters.AddWithValue("@a", parts(1))
                cmdQ.Parameters.AddWithValue("@b", parts(2))
                cmdQ.Parameters.AddWithValue("@c", parts(3))
                cmdQ.Parameters.AddWithValue("@d", parts(4))
                cmdQ.Parameters.AddWithValue("@ans", parts(5))
                cmdQ.ExecuteNonQuery()
            Next

            MessageBox.Show($"Kuis '{TextBox6.Text}' berhasil disimpan ke database!" & vbCrLf & $"PIN Kuis: {quizPin}", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' Kembali ke halaman Teacher
            For Each f As Form In Application.OpenForms
                If TypeOf f Is Teacher Then
                    f.Show()
                    DirectCast(f, Teacher).LoadQuizList()
                    Exit For
                End If
            Next

            Me.Close()
        Catch ex As Exception
            MessageBox.Show("Gagal menyimpan kuis: " & ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub

    ' === Simpan soal sementara dari TextBox ke List ===
    Private Sub SaveCurrentQuestion()
        Dim soal As String = TextBox1.Text.Trim()
        Dim a As String = TextBox2.Text.Trim()
        Dim b As String = TextBox3.Text.Trim()
        Dim c As String = TextBox4.Text.Trim()
        Dim d As String = TextBox5.Text.Trim()
        Dim correct As String = ComboBox1.Text.Trim()

        If soal = "" Then Exit Sub

        Dim data As String = $"{soal}|{a}|{b}|{c}|{d}|{correct}"

        If currentIndex < questions.Count Then
            questions(currentIndex) = data
        Else
            questions.Add(data)
        End If
    End Sub

    ' === Tampilkan soal di form sesuai index ===
    Private Sub UpdateForm()
        If currentIndex < questions.Count AndAlso questions(currentIndex) <> "" Then
            Dim parts() As String = questions(currentIndex).Split("|"c)
            If parts.Length >= 6 Then
                TextBox1.Text = parts(0)
                TextBox2.Text = parts(1)
                TextBox3.Text = parts(2)
                TextBox4.Text = parts(3)
                TextBox5.Text = parts(4)
                ComboBox1.Text = parts(5)
            End If
        Else
            TextBox1.Clear()
            TextBox2.Clear()
            TextBox3.Clear()
            TextBox4.Clear()
            TextBox5.Clear()
            ComboBox1.SelectedIndex = -1
        End If

        Me.Text = $"Create Quiz - Question {currentIndex + 1}"
    End Sub

    ' === Tombol HOME (Button4) ===
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Teacher.Show()
        Me.Hide()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Result.Show()
        Me.Hide()
    End Sub
End Class
