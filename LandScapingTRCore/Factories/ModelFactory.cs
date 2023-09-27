using LandscapingTR.Core.Entities.CompanyResources;
using LandscapingTR.Core.Entities.Domain;
using LandscapingTR.Core.Entities.Lookups;
using LandscapingTR.Core.Models.CompanyResources;
using LandscapingTR.Core.Models.Domain;
using LandscapingTR.Core.Models.Lookups;

namespace LandscapingTR.Core.Factories
{
    public class ModelFactory
    {
        public static CustomerModel Create(Customer entity)
        {
            return new CustomerModel() {
                Id = entity.Id.HasValue ? entity.Id.Value : null,
                Name = entity.Name,
                CustomerTypeId = entity.CustomerTypeId,
            };
        }

        public static Customer Parse(CustomerModel model)
        {
            return new Customer()
            {
                Id = model.Id.HasValue ? model.Id.Value : null,
                Name = model.Name,
                CustomerTypeId = model.CustomerTypeId,
            };
        }

        public static EmployeeModel Create(Employee entity)
        {
            return new EmployeeModel()
            {
                Id = entity.Id.HasValue ? entity.Id.Value : null,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Password = entity.Password,
                EmployeeTypeId = entity.EmployeeTypeId
            };
        }

        public static Employee Parse(EmployeeModel model)
        {
            return new Employee()
            {
                Id = model.Id.HasValue ? model.Id.Value : null,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Password = model.Password,
                EmployeeTypeId = model.EmployeeTypeId
            };
        }

        public static JobModel Create(Job entity)
        {
            return new JobModel()
            {
                Id = entity.Id.HasValue ? entity.Id.Value : null,
                EmployeeId = entity.EmployeeId,
                JobTypeId = entity.JobTypeId
            };
        }

        public static Job Parse(JobModel model)
        {
            return new Job()
            {
                Id = model.Id.HasValue ? model.Id.Value : null,
                EmployeeId = model.EmployeeId,
                JobTypeId = model.JobTypeId
            };
        }

        public static LocationModel Create(Location entity)
        {
            return new LocationModel()
            {
                Id = entity.Id.HasValue ? entity.Id.Value : null,
                LocationTypeId = entity.LocationTypeId,
                Address = entity.Address,
                City = entity.City,
                State = entity.State
            };
        }

        public static Location Parse(LocationModel model)
        {
            return new Location()
            {
                Id = model.Id.HasValue ? model.Id.Value : null,
                LocationTypeId = model.LocationTypeId,
                Address = model.Address,
                City = model.City,
                State = model.State
            };
        }

        public static LookupItemModel CreateLookupItemModel(LocationType entity)
        {
            return new LookupItemModel()
            {
                Id = entity.Id.HasValue ? entity.Id.Value : null,
                Active = entity.Active,
                LookupValue = entity.LocationTypeDisplayValue,
                SortOrder = entity.SortOrder
            };
        }

        public static LookupItemModel CreateLookupItemModel(CustomerType entity)
        {
            return new LookupItemModel()
            {
                Id = entity.Id.HasValue ? entity.Id.Value : null,
                Active = entity.Active,
                LookupValue = entity.CustomerTypeDisplayValue,
                SortOrder = entity.SortOrder
            };
        }

        public static LookupItemModel CreateLookupItemModel(EmployeeType entity)
        {
            return new LookupItemModel()
            {
                Id = entity.Id.HasValue ? entity.Id.Value : null,
                Active = entity.Active,
                LookupValue = entity.EmployeeTypeDisplayValue,
                SortOrder = entity.SortOrder
            };
        }

        public static LookupItemModel CreateLookupItemModel(JobType entity)
        {
            return new LookupItemModel()
            {
                Id = entity.Id.HasValue ? entity.Id.Value : null,
                Active = entity.Active,
                LookupValue = entity.JobTypeDisplayValue,
                SortOrder = entity.SortOrder
            };
        }
    }
}
