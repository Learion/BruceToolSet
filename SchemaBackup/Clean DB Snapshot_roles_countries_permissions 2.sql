-- MySQL Administrator dump 1.4
--
-- ------------------------------------------------------
-- Server version	5.0.19-nt-log


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

--
-- Definition of table `account`
--

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
  `CompanyState` varchar(250) default NULL,
  `CompanyZip` varchar(10) default NULL,
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
  `CreditCardState` varchar(250) default NULL,
  `CreditCardCardholder` varchar(250) default NULL,
  `PromoCode` varchar(250) default NULL,
  `CompanyIdCountry` int(10) unsigned default NULL,
  `CompanyPhone` varchar(35) default NULL,
  `CreditCardCVS` varchar(10) default NULL,
  PRIMARY KEY  (`Id`),
  UNIQUE KEY `NameUnique` (`Name`),
  KEY `R_6` (`IdAccountOwner`),
  KEY `R_CT1` (`CreditCardType`),
  CONSTRAINT `account_ibfk_1` FOREIGN KEY (`IdAccountOwner`) REFERENCES `account` (`Id`),
  CONSTRAINT `account_creditcardtype` FOREIGN KEY (`CreditCardType`) REFERENCES `creditcardtype` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `account`
--

/*!40000 ALTER TABLE `account` DISABLE KEYS */;
/*!40000 ALTER TABLE `account` ENABLE KEYS */;


--
-- Definition of table `competitor`
--

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

--
-- Dumping data for table `competitor`
--

/*!40000 ALTER TABLE `competitor` DISABLE KEYS */;
/*!40000 ALTER TABLE `competitor` ENABLE KEYS */;


--
-- Definition of table `country`
--

