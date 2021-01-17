using System.Linq;
using Task3EPAMCourse.ATS.Contracts;
using Task3EPAMCourse.ATS.Enums;
using Task3EPAMCourse.ATS.Service;
using Task3EPAMCourse.Billing.Contracts;
using Task3EPAMCourse.Billing.Model;

namespace Task3EPAMCourse.ATS.Model
{
    public class AutoTelephoneStation : IAts
    {
        public AutoTelephoneStation()
        {
            TerminalService = new TerminalService(this);
            SubscribeAtsEvents();
        }

        public IPortService PortService { get; } = new PortService();

        public ICallConnections CallConnections { get; } = new CallConnections();

        public ITerminalService TerminalService { get; }

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

        private void TerminalDropping(TerminalConnections connection)
        {
            CallConnections.RemoveFromWaitingCollection(connection);
        }

        private void TerminalStopping(TerminalConnections connection)
        {
            CallConnections.RemoveFromJoinedCollection(connection);
        }

        private void TerminalCalling(TerminalConnections connection)
        {
            CallConnections.AddInWaitingCollection(connection);
        }

        private void TerminalAccepting(TerminalConnections connection)
        {
            CallConnections.RemoveFromWaitingCollection(connection);
            CallConnections.AddInJoinedCollection(connection);
        }

        private void SubscribeAtsEvents()
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
    }
}
