using System.Collections.Generic;
using System;
using System.IO;
using Framework.Command;

namespace Framework.Communication
{
    public class SPI : ICommunication
    {
        string sentData;
        private bool disposedValue;

        public string SentData { get => sentData; set => sentData = value; }

        public void Send(string commandString)
        {
            sentData = "SPI:"+commandString;
        }
        public string Receive()
        {
            return "";
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~SPI()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}