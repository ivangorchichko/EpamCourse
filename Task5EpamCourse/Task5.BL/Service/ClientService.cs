using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Task5.BL.Enums;
using Task5.BL.MapperBLHelper;
using Task5.BL.Models;
using Task5.DAL.Repository.Contract;
using Task5.DomainModel.DataModel;

namespace Task5.BL.Service
{
    public class ClientService
    {
        private bool _disposed = false;
        private static IMapper _mapper;
        private static IRepository _repository;

        public ClientService(IRepository repository)
        {
            _mapper = new Mapper(AutoMapperBLConfig.Configure());
            _repository = repository;
        }

        public IEnumerable<ClientDto> GetClientDto()
        {
            var clientEntities = _repository.Get<ClientEntity>();
            var client =
                _mapper.Map<IEnumerable<ClientDto>>(clientEntities);
            return client;
        }

        public void AddClient(ClientDto clientDto)
        {
            _repository.Add<ClientEntity>(_mapper.Map<ClientEntity>(clientDto));
        }

        public void ModifyClient(ClientDto clientDto)
        {
            var clientEntity = _repository.Get<ClientEntity>().ToList()
                .Find(client => client.Id == clientDto.Id);
            clientEntity.ClientName = clientDto.ClientName;
            clientEntity.ClientTelephone = clientDto.ClientTelephone;
            clientEntity.Id = clientDto.Id;
            _repository.Save();
        }

        public void RemoveClient(ClientDto clientDto)
        {
            var clientEntity = _repository.Get<ClientEntity>()
                .ToList()
                .Find(client => client.Id == clientDto.Id);
            _repository.Remove(clientEntity);
        }

        public IEnumerable<ClientDto> GetFilteredClientDto(TextFieldFilter filter, string fieldString)
        {
            var purchases = GetClientDto();
            switch (filter)
            {
                case TextFieldFilter.ClientName:
                    {
                        purchases = purchases.Where(purchase => purchase.ClientName == fieldString).ToList();
                        break;
                    }
                case TextFieldFilter.ClientTelephone:
                    {
                        purchases = purchases.Where(purchase => purchase.ClientTelephone == fieldString).ToList();
                        break;
                    }
            }
            return purchases;
        }
    }
}
