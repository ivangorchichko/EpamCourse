using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task5.BL.Models;
using Task5EpamCourse.Models.Client;

namespace Task5EpamCourse.Service.Contracts
{
    public interface IClientMapper
    {
        ClientViewModel GetClientViewModel(int? id);

        IEnumerable<ClientViewModel> GetClientViewModel();

        ClientDto GetClientDto(ClientViewModel clientViewModel);
    }
}
