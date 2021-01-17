using System;
using Task3EPAMCourse.ATS.Contracts;
using Task3EPAMCourse.ATS.Enums;
using Task3EPAMCourse.ATS.Model;
using Task3EPAMCourse.Billing.Contracts;

namespace Task3EPAMCourse.ATS.Service
{
    public class UiManager : IUiManager
    {
        public void GetInfoOnCreateContract(ICaller caller = null)
        {
            Console.WriteLine(caller != null
                ? $"New contract is created! User {caller.CallerNumber}"
                : "No more terminals or ports, caller set as null");
        }

        public void GetInfoTerminalOperation(TerminalConnections callersConnection, TerminalOperations terminalOperations, PortCondition portCondition = PortCondition.Free)
        {
            switch (terminalOperations)
            {
                case TerminalOperations.Calling:
                {
                    Console.WriteLine(portCondition == PortCondition.Free
                        ? $"Terminal {callersConnection.Caller.Number} calls to {callersConnection.Answer.Number}"
                        : $"Terminal {callersConnection.Caller.Number} can not call to {callersConnection.Answer.Number} cause them in call orr port is off");
                    break;
                }

                case TerminalOperations.Accepting:
                {
                    Console.WriteLine(callersConnection != null
                        ? $"Terminal {callersConnection.Answer.Number} accept call with {callersConnection.Caller.Number}"
                        : $"Accepting can't happen cause terminal don't call");
                    break;
                }

                case TerminalOperations.Dropping:
                {
                    Console.WriteLine(callersConnection != null
                        ? $"Terminal {callersConnection.Answer.Number} drop calling with {callersConnection.Caller.Number}"
                        : $"Terminal can't drop calling with caller cause caller do not calling");
                    break;
                }

                case TerminalOperations.Stopping:
                {
                    Console.WriteLine(callersConnection != null
                        ? $"Terminal {callersConnection.Caller.Number} stop calling with {callersConnection.Answer.Number}"
                        : $"Terminal can't stop calling with caller cause them not in calling");
                    break;
                }
            }
        }

        public void GetInfoIfPortConditionChanged(PortCondition condition)
        {
            Console.WriteLine($"Port condition changed on {condition}");
        }
    }
}
