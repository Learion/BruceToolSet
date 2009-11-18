/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO,ANSI_QUOTES' */;

use seodb;
--
-- Definition of table "promotype"
--

CREATE TABLE `seodb`.`PromoType` (
  `Id` INTEGER UNSIGNED NOT NULL,
  `Description` VARCHAR(50) NOT NULL,
  PRIMARY KEY (`Id`)
)
ENGINE = InnoDB;

--
-- Definition of table "accounttype"
--

CREATE TABLE `seodb`.`AccountType` (
  `Id` INTEGER UNSIGNED NOT NULL,
  `Description` VARCHAR(50) NOT NULL,
  PRIMARY KEY (`Id`)
)
ENGINE = InnoDB;

--
-- Definition of table "account"
--

DROP TABLE IF EXISTS "account";
CREATE TABLE "account" (
  "Id" int(11) NOT NULL auto_increment,
  "IdAccountOwner" int(11) default NULL,
  "Name" varchar(250) default NULL,
  "MaxNumberOfUser" int(11) default NULL,
  "MaxNumberOfDomainUser" int(11) default NULL,
  "MaxNumberOfProjects" int(11) default NULL,
  "CompanyName" varchar(300) default NULL,
  "CompanyAddress1" varchar(300) default NULL,
  "CompanyAddress2" varchar(300) default NULL,
  "CompanyCity" varchar(100) default NULL,
  "CompanyState" varchar(255) default NULL,
  "CompanyZip" varchar(100) default NULL,
  "CreditCardNumber" varchar(20) default NULL,
  "CreditCardType" varchar(100) default NULL,
  "CreditCardAddress1" varchar(300) default NULL,
  "CreditCardAddress2" varchar(300) default NULL,
  "CreditCardCity" varchar(100) default NULL,
  "CreditCardZip" varchar(100) default NULL,
  "RecurringBill" bit(1) default '\0',
  "CreatedDate" datetime default NULL,
  "UpdatedDate" datetime default NULL,
  "CreatedBy" varchar(100) default NULL,
  "UpdatedBy" varchar(100) default NULL,
  "Enabled" bit(1) default '\0',
  "AccountExpirationDate" datetime default NULL,
  "CreditCardExpiration" datetime default NULL,
  "CreditCardEmail" varchar(300) default NULL,
  "CreditCardIdCountry" int(11) default NULL,
  "CreditCardState" varchar(255) default NULL,
  "CreditCardCardholder" varchar(250) default NULL,
  "PromoCode" varchar(250) default NULL,
  "CompanyIdCountry" int(10) unsigned default NULL,
  "CompanyPhone" varchar(35) default NULL,
  "CreditCardCVS" varchar(10) default NULL,
  PRIMARY KEY  ("Id"),
  UNIQUE KEY "NameUnique" ("Name"),
  KEY "R_6" ("IdAccountOwner"),
  KEY "R_CT1" ("CreditCardType"),
  CONSTRAINT "account_creditcardtype" FOREIGN KEY ("CreditCardType") REFERENCES "creditcardtype" ("Id"),
  CONSTRAINT "account_ibfk_1" FOREIGN KEY ("IdAccountOwner") REFERENCES "account" ("Id")
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Definition of table "competitor"
--

DROP TABLE IF EXISTS "competitor";
CREATE TABLE "competitor" (
  "Id" int(11) NOT NULL auto_increment,
  "Name" varchar(250) default NULL,
  "Url" varchar(2000) default NULL,
  "Description" varchar(300) default NULL,
  "IdProject" int(11) NOT NULL,
  PRIMARY KEY  ("Id"),
  KEY "R_12" ("IdProject"),
  CONSTRAINT "competitor_ibfk_1" FOREIGN KEY ("IdProject") REFERENCES "project" ("Id")
) ENGINE=InnoDB DEFAULT CHARSET=utf8;


--
-- Definition of table "country"
--

DROP TABLE IF EXISTS "country";
CREATE TABLE "country" (
  "Id" int(11) NOT NULL auto_increment,
  "Name" varchar(300) default NULL,
  "FlagUrl" varchar(1000) default NULL,
  "SearchEngineImportance" int(10) unsigned default NULL,
  PRIMARY KEY  ("Id")
) ENGINE=InnoDB DEFAULT CHARSET=utf8;


--
-- Definition of table "creditcardtype"
--

DROP TABLE IF EXISTS "creditcardtype";
CREATE TABLE "creditcardtype" (
  "Id" varchar(100) NOT NULL,
  "Name" varchar(300) default NULL,
  PRIMARY KEY  ("Id")
) ENGINE=InnoDB DEFAULT CHARSET=utf8;


--
-- Definition of table "frequency"
--

DROP TABLE IF EXISTS "frequency";
CREATE TABLE "frequency" (
  "Id" int(10) unsigned NOT NULL auto_increment,
  "Name" varchar(50) NOT NULL,
  PRIMARY KEY  ("Id")
) ENGINE=InnoDB DEFAULT CHARSET=utf8;


--
-- Definition of table "keyword"
--

DROP TABLE IF EXISTS "keyword";
CREATE TABLE "keyword" (
  "Id" int(11) NOT NULL auto_increment,
  "Keyword" varchar(300) default NULL,
  "IdKeywordList" int(11) NOT NULL,
  PRIMARY KEY  ("Id"),
  KEY "R_13" ("IdKeywordList"),
  CONSTRAINT "keyword_ibfk_1" FOREIGN KEY ("IdKeywordList") REFERENCES "keywordlist" ("Id")
) ENGINE=InnoDB DEFAULT CHARSET=utf8;


--
-- Definition of table "keywordanalysis"
--

DROP TABLE IF EXISTS "keywordanalysis";
CREATE TABLE "keywordanalysis" (
  "Id" int(10) unsigned NOT NULL auto_increment,
  "Keyword" varchar(300) NOT NULL,
  "IdRankingMonitorRun" int(10) unsigned NOT NULL,
  "GoogleResults" int(10) unsigned default NULL,
  "AllInTitle" int(10) unsigned default NULL,
  "AliasDomains" int(10) unsigned default NULL,
  "CPC" float default NULL,
  "DailySearches" int(10) unsigned default NULL,
  "Results" int(10) unsigned default NULL,
  "Pages" int(10) unsigned default NULL,
  "Engines" int(10) unsigned default NULL,
  "Status" char(1) NOT NULL default 'P',
  PRIMARY KEY  ("Id"),
  UNIQUE KEY "KeywordUnique" ("Keyword","IdRankingMonitorRun"),
  KEY "keywordanalysis_ibfk_1" ("IdRankingMonitorRun"),
  KEY "keywordanalysis_ibfk_2" ("Status"),
  CONSTRAINT "keywordanalysis_ibfk_1" FOREIGN KEY ("IdRankingMonitorRun") REFERENCES "rankingmonitorrun" ("Id"),
  CONSTRAINT "keywordanalysis_ibfk_2" FOREIGN KEY ("Status") REFERENCES "status" ("Name")
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Definition of table "keyworddeepanalysis"
--

DROP TABLE IF EXISTS "keyworddeepanalysis";
CREATE TABLE "keyworddeepanalysis" (
  "Id" int(10) unsigned NOT NULL auto_increment,
  "IdRankingMonitorDeepRun" int(10) unsigned NOT NULL,
  "Keyword" varchar(300) NOT NULL,
  "Pages" int(10) unsigned default NULL,
  "Status" char(1) NOT NULL default 'P',
  PRIMARY KEY  ("Id"),
  UNIQUE KEY "KeywordUnique" ("IdRankingMonitorDeepRun","Keyword"),
  KEY "keyworddeepanalysis_ibfk_2" ("Status"),
  CONSTRAINT "keyworddeepanalysis_ibfk_1" FOREIGN KEY ("IdRankingMonitorDeepRun") REFERENCES "rankingmonitordeeprun" ("Id"),
  CONSTRAINT "keyworddeepanalysis_ibfk_2" FOREIGN KEY ("Status") REFERENCES "status" ("Name")
) ENGINE=InnoDB DEFAULT CHARSET=utf8;


--
-- Definition of table "keywordlist"
--

DROP TABLE IF EXISTS "keywordlist";
CREATE TABLE "keywordlist" (
  "Id" int(11) NOT NULL auto_increment,
  "Name" varchar(300) default NULL,
  "IdProject" int(11) NOT NULL,
  "Enabled" bit(1) default '\0',
  PRIMARY KEY  ("Id"),
  KEY "R_14" ("IdProject"),
  CONSTRAINT "keywordlist_ibfk_1" FOREIGN KEY ("IdProject") REFERENCES "project" ("Id")
) ENGINE=InnoDB DEFAULT CHARSET=utf8;



--
-- Definition of table "monitorkeywordlist"
--

DROP TABLE IF EXISTS "monitorkeywordlist";
CREATE TABLE "monitorkeywordlist" (
  "Id" int(10) unsigned NOT NULL auto_increment,
  "IdProject" int(11) NOT NULL,
  "IdKeywordList" int(11) NOT NULL,
  PRIMARY KEY  ("Id"),
  UNIQUE KEY "MonitorKeywordList_Unique" ("IdProject","IdKeywordList"),
  KEY "keywordList" ("IdKeywordList"),
  KEY "monitorkeywordlist_ibfk_1" ("IdProject"),
  CONSTRAINT "monitorkeywordlist_ibfk_1" FOREIGN KEY ("IdProject") REFERENCES "project" ("Id"),
  CONSTRAINT "monitorkeywordlist_ibfk_2" FOREIGN KEY ("IdKeywordList") REFERENCES "keywordlist" ("Id")
) ENGINE=InnoDB DEFAULT CHARSET=utf8;


--
-- Definition of table "monitorproxyserver"
--

DROP TABLE IF EXISTS "monitorproxyserver";
CREATE TABLE "monitorproxyserver" (
  "Id" int(10) unsigned NOT NULL auto_increment,
  "IdProject" int(11) NOT NULL,
  "IdProxyServer" int(10) unsigned NOT NULL,
  PRIMARY KEY  ("Id"),
  UNIQUE KEY "MonitorProxySever_Unique" ("IdProject","IdProxyServer"),
  KEY "monitorproxyserver_ibfk_1" ("IdProxyServer"),
  KEY "IdProject" ("IdProject"),
  CONSTRAINT "monitorproxyserver_ibfk_1" FOREIGN KEY ("IdProxyServer") REFERENCES "proxyserver" ("Id"),
  CONSTRAINT "monitorproxyserver_ibfk_2" FOREIGN KEY ("IdProject") REFERENCES "project" ("Id")
) ENGINE=InnoDB DEFAULT CHARSET=utf8;


--
-- Definition of table "monitorsearchenginecountry"
--

DROP TABLE IF EXISTS "monitorsearchenginecountry";
CREATE TABLE "monitorsearchenginecountry" (
  "Id" int(10) unsigned NOT NULL auto_increment,
  "IdProject" int(11) NOT NULL,
  "IdSearchEngineCountry" int(10) unsigned NOT NULL,
  PRIMARY KEY  ("Id"),
  UNIQUE KEY "MonitorSearchEngineCountry_Unique" ("IdProject","IdSearchEngineCountry"),
  KEY "monitorsearchenginecountry_ibfk_1" ("IdSearchEngineCountry"),
  KEY "monitorsearchenginecountry_ibfk_2" ("IdProject"),
  CONSTRAINT "monitorsearchenginecountry_ibfk_1" FOREIGN KEY ("IdSearchEngineCountry") REFERENCES "searchenginecountry" ("Id"),
  CONSTRAINT "monitorsearchenginecountry_ibfk_2" FOREIGN KEY ("IdProject") REFERENCES "project" ("Id")
) ENGINE=InnoDB DEFAULT CHARSET=utf8;


--
-- Definition of table "profileproperty"
--

DROP TABLE IF EXISTS "profileproperty";
CREATE TABLE "profileproperty" (
  "Id" int(10) unsigned NOT NULL auto_increment,
  "IdUser" int(10) unsigned NOT NULL,
  "Name" varchar(50) collate utf8_unicode_ci NOT NULL,
  "StringValue" text collate utf8_unicode_ci,
  "BinaryValue" blob,
  PRIMARY KEY  ("Id"),
  KEY "ProfileProperty_ibfk_1" ("IdUser"),
  CONSTRAINT "ProfileProperty_ibfk_1" FOREIGN KEY ("IdUser") REFERENCES "seoprofile" ("Id")
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;



--
-- Definition of table "project"
--

DROP TABLE IF EXISTS "project";
CREATE TABLE "project" (
  "Id" int(11) NOT NULL auto_increment,
  "Name" varchar(1000) default NULL,
  "Domain" varchar(1000) default NULL,
  "ClientName" varchar(100) default NULL,
  "ContactEmail" varchar(300) default NULL,
  "ContactName" varchar(1000) default NULL,
  "ContactPhone" varchar(100) default NULL,
  "IdAccount" int(11) NOT NULL,
  "CreatedDate" datetime default NULL,
  "UpdatedDate" datetime default NULL,
  "CreatedBy" varchar(100) default NULL,
  "UpdatedBy" varchar(100) default NULL,
  "Enabled" bit(1) default '\0',
  "IdMonitorFrequency" int(10) unsigned default NULL,
  "MonitorUpdatedBy" varchar(100) default NULL,
  "MonitorUpdatedDate" datetime default NULL,
  PRIMARY KEY  ("Id"),
  KEY "R_11" ("IdAccount"),
  KEY "project_ibfk_2" ("IdMonitorFrequency"),
  CONSTRAINT "project_ibfk_1" FOREIGN KEY ("IdAccount") REFERENCES "account" ("Id"),
  CONSTRAINT "project_ibfk_2" FOREIGN KEY ("IdMonitorFrequency") REFERENCES "frequency" ("Id")
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Definition of table "projectpermission"
--

DROP TABLE IF EXISTS "projectpermission";
CREATE TABLE "projectpermission" (
  "Id" int(11) NOT NULL auto_increment,
  "Name" varchar(100) default NULL,
  "Description" varchar(250) default NULL,
  PRIMARY KEY  ("Id")
) ENGINE=InnoDB DEFAULT CHARSET=utf8;


--
-- Definition of table "projectpermissionrole"
--

DROP TABLE IF EXISTS "projectpermissionrole";
CREATE TABLE "projectpermissionrole" (
  "Id" int(11) NOT NULL auto_increment,
  "IdProjectRole" int(11) NOT NULL,
  "IdProjectPermission" int(11) NOT NULL,
  PRIMARY KEY  ("Id"),
  KEY "R_18" ("IdProjectRole"),
  KEY "R_19" ("IdProjectPermission"),
  CONSTRAINT "projectpermissionrole_ibfk_1" FOREIGN KEY ("IdProjectRole") REFERENCES "projectrole" ("Id"),
  CONSTRAINT "projectpermissionrole_ibfk_2" FOREIGN KEY ("IdProjectPermission") REFERENCES "projectpermission" ("Id")
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Definition of table "projectrole"
--

DROP TABLE IF EXISTS "projectrole";
CREATE TABLE "projectrole" (
  "Id" int(11) NOT NULL auto_increment,
  "Name" varchar(100) default NULL,
  "Description" varchar(250) default NULL,
  "Enabled" bit(1) default '\0',
  PRIMARY KEY  ("Id")
) ENGINE=InnoDB DEFAULT CHARSET=utf8;


--
-- Definition of table "projectuser"
--

DROP TABLE IF EXISTS "projectuser";
CREATE TABLE "projectuser" (
  "Id" int(11) NOT NULL auto_increment,
  "IdUser" int(11) NOT NULL,
  "IdProject" int(11) NOT NULL,
  "IdProjectRole" int(11) NOT NULL,
  "MonitorEmails" bit(1) default '\0',
  "Enabled" bit(1) default '\0',
  PRIMARY KEY  ("Id"),
  UNIQUE KEY "Index_unique_project_user" ("IdUser","IdProject"),
  KEY "R_15" ("IdUser"),
  KEY "R_16" ("IdProject"),
  KEY "R_17" ("IdProjectRole"),
  CONSTRAINT "projectuser_ibfk_1" FOREIGN KEY ("IdUser") REFERENCES "seotoolsetuser" ("Id"),
  CONSTRAINT "projectuser_ibfk_2" FOREIGN KEY ("IdProject") REFERENCES "project" ("Id"),
  CONSTRAINT "projectuser_ibfk_3" FOREIGN KEY ("IdProjectRole") REFERENCES "projectrole" ("Id")
) ENGINE=InnoDB DEFAULT CHARSET=utf8;


--
-- Definition of table "proxyserver"
--

DROP TABLE IF EXISTS "proxyserver";
CREATE TABLE "proxyserver" (
  "Id" int(10) unsigned NOT NULL,
  "IdCountry" int(10) unsigned NOT NULL,
  "City" varchar(100) NOT NULL,
  "ImportanceLevel" int(11) default NULL,
  PRIMARY KEY  ("Id")
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Definition of table "rankingmonitordeeprun"
--

DROP TABLE IF EXISTS "rankingmonitordeeprun";
CREATE TABLE "rankingmonitordeeprun" (
  "Id" int(10) unsigned NOT NULL auto_increment,
  "IdRankingMonitorRun" int(10) unsigned NOT NULL,
  "IdProxy" int(10) unsigned NOT NULL,
  "IdSearchEngineCountry" int(10) unsigned NOT NULL,
  "Status" char(1) NOT NULL,
  "StatusReason" text,
  "PageRank" int(10) unsigned default NULL,
  "InboundLinks" int(10) unsigned default NULL,
  "PagesIndexed" int(10) unsigned default NULL,
  PRIMARY KEY  ("Id"),
  UNIQUE KEY "RankingMonitorDeepRunUnique" ("IdRankingMonitorRun","IdProxy","IdSearchEngineCountry"),
  KEY "RankingMonitorDeepRun_ibfk_1" ("IdProxy"),
  KEY "RankingMonitorDeepRun_ibfk_2" ("IdSearchEngineCountry"),
  KEY "RankingMonitorDeepRun_ibfk_4" ("Status"),
  KEY "RankingMonitorDeepRun_ibfk_3" ("IdRankingMonitorRun"),
  CONSTRAINT "RankingMonitorDeepRun_ibfk_1" FOREIGN KEY ("IdProxy") REFERENCES "proxyserver" ("Id"),
  CONSTRAINT "RankingMonitorDeepRun_ibfk_2" FOREIGN KEY ("IdSearchEngineCountry") REFERENCES "searchenginecountry" ("Id"),
  CONSTRAINT "RankingMonitorDeepRun_ibfk_3" FOREIGN KEY ("IdRankingMonitorRun") REFERENCES "rankingmonitorrun" ("Id"),
  CONSTRAINT "RankingMonitorDeepRun_ibfk_4" FOREIGN KEY ("Status") REFERENCES "status" ("Name")
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Definition of table "rankingmonitorrun"
--

DROP TABLE IF EXISTS "rankingmonitorrun";
CREATE TABLE "rankingmonitorrun" (
  "Id" int(10) unsigned NOT NULL auto_increment,
  "IdProject" int(11) NOT NULL,
  "User" varchar(100) NOT NULL,
  "AnalysisType" char(1) NOT NULL,
  "Status" char(1) default NULL,
  "ExecutionDate" datetime default NULL,
  "EndDate" datetime default NULL,
  "StatusReason" text,
  PRIMARY KEY  ("Id"),
  KEY "RankingMonitorRun_ibfk_1" ("Status"),
  KEY "RankingMonitorRun_ibfk_2" ("IdProject"),
  CONSTRAINT "RankingMonitorRun_ibfk_1" FOREIGN KEY ("Status") REFERENCES "status" ("Name"),
  CONSTRAINT "RankingMonitorRun_ibfk_2" FOREIGN KEY ("IdProject") REFERENCES "project" ("Id")
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Definition of table "searchengine"
--

DROP TABLE IF EXISTS "searchengine";
CREATE TABLE "searchengine" (
  "Id" int(10) unsigned NOT NULL,
  "Name" varchar(100) NOT NULL,
  "Description" varchar(300) NOT NULL,
  "UrlLogo" varchar(1000) default NULL,
  "UrlBigLogo" varchar(1000) default NULL,
  PRIMARY KEY  ("Id")
) ENGINE=InnoDB DEFAULT CHARSET=utf8;


--
-- Definition of table "searchenginecountry"
--

DROP TABLE IF EXISTS "searchenginecountry";
CREATE TABLE "searchenginecountry" (
  "Id" int(10) unsigned NOT NULL,
  "IdSearchEngine" int(10) unsigned NOT NULL,
  "IdCountry" int(11) NOT NULL,
  "Url" varchar(500) default NULL,
  PRIMARY KEY  ("Id"),
  KEY "searchenginecountry_ibfk_2" ("IdCountry"),
  KEY "searchenginecountry_ibfk_1" ("IdSearchEngine"),
  CONSTRAINT "searchenginecountry_ibfk_1" FOREIGN KEY ("IdSearchEngine") REFERENCES "searchengine" ("Id"),
  CONSTRAINT "searchenginecountry_ibfk_2" FOREIGN KEY ("IdCountry") REFERENCES "country" ("Id")
) ENGINE=InnoDB DEFAULT CHARSET=utf8;






--
-- Definition of table "seoprofile"
--

DROP TABLE IF EXISTS "seoprofile";
CREATE TABLE "seoprofile" (
  "Id" int(10) unsigned NOT NULL auto_increment,
  "Name" varchar(100) collate utf8_unicode_ci NOT NULL,
  "ApplicationName" varchar(100) collate utf8_unicode_ci default NULL,
  "CreatedDate" datetime default NULL,
  "UpdatedDate" datetime default NULL,
  "ProfileType" int(11) default NULL,
  "LastActivityDate" datetime default NULL,
  "LastPropertyChangedDate" datetime default NULL,
  PRIMARY KEY  ("Id"),
  KEY "Name" ("Name")
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;



--
-- Definition of table "seotoolsetuser"
--

DROP TABLE IF EXISTS "seotoolsetuser";
CREATE TABLE "seotoolsetuser" (
  "Id" int(11) NOT NULL auto_increment,
  "FirstName" varchar(250) default NULL,
  "LastName" varchar(250) default NULL,
  "Email" varchar(300) default NULL,
  "Address1" varchar(300) default NULL,
  "Address2" varchar(300) default NULL,
  "CityTown" varchar(100) default NULL,
  "State" varchar(255) default NULL,
  "Zip" varchar(100) default NULL,
  "Telephone" varchar(255) default NULL,
  "Login" varchar(100) default NULL,
  "Password" varchar(100) default NULL,
  "PasswordQuestion" varchar(300) default NULL,
  "PasswordAnswer" varchar(300) default NULL,
  "Enabled" bit(1) default '\0',
  "LastFailedLoginDate" datetime default NULL,
  "LastActivityDate" datetime default NULL,
  "LastPasswordChangedDate" datetime default NULL,
  "IsLockedOut" bit(1) default '\0',
  "LockedOutDate" datetime default NULL,
  "FailedPasswordAttemptCount" int(11) default NULL,
  "ExpirationDate" datetime default NULL,
  "CreatedBy" varchar(100) default NULL,
  "UpdatedBy" varchar(100) default NULL,
  "IdCountry" int(11) default NULL,
  "IdUserRole" int(11) default NULL,
  "IdAccount" int(11) NOT NULL,
  "CreatedDate" datetime default NULL,
  "UpdatedDate" datetime default NULL,
  "LastLoginDate" datetime default NULL,
  PRIMARY KEY  ("Id"),
  KEY "R_1" ("IdCountry"),
  KEY "R_2" ("IdUserRole"),
  KEY "R_7" ("IdAccount"),
  CONSTRAINT "seotoolsetuser_ibfk_1" FOREIGN KEY ("IdCountry") REFERENCES "country" ("Id"),
  CONSTRAINT "seotoolsetuser_ibfk_2" FOREIGN KEY ("IdUserRole") REFERENCES "userrole" ("Id"),
  CONSTRAINT "seotoolsetuser_ibfk_3" FOREIGN KEY ("IdAccount") REFERENCES "account" ("Id")
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*
--
-- Definition of table "service"
--

DROP TABLE IF EXISTS "service";
CREATE TABLE "service" (
  "Id" int(11) NOT NULL auto_increment,
  "Name" varchar(250) default NULL,
  "Description" varchar(250) default NULL,
  "Enabled" bit(1) default '\0',
  PRIMARY KEY  ("Id")
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
*/

--
-- Definition of table "status"
--

DROP TABLE IF EXISTS "status";
CREATE TABLE "status" (
  "Name" char(1) NOT NULL,
  "Description" varchar(100) NOT NULL,
  PRIMARY KEY  ("Name")
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*
--
-- Definition of table "subscription"
--

DROP TABLE IF EXISTS "subscription";
CREATE TABLE "subscription" (
  "Id" int(11) NOT NULL auto_increment,
  "IdAccount" int(11) NOT NULL,
  "IdService" int(11) NOT NULL,
  PRIMARY KEY  ("Id"),
  KEY "R_8" ("IdAccount"),
  KEY "R_10" ("IdService"),
  CONSTRAINT "subscription_ibfk_1" FOREIGN KEY ("IdAccount") REFERENCES "account" ("Id"),
  CONSTRAINT "subscription_ibfk_2" FOREIGN KEY ("IdService") REFERENCES "service" ("Id")
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
*/
--
-- Definition of table "userpermission"
--

DROP TABLE IF EXISTS "userpermission";
CREATE TABLE "userpermission" (
  "Id" int(11) NOT NULL auto_increment,
  "Name" varchar(250) default NULL,
  "Description" varchar(300) default NULL,
  PRIMARY KEY  ("Id")
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Definition of table "userrole"
--

DROP TABLE IF EXISTS "userrole";
CREATE TABLE "userrole" (
  "Id" int(11) NOT NULL auto_increment,
  "Name" varchar(250) default NULL,
  "Description" varchar(300) default NULL,
  "Enabled" bit(1) default '\0',
  PRIMARY KEY  ("Id")
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Definition of table "PromoCode"
--

CREATE TABLE `seodb`.`PromoCode` (
  `Id` INTEGER UNSIGNED NOT NULL AUTO_INCREMENT,
  `Code` VARCHAR(32) NOT NULL,
  `BeginDate` DATETIME,
  `EndDate` DATETIME,
  `Period` INTEGER UNSIGNED,
  `PromoType` INTEGER UNSIGNED NOT NULL,
  `PromoAmount` DOUBLE,
  `AccountType` INTEGER UNSIGNED NOT NULL,
  `Description` VARCHAR(45),
  `MaxUse` INTEGER UNSIGNED,
  `TimesUsed` INTEGER UNSIGNED,
  PRIMARY KEY (`Id`),
  KEY "R_20" ("PromoType"),
  KEY "R_21" ("AccountType"),
  CONSTRAINT "promocode_ibfk_1" FOREIGN KEY ("PromoType") REFERENCES "promotype" ("Id"),
  CONSTRAINT "promocode_ibfk_2" FOREIGN KEY ("AccountType") REFERENCES "accounttype" ("Id")
)
--
-- Definition of table "userrolpermission"
--

DROP TABLE IF EXISTS "userrolpermission";
CREATE TABLE "userrolpermission" (
  "Id" int(11) NOT NULL auto_increment,
  "IdUserPermission" int(11) NOT NULL,
  "IdUserRole" int(11) NOT NULL,
  PRIMARY KEY  ("Id"),
  KEY "R_3" ("IdUserPermission"),
  KEY "R_4" ("IdUserRole"),
  CONSTRAINT "userrolpermission_ibfk_1" FOREIGN KEY ("IdUserPermission") REFERENCES "userpermission" ("Id"),
  CONSTRAINT "userrolpermission_ibfk_2" FOREIGN KEY ("IdUserRole") REFERENCES "userrole" ("Id")
) ENGINE=InnoDB DEFAULT CHARSET=utf8;


DROP TABLE IF EXISTS "enginesperproxyresultview";
DROP VIEW IF EXISTS "enginesperproxyresultview";
CREATE VIEW "enginesperproxyresultview" AS select "keyworddeepanalysis"."Keyword" AS "Keyword","keyworddeepanalysis"."Pages" AS "Pages","proxyserver"."City" AS "ProxyCity","country"."Name" AS "ProxyCountry","searchenginecountry"."Url" AS "SearchEngineUrl","searchengine"."Name" AS "SearchEngineName","rankingmonitordeeprun"."IdProxy" AS "IdProxy","rankingmonitordeeprun"."IdSearchEngineCountry" AS "IdSearchEngineCountry","rankingmonitordeeprun"."Id" AS "IdRankingMonitorDeepRun","rankingmonitordeeprun"."IdRankingMonitorRun" AS "IdRankingMonitorRun" from ((((("rankingmonitordeeprun" join "keyworddeepanalysis" on(("rankingmonitordeeprun"."Id" = "keyworddeepanalysis"."IdRankingMonitorDeepRun"))) join "searchenginecountry" on(("rankingmonitordeeprun"."IdSearchEngineCountry" = "searchenginecountry"."Id"))) join "searchengine" on(("searchenginecountry"."IdSearchEngine" = "searchengine"."Id"))) join "proxyserver" on(("rankingmonitordeeprun"."IdProxy" = "proxyserver"."Id"))) join "country" on(("proxyserver"."IdCountry" = "country"."Id")));

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;










