#region Using Directives

using System;
using System.Collections.Generic;

#endregion

namespace R3M.Controls
{
    public class CallbackEventArgs : EventArgs
    {
        private readonly IList<String> args;

        public CallbackEventArgs()
        {
            args = new List<string>();
        }

        public CallbackEventArgs(string eventArgument)
        {
            var argsSplitted = eventArgument.Split('~');
            args = new List<string>();
            for (var i = 0; i < argsSplitted.Length; i++)
            {
                args.Add(argsSplitted[i]);
            }
        }

        public string Result { get; set; }

        public IList<String> Parameters
        {
            get { return args; }
        }

        public void Add(string cadena)
        {
            args.Add(cadena);
        }
    }
}