using BusinessLogic.Interfaces;
using Persistence.Models;
using Repository;
using System.Collections.Generic;

namespace BusinessLogic.Services
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RoleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Role CreateRole(Role role)
        {
            var res = _unitOfWork.Repository<Role>().Add(role);
            _unitOfWork.Complete();
            return res;
        }

        public IEnumerable<Role> GetRoles()
        {
            return _unitOfWork.Repository<Role>().All();
        }
    }
}
