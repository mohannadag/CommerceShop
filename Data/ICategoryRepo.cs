using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public interface ICategoryRepo
    {
        Category Add(Category category);
        Category GetCategory(int id);
        IEnumerable<Category> GetCategories();
        Category Update(Category ChangedCategory);
        Category Delete(int Id);
    }
}
