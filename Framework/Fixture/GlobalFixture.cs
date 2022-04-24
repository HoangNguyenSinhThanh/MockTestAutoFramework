using System;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Command;
using System.IO;
using EnrichAutoTest.Framework.HandlingInputTest;

namespace EnrichAutoTest.Framework.Fixture
{
    public class GlobalFixture : IDisposable
    {
        private bool disposedValue;
        const string pathInputTest = @"Input.xlsx";

        public static Dictionary<string, List<CommandType>> listTestCase;
        private static AbstractFileReader FileReader()
        {
            FileInfo fi = new FileInfo(pathInputTest);
            if (fi.Extension == ".xlsx")
                return new ExcelFileReader();
            if (fi.Extension == ".yaml")
                throw new Exception("This file is not supported!");
            else
                throw new Exception("This file is not supported!");
        }
        public static Dictionary<string, List<CommandType>> GetInput()
        {
            var fileReader = new ExcelFileReader();
            fileReader.OpenFile();
            fileReader.VerifySpec();
            listTestCase = fileReader.ParseData(fileReader.GetData());
            return listTestCase;
        }
        public GlobalFixture()
        {
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
        // ~GlobalFixture()
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

    [CollectionDefinition("GlobalFixture")]
    public class GlobalFixtureCollection : ICollectionFixture<GlobalFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }
}
