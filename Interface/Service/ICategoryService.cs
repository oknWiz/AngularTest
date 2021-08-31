using InfiniteProcurement.DataModel.Models;
using System;
using System.Collections.Generic;
using InfiniteProcurement.Core.Utils;

namespace InfiniteProcurement.Interface.Service
{
    public interface ICategoryService : IDisposable
    {
        string InsertNewCategory(TblCategory newCategory);
        int UpdateCategory(TblCategory updateCategory);
        int DeleteCategory(int categoryId, string updatedUser);
        TblCategory SelectCategoryByName(string name);
        IEnumerable<object> SelectAllCategories();
        TblCategory SelectCategoryById(int id);
        IEnumerable<object> SelectCategoryList(int startIndex, int pageSize, DTOrder[] orders, DTColumn[] columns,
            DTSearch searchExp);
    }
}
