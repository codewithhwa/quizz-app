Imports MySql.Data.MySqlClient

Public Class Beranda
    Private studentID As Integer
    Private studentName As String

    Public Sub New(sID As Integer, sName As String)
        InitializeComponent()
        studentID = sID
        studentName = sName
    End Sub

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub Beranda_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label1.Text = "Masukkan Kode Kuis"
    End Sub

    ' === Tombol MULAI KUIS ===
    Private Sub Join_Click(sender As Object, e As EventArgs) Handles Join.Click
        Dim kodeKuis As String = TextBox1.Text.Trim()

        If kodeKuis = "" Then
            MessageBox.Show("Masukkan kode kuis terlebih dahulu!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Try
            bukaKoneksi()
            Dim cmd As New MySqlCommand("SELECT id_quiz, quiz_title FROM quizcode WHERE quiz_code=@code", conn)
            cmd.Parameters.AddWithValue("@code", kodeKuis)
            Dim rd As MySqlDataReader = cmd.ExecuteReader()

            If rd.Read() Then
                Dim idQuiz As Integer = rd("id_quiz")
                Dim judulKuis As String = rd("quiz_title").ToString()
                conn.Close()

                ' Buka form kuis
                Dim frmDoQuiz As New formDoquiz(idQuiz, judulKuis)
                frmDoQuiz.Show()
                Me.Hide()
            Else
                MessageBox.Show("Kode kuis tidak ditemukan. Periksa kembali PIN yang kamu masukkan!", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error)
                conn.Close()
            End If
        Catch ex As Exception
            MessageBox.Show("Terjadi kesalahan: " & ex.Message)
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    ' === Tombol KEMBALI / LOGOUT ===
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Search.Show()
        Me.Hide()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        home.Show()
        Me.Hide()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        profile.Show()
        Me.Hide()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim historyForm As New History()
        historyForm.Show()
        Me.Hide()
    End Sub
End Class
