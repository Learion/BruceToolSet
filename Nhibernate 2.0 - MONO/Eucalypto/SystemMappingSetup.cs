using NHibernateDataStore.SchemaGenerator;

[assembly: SetupMapping("News", "Eucalypto.News.Category, Eucalypto; Eucalypto.News.Item, Eucalypto")]

[assembly: SetupMapping("Forum", "Eucalypto.Forum.Category, Eucalypto; Eucalypto.Forum.Topic, Eucalypto; Eucalypto.Forum.Message, Eucalypto")]

[assembly: SetupMapping("Membership", "Eucalypto.Membership.User, Eucalypto")]

[assembly: SetupMapping("Profile", "Eucalypto.Profile.ProfileUser, Eucalypto; Eucalypto.Profile.ProfileProperty, Eucalypto")]

[assembly: SetupMapping("Roles", "Eucalypto.Roles.Role, Eucalypto; Eucalypto.Roles.UserInRole, Eucalypto")]

[assembly: SetupMapping("Wiki", "Eucalypto.Wiki.Category, Eucalypto; Eucalypto.Wiki.Article, Eucalypto; Eucalypto.Wiki.FileAttachment, Eucalypto; Eucalypto.Wiki.VersionedArticle, Eucalypto")]
