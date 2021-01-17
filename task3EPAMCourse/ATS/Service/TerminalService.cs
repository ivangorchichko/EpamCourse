using System;
using System.Collections.Generic;
using System.Linq;
using task3EPAMCourse.ATS.Contracts;
using task3EPAMCourse.ATS.Enums;
using task3EPAMCourse.ATS.Model;

namespace task3EPAMCourse.ATS.Service
{
    public class TerminalService : ITerminalService
    { 
        public IEnumerable<ITerminal> Terminals { get; } 

        public TerminalService(AutoTelephoneStation autoTelephoneStation)
        {
            Terminals = new List<ITerminal>()
            {
                 new Terminal("001", TerminalCondition.Avaible, autoTelephoneStation),
                 new Terminal("002", TerminalCondition.Avaible, autoTelephoneStation),
                 new Terminal("003", TerminalCondition.Avaible, autoTelephoneStation),
                 new Terminal("004", TerminalCondition.Avaible, autoTelephoneStation)
            };
        }

        public ITerminal GetAvaibleTerminal()
        {
            var terminal = Terminals.Where(terminal => terminal.TerminalCondition == TerminalCondition.Avaible).FirstOrDefault();
            if (terminal != null)
            {
                return terminal;
            }
            else
            {
                Console.WriteLine("All terminals are in used");
                return null;
            }
        }
    }
}
