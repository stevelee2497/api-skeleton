using Persistence.Models;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces
{
    public interface IRoleService
    {
        Role CreateRole(Role role);
        IEnumerable<Role> GetRoles();
    }
}
