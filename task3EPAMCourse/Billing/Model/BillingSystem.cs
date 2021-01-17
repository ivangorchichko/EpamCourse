using System;
using System.Collections.Generic;
using System.Linq;
using Task3EPAMCourse.ATS.Contracts;
using Task3EPAMCourse.ATS.Model;
using Task3EPAMCourse.Billing.Contracts;
using Task3EPAMCourse.Billing.Enums;
using Task3EPAMCourse.Billing.FileService;

namespace Task3EPAMCourse.Billing.Model
{
    public class BillingSystem : IBilling
    {
        private readonly IAts _ats;
        private readonly JsonRepository _jsonRepository = new JsonRepository();
        private readonly IList<CallInfo> _callsInfoCollection = new List<CallInfo>();
        private CallInfo _callInfo;

        public BillingSystem(IAts ats)
        {
            _ats = ats;
            RegistrationEvents();
        }

        public IEnumerable<CallInfo> GetCalls()
        {
            return SaveCallInfoCollection()
                .Where(x => x.DateTimeStart.Date >= DateTime.Now.AddMonths(-1).Date);
        }

        public IEnumerable<CallInfo> GetUserCallsOrderedBy(ICaller caller, OrderSequenceType orderType)
        {
            var callsInfoCollection = GetCalls();
            switch (orderType)
            {
                case OrderSequenceType.None:
                {
                    return callsInfoCollection
                        .Where(x => x.User.Number == caller.Terminal.Number);
                }

                case OrderSequenceType.Duration:
                {
                    return callsInfoCollection
                        .Where(x => x.User.Number == caller.Terminal.Number)
                        .OrderBy(x => x.Duration);
                }

                case OrderSequenceType.Cost:
                {
                    return callsInfoCollection
                        .Where(x => x.User.Number == caller.Terminal.Number)
                        .OrderBy(x => x.Cost);
                }

                case OrderSequenceType.Callers:
                {
                    return callsInfoCollection
                        .Where(x => x.User.Number == caller.Terminal.Number)
                        .OrderBy(x => x.To.Number);
                }

                default:
                {
                    return callsInfoCollection;
                }
            }
        }

        private void RegistrationEvents()
        {
            foreach (var terminal in _ats.TerminalsService.Terminals.ToList())
            {
                terminal.Call += (sender, connection) => { StartConnecting(connection); };
                terminal.StopCall += (sender, connection) => { StopConnecting(connection); };
                terminal.DropCall += (sender, connection) => { DropConnection(connection); };
            }
        }

        private void StartConnecting(TerminalConnections connection)
        {
            var callInfo = _callsInfoCollection.Where(x => x.From == connection.Caller && x.To == connection.Answer)
                .Select(x => x).FirstOrDefault();
            if (callInfo == null || callInfo.Cost != 0.0)
            {
                _callInfo = new CallInfo
                {
                    User = connection.Caller,
                    From = connection.Caller,
                    To = connection.Answer,
                    DateTimeStart = DateTime.Now,
                    CallType = CallType.Incoming,
                };
                _callsInfoCollection.Add(_callInfo);
            }
        }

        private void DropConnection(TerminalConnections connection)
        {
            var callInfo = _callsInfoCollection.Where(x => x.From == connection.Caller && x.To == connection.Answer)
                .Select(x => x).Last();
            callInfo.Duration = TimeSpan.Zero;
            callInfo.Cost = 0.0;
            var secondSideCallInfo = new CallInfo(callInfo)
            {
                User = callInfo.To,
                CallType = CallType.Skipped,
            };
            _callsInfoCollection.Add(secondSideCallInfo);
        }

        private void StopConnecting(TerminalConnections connection)
        {
            var callInfo = _callsInfoCollection.Where(x => x.From == connection.Caller && x.To == connection.Answer)
                .Select(x => x).Last();
            callInfo.Duration = DateTime.Now - _callInfo.DateTimeStart;
            callInfo.Cost = _callInfo.Duration.TotalSeconds * Contract.Rate;
            var secondSideCallInfo = new CallInfo(callInfo)
            {
                User = callInfo.To,
                CallType = CallType.Outgoing,
            };
            _callsInfoCollection.Add(secondSideCallInfo);
        }

        private IEnumerable<CallInfo> SaveCallInfoCollection()
        {
            var currentCallInfo = _jsonRepository.GetCurrentCallInfo();
            if (currentCallInfo == null)
            {
                _jsonRepository.SaveFile(_callsInfoCollection);
                _jsonRepository.IsSequenceSavedOnce = true;
                return _callsInfoCollection;
            }
            else if (!_jsonRepository.IsSequenceSavedOnce)
            {
                var callsInfoCollection = _callsInfoCollection.Union(currentCallInfo).ToList();
                _jsonRepository.SaveFile(callsInfoCollection);
                _jsonRepository.IsSequenceSavedOnce = true;
                return callsInfoCollection;
            }
            else
            {
                return currentCallInfo;
            }
        }
    }
}
