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

        public AutoTelephoneStation()
        {
            TerminalService = new TerminalService(this);
            RegistrATSEvents();
        }

        private void TerminalDroping(TerminalConnectionsEventArgs connection)
        {
            CallService.RemoveFromWaitingCollection(connection);
        }

        private void TerminalStoping(TerminalConnectionsEventArgs connection)
        {
            CallService.RemoveFromJoinedCollection(connection);
        }

        private void TerminalCalling(TerminalConnectionsEventArgs connection)
        {
            CallService.AddInWaitingCollection(connection);
        }

        private void TerminalAcepting(TerminalConnectionsEventArgs connection)
        {
            this.CallService.RemoveFromWaitingCollection(connection);
            CallService.AddInJoinedCollection(connection);
        }

        private void RegistrATSEvents()
        {
            foreach (var terminal in TerminalService.Terminals.ToList())
            {
                terminal.Call += (sender, connections) =>
                {
                    TerminalCalling(connections);
                };
                terminal.AceptCall += (sender, connection) =>
                {
                    TerminalAcepting(connection);
                };
                terminal.StopCall += (sender, connection) =>
                {
                    TerminalStoping(connection);
                };
                terminal.DropCall += (sender, connection) =>
                {
                    TerminalDroping(connection);
                };
                terminal.ChangePortCondition += (sender, condition) => 
                {
                    var terminal = sender as Terminal;
                    var port = terminal.Port;
                    port.ChangeCondition(condition);
                };
            }
        }

        public ICaller CreateContract(int callerNumber, IUIManager uiManager)
        {
            var terminal = TerminalService.GetAvaibleTerminal();
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
