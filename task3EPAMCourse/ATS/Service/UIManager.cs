using System;
using task3EPAMCourse.ATS.Contracts;
using task3EPAMCourse.ATS.Enums;
using task3EPAMCourse.ATS.Model;
using task3EPAMCourse.Billing.Contracts;

namespace task3EPAMCourse.ATS.Service
{
    public class UiManager : IUiManager
    {
        public void GetInfoOnCreateContract(ICaller caller = null)
        {
            Console.WriteLine(caller != null
                ? $"New contract is created! User {caller.CallerNumber}"
                : "No more terminals or ports, caller set as null");
        }

        public void GetInfoTerminalOperation(ITerminal firstCaller, ITerminal secondCaller, TerminalOperations terminalOperations, TerminalConnectionsEventArgs connection = null, PortCondition portCondition = PortCondition.Free)
        {
            switch (terminalOperations)
            {
                case TerminalOperations.Calling:
                {
                    Console.WriteLine(portCondition == PortCondition.Free
                        ? $"Terminal {firstCaller.Number} calls to {secondCaller.Number}"
                        : $"Terminal {firstCaller.Number} can not call to {secondCaller.Number} cause them in call orr port is off");
                    break;
                }
                case TerminalOperations.Accepting:
                {
                    Console.WriteLine(connection != null
                        ? $"Terminal {firstCaller.Number} accept call with {secondCaller.Number}"
                        : $"Terminal {firstCaller.Number} can not accept calling {secondCaller.Number} cause them do not call");
                    break;
                }
                case TerminalOperations.Dropping:
                {
                    Console.WriteLine(connection != null
                        ? $"Terminal {firstCaller.Number} drop calling with {secondCaller.Number}"
                        : $"Terminal {firstCaller.Number} can not drop calling {secondCaller.Number} cause caller do not call");
                    break;
                }
                case TerminalOperations.Stopping:
                {
                    Console.WriteLine(connection != null
                        ? $"Terminal {firstCaller.Number} stop calling with {secondCaller.Number}"
                        : $"Terminal {firstCaller.Number} not in calling with {secondCaller.Number}");
                    break;
                }
            }
        }
    }
}
