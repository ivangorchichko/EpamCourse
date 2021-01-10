using System;
using task3EPAMCourse.ATS.Contracts;
using task3EPAMCourse.ATS.Enums;

namespace task3EPAMCourse.ATS.Model
{
    public class Port : IPort
    {
        public int PortNumber { get; }
        public PortCondition Condition { get; private set; }

        public Port(int portNumber, PortCondition portCondition) 
        {
            PortNumber = portNumber;
            Condition = portCondition;
        }

        public void ChangeCondition(PortCondition condition)
        {
            Condition = condition;
        }

        public void GetPortInfo()
        {
            Console.WriteLine($"PortNumber : {PortNumber}, Condition : {Condition}");
        }
    }
}
