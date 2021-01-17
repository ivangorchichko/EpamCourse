using System;
using task3EPAMCourse.ATS.Contracts;
using task3EPAMCourse.ATS.Enums;
using System.Linq;
using task3EPAMCourse.ATS.Service;
using task3EPAMCourse.Billing.Contracts;

namespace task3EPAMCourse.ATS.Model
{
    public class Terminal : ITerminal
    {
        private readonly IAts _ats;
        private readonly IUiManager _uIManager = new UiManager();

        public string Number { get; }

        public IPort Port { get; private set; }

        public TerminalCondition TerminalCondition { get; private set; }

        public event EventHandler<TerminalConnectionsEventArgs> Call;
        public event EventHandler<TerminalConnectionsEventArgs> AcceptCall;
        public event EventHandler<TerminalConnectionsEventArgs> StopCall;
        public event EventHandler<TerminalConnectionsEventArgs> DropCall;
        public event EventHandler<PortCondition> ChangePortCondition;

        public Terminal(string number, TerminalCondition condition, IAts autoTelephoneStation)
        {
            Number = number;
            TerminalCondition = condition;
            _ats = autoTelephoneStation;
        }

        private void OnCalling(TerminalConnectionsEventArgs connection)
        {
            Call?.Invoke(this, connection);
        }

        private void OnAcceptCalling(TerminalConnectionsEventArgs connection)
        {
            AcceptCall?.Invoke(this, connection);
        }

        private void OnCallStopped(TerminalConnectionsEventArgs connection)
        {
            StopCall?.Invoke(this, connection);
        }

        private void OnCallDropped(TerminalConnectionsEventArgs connection)
        {
            DropCall?.Invoke(this, connection);
        }

        private void OnChangingPortCondition(PortCondition condition)
        {
            ChangePortCondition?.Invoke(this, condition);
        }

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

        public void Calling(ICaller answerer)
        {
            if (answerer.Terminal.Port.Condition == PortCondition.Free)
            {
                var args = new TerminalConnectionsEventArgs
                {
                    Answer = answerer.Terminal,
                    Caller = this
                };
                OnCalling(args);
                _uIManager.GetInfoTerminalOperation(this, answerer.Terminal, TerminalOperations.Calling);
                this.ChangingPortCondition(PortCondition.InCalling);
            }
            else _uIManager.GetInfoTerminalOperation(this, answerer.Terminal, TerminalOperations.Calling, null, PortCondition.Calling);
        }

        public void AcceptCalling(ICaller caller)
        {
            var connection = _ats.CallService.InWaitingConnectionCollection
                .Where(x => x.Answer == this && x.Caller == caller.Terminal)
                .Select(x => x).FirstOrDefault();
            if (connection != null)
            {
                _uIManager.GetInfoTerminalOperation(this, caller.Terminal, TerminalOperations.Accepting, connection);
                OnAcceptCalling(connection);
                this.ChangingPortCondition(PortCondition.Calling);
                caller.Terminal.ChangingPortCondition(PortCondition.Calling);
            }
            else
            {
                _uIManager.GetInfoTerminalOperation(this, caller.Terminal, TerminalOperations.Accepting);
            }
        }

        public void StopCalling(ICaller secondCaller)
        {
            var connectionWhereStopFirstCaller = _ats.CallService.InJoinedConnectionCollection
                .Where(x => x.Answer == this && x.Caller == secondCaller.Terminal)
                .Select(x => x).FirstOrDefault();
            var connectionWhereStopSecondCaller = _ats.CallService.InJoinedConnectionCollection
                .Where(x => x.Answer == secondCaller.Terminal && x.Caller == this)
                .Select(x => x).FirstOrDefault();
            if (connectionWhereStopFirstCaller != null)
            {
                _uIManager.GetInfoTerminalOperation(this, secondCaller.Terminal, TerminalOperations.Stopping, connectionWhereStopFirstCaller);
                this.ChangingPortCondition(PortCondition.Free);
                secondCaller.Terminal.ChangingPortCondition(PortCondition.Free);
                OnCallStopped(connectionWhereStopFirstCaller);
            }
            else
            {
                if (connectionWhereStopSecondCaller != null)
                {
                    _uIManager.GetInfoTerminalOperation(secondCaller.Terminal, this, TerminalOperations.Stopping, connectionWhereStopSecondCaller);
                    this.ChangingPortCondition(PortCondition.Free);
                    secondCaller.Terminal.ChangingPortCondition(PortCondition.Free);
                    OnCallStopped(connectionWhereStopSecondCaller);
                }
                else
                {
                    _uIManager.GetInfoTerminalOperation(this, secondCaller.Terminal, TerminalOperations.Stopping);
                }
            }
        }

        public void DropCalling(ICaller caller)
        {
            var connection = _ats.CallService.InWaitingConnectionCollection
                .Where(x => x.Answer == this && x.Caller == caller.Terminal)
                .Select(x => x).FirstOrDefault();
            if (connection != null)
            {
                _uIManager.GetInfoTerminalOperation(this, caller.Terminal, TerminalOperations.Dropping, connection);
                caller.Terminal.ChangingPortCondition(PortCondition.Free);
                OnCallDropped(connection);
            }
            else
            {
                _uIManager.GetInfoTerminalOperation(this, caller.Terminal, TerminalOperations.Dropping);
            }
        }

        public void UnSubcribeEvents()
        {
            Call = null;
            AcceptCall = null;
            StopCall = null;
            DropCall = null;
            ChangePortCondition = null;
        }
    }
}
