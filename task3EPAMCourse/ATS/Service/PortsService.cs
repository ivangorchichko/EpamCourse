using System;
using System.Collections.Generic;
using System.Linq;
using Task3EPAMCourse.ATS.Contracts;
using Task3EPAMCourse.ATS.Enums;
using Task3EPAMCourse.ATS.Model;

namespace Task3EPAMCourse.ATS.Service
{
    public class PortsService : IPortsService
    {
        private readonly IList<IPort> _ports = new List<IPort>()
            {
                 new Port(3301, PortCondition.Off),
                 new Port(3302, PortCondition.Off),
                 new Port(3303, PortCondition.Off),
                 new Port(3304, PortCondition.Off),
            };

        public IPort GetFreePort()
        {
            var port = _ports.FirstOrDefault(freePort => freePort.Condition == PortCondition.Off);
            if (port != null)
            {
                return port;
            }
            else
            {
                Console.WriteLine("No more free ports");
                return null;
            }
        }

        public void CreateNewPort(IPort newPort)
        {
            _ports.Add(newPort);
        }
    }
}
