using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Blog.Core.Entities
{
    [Table("Role")]
    public class Role : IdentityRole<int>
    {
    }
}
