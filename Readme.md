# Quiz App (VB.NET - Visual Studio)

## Deskripsi

Quiz App adalah aplikasi desktop berbasis VB.NET yang digunakan untuk pembuatan, pengelolaan, dan pengerjaan kuis. Aplikasi ini memiliki dua peran utama, yaitu guru sebagai pengelola kuis dan murid sebagai pengguna yang mengerjakan kuis.

## Fitur

### Multi User

* Mendukung dua jenis pengguna: Guru dan Murid
* Sistem login untuk autentikasi pengguna

### Fitur Murid

* Mencari kuis berdasarkan mata pelajaran
* Mencari kuis berdasarkan nama kuis
* Mengerjakan kuis
* Melihat nilai hasil kuis
* Melihat riwayat (history) kuis yang telah dikerjakan

### Fitur Guru

* Membuat kuis
* Menambahkan dan mengelola soal
* Melihat hasil pengerjaan kuis oleh murid
* Memantau performa murid

## Teknologi

* VB.NET
* .NET Framework
* MySQL

## Struktur Folder

* `ProjectPemvisQuiz/` : Source code utama
* `database/` : Berisi file database (.sql)

## Cara Menjalankan

1. Clone repository ini
2. Import database dari folder `database` ke MySQL
3. Buka file `.sln` menggunakan Visual Studio
4. Jalankan aplikasi

## Konfigurasi Database

Sesuaikan koneksi database pada file konfigurasi:

* Host: localhost
* User: root
* Password: sesuai pengaturan MySQL
* Database: sesuai dengan file .sql

## Catatan

Pastikan MySQL sudah berjalan sebelum aplikasi dijalankan.

## Author

* Elya
