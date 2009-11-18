using System;
using System.Collections.Generic;
using System.Text;

namespace Eucalypto.Wiki
{
    [Serializable]
    public class ArticleNotFoundException : EucalyptoException
    {
        public ArticleNotFoundException(string id)
            : base("Article " + id + " not found")
        {

        }
    }

    [Serializable]
    public class ArticleNameAlreadyExistsException : EucalyptoException
    {
        public ArticleNameAlreadyExistsException(string name)
            : base("Name " + name + " already used. The article name must be unique.")
        {

        }
    }

    [Serializable]
    public class ArticleStatusNotValidException : EucalyptoException
    {
        public ArticleStatusNotValidException(Wiki.ArticleStatus status)
            : base("Article status " + status.ToString() + " not valid")
        {

        }
    }

    [Serializable]
    public class WikiCategoryNotFoundException : EucalyptoException
    {
        public WikiCategoryNotFoundException(string id)
            : base("Wiki category " + id + " not found")
        {

        }
    }

    [Serializable]
    public class CategoryNotSpecifiedException : EucalyptoException
    {
        public CategoryNotSpecifiedException()
            : base("Categories list not specified")
        {

        }
    }
}
