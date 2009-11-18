-- MySQL Administrator dump 1.4
--
-- ------------------------------------------------------
-- Server version	5.0.19-nt


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;


--
-- Create schema seodb
--

CREATE DATABASE IF NOT EXISTS seodb;
USE seodb;
DROP TABLE IF EXISTS `enginesperproxyresultview`;
DROP VIEW IF EXISTS `enginesperproxyresultview`;
CREATE TABLE `enginesperproxyresultview` (
  `Keyword` varchar(100),
  `Pages` int(10) unsigned,
  `ProxyCity` varchar(100),
  `ProxyCountry` varchar(300),
  `SearchEngineUrl` varchar(500),
  `SearchEngineName` varchar(100),
  `IdProxy` int(10) unsigned,
  `IdSearchEngineCountry` int(10) unsigned,
  `IdRankingMonitorDeepRun` int(10) unsigned,
  `IdRankingMonitorRun` int(10) unsigned
);

DROP TABLE IF EXISTS `account`;
CREATE TABLE `account` (
  `Id` int(11) NOT NULL auto_increment,
  `IdAccountOwner` int(11) default NULL,
  `Name` varchar(250) default NULL,
  `MaxNumberOfUser` int(11) default NULL,
  `MaxNumberOfDomainUser` int(11) default NULL,
  `MaxNumberOfProjects` int(11) default NULL,
  `CompanyName` varchar(300) default NULL,
  `CompanyAddress1` varchar(300) default NULL,
  `CompanyAddress2` varchar(300) default NULL,
  `CompanyCity` varchar(100) default NULL,
  `CompanyState` varchar(255) default NULL,
  `CompanyZip` varchar(100) default NULL,
  `CreditCardNumber` varchar(20) default NULL,
  `CreditCardType` varchar(100) default NULL,
  `CreditCardAddress1` varchar(300) default NULL,
  `CreditCardAddress2` varchar(300) default NULL,
  `CreditCardCity` varchar(100) default NULL,
  `CreditCardZip` varchar(100) default NULL,
  `RecurringBill` bit(1) default '\0',
  `CreatedDate` datetime default NULL,
  `UpdatedDate` datetime default NULL,
  `CreatedBy` varchar(100) default NULL,
  `UpdatedBy` varchar(100) default NULL,
  `Enabled` bit(1) default '\0',
  `AccountExpirationDate` datetime default NULL,
  `CreditCardExpiration` datetime default NULL,
  `CreditCardEmail` varchar(300) default NULL,
  `CreditCardIdCountry` int(11) default NULL,
  `CreditCardState` varchar(255) default NULL,
  `CreditCardCardholder` varchar(250) default NULL,
  `PromoCode` varchar(250) default NULL,
  `CompanyIdCountry` int(10) unsigned default NULL,
  `CompanyPhone` varchar(255) default NULL,
  `CreditCardCVS` varchar(10) default NULL,
  PRIMARY KEY  (`Id`),
  UNIQUE KEY `NameUnique` (`Name`),
  KEY `R_6` (`IdAccountOwner`),
  KEY `R_CT1` (`CreditCardType`),
  CONSTRAINT `account_ibfk_1` FOREIGN KEY (`IdAccountOwner`) REFERENCES `account` (`Id`),
  CONSTRAINT `account_creditcardtype` FOREIGN KEY (`CreditCardType`) REFERENCES `creditcardtype` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

DROP TABLE IF EXISTS `competitor`;
CREATE TABLE `competitor` (
  `Id` int(11) NOT NULL auto_increment,
  `Name` varchar(250) default NULL,
  `Url` varchar(2000) default NULL,
  `Description` varchar(300) default NULL,
  `IdProject` int(11) NOT NULL,
  PRIMARY KEY  (`Id`),
  KEY `R_12` (`IdProject`),
  CONSTRAINT `competitor_ibfk_1` FOREIGN KEY (`IdProject`) REFERENCES `project` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

DROP TABLE IF EXISTS `country`;
CREATE TABLE `country` (
  `Id` int(11) NOT NULL auto_increment,
  `Name` varchar(300) default NULL,
  `FlagUrl` varchar(1000) default NULL,
  `SearchEngineImportance` int(10) unsigned default NULL,
  PRIMARY KEY  (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
INSERT INTO `country` (`Id`,`Name`,`FlagUrl`,`SearchEngineImportance`) VALUES 
 (1,'Afghanistan','images/country/afghanistan-little-flag.gif',NULL),
 (2,'Albania',NULL,NULL),
 (3,'Algeria',NULL,NULL),
 (4,'American Samoa',NULL,NULL),
 (5,'Andorra',NULL,NULL),
 (6,'Angola',NULL,NULL),
 (7,'Anguilla',NULL,NULL),
 (8,'Antarctica',NULL,NULL),
 (9,'Antigua And Barbuda',NULL,NULL),
 (10,'Argentina','images/country/argentina-little-flag.gif',NULL),
 (11,'Armenia',NULL,NULL),
 (12,'Aruba',NULL,NULL),
 (13,'Australia','images/country/australia-little-flag.gif',3),
 (14,'Austria',NULL,NULL),
 (15,'Azerbaijan',NULL,NULL),
 (16,'Bahamas',NULL,NULL),
 (17,'Bahrain',NULL,NULL),
 (18,'Bangladesh',NULL,NULL),
 (19,'Barbados',NULL,NULL),
 (20,'Belarus',NULL,NULL),
 (21,'Belgium','images/country/belgium-little-flag.gif',NULL),
 (22,'Belize',NULL,NULL),
 (23,'Benin',NULL,NULL),
 (24,'Bermuda',NULL,NULL),
 (25,'Bhutan',NULL,NULL),
 (26,'Bolivia',NULL,NULL),
 (27,'Bosnia And Herzegovina',NULL,NULL),
 (28,'Botswana',NULL,NULL),
 (29,'Bouvet Island',NULL,NULL),
 (30,'Brazil',NULL,NULL),
 (31,'British Indian Ocean Territory',NULL,NULL),
 (32,'Brunei Darussalam',NULL,NULL),
 (33,'Bulgaria',NULL,NULL),
 (34,'Burkina Faso',NULL,NULL),
 (35,'Burundi',NULL,NULL),
 (36,'Cambodia',NULL,NULL),
 (37,'Cameroon',NULL,NULL),
 (38,'Canada',NULL,NULL),
 (39,'Cape Verde',NULL,NULL),
 (40,'Cayman Islands',NULL,NULL),
 (41,'Central African Republic',NULL,NULL),
 (42,'Chad',NULL,NULL),
 (43,'Chile',NULL,NULL),
 (44,'China',NULL,NULL),
 (45,'Christmas Island',NULL,NULL),
 (46,'Cocos (Keeling) Islands',NULL,NULL),
 (47,'Colombia',NULL,NULL),
 (48,'Comoros',NULL,NULL),
 (49,'Congo',NULL,NULL),
 (50,'Cook Islands',NULL,NULL),
 (51,'Costa Rica',NULL,NULL),
 (52,'Cote D\'Ivoire',NULL,NULL),
 (53,'Croatia (Local Name: Hrvatska)',NULL,NULL),
 (54,'Cuba',NULL,NULL),
 (55,'Cyprus',NULL,NULL),
 (56,'Czech Republic',NULL,NULL),
 (57,'Denmark',NULL,NULL),
 (58,'Djibouti',NULL,NULL),
 (59,'Dominica',NULL,NULL),
 (60,'Dominican Republic',NULL,NULL),
 (61,'Ecuador',NULL,NULL),
 (62,'Egypt',NULL,NULL),
 (63,'El Salvador',NULL,NULL),
 (64,'Equatorial Guinea',NULL,NULL),
 (65,'Eritrea',NULL,NULL),
 (66,'Estonia',NULL,NULL),
 (67,'Ethiopia',NULL,NULL),
 (68,'Falkland Islands (Malvinas)',NULL,NULL),
 (69,'Faroe Islands',NULL,NULL),
 (70,'Fiji',NULL,NULL),
 (71,'Finland',NULL,NULL),
 (72,'France',NULL,NULL),
 (73,'France, Metropolitan',NULL,NULL),
 (74,'French Guiana',NULL,NULL),
 (75,'French Polynesia',NULL,NULL),
 (76,'French Southern Territories',NULL,NULL),
 (77,'Gabon',NULL,NULL),
 (78,'Gambia',NULL,NULL),
 (79,'Georgia',NULL,NULL),
 (80,'Germany',NULL,NULL),
 (81,'Ghana',NULL,NULL),
 (82,'Gibraltar',NULL,NULL),
 (83,'Greece',NULL,NULL),
 (84,'Greenland',NULL,NULL),
 (85,'Grenada',NULL,NULL),
 (86,'Guadeloupe',NULL,NULL),
 (87,'Guam',NULL,NULL),
 (88,'Guatemala',NULL,NULL),
 (89,'Guinea',NULL,NULL),
 (90,'Guinea-Bissau',NULL,NULL),
 (91,'Guyana',NULL,NULL),
 (92,'Haiti',NULL,NULL),
 (93,'Heard Island & Mcdonald Islands',NULL,NULL),
 (94,'Honduras',NULL,NULL),
 (95,'Hong Kong',NULL,NULL),
 (96,'Hungary',NULL,NULL),
 (97,'Iceland',NULL,NULL),
 (98,'India',NULL,NULL),
 (99,'Indonesia',NULL,NULL),
 (100,'Iran, Islamic Republic Of',NULL,NULL),
 (101,'Iraq',NULL,NULL),
 (102,'Ireland',NULL,NULL),
 (103,'Israel',NULL,NULL),
 (104,'Italy',NULL,NULL),
 (105,'Jamaica',NULL,NULL),
 (106,'Japan','images/country/japan-little-flag.gif',2),
 (107,'Jordan',NULL,NULL),
 (108,'Kazakhstan',NULL,NULL),
 (109,'Kenya',NULL,NULL),
 (110,'Kiribati',NULL,NULL),
 (111,'Korea, Democratic People\'S Republic Of',NULL,NULL),
 (112,'Korea, Republic Of',NULL,NULL),
 (113,'Kuwait',NULL,NULL),
 (114,'Kyrgyzstan',NULL,NULL),
 (115,'Lao People\'S Democratic Republic',NULL,NULL),
 (116,'Latvia',NULL,NULL),
 (117,'Lebanon',NULL,NULL),
 (118,'Lesotho',NULL,NULL),
 (119,'Liberia',NULL,NULL),
 (120,'Libyan Arab Jamahiriya',NULL,NULL),
 (121,'Liechtenstein',NULL,NULL),
 (122,'Lithuania',NULL,NULL),
 (123,'Luxembourg',NULL,NULL),
 (124,'Macau',NULL,NULL),
 (125,'Macedonia, The Former Yugoslav Republic Of',NULL,NULL),
 (126,'Madagascar',NULL,NULL),
 (127,'Malawi',NULL,NULL),
 (128,'Malaysia',NULL,NULL),
 (129,'Maldives',NULL,NULL),
 (130,'Mali',NULL,NULL),
 (131,'Malta',NULL,NULL),
 (132,'Marshall Islands',NULL,NULL),
 (133,'Martinique',NULL,NULL),
 (134,'Mauritania',NULL,NULL),
 (135,'Mauritius',NULL,NULL),
 (136,'Mayotte',NULL,NULL),
 (137,'Mexico','images/country/mexico-little-flag.gif',NULL),
 (138,'Micronesia, Federated States Of',NULL,NULL),
 (139,'Moldova, Republic Of',NULL,NULL),
 (140,'Monaco',NULL,NULL),
 (141,'Mongolia',NULL,NULL),
 (142,'Montserrat',NULL,NULL),
 (143,'Morocco',NULL,NULL),
 (144,'Mozambique',NULL,NULL),
 (145,'Myanmar',NULL,NULL),
 (146,'Namibia',NULL,NULL),
 (147,'Nauru',NULL,NULL),
 (148,'Nepal',NULL,NULL),
 (149,'Netherlands',NULL,NULL),
 (150,'Netherlands Antilles',NULL,NULL),
 (151,'New Caledonia',NULL,NULL),
 (152,'New Zealand',NULL,NULL),
 (153,'Nicaragua',NULL,NULL),
 (154,'Niger',NULL,NULL),
 (155,'Nigeria',NULL,NULL),
 (156,'Niue',NULL,NULL),
 (157,'Norfolk Island',NULL,NULL),
 (158,'Northern Mariana Islands',NULL,NULL),
 (159,'Norway',NULL,NULL),
 (160,'Oman',NULL,NULL),
 (161,'Pakistan',NULL,NULL),
 (162,'Palau',NULL,NULL),
 (163,'Panama',NULL,NULL),
 (164,'Papua New Guinea',NULL,NULL),
 (165,'Paraguay',NULL,NULL),
 (166,'Peru','images/country/peru-little-flag.gif',NULL),
 (167,'Philippines',NULL,NULL),
 (168,'Pitcairn',NULL,NULL),
 (169,'Poland',NULL,NULL),
 (170,'Portugal',NULL,NULL),
 (171,'Puerto Rico',NULL,NULL),
 (172,'Qatar',NULL,NULL),
 (173,'Reunion',NULL,NULL),
 (174,'Romania',NULL,NULL),
 (175,'Russian Federation',NULL,NULL),
 (176,'Rwanda',NULL,NULL),
 (177,'Saint Helena',NULL,NULL),
 (178,'Saint Kitts And Nevis',NULL,NULL),
 (179,'Saint Lucia',NULL,NULL),
 (180,'Saint Pierre And Miquelon',NULL,NULL),
 (181,'Saint Vincent And The Grenadines',NULL,NULL),
 (182,'Samoa',NULL,NULL),
 (183,'San Marino',NULL,NULL),
 (184,'Sao Tome And Principe',NULL,NULL),
 (185,'Saudi Arabia',NULL,NULL),
 (186,'Senegal',NULL,NULL),
 (187,'Seychelles',NULL,NULL),
 (188,'Sierra Leone',NULL,NULL),
 (189,'Singapore',NULL,NULL),
 (190,'Slovakia (Slovak Republic)',NULL,NULL),
 (191,'Slovenia',NULL,NULL),
 (192,'Solomon Islands',NULL,NULL),
 (193,'Somalia',NULL,NULL),
 (194,'South Africa','images/country/southafrica-little-flag.gif',1),
 (195,'Spain','images/country/spain-little-flag.gif',NULL),
 (196,'Sri Lanka',NULL,NULL),
 (197,'Sudan',NULL,NULL),
 (198,'Suriname',NULL,NULL),
 (199,'Svalbard And Jan Mayen Islands',NULL,NULL),
 (200,'Swaziland',NULL,NULL),
 (201,'Sweden',NULL,NULL),
 (202,'Switzerland',NULL,NULL),
 (203,'Syrian Arab Republic',NULL,NULL),
 (204,'Taiwan, Province Of China',NULL,NULL),
 (205,'Tajikistan',NULL,NULL),
 (206,'Tanzania, United Republic Of',NULL,NULL),
 (207,'Thailand',NULL,NULL),
 (208,'Togo',NULL,NULL),
 (209,'Tokelau',NULL,NULL),
 (210,'Tonga',NULL,NULL),
 (211,'Trinidad And Tobago',NULL,NULL),
 (212,'Tunisia',NULL,NULL),
 (213,'Turkey',NULL,NULL),
 (214,'Turkmenistan',NULL,NULL),
 (215,'Turks And Caicos Islands',NULL,NULL),
 (216,'Tuvalu',NULL,NULL),
 (217,'Uganda',NULL,NULL),
 (218,'Ukraine',NULL,NULL),
 (219,'United Arab Emirates',NULL,NULL),
 (220,'United Kingdom','images/country/unitedkingdom-little-flag.gif',4),
 (221,'United States','images/country/usa-little-flag.gif',5),
 (222,'United States Minor Outlying Islands',NULL,NULL),
 (223,'Uruguay',NULL,NULL),
 (224,'Uzbekistan',NULL,NULL),
 (225,'Vanuatu',NULL,NULL),
 (226,'Vatican City State (Holy See)',NULL,NULL),
 (227,'Venezuela',NULL,NULL),
 (228,'Viet Nam',NULL,NULL),
 (229,'Virgin Islands (British)',NULL,NULL),
 (230,'Virgin Islands (U.S.)',NULL,NULL),
 (231,'Wallis And Futuna Islands',NULL,NULL),
 (232,'Western Sahara',NULL,NULL),
 (233,'Yemen',NULL,NULL),
 (234,'Yugoslavia',NULL,NULL),
 (235,'Zaire',NULL,NULL),
 (236,'Zambia',NULL,NULL),
 (237,'Zimbabwe',NULL,NULL);

DROP TABLE IF EXISTS `creditcardtype`;
CREATE TABLE `creditcardtype` (
  `Id` varchar(100) NOT NULL,
  `Name` varchar(300) default NULL,
  PRIMARY KEY  (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
INSERT INTO `creditcardtype` (`Id`,`Name`) VALUES 
 ('AMEX','American Express'),
 ('MASTERDCARD','Mastercard'),
 ('VISA','Visa');

DROP TABLE IF EXISTS `frequency`;
CREATE TABLE `frequency` (
  `Id` int(10) unsigned NOT NULL auto_increment,
  `Name` varchar(50) NOT NULL,
  PRIMARY KEY  (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
INSERT INTO `frequency` (`Id`,`Name`) VALUES 
 (1,'Week'),
 (2,'TwoWeeks'),
 (3,'Month');

DROP TABLE IF EXISTS `keyword`;
CREATE TABLE `keyword` (
  `Id` int(11) NOT NULL auto_increment,
  `Keyword` varchar(100) default NULL,
  `IdKeywordList` int(11) NOT NULL,
  PRIMARY KEY  (`Id`),
  KEY `R_13` (`IdKeywordList`),
  CONSTRAINT `keyword_ibfk_1` FOREIGN KEY (`IdKeywordList`) REFERENCES `keywordlist` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

DROP TABLE IF EXISTS `keywordanalysis`;
CREATE TABLE `keywordanalysis` (
  `Id` int(10) unsigned NOT NULL auto_increment,
  `Keyword` varchar(100) NOT NULL,
  `IdRankingMonitorRun` int(10) unsigned NOT NULL,
  `GoogleResults` int(10) unsigned default NULL,
  `AllInTitle` int(10) unsigned default NULL,
  `AliasDomains` int(10) unsigned default NULL,
  `CPC` float default NULL,
  `DailySearches` int(10) unsigned default NULL,
  `Results` int(10) unsigned default NULL,
  `Pages` int(10) unsigned default NULL,
  `Engines` int(10) unsigned default NULL,
  `Status` char(1) NOT NULL default 'P',
  PRIMARY KEY  (`Id`),
  UNIQUE KEY `KeywordUnique` (`Keyword`,`IdRankingMonitorRun`),
  KEY `keywordanalysis_ibfk_1` (`IdRankingMonitorRun`),
  KEY `keywordanalysis_ibfk_2` (`Status`),
  CONSTRAINT `keywordanalysis_ibfk_1` FOREIGN KEY (`IdRankingMonitorRun`) REFERENCES `rankingmonitorrun` (`Id`),
  CONSTRAINT `keywordanalysis_ibfk_2` FOREIGN KEY (`Status`) REFERENCES `status` (`Name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

DROP TABLE IF EXISTS `keyworddeepanalysis`;
CREATE TABLE `keyworddeepanalysis` (
  `Id` int(10) unsigned NOT NULL auto_increment,
  `IdRankingMonitorDeepRun` int(10) unsigned NOT NULL,
  `Keyword` varchar(100) NOT NULL,
  `Pages` int(10) unsigned default NULL,
  `Status` char(1) NOT NULL default 'P',
  PRIMARY KEY  (`Id`),
  UNIQUE KEY `KeywordUnique` (`IdRankingMonitorDeepRun`,`Keyword`),
  KEY `keyworddeepanalysis_ibfk_2` (`Status`),
  CONSTRAINT `keyworddeepanalysis_ibfk_1` FOREIGN KEY (`IdRankingMonitorDeepRun`) REFERENCES `rankingmonitordeeprun` (`Id`),
  CONSTRAINT `keyworddeepanalysis_ibfk_2` FOREIGN KEY (`Status`) REFERENCES `status` (`Name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

DROP TABLE IF EXISTS `keywordlist`;
CREATE TABLE `keywordlist` (
  `Id` int(11) NOT NULL auto_increment,
  `Name` varchar(250) default NULL,
  `IdProject` int(11) NOT NULL,
  `Enabled` bit(1) default '\0',
  PRIMARY KEY  (`Id`),
  KEY `R_14` (`IdProject`),
  CONSTRAINT `keywordlist_ibfk_1` FOREIGN KEY (`IdProject`) REFERENCES `project` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

DROP TABLE IF EXISTS `monitorkeywordlist`;
CREATE TABLE `monitorkeywordlist` (
  `Id` int(10) unsigned NOT NULL auto_increment,
  `IdProject` int(11) NOT NULL,
  `IdKeywordList` int(11) NOT NULL,
  PRIMARY KEY  (`Id`),
  UNIQUE KEY `MonitorKeywordList_Unique` (`IdProject`,`IdKeywordList`),
  KEY `keywordList` (`IdKeywordList`),
  KEY `monitorkeywordlist_ibfk_1` (`IdProject`),
  CONSTRAINT `monitorkeywordlist_ibfk_1` FOREIGN KEY (`IdProject`) REFERENCES `project` (`Id`),
  CONSTRAINT `monitorkeywordlist_ibfk_2` FOREIGN KEY (`IdKeywordList`) REFERENCES `keywordlist` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

DROP TABLE IF EXISTS `monitorproxyserver`;
CREATE TABLE `monitorproxyserver` (
  `Id` int(10) unsigned NOT NULL auto_increment,
  `IdProject` int(11) NOT NULL,
  `IdProxyServer` int(10) unsigned NOT NULL,
  PRIMARY KEY  (`Id`),
  UNIQUE KEY `MonitorProxySever_Unique` (`IdProject`,`IdProxyServer`),
  KEY `monitorproxyserver_ibfk_1` (`IdProxyServer`),
  KEY `IdProject` (`IdProject`),
  CONSTRAINT `monitorproxyserver_ibfk_1` FOREIGN KEY (`IdProxyServer`) REFERENCES `proxyserver` (`Id`),
  CONSTRAINT `monitorproxyserver_ibfk_2` FOREIGN KEY (`IdProject`) REFERENCES `project` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

DROP TABLE IF EXISTS `monitorsearchenginecountry`;
CREATE TABLE `monitorsearchenginecountry` (
  `Id` int(10) unsigned NOT NULL auto_increment,
  `IdProject` int(11) NOT NULL,
  `IdSearchEngineCountry` int(10) unsigned NOT NULL,
  PRIMARY KEY  (`Id`),
  UNIQUE KEY `MonitorSearchEngineCountry_Unique` (`IdProject`,`IdSearchEngineCountry`),
  KEY `monitorsearchenginecountry_ibfk_1` (`IdSearchEngineCountry`),
  KEY `monitorsearchenginecountry_ibfk_2` (`IdProject`),
  CONSTRAINT `monitorsearchenginecountry_ibfk_2` FOREIGN KEY (`IdProject`) REFERENCES `project` (`Id`),
  CONSTRAINT `monitorsearchenginecountry_ibfk_1` FOREIGN KEY (`IdSearchEngineCountry`) REFERENCES `searchenginecountry` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

DROP TABLE IF EXISTS `profileproperty`;
CREATE TABLE `profileproperty` (
  `Id` int(10) unsigned NOT NULL auto_increment,
  `IdUser` int(10) unsigned NOT NULL,
  `Name` varchar(50) collate utf8_unicode_ci NOT NULL,
  `StringValue` text collate utf8_unicode_ci,
  `BinaryValue` blob,
  PRIMARY KEY  (`Id`),
  KEY `ProfileProperty_ibfk_1` (`IdUser`),
  CONSTRAINT `ProfileProperty_ibfk_1` FOREIGN KEY (`IdUser`) REFERENCES `seoprofile` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

DROP TABLE IF EXISTS `project`;
CREATE TABLE `project` (
  `Id` int(11) NOT NULL auto_increment,
  `Name` varchar(1000) default NULL,
  `Domain` varchar(1000) default NULL,
  `ClientName` varchar(100) default NULL,
  `ContactEmail` varchar(300) default NULL,
  `ContactName` varchar(1000) default NULL,
  `ContactPhone` varchar(100) default NULL,
  `IdAccount` int(11) NOT NULL,
  `CreatedDate` datetime default NULL,
  `UpdatedDate` datetime default NULL,
  `CreatedBy` varchar(100) default NULL,
  `UpdatedBy` varchar(100) default NULL,
  `Enabled` bit(1) default '\0',
  `IdMonitorFrequency` int(10) unsigned default NULL,
  `MonitorUpdatedBy` varchar(100) default NULL,
  `MonitorUpdatedDate` datetime default NULL,
  PRIMARY KEY  (`Id`),
  KEY `R_11` (`IdAccount`),
  KEY `project_ibfk_2` (`IdMonitorFrequency`),
  CONSTRAINT `project_ibfk_2` FOREIGN KEY (`IdMonitorFrequency`) REFERENCES `frequency` (`Id`),
  CONSTRAINT `project_ibfk_1` FOREIGN KEY (`IdAccount`) REFERENCES `account` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

DROP TABLE IF EXISTS `projectpermission`;
CREATE TABLE `projectpermission` (
  `Id` int(11) NOT NULL auto_increment,
  `Name` varchar(100) default NULL,
  `Description` varchar(250) default NULL,
  PRIMARY KEY  (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
INSERT INTO `projectpermission` (`Id`,`Name`,`Description`) VALUES 
 (1,'RunPageInformationTool','The User can run the Page Information Tools on the Project.'),
 (2,'RunCheckServerTool','The User can run the Site Checker on the Project.'),
 (3,'DomainRead','The User has the ability to view the Domain.'),
 (4,'DomainUpdate','The User has the ability to edit the Domain.'),
 (5,'CompetitorCreate','The User has to ability to create Competitors.'),
 (6,'CompetitorRead','The User has to ability to view Competitors.'),
 (7,'CompetitorUpdate','The User has to ability to edit Competitors.'),
 (8,'CompetitorDelete','The User has to ability to delete Competitors.'),
 (9,'KeywordListsCreate','The User has to ability to create Keyword Lists.'),
 (10,'KeywordListsRead','The User has to ability to view Keyword Lists.'),
 (11,'KeywordListsUpdate','The User has to ability to edit Keyword Lists.'),
 (12,'KeywordListsDelete','The User has to ability to delete Keyword Lists.'),
 (13,'MonitorRead','The User has the ability to read Reports about Monitor Runs'),
 (14,'MonitorConfigure','The User has the ability to set parameters for running the Monitor'),
 (15,'MonitorRun','The User has the ability to execute manually the Monitor');

DROP TABLE IF EXISTS `projectpermissionrole`;
CREATE TABLE `projectpermissionrole` (
  `Id` int(11) NOT NULL auto_increment,
  `IdProjectRole` int(11) NOT NULL,
  `IdProjectPermission` int(11) NOT NULL,
  PRIMARY KEY  (`Id`),
  KEY `R_18` (`IdProjectRole`),
  KEY `R_19` (`IdProjectPermission`),
  CONSTRAINT `projectpermissionrole_ibfk_1` FOREIGN KEY (`IdProjectRole`) REFERENCES `projectrole` (`Id`),
  CONSTRAINT `projectpermissionrole_ibfk_2` FOREIGN KEY (`IdProjectPermission`) REFERENCES `projectpermission` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
INSERT INTO `projectpermissionrole` (`Id`,`IdProjectRole`,`IdProjectPermission`) VALUES 
 (1,1,1),
 (2,1,2),
 (3,1,3),
 (4,1,4),
 (5,1,5),
 (6,1,6),
 (7,1,7),
 (8,1,8),
 (9,1,9),
 (10,1,10),
 (11,1,11),
 (12,1,12),
 (13,2,1),
 (14,2,2),
 (15,2,3),
 (16,2,6),
 (17,2,10);

DROP TABLE IF EXISTS `projectrole`;
CREATE TABLE `projectrole` (
  `Id` int(11) NOT NULL auto_increment,
  `Name` varchar(100) default NULL,
  `Description` varchar(250) default NULL,
  `Enabled` bit(1) default '\0',
  PRIMARY KEY  (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
INSERT INTO `projectrole` (`Id`,`Name`,`Description`,`Enabled`) VALUES 
 (1,'ProjectAdmin','ProjectAdmin',0x01),
 (2,'DomainUser','DomainUser',0x01);

DROP TABLE IF EXISTS `projectuser`;
CREATE TABLE `projectuser` (
  `Id` int(11) NOT NULL auto_increment,
  `IdUser` int(11) NOT NULL,
  `IdProject` int(11) NOT NULL,
  `IdProjectRole` int(11) NOT NULL,
  `MonitorEmails` bit(1) default '\0',
  `Enabled` bit(1) default '\0',
  PRIMARY KEY  (`Id`),
  KEY `R_15` (`IdUser`),
  KEY `R_16` (`IdProject`),
  KEY `R_17` (`IdProjectRole`),
  CONSTRAINT `projectuser_ibfk_1` FOREIGN KEY (`IdUser`) REFERENCES `seotoolsetuser` (`Id`),
  CONSTRAINT `projectuser_ibfk_2` FOREIGN KEY (`IdProject`) REFERENCES `project` (`Id`),
  CONSTRAINT `projectuser_ibfk_3` FOREIGN KEY (`IdProjectRole`) REFERENCES `projectrole` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

DROP TABLE IF EXISTS `proxyserver`;
CREATE TABLE `proxyserver` (
  `Id` int(10) unsigned NOT NULL,
  `IdCountry` int(10) unsigned NOT NULL,
  `City` varchar(100) NOT NULL,
  `ImportanceLevel` int(11) default NULL,
  PRIMARY KEY  (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
INSERT INTO `proxyserver` (`Id`,`IdCountry`,`City`,`ImportanceLevel`) VALUES 
 (0,221,'Primary Server',6),
 (1,221,'Dallas',5),
 (2,13,'Sydney',4),
 (3,194,'Cape Town',3),
 (4,106,'Tokyo',2),
 (5,220,'United Kingdom',1);

DROP TABLE IF EXISTS `rankingmonitordeeprun`;
CREATE TABLE `rankingmonitordeeprun` (
  `Id` int(10) unsigned NOT NULL auto_increment,
  `IdRankingMonitorRun` int(10) unsigned NOT NULL,
  `IdProxy` int(10) unsigned NOT NULL,
  `IdSearchEngineCountry` int(10) unsigned NOT NULL,
  `Status` char(1) NOT NULL,
  `StatusReason` text,
  `PageRank` int(10) unsigned default NULL,
  `InboundLinks` int(10) unsigned default NULL,
  `PagesIndexed` int(10) unsigned default NULL,
  PRIMARY KEY  (`Id`),
  UNIQUE KEY `RankingMonitorDeepRunUnique` (`IdRankingMonitorRun`,`IdProxy`,`IdSearchEngineCountry`),
  KEY `RankingMonitorDeepRun_ibfk_1` (`IdProxy`),
  KEY `RankingMonitorDeepRun_ibfk_2` (`IdSearchEngineCountry`),
  KEY `RankingMonitorDeepRun_ibfk_4` (`Status`),
  KEY `RankingMonitorDeepRun_ibfk_3` (`IdRankingMonitorRun`),
  CONSTRAINT `RankingMonitorDeepRun_ibfk_1` FOREIGN KEY (`IdProxy`) REFERENCES `proxyserver` (`Id`),
  CONSTRAINT `RankingMonitorDeepRun_ibfk_2` FOREIGN KEY (`IdSearchEngineCountry`) REFERENCES `searchenginecountry` (`Id`),
  CONSTRAINT `RankingMonitorDeepRun_ibfk_4` FOREIGN KEY (`Status`) REFERENCES `status` (`Name`),
  CONSTRAINT `RankingMonitorDeepRun_ibfk_3` FOREIGN KEY (`IdRankingMonitorRun`) REFERENCES `rankingmonitorrun` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

DROP TABLE IF EXISTS `rankingmonitorrun`;
CREATE TABLE `rankingmonitorrun` (
  `Id` int(10) unsigned NOT NULL auto_increment,
  `IdProject` int(11) NOT NULL,
  `User` varchar(100) NOT NULL,
  `AnalysisType` char(1) NOT NULL,
  `Status` char(1) default NULL,
  `ExecutionDate` datetime default NULL,
  `EndDate` datetime default NULL,
  `StatusReason` text,
  PRIMARY KEY  (`Id`),
  KEY `RankingMonitorRun_ibfk_1` (`Status`),
  KEY `RankingMonitorRun_ibfk_2` (`IdProject`),
  CONSTRAINT `RankingMonitorRun_ibfk_2` FOREIGN KEY (`IdProject`) REFERENCES `project` (`Id`),
  CONSTRAINT `RankingMonitorRun_ibfk_1` FOREIGN KEY (`Status`) REFERENCES `status` (`Name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

DROP TABLE IF EXISTS `searchengine`;
CREATE TABLE `searchengine` (
  `Id` int(10) unsigned NOT NULL,
  `Name` varchar(100) NOT NULL,
  `Description` varchar(300) NOT NULL,
  `UrlLogo` varchar(1000) default NULL,
  `UrlBigLogo` varchar(1000) default NULL,
  PRIMARY KEY  (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
INSERT INTO `searchengine` (`Id`,`Name`,`Description`,`UrlLogo`,`UrlBigLogo`) VALUES 
 (1,'Google','','images/ViewRankingMonitorReport/google-favicon.gif','images/ViewRankingMonitorReport/google-big-logo.gif'),
 (2,'Yahoo!','','images/ViewRankingMonitorReport/yahoo-favicon.gif','images/ViewRankingMonitorReport/yahoo-big-logo.gif'),
 (3,'Live','','images/ViewRankingMonitorReport/msn-favicon.gif','images/ViewRankingMonitorReport/msn-big-logo.gif');

DROP TABLE IF EXISTS `searchenginecountry`;
CREATE TABLE `searchenginecountry` (
  `Id` int(10) unsigned NOT NULL,
  `IdSearchEngine` int(10) unsigned NOT NULL,
  `IdCountry` int(11) NOT NULL,
  `Url` varchar(500) default NULL,
  PRIMARY KEY  (`Id`),
  KEY `searchenginecountry_ibfk_2` (`IdCountry`),
  KEY `searchenginecountry_ibfk_1` (`IdSearchEngine`),
  CONSTRAINT `searchenginecountry_ibfk_1` FOREIGN KEY (`IdSearchEngine`) REFERENCES `searchengine` (`Id`),
  CONSTRAINT `searchenginecountry_ibfk_2` FOREIGN KEY (`IdCountry`) REFERENCES `country` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
INSERT INTO `searchenginecountry` (`Id`,`IdSearchEngine`,`IdCountry`,`Url`) VALUES 
 (1,1,1,'www.google.com'),
 (2,2,1,'www.yahoo.com'),
 (3,3,1,'www.live.com'),
 (4,1,21,'www.google.co.uk'),
 (5,2,21,'uk.yahoo.com'),
 (6,3,21,'www.live.com/?mkt=en-gb'),
 (10,1,10,'google.com.ar '),
 (11,1,13,'google.com.au '),
 (12,1,106,'google.com.jp '),
 (13,1,137,'google.com.mx '),
 (14,1,166,'google.com.pe '),
 (15,1,194,'google.com.za '),
 (16,1,195,'google.com.es '),
 (17,1,220,'google.com.uk '),
 (18,1,221,'google.com '),
 (19,3,10,'live.com.ar '),
 (20,3,13,'live.com.au '),
 (21,3,106,'live.com.jp '),
 (22,3,137,'live.com.mx '),
 (23,3,166,'live.com.pe '),
 (24,3,194,'live.com.za '),
 (25,3,195,'live.com.es '),
 (26,3,220,'live.com.uk '),
 (27,3,221,'live.com '),
 (28,2,10,'yahoo.com.ar '),
 (29,2,13,'yahoo.com.au '),
 (30,2,106,'yahoo.com.jp '),
 (31,2,137,'yahoo.com.mx '),
 (32,2,166,'yahoo.com.pe '),
 (33,2,194,'yahoo.com.za '),
 (34,2,195,'yahoo.com.es '),
 (35,2,220,'yahoo.com.uk '),
 (36,2,221,'yahoo.com ');

DROP TABLE IF EXISTS `seoprofile`;
CREATE TABLE `seoprofile` (
  `Id` int(10) unsigned NOT NULL auto_increment,
  `Name` varchar(100) collate utf8_unicode_ci NOT NULL,
  `ApplicationName` varchar(100) collate utf8_unicode_ci default NULL,
  `CreatedDate` datetime default NULL,
  `UpdatedDate` datetime default NULL,
  `ProfileType` int(11) default NULL,
  `LastActivityDate` datetime default NULL,
  `LastPropertyChangedDate` datetime default NULL,
  PRIMARY KEY  (`Id`),
  KEY `Name` (`Name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

DROP TABLE IF EXISTS `seotoolsetuser`;
CREATE TABLE `seotoolsetuser` (
  `Id` int(11) NOT NULL auto_increment,
  `FirstName` varchar(250) default NULL,
  `LastName` varchar(250) default NULL,
  `Email` varchar(300) default NULL,
  `Address1` varchar(300) default NULL,
  `Address2` varchar(300) default NULL,
  `CityTown` varchar(255) default NULL,
  `State` varchar(255) default NULL,
  `Zip` varchar(100) default NULL,
  `Telephone` varchar(255) default NULL,
  `Login` varchar(100) default NULL,
  `Password` varchar(100) default NULL,
  `PasswordQuestion` varchar(300) default NULL,
  `PasswordAnswer` varchar(300) default NULL,
  `Enabled` bit(1) default '\0',
  `LastFailedLoginDate` datetime default NULL,
  `LastActivityDate` datetime default NULL,
  `LastPasswordChangedDate` datetime default NULL,
  `IsLockedOut` bit(1) default '\0',
  `LockedOutDate` datetime default NULL,
  `FailedPasswordAttemptCount` int(11) default NULL,
  `ExpirationDate` datetime default NULL,
  `CreatedBy` varchar(100) default NULL,
  `UpdatedBy` varchar(100) default NULL,
  `IdCountry` int(11) default NULL,
  `IdUserRole` int(11) default NULL,
  `IdAccount` int(11) NOT NULL,
  `CreatedDate` datetime default NULL,
  `UpdatedDate` datetime default NULL,
  `LastLoginDate` datetime default NULL,
  PRIMARY KEY  (`Id`),
  KEY `R_1` (`IdCountry`),
  KEY `R_2` (`IdUserRole`),
  KEY `R_7` (`IdAccount`),
  CONSTRAINT `seotoolsetuser_ibfk_1` FOREIGN KEY (`IdCountry`) REFERENCES `country` (`Id`),
  CONSTRAINT `seotoolsetuser_ibfk_2` FOREIGN KEY (`IdUserRole`) REFERENCES `userrole` (`Id`),
  CONSTRAINT `seotoolsetuser_ibfk_3` FOREIGN KEY (`IdAccount`) REFERENCES `account` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

DROP TABLE IF EXISTS `service`;
CREATE TABLE `service` (
  `Id` int(11) NOT NULL auto_increment,
  `Name` varchar(250) default NULL,
  `Description` varchar(250) default NULL,
  `Enabled` bit(1) default '\0',
  PRIMARY KEY  (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

DROP TABLE IF EXISTS `status`;
CREATE TABLE `status` (
  `Name` char(1) NOT NULL,
  `Description` varchar(100) NOT NULL,
  PRIMARY KEY  (`Name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
INSERT INTO `status` (`Name`,`Description`) VALUES 
 ('C','Complete'),
 ('F','Failed'),
 ('P','In Progress');

DROP TABLE IF EXISTS `subscription`;
CREATE TABLE `subscription` (
  `Id` int(11) NOT NULL auto_increment,
  `IdAccount` int(11) NOT NULL,
  `IdService` int(11) NOT NULL,
  PRIMARY KEY  (`Id`),
  KEY `R_8` (`IdAccount`),
  KEY `R_10` (`IdService`),
  CONSTRAINT `subscription_ibfk_1` FOREIGN KEY (`IdAccount`) REFERENCES `account` (`Id`),
  CONSTRAINT `subscription_ibfk_2` FOREIGN KEY (`IdService`) REFERENCES `service` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

DROP TABLE IF EXISTS `userpermission`;
CREATE TABLE `userpermission` (
  `Id` int(11) NOT NULL auto_increment,
  `Name` varchar(250) default NULL,
  `Description` varchar(300) default NULL,
  PRIMARY KEY  (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
INSERT INTO `userpermission` (`Id`,`Name`,`Description`) VALUES 
 (1,'ProjectsCreate','Ability to create new Projects.'),
 (2,'ProjectsRead','Ability to view Projects.'),
 (3,'ProjectsUpdate','Ability to edit and update Projects.'),
 (4,'ProjectsDelete','Ability to delete Projects.'),
 (5,'UsersCreate','Ability to create new Users.'),
 (6,'UsersRead','Ability to view Users.'),
 (7,'UsersUpdate','Ability to edit and update Users.'),
 (8,'UsersDelete','Ability to delete Users.'),
 (9,'EditPersonalInformation','Ability for a User to edit and update their own information.'),
 (10,'AccountsCreate','Ability to create new Accounts.'),
 (11,'AccountsRead','Ability to view Accounts.'),
 (12,'AccountsUpdate','Ability to edit and update Accounts.'),
 (13,'AccountsDelete','Ability to delete Accounts.'),
 (14,'AccountingReports','Ability to run reports on Accounts.'),
 (15,'PermissionsAreGlobal','The user can also modify data in other Accounts. The limits and scope are discussed in 3.1.1.2 \"Permissions are Global\" Permission.');

DROP TABLE IF EXISTS `userrole`;
CREATE TABLE `userrole` (
  `Id` int(11) NOT NULL auto_increment,
  `Name` varchar(250) default NULL,
  `Description` varchar(300) default NULL,
  `Enabled` bit(1) default '\0',
  PRIMARY KEY  (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
INSERT INTO `userrole` (`Id`,`Name`,`Description`,`Enabled`) VALUES 
 (1,'Administrator','Administrator',0x01),
 (2,'AccountingUser','AccountingUser',0x01),
 (3,'TechSupportUser','TechSupportUser',0x01),
 (4,'Employee','Employee',0x01),
 (5,'Subscriber','Subscriber',0x01),
 (6,'Client','Client',0x01);

DROP TABLE IF EXISTS `userrolpermission`;
CREATE TABLE `userrolpermission` (
  `Id` int(11) NOT NULL auto_increment,
  `IdUserPermission` int(11) NOT NULL,
  `IdUserRole` int(11) NOT NULL,
  PRIMARY KEY  (`Id`),
  KEY `R_3` (`IdUserPermission`),
  KEY `R_4` (`IdUserRole`),
  CONSTRAINT `userrolpermission_ibfk_1` FOREIGN KEY (`IdUserPermission`) REFERENCES `userpermission` (`Id`),
  CONSTRAINT `userrolpermission_ibfk_2` FOREIGN KEY (`IdUserRole`) REFERENCES `userrole` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
INSERT INTO `userrolpermission` (`Id`,`IdUserPermission`,`IdUserRole`) VALUES 
 (1,15,1),
 (2,10,1),
 (3,11,1),
 (4,12,1),
 (5,13,1),
 (6,14,1),
 (7,1,1),
 (8,2,1),
 (9,3,1),
 (10,4,1),
 (11,5,1),
 (12,6,1),
 (13,7,1),
 (14,8,1),
 (15,9,1),
 (16,15,2),
 (17,10,2),
 (18,11,2),
 (19,12,2),
 (20,13,2),
 (21,14,2),
 (22,1,2),
 (23,2,2),
 (24,3,2),
 (25,4,2),
 (26,5,2),
 (27,6,2),
 (28,7,2),
 (29,8,2),
 (30,9,2),
 (31,15,3),
 (32,10,3),
 (33,11,3),
 (34,12,3),
 (35,13,3),
 (36,1,3),
 (37,2,3),
 (38,3,3),
 (39,4,3),
 (40,5,3),
 (41,6,3),
 (42,7,3),
 (43,8,3),
 (44,9,3),
 (45,1,4),
 (46,2,4),
 (47,3,4),
 (48,4,4),
 (49,5,4),
 (50,6,4),
 (51,7,4),
 (52,8,4),
 (53,9,4),
 (54,9,6);

DROP TABLE IF EXISTS `enginesperproxyresultview`;
DROP VIEW IF EXISTS `enginesperproxyresultview`;
CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `enginesperproxyresultview` AS select `keyworddeepanalysis`.`Keyword` AS `Keyword`,`keyworddeepanalysis`.`Pages` AS `Pages`,`proxyserver`.`City` AS `ProxyCity`,`country`.`Name` AS `ProxyCountry`,`searchenginecountry`.`Url` AS `SearchEngineUrl`,`searchengine`.`Name` AS `SearchEngineName`,`rankingmonitordeeprun`.`IdProxy` AS `IdProxy`,`rankingmonitordeeprun`.`IdSearchEngineCountry` AS `IdSearchEngineCountry`,`rankingmonitordeeprun`.`Id` AS `IdRankingMonitorDeepRun`,`rankingmonitordeeprun`.`IdRankingMonitorRun` AS `IdRankingMonitorRun` from (((((`rankingmonitordeeprun` join `keyworddeepanalysis` on((`rankingmonitordeeprun`.`Id` = `keyworddeepanalysis`.`IdRankingMonitorDeepRun`))) join `searchenginecountry` on((`rankingmonitordeeprun`.`IdSearchEngineCountry` = `searchenginecountry`.`Id`))) join `searchengine` on((`searchenginecountry`.`IdSearchEngine` = `searchengine`.`Id`))) join `proxyserver` on((`rankingmonitordeeprun`.`IdProxy` = `proxyserver`.`Id`))) join `country` on((`proxyserver`.`IdCountry` = `country`.`Id`)));



/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
