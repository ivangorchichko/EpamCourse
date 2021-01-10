using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using task3EPAMCourse.ATS.Contracts;
using task3EPAMCourse.ATS.Enums;
using task3EPAMCourse.ATS.Model;

namespace task3EPAMCourse.ATS.Service
{
    public class TerminalService : ITerminalService
    { 
        public IEnumerable<ITerminal> Terminals { get; } = new List<ITerminal>()
            {
                 new Terminal("001", TerminalCondition.Avaible),
                 new Terminal("002", TerminalCondition.Avaible),
                 new Terminal("003", TerminalCondition.Avaible)
            };
        public TerminalService(IEnumerable<ITerminal> terminals)
        {
            Terminals = terminals;
        }

        public TerminalService()
        {
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
