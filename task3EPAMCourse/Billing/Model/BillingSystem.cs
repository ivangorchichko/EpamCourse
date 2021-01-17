using System;
using System.Collections.Generic;
using System.Linq;
using task3EPAMCourse.ATS.Contracts;
using task3EPAMCourse.ATS.Model;
using task3EPAMCourse.Billing.Contracts;
using task3EPAMCourse.Billing.Enums;
using task3EPAMCourse.Billing.JsonService;

namespace task3EPAMCourse.Billing.Model
{
    public class BillingSystem : IBilling
    {
        private IATS _ats;
        private static JsonFileService _json = new JsonFileService();
        private IList<CallInfo> _callsInfoCollection = new List<CallInfo>();
        private Contract _contract = new Contract();
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
            var callInfo = _callsInfoCollection.Where(x => x.From == connection.Caller && x.To == connection.Answer)
                .Select(x => x).FirstOrDefault();
            if (callInfo == null || callInfo.Cost != 0.0)
            {
                _callInfo = new CallInfo();
                _callInfo.User = connection.Caller;
                _callInfo.From = connection.Caller;
                _callInfo.To = connection.Answer;
                _callInfo.DateTimeStart = DateTime.Now;
                _callInfo.CallType = CallType.Incoming;
                _callsInfoCollection.Add(_callInfo);
            }
        }

        private void DropConnection(TerminalConnectionsEventArgs connection)
        {
            var callInfo = _callsInfoCollection.Where(x => x.From == connection.Caller && x.To == connection.Answer)
                .Select(x => x).Last();
            callInfo.Duration = TimeSpan.Zero;
            callInfo.Cost = 0.0;
            var secondSideCallInfo = new CallInfo(callInfo);
            secondSideCallInfo.User = callInfo.To;
            secondSideCallInfo.CallType = CallType.Skipped;
            _callsInfoCollection.Add(secondSideCallInfo);
        }

        private void StopConnecting(TerminalConnectionsEventArgs connection)
        {
            var callInfo = _callsInfoCollection.Where(x => x.From == connection.Caller && x.To == connection.Answer)
                .Select(x => x).Last();
            callInfo.Duration = DateTime.Now - _callInfo.DateTimeStart;
            callInfo.Cost = _callInfo.Duration.TotalSeconds * _contract.Rate;
            var secondSideCallInfo = new CallInfo(callInfo);
            secondSideCallInfo.User = callInfo.To;
            secondSideCallInfo.CallType = CallType.Outgoing;
            _callsInfoCollection.Add(secondSideCallInfo);
        }

        private IEnumerable<CallInfo> SaveCallInfoCallection()
        {
            if (_json.GetCurrentCallInfo() == null)
            {
                _json.SaveFile(_callsInfoCollection);
                _json.IsSaved = true;
                return _callsInfoCollection;
            }
            else
            if (_json.IsSaved != true)
            {
                var callsInfoCollection = _callsInfoCollection.Union(_json.GetCurrentCallInfo()).ToList();
                _json.SaveFile(callsInfoCollection);
                _json.IsSaved = true;
                return callsInfoCollection;
            }
            else return _json.GetCurrentCallInfo();

        }

        public IEnumerable<CallInfo> GetCalls()
        {
            return SaveCallInfoCallection()
                .Where(x => x.DateTimeStart.Date >= DateTime.Now.AddMonths(-1).Date);
        }

        public IEnumerable<CallInfo> GetUserCallsOrderedBy(ICaller caller, OrderSequenceType orderType)
        {
            var callsInfoCollection = SaveCallInfoCallection()
               .Where(x => x.DateTimeStart.Date >= DateTime.Now.AddMonths(-1).Date);
            switch (orderType)
            {
                case OrderSequenceType.None:
                    {
                        callsInfoCollection = callsInfoCollection
                        .Where(x => x.User.Number == caller.Terminal.Number)
                        .Select(x => x);
                        break;
                    }
                case OrderSequenceType.Duration:
                    {
                        callsInfoCollection = callsInfoCollection
                        .Where(x => x.User.Number == caller.Terminal.Number)
                        .Select(x => x)
                        .OrderBy(x => x.Duration);
                        break;
                    }
                case OrderSequenceType.Cost:
                    {
                        callsInfoCollection = callsInfoCollection
                        .Where(x => x.User.Number == caller.Terminal.Number)
                        .Select(x => x)
                        .OrderBy(x => x.Cost);
                        break;
                    }
                case OrderSequenceType.Callers:
                    {
                        callsInfoCollection = callsInfoCollection
                        .Where(x => x.User.Number == caller.Terminal.Number)
                        .Select(x => x)
                        .OrderBy(x => x.To.Number);
                        break;
                    }
            }
            return callsInfoCollection;
        }
    }
}
