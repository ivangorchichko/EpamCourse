using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task5.BL.Models;
using Task5EpamCourse.Models.Manager;

namespace Task5EpamCourse.Service.Contracts
{
    public interface IManagerMapper
    {
        ManagerViewModel GetManagerViewModel(int? id);

        IEnumerable<ManagerViewModel> GetManagerViewModel();

        ManagerDto GetManagerDto(ManagerViewModel managerViewModel);
    }
}
