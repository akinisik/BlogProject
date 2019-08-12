using Blog.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.API.Infrastructure
{
    public class ApplicationRoleManager : RoleManager<Role>
    {
        public ApplicationRoleManager(IRoleStore<Role> roleStore,
        IEnumerable<IRoleValidator<Role>> roleValidators, ILookupNormalizer keyNormalizer,
        IdentityErrorDescriber errors, ILogger<ApplicationRoleManager> logger) : base(roleStore,
        roleValidators,
        keyNormalizer, errors, logger)
        {
        }
    }
}
