namespace KonsultProfil.Api.Models
{
    public class ConsultProfile
    {
        /* 
        Skills (Språk, verktyg)
        Tidigare uppdrag (assignment)
            startdatum
            slutdatum
            uppdragsgivare
            feedback från uppdragsgivaren
            uppdragsbeskrivning
            uppdragstyp (systemutveckling osv)
            skills
            roller
        Roller
        */

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public ICollection<Skill>? Skills { get; set; }
        public ICollection<Role>? Roles { get; set; }
        public ICollection<Assignment>? Assignments { get; set; }
    }

    public class Assignment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Employer { get; set; }
        public string EmployerFeedback { get; set; }
        public string Description { get; set; }
        public string TypeOfAssignment { get; set; }
        public ICollection<Skill>? Skills { get; set; }
        public ICollection<Role>? Roles { get; set; }
    }

    public class Skill
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

    public class Role
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