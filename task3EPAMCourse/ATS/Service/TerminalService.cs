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
                 new Terminal("001", TerminalCondition.Available, autoTelephoneStation),
                 new Terminal("002", TerminalCondition.Available, autoTelephoneStation),
                 new Terminal("003", TerminalCondition.Available, autoTelephoneStation),
                 new Terminal("004", TerminalCondition.Available, autoTelephoneStation)
            };
        }

        public ITerminal GetAvailableTerminal()
        {
            var terminal = Terminals.FirstOrDefault(availableTerminal => availableTerminal.TerminalCondition == TerminalCondition.Available);
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
