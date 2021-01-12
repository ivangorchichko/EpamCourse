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

        public event EventHandler<Connections> Call;
        public event EventHandler<Connections> AceptCall;
        public event EventHandler<Connections> StopCall;
        public event EventHandler<Connections> DropCall;
        public event EventHandler<PortCondition> OnChangePortCondition;

        public Terminal(string number, TerminalCondition condition, AutoTelephoneStation autoTelephoneStation)
        {
            Number = number;
            TerminalCondition = condition;
            _ats = autoTelephoneStation;
        }
        public void ChangeTerminalCondition(TerminalCondition condition)
        {
            TerminalCondition = condition;
        }
        public void ChangePort(IPort port)
        {
            Port = port;
        }
        public void Calling(ICaller answerer)
        {
            if (answerer.Terminal.Port.Condition == PortCondition.Free)
            {
                Call?.Invoke(this, new Connections(this, answerer.Terminal));
                Console.WriteLine($"Terminal {this.Number} calls to {answerer.CallerNumber}");
                this.Port.ChangeCondition(PortCondition.InCalling);
            }
            else Console.WriteLine($"Caller {this.Number} can not call to {answerer.CallerNumber} cause them in call orr port is off");
        }
        public void AceptCalling(ICaller caller)
        {
            Connections connection = _ats.CallService.InWaitingConnectionCollection
                .Where(x => x.Answer == this && x.Caller == caller.Terminal)
                .Select(x => x).FirstOrDefault();
            if (connection != null)
            {
                Console.WriteLine($"Terminal {this.Number} acept call with {caller.CallerNumber}");
                AceptCall?.Invoke(this, connection);
                this.Port.ChangeCondition(PortCondition.Calling);
                caller.Terminal.Port.ChangeCondition(PortCondition.Calling);
            }
            else
            {
                Console.WriteLine($"Terminal {this.Number} can not acept calling {caller.CallerNumber} cause them dont call");
            }
        }
        public void StopCalling(ICaller secondCaller)
        {
            Connections connectionWherStopFirstCaller = _ats.CallService.InJoinedConnectionCollection
                .Where(x => x.Answer == this && x.Caller == secondCaller.Terminal)
                .Select(x => x).FirstOrDefault();
            Connections connectionWherStopSecondCaller = _ats.CallService.InJoinedConnectionCollection
                .Where(x => x.Answer == secondCaller.Terminal && x.Caller == this)
                .Select(x => x).FirstOrDefault();
            if (connectionWherStopFirstCaller != null)
            {
                Console.WriteLine($"Terminal {this.Number} stop calling with {secondCaller.CallerNumber}");
                this.Port.ChangeCondition(PortCondition.Free);
                secondCaller.Terminal.Port.ChangeCondition(PortCondition.Free);
                StopCall?.Invoke(this, connectionWherStopFirstCaller);
            }
            else
            {
                if (connectionWherStopSecondCaller != null)
                {
                    Console.WriteLine($"Caller {secondCaller.CallerNumber} stop calling with {this.Number}");
                    this.Port.ChangeCondition(PortCondition.Free);
                    secondCaller.Terminal.Port.ChangeCondition(PortCondition.Free);
                    StopCall?.Invoke(this, connectionWherStopSecondCaller);
                } 
                else
                {
                    Console.WriteLine($"Terminal {this.Number} not in calling with {secondCaller.CallerNumber}");
                }

            }
        }
        public void DropCalling(ICaller caller)
        {
            Connections connection = _ats.CallService.InWaitingConnectionCollection
                .Where(x => x.Answer == this && x.Caller == caller.Terminal)
                .Select(x => x).FirstOrDefault();
            if (connection != null)
            {
                Console.WriteLine($"Terminal {this.Number} drop calling with {caller.CallerNumber}");
                caller.Terminal.Port.ChangeCondition(PortCondition.Free);
                DropCall?.Invoke(this, connection);
            }
            else
            {
                Console.WriteLine($"Terminal {this.Number} can not drop calling {caller.CallerNumber} cause caller dont call");
            }
        }
    }
}
