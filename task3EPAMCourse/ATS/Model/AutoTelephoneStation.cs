using System;
using System.Collections.Generic;
using System.Linq;
using task3EPAMCourse.ATS.Contracts;
using task3EPAMCourse.ATS.Enums;
using task3EPAMCourse.ATS.Service;
using task3EPAMCourse.Billing.Model;

namespace task3EPAMCourse.ATS.Model
{
    public class AutoTelephoneStation : IATS
    {
        public IPortService PortService { get; } = new PortService();

        public ICallService CallService { get; } = new CallService();

        public ITerminalService TerminalService { get; }

        //public event EventHandler<Connections> Call;
        //public event EventHandler<Connections> Acept;

        public AutoTelephoneStation()
        {
            TerminalService = new TerminalService(this);
            RegistrATSEvents();
        }
        public void RegistrATSEvents()
        {
            foreach (var terminal in TerminalService.Terminals.ToList())
            {
                terminal.Call += (sender, connections) =>
                {
                    Calling(connections);
                };
                terminal.AceptCall += (sender, connection) =>
                {
                    Acepting(connection);
                };
                terminal.StopCall += (sender, connection) =>
                {
                    Stoping(connection);
                };
                terminal.DropCall += (sender, connection) =>
                {
                    Droping(connection);
                };
                terminal.OnChangePortCondition += (sender, condition) => 
                { 
                    
                };
            }
        }
        private void ChangePortCondition(PortCondition condition)
        {
        }
        private void Droping(Connections connection)
        {
            CallService.RemoveFromWaitingCollection(connection);
        }

        private void Stoping(Connections connection)
        {
            CallService.RemoveFromJoinedCollection(connection);
        }

        private void Calling(Connections connection)
        {
            CallService.AddInWaitingCollection(connection);
        }
        private void Acepting(Connections connection)
        {
            this.CallService.RemoveFromWaitingCollection(connection);
            CallService.AddInJoinedCollection(connection);     
        }
        public ICaller CreateContract(int callerNumber)
        {
            var terminal = TerminalService.GetAvaibleTerminal();
            var port = PortService.GetFreePort();
            if (terminal != null && port != null)
            {
                ICaller caller = new Caller(callerNumber, terminal, port);
                terminal.ChangeTerminalCondition(TerminalCondition.IsUsed);
                port.ChangeCondition(PortCondition.Free);
                return caller;
            }
            else
            {
                Console.WriteLine("No more terminals or ports");
                return null;
            }
        }

    }
}
