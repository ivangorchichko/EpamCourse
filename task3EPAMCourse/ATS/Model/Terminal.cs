using System;
using task3EPAMCourse.ATS.Contracts;
using task3EPAMCourse.ATS.Enums;

namespace task3EPAMCourse.ATS.Model
{
    public class Terminal : ITerminal
    {
        private static readonly IATS _ats = new AutoTelephoneStation();
        public string Number { get; }
        public IPort Port { get; private set; }
        public TerminalCondition TerminalCondition { get; private set; }

        public event EventHandler<TerminalCondition> TerminalConditionChange;
        public event EventHandler<IPort> ChangePort;
        public event EventHandler<ICaller> Call;
        public event EventHandler<ICaller> AceptCall;
        public event EventHandler<ICaller> StopCall;
        public event EventHandler<ICaller> DropCall;

        public Terminal(string number, TerminalCondition condition)
        {
            Number = number;
            TerminalCondition = condition;
            RegistrTerminalEventHandler();
        }
        private void OnCall(object sender, ICaller answerer)
        {
            Call?.Invoke(sender, answerer);
        }
        private void OnAceptCall(object sender, ICaller caller)
        {
            AceptCall?.Invoke(sender, caller);
        }
        private void OnStopCall(object sender, ICaller secondCaller)
        {
            StopCall?.Invoke(sender, secondCaller);
        }
        private void OnDropCall(object sender, ICaller caller)
        {
            DropCall?.Invoke(sender, caller);
        }
        private void OnChangeCondition(object sender, TerminalCondition condition)
        {
            TerminalConditionChange?.Invoke(sender, condition);
        }
        private void OnChangePortState(object sender, IPort port)
        {
            ChangePort?.Invoke(sender, port);
        }

        private void RegistrTerminalEventHandler()
        {
            TerminalConditionChange += (sender, e) =>
            {
                TerminalCondition = e;
            };

            ChangePort += (sender, e) =>
            {
                Port = e;
            };

            Call += (sender, answerer) =>
            {
                var terminal = sender as Terminal;
                Console.WriteLine($"Terminal {terminal.Number} calls to {answerer.CallerNumber}");
                _ats.Calling(new Connections(terminal, answerer.Terminal));
                terminal.Port.ChangeCondition(PortCondition.InCalling);
            };

            AceptCall += (sender, caller) =>
            {
                var answerer = sender as Terminal;
                foreach (var connection in _ats.CallService.InWaitingConnectionCollection)
                {
                    if (connection.Answer == answerer && connection.Caller == caller.Terminal)
                    {
                        Console.WriteLine($"Terminal {answerer.Number} acept call with {caller.CallerNumber}");
                        _ats.Acepting(new Connections(caller.Terminal, answerer));
                        answerer.Port.ChangeCondition(PortCondition.Calling);
                        caller.Terminal.Port.ChangeCondition(PortCondition.Calling);
                        _ats.CallService.InWaitingConnectionCollection.Remove(connection);
                        break;
                    }
                }
            };
            StopCall += (sender, secondCaller) =>
            {
                var fisrtCaller = sender as Terminal;
                foreach (var connection in _ats.CallService.InJoinedConnectionCollection)
                {
                    if (connection.Answer == fisrtCaller && connection.Caller == secondCaller.Terminal ||
                        connection.Caller == fisrtCaller && connection.Answer == secondCaller.Terminal
                    )
                    {
                        Console.WriteLine($"Terminal {fisrtCaller.Number} stop calling with {secondCaller.CallerNumber}");
                        fisrtCaller.Port.ChangeCondition(PortCondition.Free);
                        secondCaller.Terminal.Port.ChangeCondition(PortCondition.Free);
                        _ats.CallService.InJoinedConnectionCollection.Remove(connection);
                        break;
                    }
                }
            };
            DropCall += (sender, caller) => 
            {
                var answerer = sender as Terminal;
                foreach (var connection in _ats.CallService.InWaitingConnectionCollection)
                {
                    if (connection.Answer == answerer && connection.Caller == caller.Terminal)
                    {
                        Console.WriteLine($"Terminal {answerer.Number} drop calling with {caller.CallerNumber}");
                        caller.Terminal.Port.ChangeCondition(PortCondition.Free);
                        _ats.CallService.InWaitingConnectionCollection.Remove(connection);
                        break;
                    }
                }
            };
        }
        public void ChangeTerminalCondition(TerminalCondition condition)
        {
            OnChangeCondition(this, condition);
        }
        public void ChangePortSourse(IPort port)
        {
            OnChangePortState(this, port);
        }
        public void Calling(ICaller answerer)
        {
            OnCall(this, answerer);
        }
        public void AceptCalling(ICaller caller)
        {
            OnAceptCall(this, caller);
        }
        public void StopCalling(ICaller secondCaller)
        {
            OnStopCall(this, secondCaller);
        }
        public void DropCalling(ICaller caller)
        {
            OnDropCall(this, caller);
        }
    }
}
