using System;
using System.Collections.Generic;
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

        public ITerminalService TerminalService { get; } = new TerminalService();

        public event EventHandler<Connections> Call;
        public event EventHandler<Connections> Acept;

        public AutoTelephoneStation()
        {
            RegistrATSEvents();
        }
        private void RegistrATSEvents()
        {
            Call += (sender, connection) => 
            {
                CallService.AddInWaitingCollection(connection);
            };
            Acept += (sender, connection) => 
            {
                CallService.AddInJoinedCollection(connection);
            };
        }
        public void Calling(Connections connection)
        {
            OnCall(this, connection);
        }
        public void Acepting(Connections connection)
        {
            OnAcept(this, connection);
        }
        private void OnAcept(object sender, Connections connection)
        {
            Acept?.Invoke(sender, connection);
        }
        private void OnCall(object sender, Connections connection)
        {
            Call?.Invoke(sender, connection);
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
                System.Console.WriteLine("No more terminals or ports");
                return null;
            }
        }

    }
}
