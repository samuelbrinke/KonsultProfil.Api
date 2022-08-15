using KonsultProfil.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace KonsultProfil.Api.Interfaces
{
    public interface IConsultProfileRepository
    {
        Task<string> CreateConsultProfile(ConsultProfileDTO consultProfileDTO);
        Task<List<ConsultProfile>> GetAllConsultProfiles();
        Task<ConsultProfile> GetConsultProfileById(int id);
        Task<ConsultProfile> RemoveConsultProfile(int id);
    }
}
