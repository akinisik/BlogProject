using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.API.Models
{
    public class CategoryCreateModel
    {
        [Required]
        public string Name { get; set; }
    }
}
