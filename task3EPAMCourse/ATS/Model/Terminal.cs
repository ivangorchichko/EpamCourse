using System;
using task3EPAMCourse.ATS.Contracts;
using task3EPAMCourse.ATS.Enums;
using System.Linq;
using task3EPAMCourse.ATS.Service;

namespace task3EPAMCourse.ATS.Model
{
    public class Terminal : ITerminal
    {
        private static IATS _ats = new AutoTelephoneStation();
        private static IUIManager _uIManager = new UIManager();
        public string Number { get; }
        public IPort Port { get; private set; }
        public TerminalCondition TerminalCondition { get; private set; }

        public event EventHandler<TerminalConnectionsEventArgs> Call;
        public event EventHandler<TerminalConnectionsEventArgs> AceptCall;
        public event EventHandler<TerminalConnectionsEventArgs> StopCall;
        public event EventHandler<TerminalConnectionsEventArgs> DropCall;
        public event EventHandler<PortCondition> ChangePortCondition;

        public Terminal(string number, TerminalCondition condition, AutoTelephoneStation autoTelephoneStation)
        {
            Number = number;
            TerminalCondition = condition;
            _ats = autoTelephoneStation;
        }

        private void OnCalling(TerminalConnectionsEventArgs connection)
        {
            Call?.Invoke(this, connection);
        }

        private void OnAceptCalling(TerminalConnectionsEventArgs connection)
        {
            AceptCall?.Invoke(this, connection);
        }

        private void OnCallStoped(TerminalConnectionsEventArgs connection)
        {
            StopCall?.Invoke(this, connection);
        }

        private void OnCallDroped(TerminalConnectionsEventArgs connection)
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
                var args = new TerminalConnectionsEventArgs();
                args.Answer = answerer.Terminal;
                args.Caller = this;
                OnCalling(args);
                _uIManager.GetInfoTerminalOperation(this, answerer.Terminal, TerminalOperations.Calling);
                this.ChangingPortCondition(PortCondition.InCalling);
            }
            else _uIManager.GetInfoTerminalOperation(this, answerer.Terminal, TerminalOperations.Calling, null, PortCondition.Calling);
        }

        public void AceptCalling(ICaller caller)
        {
            TerminalConnectionsEventArgs connection = _ats.CallService.InWaitingConnectionCollection
                .Where(x => x.Answer == this && x.Caller == caller.Terminal)
                .Select(x => x).FirstOrDefault();
            if (connection != null)
            {
                _uIManager.GetInfoTerminalOperation(this, caller.Terminal, TerminalOperations.Acepting, connection);
                OnAceptCalling(connection);
                this.ChangingPortCondition(PortCondition.Calling);
                caller.Terminal.ChangingPortCondition(PortCondition.Calling);
            }
            else
            {
                _uIManager.GetInfoTerminalOperation(this, caller.Terminal, TerminalOperations.Acepting);
            }
        }

        public void StopCalling(ICaller secondCaller)
        {
            TerminalConnectionsEventArgs connectionWhereStopFirstCaller = _ats.CallService.InJoinedConnectionCollection
                .Where(x => x.Answer == this && x.Caller == secondCaller.Terminal)
                .Select(x => x).FirstOrDefault();
            TerminalConnectionsEventArgs connectionWhereStopSecondCaller = _ats.CallService.InJoinedConnectionCollection
                .Where(x => x.Answer == secondCaller.Terminal && x.Caller == this)
                .Select(x => x).FirstOrDefault();
            if (connectionWhereStopFirstCaller != null)
            {
                _uIManager.GetInfoTerminalOperation(this, secondCaller.Terminal, TerminalOperations.Stoping, connectionWhereStopFirstCaller);
                this.ChangingPortCondition(PortCondition.Free);
                secondCaller.Terminal.ChangingPortCondition(PortCondition.Free);
                OnCallStoped(connectionWhereStopFirstCaller);
            }
            else
            {
                if (connectionWhereStopSecondCaller != null)
                {
                    _uIManager.GetInfoTerminalOperation(secondCaller.Terminal, this, TerminalOperations.Stoping, connectionWhereStopSecondCaller);
                    this.ChangingPortCondition(PortCondition.Free);
                    secondCaller.Terminal.ChangingPortCondition(PortCondition.Free);
                    OnCallStoped(connectionWhereStopSecondCaller);
                } 
                else
                {
                    _uIManager.GetInfoTerminalOperation(this, secondCaller.Terminal, TerminalOperations.Stoping);
                }

            }
        }

        public void DropCalling(ICaller caller)
        {
            TerminalConnectionsEventArgs connection = _ats.CallService.InWaitingConnectionCollection
                .Where(x => x.Answer == this && x.Caller == caller.Terminal)
                .Select(x => x).FirstOrDefault();
            if (connection != null)
            {
                _uIManager.GetInfoTerminalOperation(this, caller.Terminal, TerminalOperations.Droping, connection);
                caller.Terminal.ChangingPortCondition(PortCondition.Free);
                OnCallDroped(connection);
            }
            else
            {
                _uIManager.GetInfoTerminalOperation(this, caller.Terminal, TerminalOperations.Droping);
            }
        }
    }
}
