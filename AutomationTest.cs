using System;
using Xunit;
using FluentAssertions;
using log4net;
using EnrichAutoTest.Framework.Fixture;
using System.Collections.Generic;
using Framework.Command;
using Framework.Device;
using Framework.Communication;
using EnrichAutoTest.Framework.HandlingInputTest;

namespace EnrichAutoTest
{
    public class AutomationTest
    {
        [Theory]
        [MemberData(nameof(ListTest), DisableDiscoveryEnumeration = true)]
        void Testcase1(string testId, List<CommandType> listCommand)
        {
            var arduino = new Arduino();
            var uart = new UART();
            var commandFactory = new CommandFactory();
            arduino.createProtocol(uart);
            for(int i = 0; i < listCommand.Count; i++)
            {
                arduino.Write(commandFactory.createCommand(listCommand[i]));
                int index = int.Parse(testId);
                var u = expectedCommand[index-1][i];
                uart.SentData.Should().BeEquivalentTo(u);
            }
        }

        List<List<string>> expectedCommand = new List<List<string>>()
        {
            new List<string> { "UART:1,0-Arduino", "UART:1,1-Arduino", "UART:0,0-Arduino"},
            new List<string> { "UART:0,0-Arduino", "UART:1,0-Arduino" }
        };
        public static IEnumerable<object[]> ListTest()
        {
            var testParams = new List<object[]>();
            var dictionary = GetInput();
            foreach(var e in dictionary)
            {
                testParams.Add(new object[] { e.Key, e.Value });
            }

            foreach (object[] element in testParams)
                yield return element;
        }

        public static Dictionary<string, List<CommandType>> GetInput()
        {
            var fileReader = new ExcelFileReader();
            fileReader.OpenFile();
            fileReader.VerifySpec();
            return fileReader.ParseData(fileReader.GetData());
        }
    }
}
