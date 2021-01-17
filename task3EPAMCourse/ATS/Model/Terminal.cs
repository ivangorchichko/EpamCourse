using System;
using System.Linq;
using Task3EPAMCourse.ATS.Contracts;
using Task3EPAMCourse.ATS.Enums;
using Task3EPAMCourse.ATS.Service;
using Task3EPAMCourse.Billing.Contracts;

namespace Task3EPAMCourse.ATS.Model
{
    public class Terminal : ITerminal
    {
        private readonly IAts _ats;

        public Terminal(string number, TerminalCondition condition, IAts autoTelephoneStation)
        {
            Number = number;
            TerminalCondition = condition;
            _ats = autoTelephoneStation;
        }

        public event EventHandler<TerminalConnections> Call;

        public event EventHandler<TerminalConnections> AcceptCall;

        public event EventHandler<TerminalConnections> StopCall;

        public event EventHandler<TerminalConnections> DropCall;

        public event EventHandler<PortCondition> ChangePortCondition;

        public string Number { get; }

        public IPort Port { get; private set; }

        public TerminalCondition TerminalCondition { get; private set; }

        public void ChangeTerminalCondition(TerminalCondition condition)
        {
            TerminalCondition = condition;
        }

        public void ChangePort(IPort port)
        {
            Port = port;
        }

        public void ChangingPortCondition(PortCondition condition)
        {
            OnChangingPortCondition(condition);
        }

        public TerminalConnections Calling(ICaller answerer)
        {
            if (answerer.Terminal.Port.Condition == PortCondition.Free)
            {
                var connection = new TerminalConnections
                {
                    Answer = answerer.Terminal,
                    Caller = this,
                };
                OnCalling(connection);
                ChangingPortCondition(PortCondition.InCalling);
                return connection;
            }

            return new TerminalConnections() { Caller = this, Answer = answerer.Terminal };
        }

        public TerminalConnections AcceptCalling(ICaller caller)
        {
            var connection = _ats.CallConnections.InWaitingConnectionCollection
                .Where(x => x.Answer == this && x.Caller == caller.Terminal)
                .Select(x => x).FirstOrDefault();
            if (connection != null)
            {
                OnAcceptCalling(connection);
                ChangingPortCondition(PortCondition.Calling);
                caller.Terminal.ChangingPortCondition(PortCondition.Calling);
                return connection;
            }
            else
            {
                return null;
            }
        }

        public TerminalConnections StopCalling(ICaller secondCaller)
        {
            var connectionWhereStopFirstCaller = _ats.CallConnections.InJoinedConnectionCollection
                .Where(x => x.Answer == secondCaller.Terminal && x.Caller == this)
                .Select(x => x).FirstOrDefault();
            if (connectionWhereStopFirstCaller != null)
            {
                ChangingPortCondition(PortCondition.Free);
                secondCaller.Terminal.ChangingPortCondition(PortCondition.Free);
                OnCallStopped(connectionWhereStopFirstCaller);
                return connectionWhereStopFirstCaller;
            }
            else
            {
                var connectionWhereStopSecondCaller = _ats.CallConnections.InJoinedConnectionCollection
                    .Where(x => x.Answer == this && x.Caller == secondCaller.Terminal)
                    .Select(x => x).FirstOrDefault();
                if (connectionWhereStopSecondCaller != null)
                {
                    ChangingPortCondition(PortCondition.Free);
                    secondCaller.Terminal.ChangingPortCondition(PortCondition.Free);
                    OnCallStopped(connectionWhereStopSecondCaller);
                    return new TerminalConnections() { Caller = connectionWhereStopSecondCaller.Answer, Answer = connectionWhereStopSecondCaller.Caller };
                }
                else
                {
                    return null;
                }
            }
        }

        public TerminalConnections DropCalling(ICaller caller)
        {
            var connection = _ats.CallConnections.InWaitingConnectionCollection
                .Where(x => x.Answer == this && x.Caller == caller.Terminal)
                .Select(x => x).FirstOrDefault();
            if (connection != null)
            {
                caller.Terminal.ChangingPortCondition(PortCondition.Free);
                OnCallDropped(connection);
                return connection;
            }
            else
            {
                return null;
            }
        }

        public void UnSubscribeEvents()
        {
            Call = null;
            AcceptCall = null;
            StopCall = null;
            DropCall = null;
            ChangePortCondition = null;
        }

        private void OnCalling(TerminalConnections connection)
        {
            Call?.Invoke(this, connection);
        }

        private void OnAcceptCalling(TerminalConnections connection)
        {
            AcceptCall?.Invoke(this, connection);
        }

        private void OnCallStopped(TerminalConnections connection)
        {
            StopCall?.Invoke(this, connection);
        }

        private void OnCallDropped(TerminalConnections connection)
        {
            DropCall?.Invoke(this, connection);
        }

        private void OnChangingPortCondition(PortCondition condition)
        {
            ChangePortCondition?.Invoke(this, condition);
        }
    }
}
