using System;
using System.Collections.Generic;

namespace Framework.Command
{
    class CommandFactory
    {
        public ICommand createCommand(CommandType commandType)
        {
            switch (commandType)
            {
                case CommandType.On0:
                    return new TurnOnLight0();
                    break;
                case CommandType.On1:
                    return new TurnOnLight1();
                    break;
                case CommandType.Off0:
                    return new TurnOffLight0();
                    break;
                case CommandType.Off1:
                    return new TurnOffLight1();
                    break;
                default:
                    throw new Exception("This command type is unsupported");
                    break;
            }
        }
    }
}