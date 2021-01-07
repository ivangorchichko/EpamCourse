using System;
using System.Collections.Generic;
using System.Text;
using task3EPAMCourse.Model.BillimgSystem;
using task3EPAMCourse.Enums;
using task3EPAMCourse.Contracts;
using System.Linq;

namespace task3EPAMCourse.Model.ATS
{
    public class AutoTelephoneStation : IATS
    {
        public IEnumerable<IPort> Ports { get; }
        public IEnumerable<ITerminal> Terminals { get; }
        public Billing Billing { get; } = new Billing();

        public AutoTelephoneStation() { }
        
        public AutoTelephoneStation(IEnumerable<IPort> ports, IEnumerable<ITerminal> terminals)
        {
            Ports = ports;
            Terminals = terminals;
        }

        public void CreateContract(ref ICaller caller, int callerNumber, ITerminal terminal)
        {
            caller = new Caller(callerNumber, terminal);
            terminal.ChangeTerminalCondition(TerminalCondition.IsUsed);
        }

        public void CallersConect(ICaller caller1, ICaller caller2)
        {
            caller1.Terminal.ConnectedOnPort(Ports.ToList()[0]);
            caller2.Terminal.ConnectedOnPort(Ports.ToList()[0]);
            Ports.ToList()[0].ChangeCondition(PortCondition.InCalling);
            Billing.StartConnecting(caller1, caller2);
        }

        public void StopCallersConect(ICaller caller1, ICaller caller2)
        {
            caller1.Terminal.ConnectedOnPort(null);
            caller2.Terminal.ConnectedOnPort(null);
            Ports.ToList()[0].ChangeCondition(PortCondition.Free);
            Billing.StopConnecting(caller1, caller2);
        }
    }
}
