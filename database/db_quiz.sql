-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Mar 18, 2026 at 12:04 PM
-- Server version: 11.7.2-MariaDB
-- PHP Version: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `db_quiz`
--

-- --------------------------------------------------------

--
-- Table structure for table `questions`
--

CREATE TABLE `questions` (
  `id_question` int(11) NOT NULL,
  `id_quiz` int(11) NOT NULL,
  `question_text` text NOT NULL,
  `option_a` varchar(255) NOT NULL,
  `option_b` varchar(255) NOT NULL,
  `option_c` varchar(255) NOT NULL,
  `option_d` varchar(255) NOT NULL,
  `correct_answer` char(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `questions`
--

INSERT INTO `questions` (`id_question`, `id_quiz`, `question_text`, `option_a`, `option_b`, `option_c`, `option_d`, `correct_answer`) VALUES
(1, 0, '1+1 =...', '2', '3', '5', '0', 'A'),
(2, 0, 'Wonpil adalah member', 'day6', 'bts', 'blackpink', 'wannaone', 'A'),
(3, 0, '5-1 = ..', '4', '7', '5', '9', 'A'),
(4, 2, '1. Sebutkan sila ke 5 pancasila', 'kemanusiaan yang adil dan beradab', 'Ketuhanan Yang Maha Esa', 'persatuan indonesia', 'keadilan sosial', 'D'),
(5, 2, 'Ada berapa sila dalam pancasila', '2', '5', '8', '6', 'B'),
(6, 3, '2+2-1 =....', '3', '2', '1', '0', 'A'),
(7, 3, '2 - 10 : 5', '5', '6', '-3', '2', 'C'),
(8, 4, '1+1=', '2', '4', '6', '1', 'A'),
(9, 4, '2+2=', '9', '3', '4', '1', 'C'),
(10, 7, 'Budi memiliki sebuah piring yang memiliki diameter 20 cm. Hitunglah luas dari piring milik budi!', '157', '200', '314', '400', 'C'),
(11, 8, 'h', 'h', 'hh', 'h', 'h', 'B'),
(12, 9, 'c', '', '', '', '', ''),
(13, 10, 'c', '', '', '', '', ''),
(14, 11, 'c', '', '', '', '', ''),
(15, 12, 'Budi memiliki sebuah piring yang memiliki diameter 20 cm. Hitunglah luas dari piring milik budi!', '157 cm²', '200 cm²', '314 cm²', '400 cm²', 'C'),
(16, 13, '1+1', '2', '3', '4', '1', 'A'),
(17, 13, '2+2', '1', '4', '-4', '5', 'B'),
(18, 14, '1+1', '2', '3', '4', '1', 'A'),
(19, 14, '2+2', '1', '4', '-4', '5', 'B'),
(20, 15, '1+1', '2', '3', '4', '1', 'A'),
(21, 15, '2+2', '1', '4', '-4', '5', 'B'),
(22, 16, '10 x 9', '90', '60', '70', '80', 'A'),
(23, 16, '3 x 60', '190', '180', '120', '220', 'B'),
(24, 16, '9 x 5', '45', '55', '75', '65', 'A'),
(25, 16, '3 x 4', '11', '15', '12', '14', 'C'),
(26, 16, '6 x 7', '55', '40', '36', '42', 'D'),
(27, 17, 'bahasa inggris dari kucing adalah', 'cat', 'dog', 'duck', 'frog', 'A'),
(28, 17, 'bahasa inggris dari kupu kupu adalah', 'butterfly', 'bee', 'cat', 'frog', 'A'),
(29, 12, 'Hasil dari 15 × 12 adalah...', '120', '150', '180', '200', 'C');

-- --------------------------------------------------------

--
-- Table structure for table `quizcode`
--

CREATE TABLE `quizcode` (
  `id_quiz` int(11) NOT NULL,
  `quiz_code` varchar(10) DEFAULT NULL,
  `quiz_title` varchar(100) DEFAULT NULL,
  `created_by` varchar(50) NOT NULL,
  `id_subject` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `quizcode`
--

INSERT INTO `quizcode` (`id_quiz`, `quiz_code`, `quiz_title`, `created_by`, `id_subject`) VALUES
(1, '791493', 'MATEMATIKA', '0', NULL),
(2, '659105', 'PKN', '6', NULL),
(3, '575993', 'MATEMATIKA', '6', NULL),
(4, '729210', 'MATEMATIKA', '1', NULL),
(12, '496053', 'Bangun Ruang', '6', 1),
(13, '240758', 'Penjumlahan', '7', 1),
(14, '261470', 'Penjumlahan', '7', 1),
(15, '512789', 'Penjumlahan', '7', 1),
(16, '974142', 'Perkalian', '1', 1),
(17, '923488', 'animals', '1', 2);

-- --------------------------------------------------------

--
-- Table structure for table `subjects`
--

CREATE TABLE `subjects` (
  `id_subject` int(11) NOT NULL,
  `subject_name` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `subjects`
--

INSERT INTO `subjects` (`id_subject`, `subject_name`) VALUES
(1, 'Matematika'),
(2, 'Bahasa Inggris'),
(3, 'Biologi'),
(4, 'Fisika'),
(5, 'Kimia'),
(6, 'Bahasa Indonesia'),
(7, 'Sejarah'),
(8, 'Agama'),
(9, 'Geografi');

-- --------------------------------------------------------

--
-- Table structure for table `user`
--

CREATE TABLE `user` (
  `id_user` int(11) NOT NULL,
  `username` varchar(50) NOT NULL,
  `email` varchar(100) NOT NULL,
  `password` varchar(50) NOT NULL,
  `role` enum('teacher','student') NOT NULL DEFAULT 'student'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `user`
--

INSERT INTO `user` (`id_user`, `username`, `email`, `password`, `role`) VALUES
(1, 'rara', 'bilasalsaratih@gmail.com', '12345678', 'teacher'),
(2, 'ratih', '24050974001@mhs.unesa.ac.id', '567894321', 'student'),
(3, 'reva', 'reva ', 'reva', 'student'),
(4, 'Ratih S', '24050974001@mhs.unesa.ac.id', 'yagitulah1', 'student'),
(5, 'rere', 'kapalxjr@gmail.com', '180406', 'student'),
(6, 'yayaa', 'eyaeyaa@gmail.com', '12345678', 'student'),
(7, 'ocang', 'yeosangganteng@gmail.com', '12345678', 'teacher'),
(8, 'rere cans', 'reva@gmail.com', '123456', 'student');

-- --------------------------------------------------------

--
-- Table structure for table `userquiz`
--

CREATE TABLE `userquiz` (
  `id_userquiz` int(11) NOT NULL,
  `id_user` int(11) DEFAULT NULL,
  `id_quiz` int(11) DEFAULT NULL,
  `score` int(11) DEFAULT NULL,
  `date_taken` datetime DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `userquiz`
--

INSERT INTO `userquiz` (`id_userquiz`, `id_user`, `id_quiz`, `score`, `date_taken`) VALUES
(1, 6, 2, 1, '2025-11-06 23:10:39'),
(2, 1, 2, 1, '2025-11-07 07:32:02'),
(3, 6, 3, 1, '2025-11-07 12:16:01'),
(4, 1, 4, 1, '2025-11-07 13:27:57'),
(5, 6, 8, 0, '2025-11-14 16:17:03'),
(6, 6, 8, 0, '2025-11-14 19:22:20'),
(7, 6, 8, 0, '2025-11-20 12:21:26'),
(8, 6, 12, 1, '2025-11-20 12:37:51'),
(9, 6, 12, 1, '2025-11-20 18:52:37'),
(10, 6, 12, 1, '2025-11-20 19:20:01'),
(11, 7, 14, 10, '2025-11-24 16:44:26'),
(12, 8, 12, 1, '2025-11-24 16:52:25'),
(13, 8, 12, 0, '2025-11-24 16:52:58'),
(14, 6, 13, 10, '2025-11-25 05:55:38'),
(15, 6, 15, 1, '2025-11-25 16:42:37'),
(16, 6, 16, 4, '2025-11-25 16:52:12'),
(17, 6, 17, 1, '2025-11-25 16:54:55');

-- --------------------------------------------------------

--
-- Table structure for table `user_answers`
--

CREATE TABLE `user_answers` (
  `id_answer` int(11) NOT NULL,
  `id_userquiz` int(11) DEFAULT NULL,
  `id_question` int(11) DEFAULT NULL,
  `selected_option` varchar(1) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `user_answers`
--

INSERT INTO `user_answers` (`id_answer`, `id_userquiz`, `id_question`, `selected_option`) VALUES
(1, 2, 4, 'C'),
(2, 2, 5, 'B'),
(3, 3, 6, 'A'),
(4, 3, 7, 'D'),
(5, 4, 8, 'A'),
(6, 4, 9, ''),
(7, 5, 11, 'A'),
(8, 6, 11, 'C'),
(9, 7, 11, 'D'),
(10, 8, 15, 'C'),
(11, 9, 15, 'C'),
(12, 10, 15, 'C'),
(13, 11, 18, 'A'),
(14, 11, 19, 'B'),
(15, 12, 15, 'C'),
(16, 13, 15, 'A'),
(17, 14, 16, 'A'),
(18, 14, 17, 'B'),
(19, 15, 20, 'A'),
(20, 15, 21, ''),
(21, 16, 22, 'A'),
(22, 16, 23, 'B'),
(23, 16, 24, 'A'),
(24, 16, 25, 'C'),
(25, 16, 26, ''),
(26, 17, 27, 'A'),
(27, 17, 28, 'D'),
(28, 7, 34, 'C');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `questions`
--
ALTER TABLE `questions`
  ADD PRIMARY KEY (`id_question`),
  ADD KEY `id_quiz` (`id_quiz`);

--
-- Indexes for table `quizcode`
--
ALTER TABLE `quizcode`
  ADD PRIMARY KEY (`id_quiz`),
  ADD KEY `id_subject` (`id_subject`);

--
-- Indexes for table `subjects`
--
ALTER TABLE `subjects`
  ADD PRIMARY KEY (`id_subject`);

--
-- Indexes for table `user`
--
ALTER TABLE `user`
  ADD PRIMARY KEY (`id_user`);

--
-- Indexes for table `userquiz`
--
ALTER TABLE `userquiz`
  ADD PRIMARY KEY (`id_userquiz`),
  ADD KEY `id_user` (`id_user`),
  ADD KEY `id_quiz` (`id_quiz`);

--
-- Indexes for table `user_answers`
--
ALTER TABLE `user_answers`
  ADD PRIMARY KEY (`id_answer`),
  ADD KEY `id_userquiz` (`id_userquiz`),
  ADD KEY `id_question` (`id_question`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `questions`
--
ALTER TABLE `questions`
  MODIFY `id_question` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=30;

--
-- AUTO_INCREMENT for table `quizcode`
--
ALTER TABLE `quizcode`
  MODIFY `id_quiz` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=18;

--
-- AUTO_INCREMENT for table `subjects`
--
ALTER TABLE `subjects`
  MODIFY `id_subject` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

--
-- AUTO_INCREMENT for table `user`
--
ALTER TABLE `user`
  MODIFY `id_user` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- AUTO_INCREMENT for table `userquiz`
--
ALTER TABLE `userquiz`
  MODIFY `id_userquiz` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=18;

--
-- AUTO_INCREMENT for table `user_answers`
--
ALTER TABLE `user_answers`
  MODIFY `id_answer` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=29;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `quizcode`
--
ALTER TABLE `quizcode`
  ADD CONSTRAINT `quizcode_ibfk_1` FOREIGN KEY (`id_subject`) REFERENCES `subjects` (`id_subject`);

--
-- Constraints for table `userquiz`
--
ALTER TABLE `userquiz`
  ADD CONSTRAINT `userquiz_ibfk_1` FOREIGN KEY (`id_user`) REFERENCES `user` (`id_user`);

--
-- Constraints for table `user_answers`
--
ALTER TABLE `user_answers`
  ADD CONSTRAINT `user_answers_ibfk_1` FOREIGN KEY (`id_userquiz`) REFERENCES `userquiz` (`id_userquiz`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
