using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using task3EPAMCourse.ATS.Contracts;
using task3EPAMCourse.ATS.Enums;
using task3EPAMCourse.ATS.Model;
using task3EPAMCourse.ATS.Service;
using task3EPAMCourse.Billing.Contracts;
using task3EPAMCourse.Billing.Enums;
using task3EPAMCourse.Billing.Model;

namespace task3EPAMCourse
{
    internal static class Program
    {
        private static readonly IAts ATs = new AutoTelephoneStation();
        private static readonly IUiManager UiManager = new UiManager();
        private static IList<ICaller> _callers = new List<ICaller>();
        private static readonly IBilling BillingSystem = new BillingSystem(ATs);

        private static void Main()
        {
            CreateContracts();
            CreateConnections();
            ShowCallerInfoCollection();
        }

        private static void CreateContracts()
        {
            _callers.Add(ATs.CreateContract(1, UiManager));
            _callers.Add(ATs.CreateContract(2, UiManager));
            _callers.Add(ATs.CreateContract(3, UiManager));
            _callers.Add(ATs.CreateContract(4, UiManager));
            _callers.Add(ATs.CreateContract(5, UiManager));
            Console.WriteLine();
        }

        private static void CreateConnections()
        {
            _callers = _callers.Where(caller => caller != null).ToList();
            _callers[0].Terminal.Calling(_callers[1]);
            _callers[1].Terminal.AcceptCalling(_callers[0]);
            Thread.Sleep(5000);
            _callers[1].Terminal.StopCalling(_callers[0]);
            Console.WriteLine();

            _callers[3].Terminal.Calling(_callers[2]);
            Thread.Sleep(6000);
            _callers[2].Terminal.DropCalling(_callers[3]);
            Console.WriteLine();

            _callers[2].Terminal.StopCalling(_callers[0]);
            _callers[1].Terminal.DropCalling(_callers[0]);
            _callers[1].Terminal.AcceptCalling(_callers[2]);
            Console.WriteLine();

            _callers[1].Terminal.Calling(_callers[3]);
            _callers[3].Terminal.AcceptCalling(_callers[1]);
            Thread.Sleep(4000);
            _callers[1].Terminal.StopCalling(_callers[3]);
            Console.WriteLine();

            _callers[0].Terminal.Calling(_callers[1]);
            _callers[1].Terminal.AcceptCalling(_callers[0]);
            Thread.Sleep(8000);
            _callers[1].Terminal.StopCalling(_callers[0]);
            Console.WriteLine();

            _callers[2].Terminal.Calling(_callers[3]);
            _callers[2].Terminal.AcceptCalling(_callers[3]);
            _callers[3].Terminal.AcceptCalling(_callers[2]);
            _callers[0].Terminal.Calling(_callers[2]);
            Thread.Sleep(6000);
            _callers[3].Terminal.StopCalling(_callers[2]);
            Console.WriteLine();

            _callers[0].Terminal.Calling(_callers[1]);
            _callers[1].Terminal.AcceptCalling(_callers[0]);
            Thread.Sleep(7000);
            _callers[1].Terminal.StopCalling(_callers[0]);
            Console.WriteLine();

            _callers[0].ChangePortCondition(PortCondition.Off);
            _callers[0].ChangePortCondition(PortCondition.Free);
            Console.WriteLine();
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
