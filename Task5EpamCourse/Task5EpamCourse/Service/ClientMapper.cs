using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Task5.BL.Contacts;
using Task5.BL.Models;
using Task5EpamCourse.MapperWebConfig;
using Task5EpamCourse.Models.Client;
using Task5EpamCourse.Models.Product;
using Task5EpamCourse.Service.Contracts;

namespace Task5EpamCourse.Service
{
    public class ClientMapper : IClientMapper
    {
        private readonly IMapper _mapper;
        private readonly IClientService _clientService;

        public ClientMapper(IClientService clientService)
        {
            _clientService = clientService;
            _mapper = new Mapper(AutoMapperWebConfig.Configure());
        }

        public ClientViewModel GetClientViewModel(int? id)
        {
            return _mapper.Map<IEnumerable<ClientViewModel>>(_clientService.GetClientsDto())
                .ToList().Find(x => x.Id == id);
        }
        public IEnumerable<ClientViewModel> GetClientViewModel()
        {
            return _mapper.Map<IEnumerable<ClientViewModel>>(_clientService.GetClientsDto())
                .OrderBy(d => d.Date);
        }

        public ClientDto GetClientDto(ClientViewModel clientViewModel)
        {
            return _mapper.Map<ClientDto>(clientViewModel);
        }
    }
}