using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task5.BL.Enums;
using Task5EpamCourse.Models.Page;

namespace Task5EpamCourse.PageHelper.Contacts
{
    public interface IPageService
    {
        ModelsInPageViewModel GetModelsInPageViewModel<TModel>(int page) where TModel : class;

        ModelsInPageViewModel GetFilteredModelsInPageViewModel<TModel>(TextFieldFilter filter, string fieldString,
            int page) where TModel : class;
    }
}
