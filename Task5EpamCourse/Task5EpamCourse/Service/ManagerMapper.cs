using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Task5.BL.Contacts;
using Task5.BL.Models;
using Task5EpamCourse.MapperWebConfig;
using Task5EpamCourse.Models.Client;
using Task5EpamCourse.Models.Manager;
using Task5EpamCourse.Service.Contracts;

namespace Task5EpamCourse.Service
{
    public class ManagerMapper : IManagerMapper
    {
        private readonly IMapper _mapper;
        private readonly IManagerService _managerService;

        public ManagerMapper(IManagerService managerService)
        {
            _managerService = managerService;
            _mapper = new Mapper(AutoMapperWebConfig.Configure());
        }

        public ManagerViewModel GetManagerViewModel(int? id)
        {
            return _mapper.Map<IEnumerable<ManagerViewModel>>(_managerService.GetManagersDto())
                .ToList().Find(x => x.Id == id);
        }
        public IEnumerable<ManagerViewModel> GetManagerViewModel()
        {
            return _mapper.Map<IEnumerable<ManagerViewModel>>(_managerService.GetManagersDto())
                .OrderBy(d => d.Date);
        }


        public ManagerDto GetManagerDto(ManagerViewModel managerViewModel)
        {
            return _mapper.Map<ManagerDto>(managerViewModel);
        }
    }
}