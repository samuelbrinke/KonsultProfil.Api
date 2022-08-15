using KonsultProfil.Api.Interfaces;
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
        private IConsultProfileRepository _consultProfileRepository;

        public ConsultProfileController(ApplicationDbContext dbContext, IConsultProfileRepository consultProfileRepository)
        {
            _dbContext = dbContext;
            _consultProfileRepository = consultProfileRepository;
            Seed();
        }

        [HttpPost] 
        public async Task<IActionResult> CreateConsultProfile(ConsultProfileDTO consultProfileDTO)
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

            return Ok($"Added profile {ProfileToDTO(profile).Id}");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ConsultProfileDTO>> GetConsultProfile(int id)
        {
            var profile = await _consultProfileRepository.GetConsultProfileById(id);

            if (profile == null)
            {
                return NotFound();
            }

            return Ok(ProfileToDTO(profile));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllConsultProfile()
        {
            var profiles = await _consultProfileRepository.GetAllConsultProfiles();

            var mapProfilesToDTO = profiles.Select(i => ProfileToDTO(i));

            return Ok(mapProfilesToDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveConsultProfile(int id)
        {
            var profile = await _consultProfileRepository.RemoveConsultProfile(id);

            if (profile == null)
            {
                return NotFound();
            }

            return Ok($"Removed {ProfileToDTO(profile).FirstName}");
        }

        private ConsultProfileDTO ProfileToDTO(ConsultProfile consultProfile)
        {
            return new ConsultProfileDTO
            {
                Id = consultProfile.Id,
                FirstName = consultProfile.FirstName,
                LastName = consultProfile.LastName,
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
