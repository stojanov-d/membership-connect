using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BE_membership_connect.Models;
using BE_membership_connect.Repository;
using BE_membership_connect.Repository.Interfaces;
using BE_membership_connect.Services.Interfaces;

namespace BE_membership_connect.Services
{
    public class MembershipService : IMembershipService
    {
        private readonly IMembershipRepository _membershipRepository;
        private readonly IHostEnvironment _env;

        public MembershipService(IMembershipRepository membershipRepository, IHostEnvironment env)
        {
            _membershipRepository = membershipRepository;
            _env = env;
        }

        public async Task<List<Membership>> GetMemberships()
        {

            if (_env.IsDevelopment())
            {
                return await _membershipRepository.LocalGetMemberships();
            }
            else
            {
                return await _membershipRepository.GetMemberships();
            }
        }

        public async Task<Membership> GetMembershipById(int id)
        {
            if (_env.IsDevelopment())
            {
                return await _membershipRepository.LocalGetMembershipById(id);
            }
            else
            {
                return await _membershipRepository.GetMembershipById(id);
            }
        }

        public async Task<Membership> CreateMembership(Membership membership)
        {
            if (_env.IsDevelopment())
            {
                return await _membershipRepository.LocalCreateMembership(membership);
            }
            else
            {
                return await _membershipRepository.CreateMembership(membership);
            }
        }

        public void DeleteMembership(int id)
        {
            if (_env.IsDevelopment())
            {
                _membershipRepository.LocalDeleteMembership(id);
            }
            else
            {
                _membershipRepository.DeleteMembership(id);
            }
        }

        public async Task<Membership> UpdateMembership(int id, Membership membership)
        {
            if (_env.IsDevelopment())
            {
                return await _membershipRepository.LocalUpdateMembership(id, membership);
            }
            else
            {
                return await _membershipRepository.UpdateMembership(id, membership);
            }
        }
    }
}