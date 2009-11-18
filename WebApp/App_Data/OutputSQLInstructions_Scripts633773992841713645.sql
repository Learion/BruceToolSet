
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
