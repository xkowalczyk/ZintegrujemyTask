-- phpMyAdmin SQL Dump
-- version 5.1.3
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Czas generowania: 13 Kwi 2025, 18:40
-- Wersja serwera: 10.4.24-MariaDB
-- Wersja PHP: 7.4.28

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Baza danych: `productdb`
--

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `productpricing`
--

CREATE TABLE `productpricing` (
  `SKU` varchar(255) NOT NULL,
  `TAX` int(11) DEFAULT NULL,
  `NettoPrice` float DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `products`
--

CREATE TABLE `products` (
  `Id` varchar(255) NOT NULL,
  `SKU` varchar(255) DEFAULT NULL,
  `Name` varchar(255) DEFAULT NULL,
  `EAN` varchar(255) DEFAULT NULL,
  `ProducerName` varchar(255) DEFAULT NULL,
  `Category` varchar(255) DEFAULT NULL,
  `IsVire` varchar(10) DEFAULT NULL,
  `Shipping` varchar(255) DEFAULT NULL,
  `IsAvailable` varchar(10) DEFAULT NULL,
  `IsVendor` varchar(10) DEFAULT NULL,
  `DefaultImage` varchar(500) DEFAULT NULL,
  `TAX` int(11) DEFAULT NULL,
  `NettoPrice` float DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `productstock`
--

CREATE TABLE `productstock` (
  `ProductId` varchar(255) NOT NULL,
  `Stock` int(11) DEFAULT NULL,
  `UnitType` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
