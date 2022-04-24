using Framework.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrichAutoTest.Framework.HandlingInputTest
{
    public abstract class AbstractFileReader
    {
        public CommandType RuleTestStep(string stringCommand)
        {
            switch (stringCommand)
            {
                case "Turn on light0":
                    return CommandType.On0;
                    break;
                case "Turn on light1":
                    return CommandType.On1;
                    break;
                case "Turn off light0":
                    return CommandType.Off0;
                    break;
                case "Turn off light1":
                    return CommandType.Off1;
                    break;
                default:
                    throw new Exception("String command is incorrect!");
            }

        }
        public abstract void OpenFile();
        public abstract void CloseFile();
        public abstract bool VerifySpec();
        public abstract List<Testspec> GetData();
        public Dictionary<string, List<CommandType>> ParseData(List<Testspec> listTestpec)
        {
            var dictCommandType = new Dictionary<string, List<CommandType>>();
            foreach(var e in listTestpec)
            {
                var listCommandType = new List<CommandType>();
                for(int i = 0; i< e.Teststep.Count; i++)
                {
                    listCommandType.Add(RuleTestStep(e.Teststep[i]));
                }
                dictCommandType.Add(e.TestId, listCommandType);
            }
            return dictCommandType;
        }
    }
}
