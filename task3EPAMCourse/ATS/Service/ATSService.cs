using System;
using System.Linq;
using task3EPAMCourse.ATS.Contracts;
using task3EPAMCourse.ATS.Enums;
using task3EPAMCourse.ATS.Model;
using task3EPAMCourse.Billing.Contracts;
using task3EPAMCourse.Billing.Model;

namespace task3EPAMCourse.ATS.Service
{
    class ATSService : IATSService
    {
        private IATS _ats = new AutoTelephoneStation();
        private IBilling _billing = new BillingSystem();

        public ATSService(IATS autoTelephoneStation)
        {
            _ats = autoTelephoneStation;
            
        }
    }
}
