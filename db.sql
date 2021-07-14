-- phpMyAdmin SQL Dump
-- version 4.6.4
-- https://www.phpmyadmin.net/
--
-- Хост: 127.0.0.1
-- Время создания: Июл 15 2021 г., 01:32
-- Версия сервера: 5.7.15
-- Версия PHP: 7.0.10

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- База данных: `politeh_prac`
--

-- --------------------------------------------------------

--
-- Структура таблицы `containers`
--

CREATE TABLE `containers` (
  `id` tinyint(3) UNSIGNED NOT NULL,
  `container_volume` bigint(20) UNSIGNED NOT NULL COMMENT 'Объем контейнера, л (дм^3)',
  `fuel_volume` bigint(20) UNSIGNED NOT NULL COMMENT 'Объем топлива, л (дм^3)',
  `fuel_id` int(10) UNSIGNED DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Дамп данных таблицы `containers`
--

INSERT INTO `containers` (`id`, `container_volume`, `fuel_volume`, `fuel_id`) VALUES
(1, 65000, 60000, 1),
(2, 65000, 60000, 2),
(3, 65000, 0, 1),
(4, 65000, 0, NULL);

-- --------------------------------------------------------

--
-- Структура таблицы `fuel_transactions`
--

CREATE TABLE `fuel_transactions` (
  `id` bigint(20) UNSIGNED NOT NULL,
  `responsible_for` bigint(20) UNSIGNED NOT NULL COMMENT 'Ответственный за транзакциюпринимающий топливо для заливки в бак,кассир, совершающий продажу',
  `time_start` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT 'Время начала заливки/продажи',
  `time_end` timestamp NULL DEFAULT NULL COMMENT 'Время конца заливки/продажи',
  `fuel_id` int(10) UNSIGNED NOT NULL,
  `container_id` tinyint(3) UNSIGNED NOT NULL,
  `pump_id` tinyint(3) UNSIGNED DEFAULT NULL COMMENT 'NULL, если происходит заливка в контейнер',
  `fuel_volume` bigint(20) NOT NULL COMMENT 'литры\nположительно, если происходит заливка в контейнер\nотрицателно, если продажа',
  `cost` bigint(20) NOT NULL COMMENT 'копейки\nцена транзакции\nотрицателно, если происходит заливка в контейнер\nположительно, если продажа'
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Дамп данных таблицы `fuel_transactions`
--

INSERT INTO `fuel_transactions` (`id`, `responsible_for`, `time_start`, `time_end`, `fuel_id`, `container_id`, `pump_id`, `fuel_volume`, `cost`) VALUES
(1, 1, '2021-07-14 17:32:27', NULL, 1, 1, 1, -20, 80000),
(2, 1, '2021-07-14 17:33:44', '2021-07-14 17:36:00', 2, 2, 1, -30, 123000);

-- --------------------------------------------------------

--
-- Структура таблицы `fuel_types`
--

CREATE TABLE `fuel_types` (
  `id` int(10) UNSIGNED NOT NULL,
  `type` tinytext NOT NULL COMMENT 'Тип или название',
  `vendor` text NOT NULL COMMENT 'Поставщик',
  `cost_buy` int(10) UNSIGNED NOT NULL COMMENT 'Цена покупки у поставщика, коп/л',
  `cost_sale` int(10) UNSIGNED NOT NULL COMMENT 'Цена продажи юзеру, коп/л',
  `data` longtext NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Дамп данных таблицы `fuel_types`
--

INSERT INTO `fuel_types` (`id`, `type`, `vendor`, `cost_buy`, `cost_sale`, `data`) VALUES
(1, 'АИ-92', 'РОСНЕФТЬ', 3000, 4000, ''),
(2, 'АИ-95', 'РОСНЕФТЬ', 3100, 4100, '');

-- --------------------------------------------------------

--
-- Структура таблицы `pumps`
--

CREATE TABLE `pumps` (
  `id` tinyint(3) UNSIGNED NOT NULL,
  `status` tinyint(4) NOT NULL,
  `tap_1` tinyint(3) UNSIGNED DEFAULT NULL,
  `tap_2` tinyint(3) UNSIGNED DEFAULT NULL,
  `tap_3` tinyint(3) UNSIGNED DEFAULT NULL,
  `tap_4` tinyint(3) UNSIGNED DEFAULT NULL,
  `tap_5` tinyint(3) UNSIGNED DEFAULT NULL,
  `tap_6` tinyint(3) UNSIGNED DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Дамп данных таблицы `pumps`
--

INSERT INTO `pumps` (`id`, `status`, `tap_1`, `tap_2`, `tap_3`, `tap_4`, `tap_5`, `tap_6`) VALUES
(1, 1, 1, 2, 3, 1, 2, 3),
(2, 1, 1, 2, 3, 1, 2, 3),
(3, 1, 1, 4, NULL, 1, 4, NULL),
(4, 1, 3, 4, NULL, 3, 4, NULL);

-- --------------------------------------------------------

--
-- Структура таблицы `shifts`
--

CREATE TABLE `shifts` (
  `id` bigint(20) UNSIGNED NOT NULL,
  `time_start` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `time_end` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Дамп данных таблицы `shifts`
--

INSERT INTO `shifts` (`id`, `time_start`, `time_end`) VALUES
(1, '2021-07-14 15:29:35', '2023-07-13 17:00:00');

-- --------------------------------------------------------

--
-- Структура таблицы `slaves`
--

CREATE TABLE `slaves` (
  `id` bigint(20) UNSIGNED NOT NULL,
  `fio` text NOT NULL COMMENT 'ФИО',
  `date_employ` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Дата принятия на работу',
  `pay_per_shift` int(10) UNSIGNED NOT NULL COMMENT 'З/П за смену (руб)',
  `rank` tinytext NOT NULL COMMENT 'Должность'
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Дамп данных таблицы `slaves`
--

INSERT INTO `slaves` (`id`, `fio`, `date_employ`, `pay_per_shift`, `rank`) VALUES
(1, 'Оля', '2021-07-14 11:05:13', 1000, 'Продован');

-- --------------------------------------------------------

--
-- Структура таблицы `slaves_shifts`
--

CREATE TABLE `slaves_shifts` (
  `slave_id` bigint(20) UNSIGNED NOT NULL,
  `shift_id` bigint(20) UNSIGNED NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Структура таблицы `users`
--

CREATE TABLE `users` (
  `id` int(10) UNSIGNED NOT NULL,
  `login` tinytext NOT NULL,
  `slave_id` bigint(20) UNSIGNED DEFAULT NULL,
  `type` tinyint(4) NOT NULL COMMENT '0-гость, нихрена не может|1-может всё, также управлять пользователями|2-может управлять сменами, смотреть статистику|3-может продавать бензыч|4-может управлять баками и колонками, принимать бензыч'
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Дамп данных таблицы `users`
--

INSERT INTO `users` (`id`, `login`, `slave_id`, `type`) VALUES
(1, 'prodovan', 1, 3);

--
-- Индексы сохранённых таблиц
--

--
-- Индексы таблицы `containers`
--
ALTER TABLE `containers`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fuel_types_idx` (`fuel_id`);

--
-- Индексы таблицы `fuel_transactions`
--
ALTER TABLE `fuel_transactions`
  ADD PRIMARY KEY (`id`),
  ADD KEY `slaves_idx` (`responsible_for`),
  ADD KEY `fuel_types_idx` (`fuel_id`),
  ADD KEY `containers_idx` (`container_id`),
  ADD KEY `pumps_idx` (`pump_id`);

--
-- Индексы таблицы `fuel_types`
--
ALTER TABLE `fuel_types`
  ADD PRIMARY KEY (`id`);

--
-- Индексы таблицы `pumps`
--
ALTER TABLE `pumps`
  ADD PRIMARY KEY (`id`),
  ADD KEY `tap_1_idx` (`tap_1`),
  ADD KEY `tap_2_idx` (`tap_2`),
  ADD KEY `tap_3_idx` (`tap_3`),
  ADD KEY `tap_4_idx` (`tap_4`),
  ADD KEY `tap_5_idx` (`tap_5`),
  ADD KEY `tap_6_idx` (`tap_6`);

--
-- Индексы таблицы `shifts`
--
ALTER TABLE `shifts`
  ADD PRIMARY KEY (`id`);

--
-- Индексы таблицы `slaves`
--
ALTER TABLE `slaves`
  ADD PRIMARY KEY (`id`);

--
-- Индексы таблицы `slaves_shifts`
--
ALTER TABLE `slaves_shifts`
  ADD PRIMARY KEY (`slave_id`,`shift_id`),
  ADD KEY `slaves_idx` (`slave_id`) USING BTREE,
  ADD KEY `shifts_idx` (`shift_id`) USING BTREE;

--
-- Индексы таблицы `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`id`),
  ADD KEY `slaves_idx` (`slave_id`);

--
-- AUTO_INCREMENT для сохранённых таблиц
--

--
-- AUTO_INCREMENT для таблицы `containers`
--
ALTER TABLE `containers`
  MODIFY `id` tinyint(3) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;
--
-- AUTO_INCREMENT для таблицы `fuel_transactions`
--
ALTER TABLE `fuel_transactions`
  MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;
--
-- AUTO_INCREMENT для таблицы `fuel_types`
--
ALTER TABLE `fuel_types`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;
--
-- AUTO_INCREMENT для таблицы `pumps`
--
ALTER TABLE `pumps`
  MODIFY `id` tinyint(3) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;
--
-- AUTO_INCREMENT для таблицы `shifts`
--
ALTER TABLE `shifts`
  MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;
--
-- AUTO_INCREMENT для таблицы `slaves`
--
ALTER TABLE `slaves`
  MODIFY `id` bigint(20) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;
--
-- AUTO_INCREMENT для таблицы `users`
--
ALTER TABLE `users`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;
--
-- Ограничения внешнего ключа сохраненных таблиц
--

--
-- Ограничения внешнего ключа таблицы `containers`
--
ALTER TABLE `containers`
  ADD CONSTRAINT `cont_fuel_types_key` FOREIGN KEY (`fuel_id`) REFERENCES `fuel_types` (`id`) ON UPDATE CASCADE;

--
-- Ограничения внешнего ключа таблицы `fuel_transactions`
--
ALTER TABLE `fuel_transactions`
  ADD CONSTRAINT `trans_containers_key` FOREIGN KEY (`container_id`) REFERENCES `containers` (`id`) ON UPDATE CASCADE,
  ADD CONSTRAINT `trans_fuel_types_key` FOREIGN KEY (`fuel_id`) REFERENCES `fuel_types` (`id`) ON UPDATE CASCADE,
  ADD CONSTRAINT `trans_pumps_key` FOREIGN KEY (`pump_id`) REFERENCES `pumps` (`id`) ON UPDATE CASCADE,
  ADD CONSTRAINT `trans_slaves_key` FOREIGN KEY (`responsible_for`) REFERENCES `slaves` (`id`) ON DELETE NO ACTION ON UPDATE CASCADE;

--
-- Ограничения внешнего ключа таблицы `pumps`
--
ALTER TABLE `pumps`
  ADD CONSTRAINT `pump_tap_1_key` FOREIGN KEY (`tap_1`) REFERENCES `containers` (`id`) ON UPDATE CASCADE,
  ADD CONSTRAINT `pump_tap_2_key` FOREIGN KEY (`tap_2`) REFERENCES `containers` (`id`) ON UPDATE CASCADE,
  ADD CONSTRAINT `pump_tap_3_key` FOREIGN KEY (`tap_3`) REFERENCES `containers` (`id`) ON UPDATE CASCADE,
  ADD CONSTRAINT `pump_tap_4_key` FOREIGN KEY (`tap_4`) REFERENCES `containers` (`id`) ON UPDATE CASCADE,
  ADD CONSTRAINT `pump_tap_5_key` FOREIGN KEY (`tap_5`) REFERENCES `containers` (`id`) ON UPDATE CASCADE,
  ADD CONSTRAINT `pump_tap_6_key` FOREIGN KEY (`tap_6`) REFERENCES `containers` (`id`) ON UPDATE CASCADE;

--
-- Ограничения внешнего ключа таблицы `slaves_shifts`
--
ALTER TABLE `slaves_shifts`
  ADD CONSTRAINT `shift_key` FOREIGN KEY (`shift_id`) REFERENCES `shifts` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `slave_key` FOREIGN KEY (`slave_id`) REFERENCES `slaves` (`id`) ON DELETE NO ACTION ON UPDATE CASCADE;

--
-- Ограничения внешнего ключа таблицы `users`
--
ALTER TABLE `users`
  ADD CONSTRAINT `user_slave_key` FOREIGN KEY (`slave_id`) REFERENCES `slaves` (`id`) ON UPDATE CASCADE;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
