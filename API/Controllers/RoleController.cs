using API.DTOs;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace API.Controllers
{
    [Route("api/role")]
    [ApiController]
    [Produces("application/json")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public SuccessResponse<IEnumerable<RoleDto>> GetRoles()
        {
            return new SuccessResponse<IEnumerable<RoleDto>>(_roleService.GetRoles().Select(x => new RoleDto { Id = x.Id, Name = x.Name }));
        }
    }
}