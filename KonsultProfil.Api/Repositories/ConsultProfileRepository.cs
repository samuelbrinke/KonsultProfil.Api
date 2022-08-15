using KonsultProfil.Api.Interfaces;
using KonsultProfil.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KonsultProfil.Api.Repositories
{
    public class ConsultProfileRepository : IConsultProfileRepository
    {
        private ApplicationDbContext _dbContext;
        public ConsultProfileRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<string> CreateConsultProfile(ConsultProfileDTO consultProfileDTO)
        {
            var profile = new ConsultProfile
            {
                FirstName = consultProfileDTO.FirstName,
                LastName = consultProfileDTO.LastName,
                Description = consultProfileDTO.Description,
                Assignments = consultProfileDTO.Assignments,
                Skills = consultProfileDTO.Skills,
                Roles = consultProfileDTO.Roles,
            };

            _dbContext.Add(profile);

            await _dbContext.SaveChangesAsync();
            return "Ok";
        }

        public async Task<List<ConsultProfile>> GetAllConsultProfiles()
        {
            var profiles = await _dbContext.ConsultProfiles
                .Include("Skills")
                .Include("Roles")
                .Include("Assignments.Skills")
                .Include("Assignments.Roles")
                .Select(i => i).ToListAsync();
            return profiles;
        }

        public async Task<ConsultProfile> GetConsultProfileById(int id)
        {
            var profile = await _dbContext.ConsultProfiles
              .Include("Skills")
              .Include("Roles")
              .Include("Assignments.Skills")
              .Include("Assignments.Roles")
              .FirstOrDefaultAsync(i => i.Id == id);

            if(profile == null)
            {
                return null;
            }

            return profile;
        }

        public async Task<ConsultProfile> RemoveConsultProfile(int id)
        {
            var profile = await _dbContext.ConsultProfiles.FirstOrDefaultAsync(i => i.Id == id);

            if(profile == null)
            {
                return null;
            }

            _dbContext.ConsultProfiles.Remove(profile);

            await _dbContext.SaveChangesAsync();

            return profile;
        }
    }
}
