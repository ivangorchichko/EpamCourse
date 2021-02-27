using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task5.BL.Enums;
using Task5.BL.Models;

namespace Task5.BL.Contacts
{
    public interface IClientService
    {
        IEnumerable<ClientDto> GetClientDto();
        void AddClient(ClientDto clientDto);
        void ModifyClient(ClientDto clientDto);
        void RemoveClient(ClientDto clientDto);
        IEnumerable<ClientDto> GetFilteredClientDto(TextFieldFilter filter, string fieldString);
    }
}
