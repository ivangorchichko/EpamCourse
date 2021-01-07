using System;
using task3EPAMCourse.Model.BillimgSystem;
using task3EPAMCourse.Model.ATS;
using System.Collections;
using System.Collections.Generic;
using task3EPAMCourse.Enums;
using System.Linq;
using task3EPAMCourse.Contracts;

namespace task3EPAMCourse
{
    class Program
    {
        static void Main(string[] args)
        {
            IEnumerable<IPort> ports = new List<IPort>()
            {
                 new Port(3301, PortCondition.Free),
                 new Port(3302, PortCondition.Free),
                 new Port(3303, PortCondition.Free)
            };
            IEnumerable<ITerminal> terminals = new List<ITerminal>()
            {
                 new Terminal("001", TerminalCondition.Avaible),
                 new Terminal("002", TerminalCondition.Avaible),
                 new Terminal("003", TerminalCondition.Avaible)
            };

            IATS ATS = new AutoTelephoneStation(ports, terminals);
            ICaller caller = new Caller();
            ICaller caller1 = new Caller();
            ATS.CreateContract(ref caller, 123123, ATS.Terminals.ToList()[0]);
            ATS.CreateContract(ref caller1, 123023, ATS.Terminals.ToList()[1]);

            ATS.CallersConect(caller, caller1);
            
            ATS.StopCallersConect(caller, caller1);
        }
    }
}
