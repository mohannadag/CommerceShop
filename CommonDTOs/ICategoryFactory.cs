using CommerceShop.Areas.Admin.Models;
using Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonDTOs
{
    public interface ICategoryFactory
    {
        Category ConvertToCategory(CategoryViewModel obj);
    }
}
