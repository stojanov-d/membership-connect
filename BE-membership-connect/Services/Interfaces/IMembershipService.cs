using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BE_membership_connect.Models;

namespace BE_membership_connect.Services.Interfaces
{
    public interface IMembershipService
    {
        Task<List<Membership>> GetMemberships();
        Task<Membership> GetMembershipById(int id);
        Task<Membership> CreateMembership(Membership membership);
        void DeleteMembership(int id);
        Task<Membership> UpdateMembership(int id, Membership membership);

    }
}