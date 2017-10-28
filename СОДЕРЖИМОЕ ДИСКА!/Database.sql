-- phpMyAdmin SQL Dump
-- version 4.6.4
-- https://www.phpmyadmin.net/
--
-- Хост: localhost
-- Время создания: Дек 15 2016 г., 20:34
-- Версия сервера: 5.7.16-log
-- Версия PHP: 5.6.25

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- База данных: `remontbytovoytekhniki`
--

-- --------------------------------------------------------

--
-- Структура таблицы `categories`
--

CREATE TABLE `categories` (
  `id_category` int(11) NOT NULL,
  `category` text
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Дамп данных таблицы `categories`
--

INSERT INTO `categories` (`id_category`, `category`) VALUES
(1, 'Холодильники'),
(2, 'Вытяжки'),
(3, 'Мультиварки'),
(4, 'Блендер'),
(5, 'Фритюрницы'),
(6, 'Миксеры'),
(7, 'Стиральная машина'),
(8, 'Утюг'),
(9, 'Пылесос'),
(10, 'Микроволновки');

-- --------------------------------------------------------

--
-- Структура таблицы `clients`
--

CREATE TABLE `clients` (
  `id_client` int(11) NOT NULL,
  `familiya` text,
  `imya` text,
  `otchestvo` text,
  `telephon` text,
  `addres` text
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Дамп данных таблицы `clients`
--

INSERT INTO `clients` (`id_client`, `familiya`, `imya`, `otchestvo`, `telephon`, `addres`) VALUES
(1, 'Лепетило', 'Владислав', 'Витальевич', '6307766', 'ул.Ольшевского, д. 29 к.1, кв. 48'),
(2, 'Петров', 'Сергей', 'Иванович', '354185', 'ул. Красномарская, д.78 кв. 2'),
(3, 'Иванов', 'Иван', 'Иванович', '7543255', 'ул. Воробейная, д.25 кв. 26'),
(4, 'Стольной', 'Алексей', 'Сергеевич', '3571595', 'ул. Пустая, д.3 кв. 12'),
(10, 'Продуктов', 'Петька', 'Столович', '+963251598754', 'ул. Непонятная, д.1656156, кв. -1'),
(11, 'Привет', 'Hello', 'ASEGRHT', 'DFGHJ', 'ул. Воробейная, д.25 кв. 26'),
(12, 'Нормал', 'Сергей', 'Андреевич', '1597856', 'ул. Воробейная, д.25 кв. 26'),
(13, 'Гогнчарик', 'Никита', 'Андреевич', '+37533*******', 'ул. Воробейная, д.25 кв. 26'),
(14, 'Грушевский', 'Максим', 'Александрович', '+37529*******', 'ул. Воробейная, д.25 кв. 26'),
(15, 'цыу6квеанщ', 'глвекгуфцеу', 'ыведганжл', 'еоркняукыгшщведв', 'ул. Воробейная, д.25 кв. 26'),
(16, 'Петров', 'Сергей', 'Иванович', '9876543', 'ул. Красномарская, д.78 кв. 2'),
(17, 'Тестов', 'Тест', 'Тестович', '+3754567889', 'ул. Воробейная, д.25 кв. 26');

-- --------------------------------------------------------

--
-- Структура таблицы `detali`
--

CREATE TABLE `detali` (
  `id_detal` int(11) NOT NULL,
  `nazvanie` text,
  `kolichesstvo` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Дамп данных таблицы `detali`
--

INSERT INTO `detali` (`id_detal`, `nazvanie`, `kolichesstvo`) VALUES
(1, 'Шестерёнка', 100000),
(2, 'Провод', 20),
(3, 'Пластик', 150),
(4, 'Лампочка', 10),
(5, 'Печатная плата', 150),
(6, 'Светодиод', 64),
(7, 'Динамик', 1),
(8, 'Пружина', 234);

-- --------------------------------------------------------

--
-- Структура таблицы `dolgnosti`
--

CREATE TABLE `dolgnosti` (
  `id_dolgnost` int(11) NOT NULL,
  `nazvanie` text
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Дамп данных таблицы `dolgnosti`
--

INSERT INTO `dolgnosti` (`id_dolgnost`, `nazvanie`) VALUES
(1, 'Мастер по ремонту оборудования');

-- --------------------------------------------------------

--
-- Структура таблицы `firms`
--

CREATE TABLE `firms` (
  `id_firm` int(11) NOT NULL,
  `firma` text
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Дамп данных таблицы `firms`
--

INSERT INTO `firms` (`id_firm`, `firma`) VALUES
(1, 'SAMSUNG'),
(2, 'ATLANT'),
(3, 'CANDY'),
(4, 'ARISTON'),
(5, 'LG'),
(6, 'Liebherr'),
(7, 'Tefal'),
(8, 'Chair');

-- --------------------------------------------------------

--
-- Структура таблицы `garantii`
--

CREATE TABLE `garantii` (
  `id_garantiya` int(11) NOT NULL,
  `garantiyniy_srok` date DEFAULT NULL,
  `nazvanie_centra` text,
  `addres` text
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Дамп данных таблицы `garantii`
--

INSERT INTO `garantii` (`id_garantiya`, `garantiyniy_srok`, `nazvanie_centra`, `addres`) VALUES
(1, '2016-11-30', 'Сервисный центр по ремонту бытовых товаров', 'Ул. Гончарикова, д.20'),
(2, '2016-02-01', 'Сервисный центр по ремонту бытовых товаров', 'Ул. Гончарикова, д.20');

-- --------------------------------------------------------

--
-- Структура таблицы `models`
--

CREATE TABLE `models` (
  `id_model` int(11) NOT NULL,
  `nazvanie` text
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Дамп данных таблицы `models`
--

INSERT INTO `models` (`id_model`, `nazvanie`) VALUES
(1, 'SBS 7212'),
(2, 'FV3930');

-- --------------------------------------------------------

--
-- Структура таблицы `sex`
--

CREATE TABLE `sex` (
  `id_sex` int(11) NOT NULL,
  `sex` text
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Дамп данных таблицы `sex`
--

INSERT INTO `sex` (`id_sex`, `sex`) VALUES
(1, 'Мужской'),
(2, 'Женский');

-- --------------------------------------------------------

--
-- Структура таблицы `sotrudniki`
--

CREATE TABLE `sotrudniki` (
  `id_sotrudnik` int(11) NOT NULL,
  `id_sex` int(11) DEFAULT NULL,
  `id_dolgnost` int(11) DEFAULT NULL,
  `familiya` text,
  `imya` text,
  `otchestvo` text,
  `telephon` text,
  `addres` text
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Дамп данных таблицы `sotrudniki`
--

INSERT INTO `sotrudniki` (`id_sotrudnik`, `id_sex`, `id_dolgnost`, `familiya`, `imya`, `otchestvo`, `telephon`, `addres`) VALUES
(1, 1, 1, 'Незабудка', 'Артём', 'Иванович', '3574898', 'ул. Парковская, д.10, кв.24'),
(2, 2, 1, 'Вешко', 'Ольга', 'Сергеевна', '4568228', 'ул. Фрунзенская, д.15, кв.2'),
(3, 2, 1, 'Незнакомка', 'Юля', 'Петровна', '7533575', 'ул. Компьютерская, д.8, кв.28'),
(4, 1, 1, 'Лолка', 'Иван', 'Витальевич', '3955482', 'ул. Столская, д.2, кв.13');

-- --------------------------------------------------------

--
-- Структура таблицы `tovary`
--

CREATE TABLE `tovary` (
  `id_tovar` int(11) NOT NULL,
  `id_firm` int(11) DEFAULT NULL,
  `id_category` int(11) DEFAULT NULL,
  `id_model` int(11) DEFAULT NULL,
  `seriyniy_nomer` text,
  `tehnichiskie_haracteristici` text,
  `garantiyniy_srok` date DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Дамп данных таблицы `tovary`
--

INSERT INTO `tovary` (`id_tovar`, `id_firm`, `id_category`, `id_model`, `seriyniy_nomer`, `tehnichiskie_haracteristici`, `garantiyniy_srok`) VALUES
(1, 6, 1, 1, 'F545465A-6545Q', 'Общий полезный объём: 651 л; Класс энергопотребления: A+; Высота: 185.2 см; Ширина: 121 см; Количество отделений морозильной камеры: 8', '2017-01-12'),
(2, 7, 8, 2, 'FT53165-P56561', 'Длена сетевого шнура: 2 м; Максимальная мощность: 2300 ВТ', '2016-12-21');

-- --------------------------------------------------------

--
-- Структура таблицы `vidremonta`
--

CREATE TABLE `vidremonta` (
  `id_vidremont` int(11) NOT NULL,
  `nazvanie` text
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Дамп данных таблицы `vidremonta`
--

INSERT INTO `vidremonta` (`id_vidremont`, `nazvanie`) VALUES
(1, 'Замена детали'),
(2, 'Пайка');

-- --------------------------------------------------------

--
-- Структура таблицы `zakazy`
--

CREATE TABLE `zakazy` (
  `id_zakaz` int(11) NOT NULL,
  `id_client` int(11) DEFAULT NULL,
  `id_tovar` int(11) DEFAULT NULL,
  `id_garantiya` int(11) DEFAULT NULL,
  `id_vidremont` int(11) DEFAULT NULL,
  `id_sotrudnik` int(11) DEFAULT NULL,
  `id_detal` int(11) DEFAULT NULL,
  `stoimost_remonta` int(11) DEFAULT NULL,
  `alert_client` tinyint(1) NOT NULL,
  `data_postupleniya_zakaza` date DEFAULT NULL,
  `data_ispolneniya_zakaza` date DEFAULT NULL,
  `data_polucheniya_tovara` date DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Дамп данных таблицы `zakazy`
--

INSERT INTO `zakazy` (`id_zakaz`, `id_client`, `id_tovar`, `id_garantiya`, `id_vidremont`, `id_sotrudnik`, `id_detal`, `stoimost_remonta`, `alert_client`, `data_postupleniya_zakaza`, `data_ispolneniya_zakaza`, `data_polucheniya_tovara`) VALUES
(14, 1, 1, 1, 1, 1, 1, 1234, 1, '2016-12-19', '2016-12-21', '2016-12-24'),
(15, 1, 2, 2, 2, 2, 2, 324, 0, '2016-12-04', '2016-12-21', '2016-12-30');

--
-- Индексы сохранённых таблиц
--

--
-- Индексы таблицы `categories`
--
ALTER TABLE `categories`
  ADD PRIMARY KEY (`id_category`);

--
-- Индексы таблицы `clients`
--
ALTER TABLE `clients`
  ADD PRIMARY KEY (`id_client`);

--
-- Индексы таблицы `detali`
--
ALTER TABLE `detali`
  ADD PRIMARY KEY (`id_detal`);

--
-- Индексы таблицы `dolgnosti`
--
ALTER TABLE `dolgnosti`
  ADD PRIMARY KEY (`id_dolgnost`);

--
-- Индексы таблицы `firms`
--
ALTER TABLE `firms`
  ADD PRIMARY KEY (`id_firm`);

--
-- Индексы таблицы `garantii`
--
ALTER TABLE `garantii`
  ADD PRIMARY KEY (`id_garantiya`);

--
-- Индексы таблицы `models`
--
ALTER TABLE `models`
  ADD PRIMARY KEY (`id_model`);

--
-- Индексы таблицы `sex`
--
ALTER TABLE `sex`
  ADD PRIMARY KEY (`id_sex`);

--
-- Индексы таблицы `sotrudniki`
--
ALTER TABLE `sotrudniki`
  ADD PRIMARY KEY (`id_sotrudnik`),
  ADD KEY `sotrudniki_ibfk_1` (`id_sex`),
  ADD KEY `sotrudniki_ibfk_2` (`id_dolgnost`);

--
-- Индексы таблицы `tovary`
--
ALTER TABLE `tovary`
  ADD PRIMARY KEY (`id_tovar`),
  ADD KEY `tovary_ibfk_1` (`id_firm`),
  ADD KEY `tovary_ibfk_2` (`id_category`),
  ADD KEY `tovary_ibfk_3` (`id_model`);

--
-- Индексы таблицы `vidremonta`
--
ALTER TABLE `vidremonta`
  ADD PRIMARY KEY (`id_vidremont`);

--
-- Индексы таблицы `zakazy`
--
ALTER TABLE `zakazy`
  ADD PRIMARY KEY (`id_zakaz`),
  ADD KEY `zakazy_ibfk_1` (`id_client`),
  ADD KEY `zakazy_ibfk_2` (`id_tovar`),
  ADD KEY `zakazy_ibfk_3` (`id_garantiya`),
  ADD KEY `zakazy_ibfk_4` (`id_vidremont`),
  ADD KEY `zakazy_ibfk_5` (`id_sotrudnik`),
  ADD KEY `zakazy_ibfk_6` (`id_detal`);

--
-- AUTO_INCREMENT для сохранённых таблиц
--

--
-- AUTO_INCREMENT для таблицы `categories`
--
ALTER TABLE `categories`
  MODIFY `id_category` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;
--
-- AUTO_INCREMENT для таблицы `clients`
--
ALTER TABLE `clients`
  MODIFY `id_client` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=19;
--
-- AUTO_INCREMENT для таблицы `detali`
--
ALTER TABLE `detali`
  MODIFY `id_detal` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;
--
-- AUTO_INCREMENT для таблицы `dolgnosti`
--
ALTER TABLE `dolgnosti`
  MODIFY `id_dolgnost` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;
--
-- AUTO_INCREMENT для таблицы `firms`
--
ALTER TABLE `firms`
  MODIFY `id_firm` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;
--
-- AUTO_INCREMENT для таблицы `garantii`
--
ALTER TABLE `garantii`
  MODIFY `id_garantiya` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;
--
-- AUTO_INCREMENT для таблицы `models`
--
ALTER TABLE `models`
  MODIFY `id_model` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;
--
-- AUTO_INCREMENT для таблицы `sex`
--
ALTER TABLE `sex`
  MODIFY `id_sex` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;
--
-- AUTO_INCREMENT для таблицы `sotrudniki`
--
ALTER TABLE `sotrudniki`
  MODIFY `id_sotrudnik` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;
--
-- AUTO_INCREMENT для таблицы `tovary`
--
ALTER TABLE `tovary`
  MODIFY `id_tovar` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;
--
-- AUTO_INCREMENT для таблицы `vidremonta`
--
ALTER TABLE `vidremonta`
  MODIFY `id_vidremont` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;
--
-- AUTO_INCREMENT для таблицы `zakazy`
--
ALTER TABLE `zakazy`
  MODIFY `id_zakaz` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=17;
--
-- Ограничения внешнего ключа сохраненных таблиц
--

--
-- Ограничения внешнего ключа таблицы `sotrudniki`
--
ALTER TABLE `sotrudniki`
  ADD CONSTRAINT `sotrudniki_ibfk_1` FOREIGN KEY (`id_sex`) REFERENCES `sex` (`id_sex`) ON DELETE CASCADE,
  ADD CONSTRAINT `sotrudniki_ibfk_2` FOREIGN KEY (`id_dolgnost`) REFERENCES `dolgnosti` (`id_dolgnost`) ON DELETE CASCADE;

--
-- Ограничения внешнего ключа таблицы `tovary`
--
ALTER TABLE `tovary`
  ADD CONSTRAINT `tovary_ibfk_1` FOREIGN KEY (`id_firm`) REFERENCES `firms` (`id_firm`) ON DELETE CASCADE,
  ADD CONSTRAINT `tovary_ibfk_2` FOREIGN KEY (`id_category`) REFERENCES `categories` (`id_category`) ON DELETE CASCADE,
  ADD CONSTRAINT `tovary_ibfk_3` FOREIGN KEY (`id_model`) REFERENCES `models` (`id_model`) ON DELETE CASCADE;

--
-- Ограничения внешнего ключа таблицы `zakazy`
--
ALTER TABLE `zakazy`
  ADD CONSTRAINT `zakazy_ibfk_1` FOREIGN KEY (`id_client`) REFERENCES `clients` (`id_client`) ON DELETE CASCADE,
  ADD CONSTRAINT `zakazy_ibfk_2` FOREIGN KEY (`id_tovar`) REFERENCES `tovary` (`id_tovar`) ON DELETE CASCADE,
  ADD CONSTRAINT `zakazy_ibfk_3` FOREIGN KEY (`id_garantiya`) REFERENCES `garantii` (`id_garantiya`) ON DELETE CASCADE,
  ADD CONSTRAINT `zakazy_ibfk_4` FOREIGN KEY (`id_vidremont`) REFERENCES `vidremonta` (`id_vidremont`) ON DELETE CASCADE,
  ADD CONSTRAINT `zakazy_ibfk_5` FOREIGN KEY (`id_sotrudnik`) REFERENCES `sotrudniki` (`id_sotrudnik`) ON DELETE CASCADE,
  ADD CONSTRAINT `zakazy_ibfk_6` FOREIGN KEY (`id_detal`) REFERENCES `detali` (`id_detal`) ON DELETE CASCADE;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
