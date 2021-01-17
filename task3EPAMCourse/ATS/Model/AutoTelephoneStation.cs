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
            TerminalsService = new TerminalsService(this);
            SubscribeAtsEvents();
        }

        public IPortsService PortsService { get; } = new PortsService();

        public ICallConnections CallConnections { get; } = new CallConnections();

        public ITerminalsService TerminalsService { get; }

        public ICaller CreateContract(int callerNumber)
        {
            var terminal = TerminalsService.GetAvailableTerminal();
            var port = PortsService.GetFreePort();
            if (terminal != null && port != null)
            {
                ICaller caller = new Caller(callerNumber, terminal, port);
                terminal.ChangeTerminalCondition(TerminalCondition.IsUsed);
                port.ChangeCondition(PortCondition.Free);
                return caller;
            }
            else
            {
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
            foreach (var terminal in TerminalsService.Terminals.ToList())
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
