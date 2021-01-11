using System;
using task3EPAMCourse.ATS.Contracts;
using task3EPAMCourse.ATS.Enums;

namespace task3EPAMCourse.ATS.Model
{
    public class Terminal : ITerminal
    {
        private static IATS _ats = new AutoTelephoneStation();
        public string Number { get; }
        public IPort Port { get; private set; }
        public TerminalCondition TerminalCondition { get; private set; }

        //public event EventHandler<TerminalCondition> TerminalConditionChange;
        //public event EventHandler<IPort> ChangePort;
        public event EventHandler<Connections> Call;
        public event EventHandler<Connections> AceptCall;
        public event EventHandler<Connections> StopCall;
        public event EventHandler<Connections> DropCall;

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
            Call?.Invoke(this, new Connections(this, answerer.Terminal));
            Console.WriteLine($"Terminal {this.Number} calls to {answerer.CallerNumber}");
            this.Port.ChangeCondition(PortCondition.InCalling);
        }
        public void AceptCalling(ICaller caller)
        {
            foreach (var connection in _ats.CallService.InWaitingConnectionCollection)
            {
                if (connection.Answer == this && connection.Caller == caller.Terminal)
                {
                    Console.WriteLine($"Terminal {this.Number} acept call with {caller.CallerNumber}");
                    AceptCall?.Invoke(this, connection);
                    this.Port.ChangeCondition(PortCondition.Calling);
                    caller.Terminal.Port.ChangeCondition(PortCondition.Calling);
                    break;
                }
            }
        }
        public void StopCalling(ICaller secondCaller)
        {
            foreach (var connection in _ats.CallService.InJoinedConnectionCollection)
            {
                if (connection.Answer == this && connection.Caller == secondCaller.Terminal)
                {
                    Console.WriteLine($"Terminal {this.Number} stop calling with {secondCaller.CallerNumber}");
                    this.Port.ChangeCondition(PortCondition.Free);
                    secondCaller.Terminal.Port.ChangeCondition(PortCondition.Free);
                    StopCall?.Invoke(this, connection);
                    break;
                }
                else
                {
                    if (connection.Caller == this && connection.Answer == secondCaller.Terminal)
                    {
                        Console.WriteLine($"Terminal {this.Number} stop calling with {secondCaller.CallerNumber}");
                        this.Port.ChangeCondition(PortCondition.Free);
                        secondCaller.Terminal.Port.ChangeCondition(PortCondition.Free);
                        StopCall?.Invoke(this, connection);
                        break;
                    }
                }
            }
        }
        public void DropCalling(ICaller caller)
        {
            foreach (var connection in _ats.CallService.InWaitingConnectionCollection)
            {
                if (connection.Answer == this && connection.Caller == caller.Terminal)
                {
                    Console.WriteLine($"Terminal {this.Number} drop calling with {caller.CallerNumber}");
                    caller.Terminal.Port.ChangeCondition(PortCondition.Free);
                    DropCall?.Invoke(this, connection);
                    _ats.CallService.InWaitingConnectionCollection.Remove(connection);
                    break;
                }
            }
        }
    }
}
