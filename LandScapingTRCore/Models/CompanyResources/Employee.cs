using LandscapingTRCore.Models.Lookups;

namespace LandscapingTRCore.Models.CompanyResources
{
    public class Employee
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int EmployeeId { get; set; }

        public string Password { get; set; }

        public int EmployeeTypeId { get; set; }

        public EmployeeType EmployeeType { get; set; }
    }
}
