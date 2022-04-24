using System;
using System.Collections.Generic;

namespace Framework.Command
{
    public class TurnOnLight1 : ICommand
    {
        public string generateCmd()
        {
            return "1,0";
        }
    }
}