using System;
using System.Collections.Generic;

namespace Framework.Command
{
    public class TurnOffLight1 : ICommand
    {
        public string generateCmd()
        {
            return "1,1";
        }
    }
}