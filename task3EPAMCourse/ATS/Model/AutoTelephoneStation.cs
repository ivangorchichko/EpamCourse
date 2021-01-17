using System.Linq;
using task3EPAMCourse.ATS.Contracts;
using task3EPAMCourse.ATS.Enums;
using task3EPAMCourse.ATS.Service;
using task3EPAMCourse.Billing.Contracts;
using task3EPAMCourse.Billing.Model;

namespace task3EPAMCourse.ATS.Model
{
    public class AutoTelephoneStation : IAts
    {
        public IPortService PortService { get; } = new PortService();

        public ICallService CallService { get; } = new CallService();

        public ITerminalService TerminalService { get; }

        public AutoTelephoneStation()
        {
            TerminalService = new TerminalService(this);
            RegistrationAtsEvents();
        }

        private void TerminalDropping(TerminalConnectionsEventArgs connection)
        {
            CallService.RemoveFromWaitingCollection(connection);
        }

        private void TerminalStopping(TerminalConnectionsEventArgs connection)
        {
            CallService.RemoveFromJoinedCollection(connection);
        }

        private void TerminalCalling(TerminalConnectionsEventArgs connection)
        {
            CallService.AddInWaitingCollection(connection);
        }

        private void TerminalAccepting(TerminalConnectionsEventArgs connection)
        {
            this.CallService.RemoveFromWaitingCollection(connection);
            CallService.AddInJoinedCollection(connection);
        }

        private void RegistrationAtsEvents()
        {
            foreach (var terminal in TerminalService.Terminals.ToList())
            {
                terminal.Call += (sender, connections) =>
                {
                    TerminalCalling(connections);
                };
                terminal.AcceptCall += (sender, connection) =>
                {
                    TerminalAccepting(connection);
                };
                terminal.StopCall += (sender, connection) =>
                {
                    TerminalStopping(connection);
                };
                terminal.DropCall += (sender, connection) =>
                {
                    TerminalDropping(connection);
                };
                terminal.ChangePortCondition += (sender, condition) => 
                {
                    if (sender is Terminal chosenTerminal)
                    {
                        var port = chosenTerminal.Port;
                        port.ChangeCondition(condition);
                    }
                };
            }
        }

        public ICaller CreateContract(int callerNumber, IUiManager uiManager)
        {
            var terminal = TerminalService.GetAvailableTerminal();
            var port = PortService.GetFreePort();
            if (terminal != null && port != null)
            {
                ICaller caller = new Caller(callerNumber, terminal, port);
                terminal.ChangeTerminalCondition(TerminalCondition.IsUsed);
                port.ChangeCondition(PortCondition.Free);
                uiManager.GetInfoOnCreateContract(caller);
                return caller;
            }
            else
            {
                uiManager.GetInfoOnCreateContract();
                return null;
            }
        }
    }
}
