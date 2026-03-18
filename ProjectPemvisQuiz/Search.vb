Imports MySql.Data.MySqlClient

Public Class Search
    Private studentID As Integer
    Private studentName As String
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        LoadSearchResults()
    End Sub

    Private Sub LoadSearchResults()
        FlowLayoutPanel1.Controls.Clear()

        Dim keyword As String = TextBox1.Text.Trim()
        If keyword = "" Then
            MessageBox.Show("Masukkan kata pencarian dulu!")
            Exit Sub
        End If

        Try
            bukaKoneksi()

            Dim query As String = "
                SELECT uq.id_userquiz, q.quiz_title, uq.score, uq.date_taken
                FROM userquiz uq
                JOIN quizcode q ON uq.id_quiz = q.id_quiz
                WHERE uq.id_user = @uid AND q.quiz_title LIKE @key
                ORDER BY uq.date_taken DESC
            "

            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@uid", currentUserID)
                cmd.Parameters.AddWithValue("@key", "%" & keyword & "%")

                Using rd As MySqlDataReader = cmd.ExecuteReader()
                    While rd.Read()
                        Dim panel As New Panel() With {
                            .Width = 740,
                            .Height = 75, ' Tinggi panel ditambah sedikit biar muat label + tombol
                            .BackColor = Color.White,
                            .BorderStyle = BorderStyle.FixedSingle,
                            .Margin = New Padding(2)
                        }

                        '=== Label info kuis ===
                        Dim lbl As New Label() With {
                            .Font = New Font("Segoe UI", 9, FontStyle.Regular),
                            .AutoSize = True,
                            .MaximumSize = New Size(700, 0),
                            .Location = New Point(10, 8),
                            .Text = rd("quiz_title").ToString() &
                                    vbCrLf & "Tgl: " &
                                    Convert.ToDateTime(rd("date_taken")).ToString("dd MMM yyyy") &
                                    " | Score: " & rd("score").ToString()
                        }

                        panel.Controls.Add(lbl)
                        panel.PerformLayout() ' Hitung tinggi label otomatis

                        '=== Tombol "Lihat" di bawah teks ===
                        Dim btnView As New Button() With {
                            .Text = "Lihat",
                            .Width = 70,
                            .Height = 28,
                            .BackColor = Color.SkyBlue,
                            .FlatStyle = FlatStyle.Flat,
                            .Font = New Font("Segoe UI", 9, FontStyle.Bold),
                            .Tag = rd("id_userquiz")
                        }

                        ' Posisi tombol tepat di bawah teks
                        btnView.Location = New Point(lbl.Left, lbl.Bottom + 5)

                        AddHandler btnView.Click, AddressOf ViewHistory_Click

                        ' Tambahkan ke panel
                        panel.Controls.Add(btnView)
                        FlowLayoutPanel1.Controls.Add(panel)

                    End While
                End Using
            End Using

        Catch ex As Exception
            MessageBox.Show("Error load hasil: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            conn.Close()
        End Try
    End Sub

    Private Sub ViewHistory_Click(sender As Object, e As EventArgs)
        Dim btn As Button = CType(sender, Button)
        Dim idUserQuiz As Integer = CInt(btn.Tag)

        Try
            ' Pastikan semua reader ditutup sebelum buka koneksi baru
            If conn IsNot Nothing AndAlso conn.State = ConnectionState.Open Then
                Try
                    Dim closeCmd As New MySqlCommand("DO 1;", conn)
                    closeCmd.ExecuteNonQuery()
                Catch
                End Try
            End If

            bukaKoneksi()
            Dim cmd As New MySqlCommand("
            SELECT uq.id_user, u.username
            FROM userquiz uq
            JOIN user u ON uq.id_user = u.id_user
            WHERE uq.id_userquiz = @uqid
        ", conn)

            cmd.Parameters.AddWithValue("@uqid", idUserQuiz)
            Dim rd = cmd.ExecuteReader()

            If rd.Read() Then
                Dim sid As Integer = rd("id_user")
                Dim sname As String = rd("username").ToString()
                rd.Close() ' ✅ Tambahkan ini sebelum buka form baru!

                Dim hist As New StudentHistory(sid, sname)
                hist.Show()
                Me.Hide()
            Else
                rd.Close()
            End If

        Catch ex As Exception
            MessageBox.Show("Gagal memuat riwayat kuis: " & ex.Message)
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        profile.Show()
        Me.Hide()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim historyForm As New StudentHistory(studentID, studentName)
        historyForm.Show()
        Me.Hide()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Beranda.Show()
        Me.Hide()
    End Sub
End Class
