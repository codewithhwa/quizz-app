Imports MySql.Data.MySqlClient
Imports System.Drawing

Public Class History

    Private Sub History_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadHistoryList()
    End Sub

    Private Sub LoadHistoryList()
        Try
            bukaKoneksi()

            Dim query As String = "SELECT qc.id_quiz, qc.quiz_title, uq.score, uq.date_taken " &
                                  "FROM userquiz uq " &
                                  "JOIN quizcode qc ON uq.id_quiz = qc.id_quiz " &
                                  "WHERE uq.id_user = @id"

            Dim cmd As New MySqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@id", currentUserID)

            Dim reader As MySqlDataReader = cmd.ExecuteReader()

            FlowLayoutPanel1.Controls.Clear()

            While reader.Read()
                Dim idQuiz As Integer = reader("id_quiz")
                Dim title As String = reader("quiz_title").ToString()
                Dim score As Integer = If(IsDBNull(reader("score")), 0, CInt(reader("score")))
                Dim dateTaken As String = If(IsDBNull(reader("date_taken")), "-", reader("date_taken").ToString())

                ' === Panel untuk satu riwayat kuis ===
                Dim card As New Panel()
                card.Size = New Size(460, 80)
                card.Margin = New Padding(10)
                card.BackColor = Color.FromArgb(240, 248, 255)
                card.BorderStyle = BorderStyle.FixedSingle

                ' === Label Judul Kuis ===
                Dim lblTitle As New Label()
                lblTitle.Text = title
                lblTitle.Font = New Font("Segoe UI", 11, FontStyle.Bold)
                lblTitle.AutoSize = True
                lblTitle.Location = New Point(10, 10)

                ' === Label Tanggal ===
                Dim lblTanggal As New Label()
                lblTanggal.Text = "Tanggal: " & dateTaken
                lblTanggal.Font = New Font("Segoe UI", 9)
                lblTanggal.AutoSize = True
                lblTanggal.Location = New Point(10, 35)

                ' === Label Skor ===
                Dim lblScore As New Label()
                lblScore.Text = "Skor: " & score.ToString()
                lblScore.Font = New Font("Segoe UI", 9)
                lblScore.AutoSize = True
                lblScore.Location = New Point(10, 55)

                ' === Tombol Lihat ===
                Dim btnView As New Button()
                btnView.Text = "Lihat"
                btnView.Tag = idQuiz
                btnView.Size = New Size(70, 30)
                btnView.Location = New Point(370, 25)
                AddHandler btnView.Click, AddressOf BtnView_Click

                ' Tambahkan ke panel
                card.Controls.Add(lblTitle)
                card.Controls.Add(lblTanggal)
                card.Controls.Add(lblScore)
                card.Controls.Add(btnView)

                ' Tambahkan ke FlowLayoutPanel
                FlowLayoutPanel1.Controls.Add(card)
            End While

            reader.Close()
            conn.Close()

        Catch ex As Exception
            MessageBox.Show("Gagal memuat riwayat kuis: " & ex.Message)
        End Try
    End Sub

    Private Sub BtnView_Click(sender As Object, e As EventArgs)
        Dim btn As Button = CType(sender, Button)
        Dim idQuiz As Integer = CInt(btn.Tag)

        ' Buka form detail hasil (StudentHistory)
        Dim f As New StudentHistory(idQuiz, currentUserID)
        f.ShowDialog()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Beranda.Show()
        Me.Hide()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Search.Show()
        Me.Hide()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click

    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        profile.Show()
        Me.Hide()
    End Sub
End Class
