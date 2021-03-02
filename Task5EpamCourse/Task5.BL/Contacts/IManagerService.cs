using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Task5.BL.Enums;
using Task5.BL.Models;
using Task5.DomainModel.DataModel;

namespace Task5.BL.Contacts
{
    public interface IManagerService
    {
        IEnumerable<ManagerDto> GetManagersDto();

        IEnumerable<ManagerDto> GetManagersDto(int page, Expression<Func<ManagerEntity, bool>> predicate = null);

        void AddManager(ManagerDto managerDto);

        void ModifyManager(ManagerDto managerDto);

        void RemoveManager(ManagerDto managerDto);

        IEnumerable<ManagerDto> GetFilteredManagerDto(TextFieldFilter filter, string fieldString, int page);
    }
}
