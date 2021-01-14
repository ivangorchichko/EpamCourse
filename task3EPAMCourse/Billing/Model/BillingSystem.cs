using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using task3EPAMCourse.ATS.Contracts;
using task3EPAMCourse.ATS.Model;
using task3EPAMCourse.Billing.Contracts;

namespace task3EPAMCourse.Billing.Model
{
    public class BillingSystem : IBilling
    {
        private IATS _ats;
        private IList<CallInfo> CallsInfo = new List<CallInfo>();

        private CallInfo _callInfo;
        public BillingSystem(IATS ATS)
        {
            _ats = ATS;
            RegistrEvents();
        }

        private void RegistrEvents()
        {
            foreach (var terminal in _ats.TerminalService.Terminals.ToList())
            {
                terminal.Call += (sender, connection) =>
                {
                    StartConnecting(connection);
                };
                terminal.StopCall += (sender, connection) =>
                {
                    StopConnecting(connection);
                };
                terminal.DropCall += (sender, connection) =>
                {
                    DropConnection(connection);
                };
            }
        }

        private void StartConnecting(TerminalConnectionsEventArgs connection)
        {
            var callInfo = CallsInfo.Where(x => x.From == connection.Caller && x.To == connection.Answer)
                .Select(x => x).FirstOrDefault();
            if (callInfo == null)
            {
                _callInfo = new CallInfo();
                _callInfo.From = connection.Caller;
                _callInfo.To = connection.Answer;
                _callInfo.DateTimeStart = DateTime.Now;
                CallsInfo.Add(_callInfo);
            }
        }

        private void DropConnection(TerminalConnectionsEventArgs connection)
        {
            var callInfo = CallsInfo.Where(x => x.From == connection.Caller && x.To == connection.Answer)
                .Select(x => x).FirstOrDefault();
            callInfo.Duration = TimeSpan.Zero;
            callInfo.Cost = 0.0;
        }

        private void StopConnecting(TerminalConnectionsEventArgs connection)
        {
            var callInfo = CallsInfo.Where(x => x.From == connection.Caller && x.To == connection.Answer)
                .Select(x => x).FirstOrDefault();
            callInfo.Duration = DateTime.Now - _callInfo.DateTimeStart;
            callInfo.Cost = _callInfo.Duration.TotalSeconds * 0.2;
        }

        public IList<CallInfo> GetCalls()
        {
            return CallsInfo;
        }
    }
}