DROP TABLE IF EXISTS `country`;
CREATE TABLE `country` (
  `Id` int(11) NOT NULL auto_increment,
  `Name` varchar(300) default NULL,
  PRIMARY KEY  (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `country`
--

/*!40000 ALTER TABLE `country` DISABLE KEYS */;
INSERT INTO `country` (`Id`,`Name`) VALUES 
 (1,'Afghanistan'),
 (2,'Albania'),
 (3,'Algeria'),
 (4,'American Samoa'),
 (5,'Andorra'),
 (6,'Angola'),
 (7,'Anguilla'),
 (8,'Antarctica'),
 (9,'Antigua And Barbuda'),
 (10,'Argentina'),
 (11,'Armenia'),
 (12,'Aruba'),
 (13,'Australia'),
 (14,'Austria'),
 (15,'Azerbaijan'),
 (16,'Bahamas'),
 (17,'Bahrain'),
 (18,'Bangladesh'),
 (19,'Barbados'),
 (20,'Belarus'),
 (21,'Belgium'),
 (22,'Belize'),
 (23,'Benin'),
 (24,'Bermuda'),
 (25,'Bhutan'),
 (26,'Bolivia'),
 (27,'Bosnia And Herzegovina'),
 (28,'Botswana'),
 (29,'Bouvet Island'),
 (30,'Brazil'),
 (31,'British Indian Ocean Territory'),
 (32,'Brunei Darussalam'),
 (33,'Bulgaria'),
 (34,'Burkina Faso'),
 (35,'Burundi'),
 (36,'Cambodia'),
 (37,'Cameroon'),
 (38,'Canada'),
 (39,'Cape Verde'),
 (40,'Cayman Islands'),
 (41,'Central African Republic'),
 (42,'Chad'),
 (43,'Chile'),
 (44,'China'),
 (45,'Christmas Island'),
 (46,'Cocos (Keeling) Islands'),
 (47,'Colombia'),
 (48,'Comoros'),
 (49,'Congo'),
 (50,'Cook Islands'),
 (51,'Costa Rica'),
 (52,'Cote D\'Ivoire'),
 (53,'Croatia (Local Name: Hrvatska)'),
 (54,'Cuba'),
 (55,'Cyprus'),
 (56,'Czech Republic'),
 (57,'Denmark'),
 (58,'Djibouti'),
 (59,'Dominica'),
 (60,'Dominican Republic'),
 (61,'Ecuador'),
 (62,'Egypt'),
 (63,'El Salvador'),
 (64,'Equatorial Guinea'),
 (65,'Eritrea'),
 (66,'Estonia'),
 (67,'Ethiopia'),
 (68,'Falkland Islands (Malvinas)'),
 (69,'Faroe Islands'),
 (70,'Fiji'),
 (71,'Finland'),
 (72,'France'),
 (73,'France, Metropolitan'),
 (74,'French Guiana'),
 (75,'French Polynesia'),
 (76,'French Southern Territories'),
 (77,'Gabon'),
 (78,'Gambia'),
 (79,'Georgia'),
 (80,'Germany'),
 (81,'Ghana'),
 (82,'Gibraltar'),
 (83,'Greece'),
 (84,'Greenland'),
 (85,'Grenada'),
 (86,'Guadeloupe'),
 (87,'Guam'),
 (88,'Guatemala'),
 (89,'Guinea'),
 (90,'Guinea-Bissau'),
 (91,'Guyana'),
 (92,'Haiti'),
 (93,'Heard Island & Mcdonald Islands'),
 (94,'Honduras'),
 (95,'Hong Kong'),
 (96,'Hungary'),
 (97,'Iceland'),
 (98,'India'),
 (99,'Indonesia'),
 (100,'Iran, Islamic Republic Of'),
 (101,'Iraq'),
 (102,'Ireland'),
 (103,'Israel'),
 (104,'Italy'),
 (105,'Jamaica'),
 (106,'Japan'),
 (107,'Jordan'),
 (108,'Kazakhstan'),
 (109,'Kenya'),
 (110,'Kiribati'),
 (111,'Korea, Democratic People\'S Republic Of'),
 (112,'Korea, Republic Of'),
 (113,'Kuwait'),
 (114,'Kyrgyzstan'),
 (115,'Lao People\'S Democratic Republic'),
 (116,'Latvia'),
 (117,'Lebanon'),
 (118,'Lesotho'),
 (119,'Liberia'),
 (120,'Libyan Arab Jamahiriya'),
 (121,'Liechtenstein'),
 (122,'Lithuania'),
 (123,'Luxembourg'),
 (124,'Macau'),
 (125,'Macedonia, The Former Yugoslav Republic Of'),
 (126,'Madagascar'),
 (127,'Malawi'),
 (128,'Malaysia'),
 (129,'Maldives'),
 (130,'Mali'),
 (131,'Malta'),
 (132,'Marshall Islands'),
 (133,'Martinique'),
 (134,'Mauritania'),
 (135,'Mauritius'),
 (136,'Mayotte'),
 (137,'Mexico'),
 (138,'Micronesia, Federated States Of'),
 (139,'Moldova, Republic Of'),
 (140,'Monaco'),
 (141,'Mongolia'),
 (142,'Montserrat'),
 (143,'Morocco'),
 (144,'Mozambique'),
 (145,'Myanmar'),
 (146,'Namibia'),
 (147,'Nauru'),
 (148,'Nepal'),
 (149,'Netherlands'),
 (150,'Netherlands Antilles'),
 (151,'New Caledonia'),
 (152,'New Zealand'),
 (153,'Nicaragua'),
 (154,'Niger'),
 (155,'Nigeria'),
 (156,'Niue'),
 (157,'Norfolk Island'),
 (158,'Northern Mariana Islands'),
 (159,'Norway'),
 (160,'Oman'),
 (161,'Pakistan'),
 (162,'Palau'),
 (163,'Panama'),
 (164,'Papua New Guinea'),
 (165,'Paraguay'),
 (166,'Peru'),
 (167,'Philippines'),
 (168,'Pitcairn'),
 (169,'Poland'),
 (170,'Portugal'),
 (171,'Puerto Rico'),
 (172,'Qatar'),
 (173,'Reunion'),
 (174,'Romania'),
 (175,'Russian Federation'),
 (176,'Rwanda'),
 (177,'Saint Helena'),
 (178,'Saint Kitts And Nevis'),
 (179,'Saint Lucia'),
 (180,'Saint Pierre And Miquelon'),
 (181,'Saint Vincent And The Grenadines'),
 (182,'Samoa'),
 (183,'San Marino'),
 (184,'Sao Tome And Principe'),
 (185,'Saudi Arabia'),
 (186,'Senegal'),
 (187,'Seychelles'),
 (188,'Sierra Leone'),
 (189,'Singapore'),
 (190,'Slovakia (Slovak Republic)'),
 (191,'Slovenia'),
 (192,'Solomon Islands'),
 (193,'Somalia'),
 (194,'South Africa'),
 (195,'Spain'),
 (196,'Sri Lanka'),
 (197,'Sudan'),
 (198,'Suriname'),
 (199,'Svalbard And Jan Mayen Islands'),
 (200,'Swaziland'),
 (201,'Sweden'),
 (202,'Switzerland'),
 (203,'Syrian Arab Republic'),
 (204,'Taiwan, Province Of China'),
 (205,'Tajikistan'),
 (206,'Tanzania, United Republic Of'),
 (207,'Thailand'),
 (208,'Togo'),
 (209,'Tokelau'),
 (210,'Tonga'),
 (211,'Trinidad And Tobago'),
 (212,'Tunisia'),
 (213,'Turkey'),
 (214,'Turkmenistan'),
 (215,'Turks And Caicos Islands'),
 (216,'Tuvalu'),
 (217,'Uganda'),
 (218,'Ukraine'),
 (219,'United Arab Emirates'),
 (220,'United Kingdom'),
 (221,'United States'),
 (222,'United States Minor Outlying Islands'),
 (223,'Uruguay'),
 (224,'Uzbekistan'),
 (225,'Vanuatu'),
 (226,'Vatican City State (Holy See)'),
 (227,'Venezuela'),
 (228,'Viet Nam'),
 (229,'Virgin Islands (British)'),
 (230,'Virgin Islands (U.S.)'),
 (231,'Wallis And Futuna Islands'),
 (232,'Western Sahara'),
 (233,'Yemen'),
 (234,'Yugoslavia'),
 (235,'Zaire'),
 (236,'Zambia'),
 (237,'Zimbabwe');
/*!40000 ALTER TABLE `country` ENABLE KEYS */;


--
-- Definition of table `creditcardtype`
--

DROP TABLE IF EXISTS `creditcardtype`;
CREATE TABLE `creditcardtype` (
  `Id` varchar(100) NOT NULL,
  `Name` varchar(300) default NULL,
  PRIMARY KEY  (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `creditcardtype`
--

/*!40000 ALTER TABLE `creditcardtype` DISABLE KEYS */;
INSERT INTO `creditcardtype` (`Id`,`Name`) VALUES 
 ('AMEX','American Express'),
 ('MASTERDCARD','Mastercard'),
 ('VISA','Visa');
/*!40000 ALTER TABLE `creditcardtype` ENABLE KEYS */;


--
-- Definition of table `frequency`
--

DROP TABLE IF EXISTS `frequency`;
CREATE TABLE `frequency` (
  `Id` int(10) unsigned NOT NULL auto_increment,
  `Name` varchar(50) NOT NULL,
  PRIMARY KEY  (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `frequency`
--

/*!40000 ALTER TABLE `frequency` DISABLE KEYS */;
INSERT INTO `frequency` (`Id`,`Name`) VALUES 
 (1,'Week'),
 (2,'TwoWeeks'),
 (3,'Month');
/*!40000 ALTER TABLE `frequency` ENABLE KEYS */;


--
-- Definition of table `keyword`
--

DROP TABLE IF EXISTS `keyword`;
CREATE TABLE `keyword` (
  `Id` int(11) NOT NULL auto_increment,
  `Keyword` varchar(100) default NULL,
  `IdKeywordList` int(11) NOT NULL,
  PRIMARY KEY  (`Id`),
  KEY `R_13` (`IdKeywordList`),
  CONSTRAINT `keyword_ibfk_1` FOREIGN KEY (`IdKeywordList`) REFERENCES `keywordlist` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `keyword`
--

/*!40000 ALTER TABLE `keyword` DISABLE KEYS */;
/*!40000 ALTER TABLE `keyword` ENABLE KEYS */;


--
-- Definition of table `keywordanalysis`
--

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
  PRIMARY KEY  (`Id`),
  UNIQUE KEY `KeywordUnique` (`Keyword`,`IdRankingMonitorRun`),
  KEY `eywordanalysis_ibfk_1` (`IdRankingMonitorRun`),
  CONSTRAINT `keywordanalysis_ibfk_1` FOREIGN KEY (`IdRankingMonitorRun`) REFERENCES `rankingmonitorrun` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `keywordanalysis`
--

/*!40000 ALTER TABLE `keywordanalysis` DISABLE KEYS */;
/*!40000 ALTER TABLE `keywordanalysis` ENABLE KEYS */;


--
-- Definition of table `keyworddeepanalysis`
--

DROP TABLE IF EXISTS `keyworddeepanalysis`;
CREATE TABLE `keyworddeepanalysis` (
  `Id` int(10) unsigned NOT NULL auto_increment,
  `IdRankingMonitorDeepRun` int(10) unsigned NOT NULL,
  `Keyword` varchar(100) NOT NULL,
  `Pages` int(10) unsigned default NULL,
  PRIMARY KEY  (`Id`),
  UNIQUE KEY `KeywordUnique` (`IdRankingMonitorDeepRun`,`Keyword`),
  CONSTRAINT `keyworddeepanalysis_ibfk_1` FOREIGN KEY (`IdRankingMonitorDeepRun`) REFERENCES `rankingmonitordeeprun` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `keyworddeepanalysis`
--

/*!40000 ALTER TABLE `keyworddeepanalysis` DISABLE KEYS */;
/*!40000 ALTER TABLE `keyworddeepanalysis` ENABLE KEYS */;


--
-- Definition of table `keywordlist`
--

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

--
-- Dumping data for table `keywordlist`
--

/*!40000 ALTER TABLE `keywordlist` DISABLE KEYS */;
/*!40000 ALTER TABLE `keywordlist` ENABLE KEYS */;


--
-- Definition of table `monitorkeywordlist`
--

DROP TABLE IF EXISTS `monitorkeywordlist`;
CREATE TABLE `monitorkeywordlist` (
  `Id` int(10) unsigned NOT NULL auto_increment,
  `IdProject` int(11) NOT NULL,
  `IdKeywordList` int(11) NOT NULL,
  PRIMARY KEY  (`Id`),
  KEY `keywordList` (`IdKeywordList`),
  KEY `monitorkeywordlist_ibfk_1` (`IdProject`),
  CONSTRAINT `monitorkeywordlist_ibfk_1` FOREIGN KEY (`IdProject`) REFERENCES `project` (`Id`),
  CONSTRAINT `monitorkeywordlist_ibfk_2` FOREIGN KEY (`IdKeywordList`) REFERENCES `keywordlist` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `monitorkeywordlist`
--

/*!40000 ALTER TABLE `monitorkeywordlist` DISABLE KEYS */;
/*!40000 ALTER TABLE `monitorkeywordlist` ENABLE KEYS */;


--
-- Definition of table `monitorproxyserver`
--

DROP TABLE IF EXISTS `monitorproxyserver`;
CREATE TABLE `monitorproxyserver` (
  `Id` int(10) unsigned NOT NULL auto_increment,
  `IdProject` int(11) NOT NULL,
  `IdProxyServer` int(10) unsigned NOT NULL,
  PRIMARY KEY  (`Id`),
  KEY `monitorproxyserver_ibfk_1` (`IdProxyServer`),
  KEY `IdProject` (`IdProject`),
  CONSTRAINT `monitorproxyserver_ibfk_2` FOREIGN KEY (`IdProject`) REFERENCES `project` (`Id`),
  CONSTRAINT `monitorproxyserver_ibfk_1` FOREIGN KEY (`IdProxyServer`) REFERENCES `proxyserver` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='InnoDB free: 10240 kB; (`IdProject`) REFER `seodb/proxyserve';

--
-- Dumping data for table `monitorproxyserver`
--

/*!40000 ALTER TABLE `monitorproxyserver` DISABLE KEYS */;
/*!40000 ALTER TABLE `monitorproxyserver` ENABLE KEYS */;


--
-- Definition of table `monitorsearchenginecountry`
--

DROP TABLE IF EXISTS `monitorsearchenginecountry`;
CREATE TABLE `monitorsearchenginecountry` (
  `Id` int(10) unsigned NOT NULL auto_increment,
  `IdProject` int(11) NOT NULL,
  `IdSearchEngineCountry` int(10) unsigned NOT NULL,
  PRIMARY KEY  (`Id`),
  KEY `monitorsearchenginecountry_ibfk_1` (`IdSearchEngineCountry`),
  KEY `monitorsearchenginecountry_ibfk_2` (`IdProject`),
  CONSTRAINT `monitorsearchenginecountry_ibfk_2` FOREIGN KEY (`IdProject`) REFERENCES `project` (`Id`),
  CONSTRAINT `monitorsearchenginecountry_ibfk_1` FOREIGN KEY (`IdSearchEngineCountry`) REFERENCES `searchenginecountry` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `monitorsearchenginecountry`
--

/*!40000 ALTER TABLE `monitorsearchenginecountry` DISABLE KEYS */;
/*!40000 ALTER TABLE `monitorsearchenginecountry` ENABLE KEYS */;


--
-- Definition of table `profileproperty`
--

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

--
-- Dumping data for table `profileproperty`
--

/*!40000 ALTER TABLE `profileproperty` DISABLE KEYS */;
/*!40000 ALTER TABLE `profileproperty` ENABLE KEYS */;


--
-- Definition of table `project`
--

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

--
-- Dumping data for table `project`
--

/*!40000 ALTER TABLE `project` DISABLE KEYS */;
/*!40000 ALTER TABLE `project` ENABLE KEYS */;


--
-- Definition of table `projectpermission`
--

DROP TABLE IF EXISTS `projectpermission`;
CREATE TABLE `projectpermission` (
  `Id` int(11) NOT NULL auto_increment,
  `Name` varchar(100) default NULL,
  `Description` varchar(250) default NULL,
  PRIMARY KEY  (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `projectpermission`
--

/*!40000 ALTER TABLE `projectpermission` DISABLE KEYS */;
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
/*!40000 ALTER TABLE `projectpermission` ENABLE KEYS */;


--
-- Definition of table `projectpermissionrole`
--

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

--
-- Dumping data for table `projectpermissionrole`
--

/*!40000 ALTER TABLE `projectpermissionrole` DISABLE KEYS */;
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
/*!40000 ALTER TABLE `projectpermissionrole` ENABLE KEYS */;


--
-- Definition of table `projectrole`
--

DROP TABLE IF EXISTS `projectrole`;
CREATE TABLE `projectrole` (
  `Id` int(11) NOT NULL auto_increment,
  `Name` varchar(100) default NULL,
  `Description` varchar(250) default NULL,
  `Enabled` bit(1) default '\0',
  PRIMARY KEY  (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `projectrole`
--

/*!40000 ALTER TABLE `projectrole` DISABLE KEYS */;
INSERT INTO `projectrole` (`Id`,`Name`,`Description`,`Enabled`) VALUES 
 (1,'ProjectAdmin','ProjectAdmin',0x01),
 (2,'DomainUser','DomainUser',0x01);
/*!40000 ALTER TABLE `projectrole` ENABLE KEYS */;


--
-- Definition of table `projectuser`
--

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

--
-- Dumping data for table `projectuser`
--

/*!40000 ALTER TABLE `projectuser` DISABLE KEYS */;
/*!40000 ALTER TABLE `projectuser` ENABLE KEYS */;


--
-- Definition of table `proxyserver`
--

DROP TABLE IF EXISTS `proxyserver`;
CREATE TABLE `proxyserver` (
  `Id` int(10) unsigned NOT NULL,
  `IdCountry` int(10) unsigned NOT NULL,
  `City` varchar(100) NOT NULL,
  `ImportanceLevel` int(11) default NULL,
  PRIMARY KEY  (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `proxyserver`
--

/*!40000 ALTER TABLE `proxyserver` DISABLE KEYS */;
INSERT INTO `proxyserver` (`Id`,`IdCountry`,`City`,`ImportanceLevel`) VALUES 
 (1,221,'Dallas',1),
 (2,13,'Sydney',2),
 (3,194,'Cape Town',3),
 (4,106,'Tokyo',4),
 (5,220,'United Kingdom',5);
/*!40000 ALTER TABLE `proxyserver` ENABLE KEYS */;


--
-- Definition of table `rankingmonitordeeprun`
--

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
  KEY `RankingMonitorDeepRun_ibfk_1` (`IdProxy`),
  KEY `RankingMonitorDeepRun_ibfk_2` (`IdSearchEngineCountry`),
  KEY `RankingMonitorDeepRun_ibfk_4` (`Status`),
  KEY `RankingMonitorDeepRun_ibfk_3` (`IdRankingMonitorRun`),
  CONSTRAINT `RankingMonitorDeepRun_ibfk_1` FOREIGN KEY (`IdProxy`) REFERENCES `proxyserver` (`Id`),
  CONSTRAINT `RankingMonitorDeepRun_ibfk_2` FOREIGN KEY (`IdSearchEngineCountry`) REFERENCES `searchenginecountry` (`Id`),
  CONSTRAINT `RankingMonitorDeepRun_ibfk_4` FOREIGN KEY (`Status`) REFERENCES `status` (`Name`),
  CONSTRAINT `RankingMonitorDeepRun_ibfk_3` FOREIGN KEY (`IdRankingMonitorRun`) REFERENCES `rankingmonitorrun` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `rankingmonitordeeprun`
--

/*!40000 ALTER TABLE `rankingmonitordeeprun` DISABLE KEYS */;
/*!40000 ALTER TABLE `rankingmonitordeeprun` ENABLE KEYS */;


--
-- Definition of table `rankingmonitorrun`
--

DROP TABLE IF EXISTS `rankingmonitorrun`;
CREATE TABLE `rankingmonitorrun` (
  `Id` int(10) unsigned NOT NULL,
  `IdProject` int(11) NOT NULL,
  `User` varchar(100) NOT NULL,
  `AnalysisType` char(1) NOT NULL,
  `Status` char(1) default NULL,
  `ExecutionDate` datetime default NULL,
  PRIMARY KEY  (`Id`),
  KEY `RankingMonitorRun_ibfk_1` (`Status`),
  KEY `RankingMonitorRun_ibfk_2` (`IdProject`),
  CONSTRAINT `RankingMonitorRun_ibfk_2` FOREIGN KEY (`IdProject`) REFERENCES `project` (`Id`),
  CONSTRAINT `RankingMonitorRun_ibfk_1` FOREIGN KEY (`Status`) REFERENCES `status` (`Name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `rankingmonitorrun`
--

/*!40000 ALTER TABLE `rankingmonitorrun` DISABLE KEYS */;
/*!40000 ALTER TABLE `rankingmonitorrun` ENABLE KEYS */;


--
-- Definition of table `searchengine`
--

DROP TABLE IF EXISTS `searchengine`;
CREATE TABLE `searchengine` (
  `Id` int(10) unsigned NOT NULL,
  `Name` varchar(100) NOT NULL,
  `Description` varchar(300) NOT NULL,
  PRIMARY KEY  (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `searchengine`
--

/*!40000 ALTER TABLE `searchengine` DISABLE KEYS */;
INSERT INTO `searchengine` (`Id`,`Name`,`Description`) VALUES 
 (1,'Google',''),
 (2,'Yahoo!',''),
 (3,'Live','');
/*!40000 ALTER TABLE `searchengine` ENABLE KEYS */;


--
-- Definition of table `searchenginecountry`
--

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

--
-- Dumping data for table `searchenginecountry`
--

/*!40000 ALTER TABLE `searchenginecountry` DISABLE KEYS */;
INSERT INTO `searchenginecountry` (`Id`,`IdSearchEngine`,`IdCountry`,`Url`) VALUES 
 (1,1,221,'www.google.com'),
 (2,2,221,'www.yahoo.com'),
 (3,3,221,'www.live.com'),
 (4,1,220,'www.google.co.uk'),
 (5,2,220,'uk.yahoo.com'),
 (6,3,220,'www.live.com/?mkt=en-gb');
/*!40000 ALTER TABLE `searchenginecountry` ENABLE KEYS */;


--
-- Definition of table `seoprofile`
--

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

--
-- Dumping data for table `seoprofile`
--

/*!40000 ALTER TABLE `seoprofile` DISABLE KEYS */;
/*!40000 ALTER TABLE `seoprofile` ENABLE KEYS */;


--
-- Definition of table `seotoolsetuser`
--

DROP TABLE IF EXISTS `seotoolsetuser`;
CREATE TABLE `seotoolsetuser` (
  `Id` int(11) NOT NULL auto_increment,
  `FirstName` varchar(250) default NULL,
  `LastName` varchar(250) default NULL,
  `Email` varchar(300) default NULL,
  `Address1` varchar(1000) default NULL,
  `Address2` varchar(1000) default NULL,
  `CityTown` varchar(250) default NULL,
  `State` varchar(250) default NULL,
  `Zip` varchar(100) default NULL,
  `Telephone` varchar(100) default NULL,
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

--
-- Dumping data for table `seotoolsetuser`
--

/*!40000 ALTER TABLE `seotoolsetuser` DISABLE KEYS */;
/*!40000 ALTER TABLE `seotoolsetuser` ENABLE KEYS */;


--
-- Definition of table `service`
--

DROP TABLE IF EXISTS `service`;
CREATE TABLE `service` (
  `Id` int(11) NOT NULL auto_increment,
  `Name` varchar(250) default NULL,
  `Description` varchar(250) default NULL,
  `Enabled` bit(1) default '\0',
  PRIMARY KEY  (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `service`
--

/*!40000 ALTER TABLE `service` DISABLE KEYS */;
/*!40000 ALTER TABLE `service` ENABLE KEYS */;


--
-- Definition of table `status`
--

DROP TABLE IF EXISTS `status`;
CREATE TABLE `status` (
  `Name` char(1) NOT NULL,
  `Description` varchar(100) NOT NULL,
  PRIMARY KEY  (`Name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `status`
--

/*!40000 ALTER TABLE `status` DISABLE KEYS */;
INSERT INTO `status` (`Name`,`Description`) VALUES 
 ('C','Complete'),
 ('F','Failed'),
 ('P','In Progress');
/*!40000 ALTER TABLE `status` ENABLE KEYS */;


--
-- Definition of table `subscription`
--

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

--
-- Dumping data for table `subscription`
--

/*!40000 ALTER TABLE `subscription` DISABLE KEYS */;
/*!40000 ALTER TABLE `subscription` ENABLE KEYS */;


--
-- Definition of table `userpermission`
--

DROP TABLE IF EXISTS `userpermission`;
CREATE TABLE `userpermission` (
  `Id` int(11) NOT NULL auto_increment,
  `Name` varchar(250) default NULL,
  `Description` varchar(300) default NULL,
  PRIMARY KEY  (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `userpermission`
--

/*!40000 ALTER TABLE `userpermission` DISABLE KEYS */;
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
/*!40000 ALTER TABLE `userpermission` ENABLE KEYS */;


--
-- Definition of table `userrole`
--

DROP TABLE IF EXISTS `userrole`;
CREATE TABLE `userrole` (
  `Id` int(11) NOT NULL auto_increment,
  `Name` varchar(250) default NULL,
  `Description` varchar(300) default NULL,
  `Enabled` bit(1) default '\0',
  PRIMARY KEY  (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `userrole`
--

/*!40000 ALTER TABLE `userrole` DISABLE KEYS */;
INSERT INTO `userrole` (`Id`,`Name`,`Description`,`Enabled`) VALUES 
 (1,'Administrator','Administrator',0x01),
 (2,'AccountingUser','AccountingUser',0x01),
 (3,'TechSupportUser','TechSupportUser',0x01),
 (4,'Employee','Employee',0x01),
 (5,'Subscriber','Subscriber',0x01),
 (6,'Client','Client',0x01);
/*!40000 ALTER TABLE `userrole` ENABLE KEYS */;


--
-- Definition of table `userrolpermission`
--

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

--
-- Dumping data for table `userrolpermission`
--

/*!40000 ALTER TABLE `userrolpermission` DISABLE KEYS */;
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
/*!40000 ALTER TABLE `userrolpermission` ENABLE KEYS */;


--
-- Definition of procedure `cleanDB`
--

DROP PROCEDURE IF EXISTS `cleanDB`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER' */ $$
 $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;



/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
