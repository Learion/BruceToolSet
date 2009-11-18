
/* Drop the OriginalDate column from the WikiVersionedArticle table */
CREATE TABLE WikiVersionedArticleTmp (Id TEXT not null, IdArticle TEXT not null, Version INTEGER not null, Title TEXT not null, Owner TEXT not null, UpdateUser TEXT not null, Description TEXT, Body TEXT, TOC TEXT, Author TEXT, InsertDate DATETIME not null, UpdateDate DATETIME not null, Tag TEXT, primary key (Id), unique (IdArticle, Version));

INSERT INTO WikiVersionedArticleTmp SELECT Id, IdArticle, Version, Title, Owner, UpdateUser, Description, Body, TOC, Author, InsertDate, UpdateDate, Tag  FROM WikiVersionedArticle;

DROP TABLE WikiVersionedArticle; 

ALTER TABLE WikiVersionedArticleTmp RENAME TO WikiVersionedArticle;