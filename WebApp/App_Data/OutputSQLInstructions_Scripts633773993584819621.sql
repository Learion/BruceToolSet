
alter table KeywordList  drop foreign key FK2D3478E0584CF274


alter table SEOToolsetUser  drop foreign key FKAE7A2571EE5B5772


alter table SEOToolsetUser  drop foreign key FKAE7A257114167040


alter table SEOToolsetUser  drop foreign key FKAE7A2571B0E8D820


alter table Project  drop foreign key FKCFC6D85AEE5B5772


alter table Project  drop foreign key FKCFC6D85ACAF93A6F


alter table RankingMonitorRun  drop foreign key FK201ED801584CF274


alter table RankingMonitorRun  drop foreign key FK201ED8017A259B66


alter table RankingMonitorDeepRun  drop foreign key FK2174B2BDB4F9EFD5


alter table RankingMonitorDeepRun  drop foreign key FK2174B2BD193950DF


alter table RankingMonitorDeepRun  drop foreign key FK2174B2BD2A419173


alter table RankingMonitorDeepRun  drop foreign key FK2174B2BD7A259B66


alter table ProjectPermissionRole  drop foreign key FKF1749E6A6670C2B4


alter table ProjectPermissionRole  drop foreign key FKF1749E6A2B062286


alter table ProfileProperty  drop foreign key FK6972F87C10D6C410


alter table Keyword  drop foreign key FK9A18B6BD9F22E2C6


alter table Account  drop foreign key FKBE1051AF6D634F7


alter table Account  drop foreign key FKBE1051AF868E1E38


alter table MonitorProxyServer  drop foreign key FK7B1B3737584CF274


alter table MonitorProxyServer  drop foreign key FK7B1B373718452F2F


alter table ProxyServer  drop foreign key FK60185D7B14167040


alter table MonitorSearchEngineCountry  drop foreign key FK368754942A419173


alter table MonitorSearchEngineCountry  drop foreign key FK36875494584CF274


alter table Competitor  drop foreign key FK698FB1B0584CF274


alter table SearchEngineCountry  drop foreign key FK3991303E4913A9A2


alter table SearchEngineCountry  drop foreign key FK3991303E14167040


alter table ProjectUser  drop foreign key FK9079709E584CF274


alter table ProjectUser  drop foreign key FK9079709E2B062286


alter table ProjectUser  drop foreign key FK9079709E9F7A3718


alter table KeywordAnalysis  drop foreign key FK6941F6E193950DF


alter table KeywordDeepAnalysis  drop foreign key FKED13DAD26534913E


alter table UserRolPermission  drop foreign key FK7807F044396F2BBF


alter table UserRolPermission  drop foreign key FK7807F044B0E8D820


alter table MonitorKeywordList  drop foreign key FK389B5878584CF274


alter table MonitorKeywordList  drop foreign key FK389B58789F22E2C6

