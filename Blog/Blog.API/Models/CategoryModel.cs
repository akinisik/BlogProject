using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.API.Models
{
    public class CategoryModel
    {
        public string Name { get; set; }
        public IEnumerable<ArticleModel> Articles { get; set; }
    }
}
