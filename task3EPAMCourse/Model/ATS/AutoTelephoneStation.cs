using System;
using System.Collections.Generic;
using System.Text;
using task3EPAMCourse.Model.ATS;

namespace task3EPAMCourse.Model
{
    public class AutoTelephoneStation
    {
        public IEnumerable<Port> Ports { get; }
        public IEnumerable<Terminal> Terminals { get; }
        public IEnumerable<Contract> Contracts { get; }

        public AutoTelephoneStation() { }

        public AutoTelephoneStation(IEnumerable<Port> ports, IEnumerable<Terminal> terminals, IEnumerable<Contract> contracts)
        {
            Ports = ports;
            Terminals = terminals;
            Contracts = contracts;
        }

        public void ConnectContract(Caller caller)
        {

        }
    }
}
