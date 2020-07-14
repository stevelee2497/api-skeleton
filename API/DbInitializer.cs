using BusinessLogic.Interfaces;
using Common.Constants;
using Persistence.Models;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using System;
using Repository;

namespace API
{
    public static class DbInitializer
    {
        public static void Initialize(IServiceProvider services)
        {
            var unitOfWork = services.GetService<IUnitOfWork>();
            if (unitOfWork.Repository<Role>().Count() != 0)
                return;

            var roleService = services.GetService<IRoleService>();
            var roles = new List<Role> 
            {
                new Role { Name = DefaultRole.User },
                new Role { Name = DefaultRole.Admin }
            };
            roles.ForEach(role => roleService.CreateRole(role));
        }
    }
}
