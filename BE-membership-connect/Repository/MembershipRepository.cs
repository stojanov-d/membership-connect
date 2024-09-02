using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BE_membership_connect.Database;
using BE_membership_connect.Models;
using BE_membership_connect.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BE_membership_connect.Repository
{
    public class MembershipRepository : IMembershipRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly StagingDbContext _stagingDbContext;

        public MembershipRepository(AppDbContext appDbContext, StagingDbContext stagingDbContext)
        {
            _appDbContext = appDbContext;
            _stagingDbContext = stagingDbContext;
        }

        public async Task<List<Membership>> LocalGetMemberships()
        {
            var memberships = await _appDbContext.Memberships.ToListAsync();
            return memberships;
        }

        public async Task<List<Membership>> GetMemberships()
        {
            var memberships = await _stagingDbContext.Memberships.ToListAsync();
            return memberships;
        }

        public async Task<Membership> LocalGetMembershipById(int id)
        {
            var membership = await _appDbContext.Memberships.FindAsync(id);

            if (membership == null)
            {
                throw new Exception("Membership not found");
            }

            return membership;
        }

        public async Task<Membership> GetMembershipById(int id)
        {
            var membership = await _stagingDbContext.Memberships.FindAsync(id);

            if (membership == null)
            {
                throw new Exception("Membership not found");
            }

            return membership;
        }

        public async Task<Membership> LocalCreateMembership(Membership membership)
        {
            _appDbContext.Memberships.Add(membership);
            await _appDbContext.SaveChangesAsync();
            return membership;
        }

        public async Task<Membership> CreateMembership(Membership membership)
        {
            _stagingDbContext.Memberships.Add(membership);
            await _stagingDbContext.SaveChangesAsync();
            return membership;
        }

        public void LocalDeleteMembership(int id)
        {
            var membership = _appDbContext.Memberships.Find(id);

            if (membership == null)
            {
                throw new Exception("Membership not found");
            }

            _appDbContext.Memberships.Remove(membership);
            _appDbContext.SaveChanges();
        }

        public void DeleteMembership(int id)
        {
            var membership = _stagingDbContext.Memberships.Find(id);

            if (membership == null)
            {
                throw new Exception("Membership not found");
            }

            _stagingDbContext.Memberships.Remove(membership);
            _stagingDbContext.SaveChanges();
        }

        public async Task<Membership> LocalUpdateMembership(int id, Membership membership)
        {
            var existingMembership = _appDbContext.Memberships.Find(id);

            if (existingMembership == null)
            {
                throw new Exception("Membership not found");
            }

            existingMembership.Name = membership.Name;
            existingMembership.Price = membership.Price;
            existingMembership.DurationInMonths = membership.DurationInMonths;
            existingMembership.Description = membership.Description;

            await _appDbContext.SaveChangesAsync();

            return existingMembership;

        }

        public async Task<Membership> UpdateMembership(int id, Membership membership)
        {
            var existingMembership = _stagingDbContext.Memberships.Find(id);

            if (existingMembership == null)
            {
                throw new Exception("Membership not found");
            }

            existingMembership.Name = membership.Name;
            existingMembership.Price = membership.Price;
            existingMembership.DurationInMonths = membership.DurationInMonths;
            existingMembership.Description = membership.Description;

            await _stagingDbContext.SaveChangesAsync();

            return existingMembership;
        }
    }
}