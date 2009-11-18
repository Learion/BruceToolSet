using System;

namespace WebDemo.code
{
    public interface IErrorMessage
    {
        void SetError(Type context, string message);
        void SetError(Type context, Exception ex);
    }
}
