using System;
using System.Collections.Generic;
using System.Text;

namespace Eucalypto.Forum
{
    [Serializable]
    public class TopicNotFoundException : EucalyptoException
    {
        public TopicNotFoundException(string id)
            : base("Topic " + id + " not found")
        {

        }
    }

    [Serializable]
    public class MessageNotFoundException : EucalyptoException
    {
        public MessageNotFoundException(string id)
            : base("Message " + id + " not found")
        {

        }
    }

    [Serializable]
    public class ForumCategoryNotFoundException : EucalyptoException
    {
        public ForumCategoryNotFoundException(string id)
            : base("Forum " + id + " not found")
        {

        }
    }

    [Serializable]
    public class ForumNotSpecifiedException : EucalyptoException
    {
        public ForumNotSpecifiedException()
            : base("Forum list not specified")
        {

        }
    }
}
