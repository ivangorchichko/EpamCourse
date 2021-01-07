using System;
using System.Collections.Generic;
using System.Text;
using task3EPAMCourse.Contracts;
using task3EPAMCourse.Enums;

namespace task3EPAMCourse.Model.ATS
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
    }
}
