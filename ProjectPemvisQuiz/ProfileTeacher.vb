Imports MySql.Data.MySqlClient
Imports System.IO

Public Class ProfileTeacher
    Private profilePicPath As String = ""

    Private Sub ProfileTeacher_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadProfileData()
    End Sub

    Private Sub LoadProfileData()
        Try
            bukaKoneksi()

            ' Ambil data user berdasarkan ID (lebih aman daripada username)
            Dim cmd As New MySqlCommand("SELECT username, email, role FROM user WHERE id_user=@id", conn)
            cmd.Parameters.AddWithValue("@id", currentUserID)

            Dim rd As MySqlDataReader = cmd.ExecuteReader()
            If rd.Read() Then
                txtUsername.Text = rd("username").ToString()
                txtEmail.Text = rd("email").ToString()
                txtRole.Text = rd("role").ToString().ToUpper()
            End If
            rd.Close()
            conn.Close()

            ' === Cek foto profil ===
            Dim picsDir As String = Path.Combine(Application.StartupPath, "profile_pics")
            Dim picFile As String = Path.Combine(picsDir, $"{currentUserID}.png") ' gunakan ID biar unik

            If File.Exists(picFile) Then
                picProfile.Image = Image.FromFile(picFile)
                profilePicPath = picFile
            Else
                ' Gambar default
                picProfile.Image = My.Resources.no_profile
            End If

        Catch ex As Exception
            MessageBox.Show("Gagal memuat profil: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        txtUsername.ReadOnly = True
        txtRole.ReadOnly = True
    End Sub

    ' === Tombol Upload Foto ===
    Private Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        Dim ofd As New OpenFileDialog()
        ofd.Filter = "File Gambar|*.jpg;*.jpeg;*.png;*.bmp"
        ofd.Title = "Pilih Foto Profil"

        If ofd.ShowDialog() = DialogResult.OK Then
            Try
                Dim picsDir As String = Path.Combine(Application.StartupPath, "profile_pics")
                If Not Directory.Exists(picsDir) Then
                    Directory.CreateDirectory(picsDir)
                End If

                ' Gunakan ID user sebagai nama file biar unik dan aman
                Dim destFile As String = Path.Combine(picsDir, $"{currentUserID}.png")
                File.Copy(ofd.FileName, destFile, True)

                picProfile.Image = Image.FromFile(destFile)
                profilePicPath = destFile

                MessageBox.Show("Foto profil berhasil diperbarui!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)

            Catch ex As Exception
                MessageBox.Show("Gagal mengganti foto: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    ' === Tombol Simpan (update email) ===
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            bukaKoneksi()
            Dim cmd As New MySqlCommand("UPDATE user SET email=@mail WHERE id_user=@id", conn)
            cmd.Parameters.AddWithValue("@mail", txtEmail.Text)
            cmd.Parameters.AddWithValue("@id", currentUserID)
            cmd.ExecuteNonQuery()

            conn.Close()
            MessageBox.Show("Profil berhasil diperbarui!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            MessageBox.Show("Gagal menyimpan profil: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    ' === Tombol Kembali ke Beranda ===
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Teacher.Show()
        Me.Hide()
    End Sub
End Class