drop table if exists KeywordList
drop table if exists SearchEngine
drop table if exists SEOToolsetUser
drop table if exists CreditCardType
drop table if exists Frequency
drop table if exists Project
drop table if exists Status
drop table if exists RankingMonitorRun
drop table if exists Country
drop table if exists RankingMonitorDeepRun
drop table if exists UserPermission
drop table if exists ProjectPermissionRole
drop table if exists ProfileProperty
drop table if exists Keyword
drop table if exists Account
drop table if exists MonitorProxyServer
drop table if exists ProxyServer
drop table if exists MonitorSearchEngineCountry
drop table if exists ProjectPermission
drop table if exists Competitor
drop table if exists SEOProfile
drop table if exists SearchEngineCountry
drop table if exists UserRole
drop table if exists ProjectUser
drop table if exists KeywordAnalysis
drop table if exists KeywordDeepAnalysis
drop table if exists UserRolPermission
drop table if exists ProjectRole
drop table if exists MonitorKeywordList
create table KeywordList (
  Id INTEGER NOT NULL AUTO_INCREMENT,
   Name VARCHAR(83),
   Enabled TINYINT(1),
   IdProject INTEGER not null,
   primary key (Id)
)
create table SearchEngine (
  Id INTEGER NOT NULL AUTO_INCREMENT,
   Name VARCHAR(100) not null,
   Description TEXT not null,
   UrlLogo TEXT not null,
   UrlBigLogo TEXT not null,
   primary key (Id)
)
create table SEOToolsetUser (
  Id INTEGER NOT NULL AUTO_INCREMENT,
   FirstName VARCHAR(250),
   LastName VARCHAR(250),
   Email TEXT,
   Address1 TEXT,
   Address2 TEXT,
   CityTown VARCHAR(250),
   State VARCHAR(250),
   Zip VARCHAR(100),
   Telephone VARCHAR(100),
   Login VARCHAR(100),
   Password VARCHAR(100),
   PasswordQuestion TEXT,
   PasswordAnswer TEXT,
   LastFailedLoginDate DATETIME,
   LastActivityDate DATETIME,
   LastPasswordChangedDate DATETIME,
   IsLockedOut TINYINT(1),
   LockedOutDate DATETIME,
   FailedPasswordAttemptCount INTEGER,
   ExpirationDate DATETIME,
   CreatedBy VARCHAR(100),
   UpdatedBy VARCHAR(100),
   Enabled TINYINT(1),
   CreatedDate DATETIME,
   UpdatedDate DATETIME,
   IdAccount INTEGER,
   IdCountry INTEGER,
   IdUserRole INTEGER,
   LastLoginDate DATETIME,
   primary key (Id)
)
create table CreditCardType (
  Id VARCHAR(255) not null,
   Name TEXT,
   primary key (Id)
)
create table Frequency (
  Id INTEGER NOT NULL AUTO_INCREMENT,
   Name VARCHAR(16) not null,
   primary key (Id)
)
create table Project (
  Id INTEGER NOT NULL AUTO_INCREMENT,
   Domain TEXT,
   ClientName VARCHAR(100),
   ContactEmail TEXT,
   ContactName TEXT,
   ContactPhone VARCHAR(100),
   CreatedDate DATETIME,
   UpdatedDate DATETIME,
   CreatedBy VARCHAR(100),
   UpdatedBy VARCHAR(100),
   Enabled TINYINT(1),
   Name VARCHAR(100),
   IdAccount INTEGER not null,
   MonitorUpdatedBy VARCHAR(33),
   MonitorUpdatedDate DATETIME,
   IdMonitorFrequency INTEGER,
   primary key (Id)
)
create table Status (
  Name VARCHAR(255) not null,
   Description VARCHAR(33) not null,
   primary key (Name)
)
create table RankingMonitorRun (
  Id INTEGER NOT NULL AUTO_INCREMENT,
   User VARCHAR(33) not null,
   AnalysisType VARCHAR(255) not null,
   ExecutionDate DATETIME not null,
   EndDate DATETIME,
   IdProject INTEGER not null,
   Status VARCHAR(255),
   StatusReason TEXT,
   primary key (Id)
)
create table Country (
  Id INTEGER NOT NULL AUTO_INCREMENT,
   Name TEXT,
   FlagUrl TEXT,
   SearchEngineImportance INTEGER,
   primary key (Id)
)
create table RankingMonitorDeepRun (
  Id INTEGER NOT NULL AUTO_INCREMENT,
   StatusReason TEXT,
   PageRank INTEGER,
   InboundLinks INTEGER,
   PagesIndexed INTEGER,
   IdProxy INTEGER not null,
   IdRankingMonitorRun INTEGER not null,
   IdSearchEngineCountry INTEGER not null,
   Status VARCHAR(255) not null,
   primary key (Id)
)
create table UserPermission (
  Id INTEGER NOT NULL AUTO_INCREMENT,
   Name VARCHAR(250),
   Description TEXT,
   primary key (Id)
)
create table ProjectPermissionRole (
  Id INTEGER NOT NULL AUTO_INCREMENT,
   IdProjectPermission INTEGER not null,
   IdProjectRole INTEGER not null,
   primary key (Id)
)
create table ProfileProperty (
  Id INTEGER NOT NULL AUTO_INCREMENT,
   IdUser INTEGER not null,
   Name VARCHAR(100) not null,
   StringValue TEXT,
   BinaryValue LONGBLOB,
   primary key (Id),
  unique (IdUser, Name)
)
create table Keyword (
  Id INTEGER NOT NULL AUTO_INCREMENT,
   Keyword VARCHAR(100),
   IdKeywordList INTEGER not null,
   primary key (Id)
)
create table Account (
  Id INTEGER NOT NULL AUTO_INCREMENT,
   Name VARCHAR(250),
   MaxNumberOfUser INTEGER,
   MaxNumberOfDomainUser INTEGER,
   MaxNumberOfProjects INTEGER,
   CompanyName TEXT,
   CompanyAddress1 TEXT,
   CompanyAddress2 TEXT,
   CompanyCity VARCHAR(100),
   CompanyState VARCHAR(100),
   CompanyZip VARCHAR(100),
   CreditCardNumber VARCHAR(20),
   CreditCardCVS VARCHAR(10),
   CreditCardAddress1 TEXT,
   CreditCardAddress2 TEXT,
   CreditCardCity VARCHAR(100),
   CreditCardZip VARCHAR(10),
   RecurringBill TINYINT(1),
   CreatedDate DATETIME,
   UpdatedDate DATETIME,
   CreatedBy VARCHAR(100),
   UpdatedBy VARCHAR(100),
   Enabled TINYINT(1),
   AccountExpirationDate DATETIME,
   CreditCardExpiration DATETIME,
   CreditCardEmail VARCHAR(255),
   CreditCardIdCountry INTEGER,
   CreditCardState VARCHAR(255),
   CreditCardCardholder VARCHAR(255),
   PromoCode VARCHAR(255),
   CompanyIdCountry INTEGER,
   CompanyPhone VARCHAR(255),
   CreditCardType VARCHAR(255),
   IdAccountOwner INTEGER,
   primary key (Id)
)
create table MonitorProxyServer (
  Id INTEGER NOT NULL AUTO_INCREMENT,
   IdProject INTEGER not null,
   IdProxyServer INTEGER not null,
   primary key (Id)
)
create table ProxyServer (
  Id INTEGER NOT NULL AUTO_INCREMENT,
   IdCountry INTEGER not null,
   City VARCHAR(33) not null,
   ImportanceLevel INTEGER,
   primary key (Id)
)
create table MonitorSearchEngineCountry (
  Id INTEGER NOT NULL AUTO_INCREMENT,
   IdSearchEngineCountry INTEGER not null,
   IdProject INTEGER not null,
   primary key (Id)
)
create table ProjectPermission (
  Id INTEGER NOT NULL AUTO_INCREMENT,
   Name VARCHAR(100),
   Description VARCHAR(250),
   primary key (Id)
)
create table Competitor (
  Id INTEGER NOT NULL AUTO_INCREMENT,
   Name VARCHAR(250),
   Url TEXT,
   Description TEXT,
   IdProject INTEGER not null,
   primary key (Id)
)
create table SEOProfile (
  Id INTEGER NOT NULL AUTO_INCREMENT,
   Name VARCHAR(100) not null,
   ApplicationName VARCHAR(100) not null,
   CreatedDate DATETIME not null,
   UpdatedDate DATETIME not null,
   ProfileType INTEGER not null,
   LastActivityDate DATETIME not null,
   LastPropertyChangedDate DATETIME not null,
   primary key (Id),
  unique (Name, ApplicationName)
)
create table SearchEngineCountry (
  Id INTEGER NOT NULL AUTO_INCREMENT,
   Url VARCHAR(166),
   IdSearchEngine INTEGER not null,
   IdCountry INTEGER not null,
   primary key (Id)
)
create table UserRole (
  Id INTEGER NOT NULL AUTO_INCREMENT,
   Name VARCHAR(250),
   Description TEXT,
   Enabled TINYINT(1),
   primary key (Id)
)
create table ProjectUser (
  Id INTEGER NOT NULL AUTO_INCREMENT,
   Enabled TINYINT(1),
   MonitorEmails TINYINT(1),
   IdProject INTEGER not null,
   IdProjectRole INTEGER not null,
   IdUser INTEGER not null,
   primary key (Id)
)
create table KeywordAnalysis (
  Id INTEGER NOT NULL AUTO_INCREMENT,
   Keyword VARCHAR(200) not null,
   GoogleResults INTEGER,
   AllInTitle INTEGER,
   AliasDomains INTEGER,
   CPC DOUBLE,
   DailySearches INTEGER,
   Results INTEGER,
   Engines INTEGER,
   Pages INTEGER,
   Status VARCHAR(255) not null,
   IdRankingMonitorRun INTEGER not null,
   primary key (Id)
)
create table KeywordDeepAnalysis (
  Id INTEGER NOT NULL AUTO_INCREMENT,
   Keyword VARCHAR(33) not null,
   Pages INTEGER,
   Status VARCHAR(255) not null,
   IdRankingMonitorDeepRun INTEGER not null,
   primary key (Id)
)
create table UserRolPermission (
  Id INTEGER NOT NULL AUTO_INCREMENT,
   IdUserPermission INTEGER not null,
   IdUserRole INTEGER not null,
   primary key (Id)
)
create table ProjectRole (
  Id INTEGER NOT NULL AUTO_INCREMENT,
   Name VARCHAR(100),
   Description VARCHAR(250),
   Enabled TINYINT(1),
   primary key (Id)
)
create table MonitorKeywordList (
  Id INTEGER NOT NULL AUTO_INCREMENT,
   IdProject INTEGER not null,
   IdKeywordList INTEGER not null,
   primary key (Id)
)
alter table KeywordList add index (IdProject), add constraint FK2D3478E0584CF274 foreign key (IdProject) references Project (Id)
alter table SEOToolsetUser add index (IdAccount), add constraint FKAE7A2571EE5B5772 foreign key (IdAccount) references Account (Id)
alter table SEOToolsetUser add index (IdCountry), add constraint FKAE7A257114167040 foreign key (IdCountry) references Country (Id)
alter table SEOToolsetUser add index (IdUserRole), add constraint FKAE7A2571B0E8D820 foreign key (IdUserRole) references UserRole (Id)
alter table Project add index (IdAccount), add constraint FKCFC6D85AEE5B5772 foreign key (IdAccount) references Account (Id)
alter table Project add index (IdMonitorFrequency), add constraint FKCFC6D85ACAF93A6F foreign key (IdMonitorFrequency) references Frequency (Id)
alter table RankingMonitorRun add index (IdProject), add constraint FK201ED801584CF274 foreign key (IdProject) references Project (Id)
alter table RankingMonitorRun add index (Status), add constraint FK201ED8017A259B66 foreign key (Status) references Status (Name)
alter table RankingMonitorDeepRun add index (IdProxy), add constraint FK2174B2BDB4F9EFD5 foreign key (IdProxy) references ProxyServer (Id)
alter table RankingMonitorDeepRun add index (IdRankingMonitorRun), add constraint FK2174B2BD193950DF foreign key (IdRankingMonitorRun) references RankingMonitorRun (Id)
alter table RankingMonitorDeepRun add index (IdSearchEngineCountry), add constraint FK2174B2BD2A419173 foreign key (IdSearchEngineCountry) references SearchEngineCountry (Id)
alter table RankingMonitorDeepRun add index (Status), add constraint FK2174B2BD7A259B66 foreign key (Status) references Status (Name)
alter table ProjectPermissionRole add index (IdProjectPermission), add constraint FKF1749E6A6670C2B4 foreign key (IdProjectPermission) references ProjectPermission (Id)
alter table ProjectPermissionRole add index (IdProjectRole), add constraint FKF1749E6A2B062286 foreign key (IdProjectRole) references ProjectRole (Id)
alter table ProfileProperty add index (IdUser), add constraint FK6972F87C10D6C410 foreign key (IdUser) references SEOProfile (Id)
alter table Keyword add index (IdKeywordList), add constraint FK9A18B6BD9F22E2C6 foreign key (IdKeywordList) references KeywordList (Id)
alter table Account add index (CreditCardType), add constraint FKBE1051AF6D634F7 foreign key (CreditCardType) references CreditCardType (Id)
alter table Account add index (IdAccountOwner), add constraint FKBE1051AF868E1E38 foreign key (IdAccountOwner) references Account (Id)
alter table MonitorProxyServer add index (IdProject), add constraint FK7B1B3737584CF274 foreign key (IdProject) references Project (Id)
alter table MonitorProxyServer add index (IdProxyServer), add constraint FK7B1B373718452F2F foreign key (IdProxyServer) references ProxyServer (Id)
alter table ProxyServer add index (IdCountry), add constraint FK60185D7B14167040 foreign key (IdCountry) references Country (Id)
alter table MonitorSearchEngineCountry add index (IdSearchEngineCountry), add constraint FK368754942A419173 foreign key (IdSearchEngineCountry) references SearchEngineCountry (Id)
alter table MonitorSearchEngineCountry add index (IdProject), add constraint FK36875494584CF274 foreign key (IdProject) references Project (Id)
alter table Competitor add index (IdProject), add constraint FK698FB1B0584CF274 foreign key (IdProject) references Project (Id)
alter table SearchEngineCountry add index (IdSearchEngine), add constraint FK3991303E4913A9A2 foreign key (IdSearchEngine) references SearchEngine (Id)
alter table SearchEngineCountry add index (IdCountry), add constraint FK3991303E14167040 foreign key (IdCountry) references Country (Id)
alter table ProjectUser add index (IdProject), add constraint FK9079709E584CF274 foreign key (IdProject) references Project (Id)
alter table ProjectUser add index (IdProjectRole), add constraint FK9079709E2B062286 foreign key (IdProjectRole) references ProjectRole (Id)
alter table ProjectUser add index (IdUser), add constraint FK9079709E9F7A3718 foreign key (IdUser) references SEOToolsetUser (Id)
alter table KeywordAnalysis add index (IdRankingMonitorRun), add constraint FK6941F6E193950DF foreign key (IdRankingMonitorRun) references RankingMonitorRun (Id)
alter table KeywordDeepAnalysis add index (IdRankingMonitorDeepRun), add constraint FKED13DAD26534913E foreign key (IdRankingMonitorDeepRun) references RankingMonitorDeepRun (Id)
alter table UserRolPermission add index (IdUserPermission), add constraint FK7807F044396F2BBF foreign key (IdUserPermission) references UserPermission (Id)
alter table UserRolPermission add index (IdUserRole), add constraint FK7807F044B0E8D820 foreign key (IdUserRole) references UserRole (Id)
alter table MonitorKeywordList add index (IdProject), add constraint FK389B5878584CF274 foreign key (IdProject) references Project (Id)
alter table MonitorKeywordList add index (IdKeywordList), add constraint FK389B58789F22E2C6 foreign key (IdKeywordList) references KeywordList (Id)
