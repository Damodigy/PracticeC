SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='TRADITIONAL,ALLOW_INVALID_DATES';

CREATE SCHEMA IF NOT EXISTS `mydb` DEFAULT CHARACTER SET utf8 COLLATE utf8_general_ci ;
USE `mydb` ;

-- -----------------------------------------------------
-- Table `mydb`.`slaves`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`slaves` (
  `id` BIGINT UNSIGNED NOT NULL AUTO_INCREMENT,
  `fio` TEXT NOT NULL COMMENT 'ФИО',
  `date_employ` TIMESTAMP NOT NULL COMMENT 'Дата принятия на работу',
  `pay_per_shift` INT UNSIGNED NOT NULL COMMENT 'З/П за смену (руб)',
  `rank` TINYTEXT NOT NULL COMMENT 'Должность',
  PRIMARY KEY (`id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`shifts`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`shifts` (
  `id` BIGINT UNSIGNED NOT NULL AUTO_INCREMENT,
  `time_start` TIMESTAMP NOT NULL,
  `time_end` TIMESTAMP NOT NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`slaves_shifts`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`slaves_shifts` (
  `slave_id` BIGINT UNSIGNED NOT NULL,
  `shift_id` BIGINT UNSIGNED NOT NULL,
  INDEX `slave_id_idx` (`slave_id` ASC),
  INDEX `shift_key_idx` (`shift_id` ASC),
  CONSTRAINT `slave_key`
    FOREIGN KEY (`slave_id`)
    REFERENCES `mydb`.`slaves` (`id`)
    ON DELETE NO ACTION
    ON UPDATE CASCADE,
  CONSTRAINT `shift_key`
    FOREIGN KEY (`shift_id`)
    REFERENCES `mydb`.`shifts` (`id`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`fuel_types`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`fuel_types` (
  `id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `type` TINYTEXT NOT NULL COMMENT 'Тип или название',
  `vendor` TEXT NOT NULL COMMENT 'Поставщик',
  `cost_buy` INT UNSIGNED NOT NULL COMMENT 'Цена покупки у поставщика, коп/л',
  `cost_sale` INT UNSIGNED NOT NULL COMMENT 'Цена продажи юзеру, коп/л',
  `data` LONGTEXT NOT NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`containers`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`containers` (
  `id` TINYINT UNSIGNED NOT NULL AUTO_INCREMENT,
  `container_volume` BIGINT UNSIGNED NOT NULL COMMENT 'Объем контейнера, л (дм^3)',
  `fuel_volume` BIGINT UNSIGNED NOT NULL COMMENT 'Объем топлива, л (дм^3)',
  `fuel_id` INT UNSIGNED NULL,
  PRIMARY KEY (`id`),
  INDEX `fuel_types_key_idx` (`fuel_id` ASC),
  CONSTRAINT `fuel_types_key`
    FOREIGN KEY (`fuel_id`)
    REFERENCES `mydb`.`fuel_types` (`id`)
    ON DELETE RESTRICT
    ON UPDATE CASCADE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`pumps`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`pumps` (
  `id` TINYINT UNSIGNED NOT NULL AUTO_INCREMENT,
  `status` TINYINT NOT NULL,
  `tap_1` TINYINT UNSIGNED NULL,
  `tap_2` TINYINT UNSIGNED NULL,
  `tap_3` TINYINT UNSIGNED NULL,
  `tap_4` TINYINT UNSIGNED NULL,
  `tap_5` TINYINT UNSIGNED NULL,
  `tap_6` TINYINT UNSIGNED NULL,
  PRIMARY KEY (`id`),
  INDEX `tap_1_key_idx` (`tap_1` ASC, `tap_2` ASC, `tap_3` ASC, `tap_4` ASC, `tap_5` ASC, `tap_6` ASC),
  CONSTRAINT `containers_key`
    FOREIGN KEY (`tap_1` , `tap_2` , `tap_3` , `tap_4` , `tap_5` , `tap_6`)
    REFERENCES `mydb`.`containers` (`id` , `id` , `id` , `id` , `id` , `id`)
    ON DELETE RESTRICT
    ON UPDATE CASCADE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`fuel_transactions`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`fuel_transactions` (
  `shift_id` BIGINT UNSIGNED NOT NULL,
  `responsible_for` BIGINT UNSIGNED NOT NULL COMMENT 'Ответственный за ттранзакцию\nпринимающий топливо для заливки в бак,\nкассир, совершающий продажу',
  `time_start` TIMESTAMP NOT NULL DEFAULT NOW() COMMENT 'Время начала заливки/продажи',
  `time_end` TIMESTAMP NULL COMMENT 'Время конца заливки/продажи',
  `fuel_id` INT UNSIGNED NOT NULL,
  `container_id` TINYINT UNSIGNED NOT NULL,
  `pump_id` TINYINT UNSIGNED NULL COMMENT 'NULL, если происходит заливка в контейнер',
  `fuel_volume` BIGINT NOT NULL COMMENT 'литры\nположительно, если происходит заливка в контейнер\nотрицателно, если продажа',
  `cost` BIGINT NOT NULL COMMENT 'копейки\nцена транзакции\nотрицателно, если происходит заливка в контейнер\nположительно, если продажа',
  INDEX `slaves_key_idx` (`responsible_for` ASC),
  INDEX `shifts_key_idx` (`shift_id` ASC),
  INDEX `fuel_types_key_idx` (`fuel_id` ASC),
  INDEX `containers_key_idx` (`container_id` ASC),
  INDEX `pumps_key_idx` (`pump_id` ASC),
  CONSTRAINT `slaves_key`
    FOREIGN KEY (`responsible_for`)
    REFERENCES `mydb`.`slaves` (`id`)
    ON DELETE NO ACTION
    ON UPDATE CASCADE,
  CONSTRAINT `shifts_key`
    FOREIGN KEY (`shift_id`)
    REFERENCES `mydb`.`shifts` (`id`)
    ON DELETE NO ACTION
    ON UPDATE CASCADE,
  CONSTRAINT `fuel_types_key`
    FOREIGN KEY (`fuel_id`)
    REFERENCES `mydb`.`fuel_types` (`id`)
    ON DELETE RESTRICT
    ON UPDATE CASCADE,
  CONSTRAINT `containers_key`
    FOREIGN KEY (`container_id`)
    REFERENCES `mydb`.`containers` (`id`)
    ON DELETE RESTRICT
    ON UPDATE CASCADE,
  CONSTRAINT `pumps_key`
    FOREIGN KEY (`pump_id`)
    REFERENCES `mydb`.`pumps` (`id`)
    ON DELETE RESTRICT
    ON UPDATE CASCADE)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
