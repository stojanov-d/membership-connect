using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BE_membership_connect.Models;
using BE_membership_connect.Services;
using BE_membership_connect.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BE_membership_connect.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MembershipsController : ControllerBase
    {
        private readonly IMembershipService _membershipService;

        public MembershipsController(IMembershipService membershipService)
        {
            _membershipService = membershipService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMemberships()
        {
            var memberships = await _membershipService.GetMemberships();
            return Ok(memberships);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMembership(Membership membership)
        {
            await _membershipService.CreateMembership(membership);
            return Ok("Successfully created membership");
        }

        [HttpDelete("{id}")]
        public Task<IActionResult> DeleteMembership([FromRoute] int id)
        {
            _membershipService.DeleteMembership(id);
            return Task.FromResult<IActionResult>(Ok("Successfully deleted membership with id: " + id));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMembership([FromRoute] int id, Membership membership)
        {
            await _membershipService.UpdateMembership(id, membership);
            return Ok("Successfully updated membership");
        }
    }
}