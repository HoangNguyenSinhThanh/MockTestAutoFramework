using System.Collections.Generic;
using System;
using System.IO;
using Framework.Communication;
using Framework.Command;

namespace Framework.Device
{
    public class Arduino : IDevice
    {
        private ICommunication protocol;
        public void createProtocol(ICommunication protocol)
        {
            this.protocol = protocol;
        }
        public void Write(ICommand command)
        {
            string senData = command.generateCmd()+"-Arduino";
            protocol.Send(senData);
        }

        public void Receive()
        {

        }

        public void Transmit()
        {

        }
        public void Dispose()
        {

        }
    }
}