using System;
using task3EPAMCourse.ATS.Contracts;
using task3EPAMCourse.ATS.Enums;
using System.Linq;

namespace task3EPAMCourse.ATS.Model
{
    public class Terminal : ITerminal
    {
        private static IATS _ats = new AutoTelephoneStation();
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

        private void OnChangePortCondition(PortCondition condition)
        {
            ChangePortCondition?.Invoke(this, condition);
        }

        private void OnCall(TerminalConnectionsEventArgs connection)
        {
            Call?.Invoke(this, connection);
        }

        private void OnAceptCall(TerminalConnectionsEventArgs connection)
        {
            AceptCall?.Invoke(this, connection);
        }

        private void OnStopCall(TerminalConnectionsEventArgs connection)
        {
            StopCall?.Invoke(this, connection);
        }

        private void OnDropCall(TerminalConnectionsEventArgs connection)
        {
            DropCall?.Invoke(this, connection);
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
            OnChangePortCondition(condition);
        }

        public void Calling(ICaller answerer)
        {
            if (answerer.Terminal.Port.Condition == PortCondition.Free)
            {
                var args = new TerminalConnectionsEventArgs();
                args.Answer = answerer.Terminal;
                args.Caller = this;
                OnCall(args);
                Console.WriteLine($"Terminal {this.Number} calls to {answerer.CallerNumber}");
                this.ChangingPortCondition(PortCondition.InCalling);
            }
            else Console.WriteLine($"Caller {this.Number} can not call to {answerer.CallerNumber} cause them in call orr port is off");
        }

        public void AceptCalling(ICaller caller)
        {
            TerminalConnectionsEventArgs connection = _ats.CallService.InWaitingConnectionCollection
                .Where(x => x.Answer == this && x.Caller == caller.Terminal)
                .Select(x => x).FirstOrDefault();
            if (connection != null)
            {
                Console.WriteLine($"Terminal {this.Number} acept call with {caller.CallerNumber}");
                OnAceptCall(connection);
                this.ChangingPortCondition(PortCondition.Calling);
                caller.Terminal.ChangingPortCondition(PortCondition.Calling);
            }
            else
            {
                Console.WriteLine($"Terminal {this.Number} can not acept calling {caller.CallerNumber} cause them dont call");
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
                Console.WriteLine($"Terminal {this.Number} stop calling with {secondCaller.CallerNumber}");
                this.ChangingPortCondition(PortCondition.Free);
                secondCaller.Terminal.ChangingPortCondition(PortCondition.Free);
                OnStopCall(connectionWhereStopFirstCaller);
            }
            else
            {
                if (connectionWhereStopSecondCaller != null)
                {
                    Console.WriteLine($"Caller {secondCaller.CallerNumber} stop calling with {this.Number}");
                    this.ChangingPortCondition(PortCondition.Free);
                    secondCaller.Terminal.ChangingPortCondition(PortCondition.Free);
                    OnStopCall(connectionWhereStopSecondCaller);
                } 
                else
                {
                    Console.WriteLine($"Terminal {this.Number} not in calling with {secondCaller.CallerNumber}");
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
                Console.WriteLine($"Terminal {this.Number} drop calling with {caller.CallerNumber}");
                caller.Terminal.ChangingPortCondition(PortCondition.Free);
                OnDropCall(connection);
            }
            else
            {
                Console.WriteLine($"Terminal {this.Number} can not drop calling {caller.CallerNumber} cause caller dont call");
            }
        }
    }
}
