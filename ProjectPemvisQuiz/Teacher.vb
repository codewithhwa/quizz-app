Imports MySql.Data.MySqlClient
Imports System.Drawing

Public Class Teacher

    Private Sub Teacher_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadQuizList()
    End Sub

    Public Sub LoadQuizList()
        Try
            bukaKoneksi()

            ' Ambil semua quiz milik guru yang login
            Dim query As String = "SELECT qc.id_quiz, qc.quiz_title AS title, COUNT(q.id_question) AS jumlah_soal " &
                      "FROM quizcode qc LEFT JOIN questions q ON qc.id_quiz = q.id_quiz " &
                      "WHERE qc.created_by = @id GROUP BY qc.id_quiz, qc.quiz_title"


            Dim cmd As New MySqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@id", currentUserID)

            Dim reader As MySqlDataReader = cmd.ExecuteReader()

            PanelQuizContainer.Controls.Clear()

            Dim yOffset As Integer = 10

            While reader.Read()
                Dim idQuiz As Integer = reader("id_quiz")
                Dim title As String = reader("title").ToString()
                Dim jumlahSoal As Integer = Convert.ToInt32(reader("jumlah_soal"))

                ' === Panel untuk 1 kuis ===
                Dim panelItem As New Panel()
                panelItem.Size = New Size(PanelQuizContainer.Width - 25, 70)
                panelItem.Location = New Point(10, yOffset)
                panelItem.BackColor = Color.FromArgb(240, 248, 255)
                panelItem.BorderStyle = BorderStyle.FixedSingle

                ' === Label judul kuis ===
                Dim lblTitle As New Label()
                lblTitle.Text = title
                lblTitle.Font = New Font("Segoe UI", 11, FontStyle.Bold)
                lblTitle.AutoSize = True
                lblTitle.Location = New Point(10, 10)

                ' === Label jumlah soal ===
                Dim lblJumlah As New Label()
                lblJumlah.Text = "Jumlah Soal: " & jumlahSoal.ToString()
                lblJumlah.Font = New Font("Segoe UI", 9)
                lblJumlah.AutoSize = True
                lblJumlah.Location = New Point(10, 40)

                ' === Tombol Edit ===
                Dim btnEdit As New Button()
                btnEdit.Text = "Edit"
                btnEdit.Tag = idQuiz
                btnEdit.Size = New Size(60, 30)
                btnEdit.Location = New Point(panelItem.Width - 140, 20)
                AddHandler btnEdit.Click, AddressOf ButtonEdit_Click

                ' === Tombol Hapus ===
                Dim btnDelete As New Button()
                btnDelete.Text = "Hapus"
                btnDelete.Tag = idQuiz
                btnDelete.Size = New Size(60, 30)
                btnDelete.Location = New Point(panelItem.Width - 70, 20)
                AddHandler btnDelete.Click, AddressOf ButtonDelete_Click

                ' Tambahkan semua elemen ke panel
                panelItem.Controls.Add(lblTitle)
                panelItem.Controls.Add(lblJumlah)
                panelItem.Controls.Add(btnEdit)
                panelItem.Controls.Add(btnDelete)

                ' Tambahkan panel ke container
                PanelQuizContainer.Controls.Add(panelItem)

                yOffset += 80
            End While

            reader.Close()
            conn.Close()

        Catch ex As Exception
            MessageBox.Show("Gagal memuat daftar kuis: " & ex.Message)
        End Try
    End Sub

    ' === Tombol Edit ===
    Private Sub ButtonEdit_Click(sender As Object, e As EventArgs)
        Dim btn As Button = CType(sender, Button)
        Dim quizID As Integer = CInt(btn.Tag)
        Dim quizTitle As String = ""
        If TypeOf btn.Parent Is Panel Then
            For Each c As Control In btn.Parent.Controls
                If TypeOf c Is Label AndAlso CType(c, Label).Font.Bold Then
                    quizTitle = c.Text
                    Exit For
                End If
            Next
        End If

        ' Buka form editquiz dengan id_quiz dan judul kuis
        Dim f As New editquiz(quizID, quizTitle)
        f.Show()
        Me.Hide()
    End Sub

    ' === Tombol Hapus ===
    Private Sub ButtonDelete_Click(sender As Object, e As EventArgs)
        Dim btn As Button = CType(sender, Button)
        Dim quizID As Integer = CInt(btn.Tag)

        If MessageBox.Show("Yakin ingin menghapus kuis ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Try
                bukaKoneksi()
                Dim cmd As New MySqlCommand("DELETE FROM quiz WHERE id_quiz=@id", conn)
                cmd.Parameters.AddWithValue("@id", quizID)
                cmd.ExecuteNonQuery()
                conn.Close()

                MessageBox.Show("Kuis berhasil dihapus.")
                LoadQuizList() ' Refresh daftar kuis
            Catch ex As Exception
                MessageBox.Show("Gagal menghapus kuis: " & ex.Message)
            End Try
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        home.Show()
        Me.Hide()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        ProfileTeacher.Show()
        Me.Hide()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        createquiz.Show()
        Me.Hide()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Result.Show()
        Me.Hide()
    End Sub

    Private Sub PanelQuizContainer_Paint(sender As Object, e As PaintEventArgs) Handles PanelQuizContainer.Paint

    End Sub
End Class
