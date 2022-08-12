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
                Description = consultProfileDTO.Description
            };

            _dbContext.Add(profile);

            await _dbContext.SaveChangesAsync();

            return Ok($"Added profile {profile.Id}");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ConsultProfile>> GetConsultProfile(int id)
        {
            var profile = await _dbContext.ConsultProfiles.FirstOrDefaultAsync(i => i.Id == id);

            if (profile == null)
            {
                return NotFound();
            }

            return profile;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllConsultProfile()
        {
            var profiles = await _dbContext.ConsultProfiles.ToListAsync();

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
                    Description = "Junior dev"
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
