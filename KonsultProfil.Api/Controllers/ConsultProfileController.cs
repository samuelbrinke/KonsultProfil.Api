using KonsultProfil.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KonsultProfil.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultProfileController : ControllerBase
    {
        private ApplicationDbContext _dbContext;
        public ConsultProfileController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            Seed();
        }

        [HttpPost] 
        public async Task<IActionResult> CreateConsultProfile(ConsultProfileDTO consultProfileDTO)
        {
            var profile = new ConsultProfile
            {
                FirstName = consultProfileDTO.FirstName,
                LastName = consultProfileDTO.lastName,
                Description = consultProfileDTO.Description,
                Assignments = consultProfileDTO.Assignments,
                Skills = consultProfileDTO.Skills,
                Roles = consultProfileDTO.Roles,
            };

            _dbContext.Add(profile);

            await _dbContext.SaveChangesAsync();

            return Ok($"Added profile {profile.Id}");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ConsultProfileDTO>> GetConsultProfile(int id)
        {
            var profile = await _dbContext.ConsultProfiles.FirstOrDefaultAsync(i => i.Id == id);

            if (profile == null)
            {
                return NotFound();
            }

            return ProfileToDTO(profile);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllConsultProfile()
        {
            var profiles = await _dbContext.ConsultProfiles.Select(i => ProfileToDTO(i)).ToListAsync();

            return Ok(profiles);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveConsultProfile(int id)
        {
            var profile = await _dbContext.ConsultProfiles.FirstOrDefaultAsync(i => i.Id == id);

            if (profile == null)
            {
                return NotFound();
            }

            _dbContext.ConsultProfiles.Remove(profile);

            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        private ConsultProfileDTO ProfileToDTO(ConsultProfile consultProfile)
        {
            return new ConsultProfileDTO
            {
                Id = consultProfile.Id,
                FirstName = consultProfile.FirstName,
                lastName = consultProfile.LastName,
                Description = consultProfile.Description,
                Assignments = consultProfile.Assignments,
                Skills = consultProfile.Skills,
                Roles = consultProfile.Roles
            };
        } 

        private void Seed()
        {
            if(_dbContext.ConsultProfiles.Any())
            {
                return;
            }
            _dbContext.ConsultProfiles.AddRange(
                new ConsultProfile
                {
                    Id = 1,
                    FirstName = "William",
                    LastName = "Shakespeare",
                    Description = "Junior dev",
                    Skills = new List<Skill> {
                        new Skill { 
                            Name = "C#",
                            Description = "Microsoft programming language",
                            Level = 10,
                            YearsOfExperience = 20,
                        }
                    },
                     Roles = new List<Role> {
                        new Role { 
                            Name = "Software Developer",
                            Description = "Creating cool softwares",
                            Level = 6,
                            YearsOfExperience = 15,
                        }
                    }
                },
                new ConsultProfile
                {
                    Id = 2,
                    FirstName = "Kalle",
                    LastName = "Anka",
                    Description = "Senior dev"
                }, new ConsultProfile
                {
                    Id = 3,
                    FirstName = "Pelle",
                    LastName = "Svanslös",
                    Description = "Project Manager"
                }
             );

            _dbContext.SaveChanges();
        }
    }
}
