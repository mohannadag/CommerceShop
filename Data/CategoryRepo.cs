using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class CategoryRepo : ICategoryRepo
    {
        private readonly ApplicationDbContext context;

        public CategoryRepo(ApplicationDbContext context)
        {
            this.context = context;
        }
        public Category Add(Category category)
        {
            context.Add(category);
            context.SaveChangesAsync();
            return category;
        }

        public Category Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Category> GetCategories()
        {
            throw new NotImplementedException();
        }

        public Category GetCategory(int id)
        {
            throw new NotImplementedException();
        }

        public Category Update(Category ChangedCategory)
        {
            throw new NotImplementedException();
        }
    }
}
