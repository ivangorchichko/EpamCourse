using Task3EPAMCourse.ATS.Contracts;
using Task3EPAMCourse.ATS.Enums;

namespace Task3EPAMCourse.ATS.Model
{
    public class Port : IPort
    {
        public Port(int portNumber, PortCondition portCondition)
        {
            PortNumber = portNumber;
            Condition = portCondition;
        }

        public PortCondition Condition { get; private set; }

        private int PortNumber { get; }

        public void ChangeCondition(PortCondition condition)
        {
            Condition = condition;
        }

        public override string ToString()
        {
            return $"PortNumber : {PortNumber}, Condition : {Condition}";
        }
    }
}
