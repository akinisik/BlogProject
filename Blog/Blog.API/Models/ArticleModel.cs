using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.API.Models
{
    public class ArticleModel
    {
        public string Title { get; set; }
        
        public string Content { get; set; }

        public CategoryModel Category { get; set; }

        public UserModel User { get; set; }
    }
}
