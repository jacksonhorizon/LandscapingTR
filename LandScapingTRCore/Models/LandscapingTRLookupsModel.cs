using LandscapingTR.Core.Models.Lookups;

namespace LandscapingTR.Core.Models
{
    public class LandscapingTRLookupsModel
    {
        public List<LookupItemModel> CustomerTypes;

        public List<LookupItemModel> EmployeeTypes;

        public List<LookupItemModel> JobTypes;

        public List<LookupItemModel> LocationTypes;

        public LandscapingTRLookupsModel() { 
            CustomerTypes = new List<LookupItemModel>();
            EmployeeTypes = new List<LookupItemModel>();
            JobTypes = new List<LookupItemModel>();
            LocationTypes = new List<LookupItemModel>();
        }
    }
}
