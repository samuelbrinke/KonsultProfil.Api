namespace KonsultProfil.Api.Models
{
    public class ConsultProfileDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string lastName { get; set; }
        public string Description { get; set; }
        public ICollection<Skill>? Skills { get; internal set; }
        public ICollection<Role>? Roles { get; internal set; }
    }

    public class SkillDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        /// <summary>
        /// 0-10
        /// </summary>
        public int Level { get; set; }
        public int YearsOfExperience { get; set; }
    }

    public class RoleDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        /// <summary>
        /// 0-10
        /// </summary>
        public int Level { get; set; }
        public int YearsOfExperience { get; set; }
    }
}
