using System;
using System.Collections.Generic;
using System.Text;

namespace Eucalypto.Notification
{

    [Serializable]
    public class SmtpNotificationException : EucalyptoException
    {
        public SmtpNotificationException(string destinationUser, Exception innerException)
            : base("Failed to send Smtp notification to " + destinationUser, innerException)
        {

        }
    }
}
