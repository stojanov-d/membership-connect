using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BE_membership_connect.Models;

namespace BE_membership_connect.Repository.Interfaces
{
    public interface IMembershipRepository
    {
        Task<List<Membership>> LocalGetMemberships();
        Task<List<Membership>> GetMemberships();
        Task<Membership> LocalGetMembershipById(int id);
        Task<Membership> GetMembershipById(int id);
        Task<Membership> LocalCreateMembership(Membership membership);
        Task<Membership> CreateMembership(Membership membership);
        void LocalDeleteMembership(int id);
        void DeleteMembership(int id);
        Task<Membership> LocalUpdateMembership(int id, Membership membership);
        Task<Membership> UpdateMembership(int id, Membership membership);
    }
}