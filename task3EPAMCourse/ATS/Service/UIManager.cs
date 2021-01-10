using System;
using task3EPAMCourse.ATS.Contracts;

namespace task3EPAMCourse.ATS.Service
{
    public class UIManager : IUIManager
    {
        public void ShowOperationsMenu()
        {
            Console.WriteLine("Choose operation with ATS: \n" +
                  "1 - add new user\n" +
                  "2 - user menu\n"
                  );
        }
        public int GetUserId()
        {
            Console.WriteLine("Input new user callerID : ");
            return int.Parse(Console.ReadLine());
        }
    }
}
