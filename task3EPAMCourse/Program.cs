using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Task3EPAMCourse.ATS.Contracts;
using Task3EPAMCourse.ATS.Enums;
using Task3EPAMCourse.ATS.Model;
using Task3EPAMCourse.ATS.Service;
using Task3EPAMCourse.Billing.Contracts;
using Task3EPAMCourse.Billing.Enums;
using Task3EPAMCourse.Billing.Model;

namespace Task3EPAMCourse
{
    internal static class Program
    {
        private static readonly IAts ATs = new AutoTelephoneStation();
        private static readonly IUiManager UiManager = new UiManager();
        private static readonly IBilling BillingSystem = new BillingSystem(ATs);
        private static IList<ICaller> _callers = new List<ICaller>();

        private static void Main()
        {
            CreateContracts();
            CreateConnections();
            UnSubscribeTerminalEvents();
            ShowCallerInfoCollection();
        }

        private static void CreateContracts()
        {
            _callers.Add(ATs.CreateContract(1, UiManager));
            _callers.Add(ATs.CreateContract(2, UiManager));
            _callers.Add(ATs.CreateContract(3, UiManager));
            _callers.Add(ATs.CreateContract(4, UiManager));
            ATs.TerminalService.CreateNewTerminal(new Terminal("005", TerminalCondition.Available, ATs));
            ATs.PortService.CreateNewPort(new Port(3305, PortCondition.Off));
            _callers.Add(ATs.CreateContract(5, UiManager));
            Console.WriteLine();
        }

        private static void CreateConnections()
        {
            _callers = _callers.Where(caller => caller != null).ToList();
            UiManager.GetInfoTerminalOperation(_callers[0].Terminal.Calling(_callers[1]), TerminalOperations.Calling, _callers[1].Terminal.Port.Condition);
            UiManager.GetInfoTerminalOperation(_callers[1].Terminal.AcceptCalling(_callers[0]), TerminalOperations.Accepting);
            Thread.Sleep(5000);
            UiManager.GetInfoTerminalOperation(_callers[1].Terminal.StopCalling(_callers[0]), TerminalOperations.Stopping);
            Console.WriteLine();

            UiManager.GetInfoTerminalOperation(_callers[3].Terminal.Calling(_callers[2]), TerminalOperations.Calling, _callers[2].Terminal.Port.Condition);
            Thread.Sleep(6000);
            UiManager.GetInfoTerminalOperation(_callers[2].Terminal.DropCalling(_callers[3]), TerminalOperations.Dropping);
            Console.WriteLine();

            UiManager.GetInfoTerminalOperation(_callers[2].Terminal.StopCalling(_callers[0]), TerminalOperations.Stopping);
            UiManager.GetInfoTerminalOperation(_callers[1].Terminal.DropCalling(_callers[0]), TerminalOperations.Dropping);
            UiManager.GetInfoTerminalOperation(_callers[1].Terminal.AcceptCalling(_callers[2]), TerminalOperations.Accepting);
            Console.WriteLine();

            UiManager.GetInfoTerminalOperation(_callers[1].Terminal.Calling(_callers[3]), TerminalOperations.Calling, _callers[3].Terminal.Port.Condition);
            UiManager.GetInfoTerminalOperation(_callers[3].Terminal.AcceptCalling(_callers[1]), TerminalOperations.Accepting);
            Thread.Sleep(4000);
            UiManager.GetInfoTerminalOperation(_callers[1].Terminal.StopCalling(_callers[3]), TerminalOperations.Stopping);
            Console.WriteLine();

            UiManager.GetInfoTerminalOperation(_callers[0].Terminal.Calling(_callers[1]), TerminalOperations.Calling, _callers[1].Terminal.Port.Condition);
            UiManager.GetInfoTerminalOperation(_callers[1].Terminal.AcceptCalling(_callers[0]), TerminalOperations.Accepting);
            Thread.Sleep(8000);
            UiManager.GetInfoTerminalOperation(_callers[0].Terminal.StopCalling(_callers[1]), TerminalOperations.Stopping);
            Console.WriteLine();

            UiManager.GetInfoTerminalOperation(_callers[2].Terminal.Calling(_callers[3]), TerminalOperations.Calling, _callers[1].Terminal.Port.Condition);
            UiManager.GetInfoTerminalOperation(_callers[3].Terminal.AcceptCalling(_callers[2]), TerminalOperations.Accepting);
            Thread.Sleep(6000);
            UiManager.GetInfoTerminalOperation(_callers[3].Terminal.StopCalling(_callers[2]), TerminalOperations.Stopping);
            Console.WriteLine();

            UiManager.GetInfoTerminalOperation(_callers[0].Terminal.Calling(_callers[1]), TerminalOperations.Calling, _callers[1].Terminal.Port.Condition);
            UiManager.GetInfoTerminalOperation(_callers[1].Terminal.AcceptCalling(_callers[0]), TerminalOperations.Accepting);
            Thread.Sleep(7000);
            UiManager.GetInfoTerminalOperation(_callers[0].Terminal.StopCalling(_callers[1]), TerminalOperations.Stopping);
            Console.WriteLine();

            UiManager.GetInfoIfPortConditionChanged(_callers[0].ChangePortCondition(PortCondition.Off));
            UiManager.GetInfoIfPortConditionChanged(_callers[0].ChangePortCondition(PortCondition.Free));
            Console.WriteLine();
        }

        private static void UnSubscribeTerminalEvents()
        {
            foreach (var caller in _callers)
            {
                caller.Terminal.UnSubscribeEvents();
            }
        }

        private static void ShowCallerInfoCollection()
        {
            Console.WriteLine("All CallsInfo with old\n");
            foreach (var callInfo in BillingSystem.GetCalls())
            {
                Console.WriteLine(callInfo.ToString());
                Console.WriteLine();
            }

            var firstUserCallInfoCollection = BillingSystem.GetUserCallsOrderedBy(_callers[0], OrderSequenceType.Cost);
            Console.WriteLine("User " + _callers[0].CallerNumber + "  Calls ordered by cost\n");
            foreach (var callInfo in firstUserCallInfoCollection)
            {
                Console.WriteLine(callInfo.ToString());
                Console.WriteLine();
            }

            var secondUserCallInfoCollection = BillingSystem.GetUserCallsOrderedBy(_callers[1], OrderSequenceType.Duration);
            Console.WriteLine("User " + _callers[1].CallerNumber + "  Calls ordered by duration\n");
            foreach (var callInfo in secondUserCallInfoCollection)
            {
                Console.WriteLine(callInfo.ToString());
                Console.WriteLine();
            }

            var thirdUserCallInfoCollection = BillingSystem.GetUserCallsOrderedBy(_callers[2], OrderSequenceType.Callers);
            Console.WriteLine("User " + _callers[2].CallerNumber + "  Calls ordered by callers\n");
            foreach (var callInfo in thirdUserCallInfoCollection)
            {
                Console.WriteLine(callInfo.ToString());
                Console.WriteLine();
            }

            var fourthUserCallInfoCollection = BillingSystem.GetUserCallsOrderedBy(_callers[3], OrderSequenceType.None);
            Console.WriteLine("User " + _callers[3].CallerNumber + "  Calls\n");
            foreach (var callInfo in fourthUserCallInfoCollection)
            {
                Console.WriteLine(callInfo.ToString());
                Console.WriteLine();
            }
        }
    }
}
