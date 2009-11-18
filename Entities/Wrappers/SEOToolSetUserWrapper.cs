using System;
using System.Collections.Generic;
using System.Text;

namespace SEOToolSet.Entities.Wrappers
{
    public class SEOToolSetUserWrapper
    {
        public int Id { get; set; }

        public string FullName
        {
            get { return (FirstName ?? string.Empty) + (LastName ?? string.Empty); }
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string CityTown { get; set; }

        public string State { get; set; }

        public string Zip { get; set; }

        public string Telephone { get; set; }

        public string Login { get; set; }

        public CountryWrapper Country { get; set; }

        public int ProjectsInvolving { get; set; }

        private Role Role { get; set; }

        public string UserRoleName
        {
            get { return Role == null ? null : Role.Name; }
        }

        public int UserRoleId
        {
            get { return Role == null ? -1 : Role.Id; }
        }

        public static implicit operator SEOToolSetUserWrapper(SEOToolsetUser user)
        {
            return user == null
                       ? null
                       : new SEOToolSetUserWrapper
                       {
                           Id = user.Id,
                           FirstName = user.FirstName,
                           LastName = user.LastName,
                           Email = user.Email,
                           Address1 = user.Address1,
                           Address2 = user.Address2,
                           CityTown = user.CityTown,
                           State = user.State,
                           Zip = user.Zip,
                           Telephone = user.Telephone,
                           Login = user.Login,
                           Country = user.Country,
                           Role = user.UserRole
                       };
        }
    }
}
