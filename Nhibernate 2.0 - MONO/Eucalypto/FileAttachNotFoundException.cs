using System;

namespace Eucalypto
{
    [Serializable]
    public class FileAttachNotFoundException : EucalyptoException
    {
        public FileAttachNotFoundException(string id)
            : base("FileAttach " + id + " not found")
        {

        }
    }
}