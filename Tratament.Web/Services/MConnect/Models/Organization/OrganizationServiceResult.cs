using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Tratament.Web.ServicesModels.Org
{
    public class OrganizationServiceResult
    {

        public OrganizationServiceResult()
        {
            Administrators = new List<Administrator>();
            //Activities = new List<Activity>();
            OrganizationAddress = new Address();
        }

        public string IDNO { get; set; }

        public string UniqueIdentificationCode { get; set; }

        public string FullNameOfLegalEntity { get; set; }

        public string ShortNameOfLegalEntity { get; set; }

        public int State { get; set; }

        public int LegalForm { get; set; }

        public Address OrganizationAddress { get; set; }

        public List<Administrator> Administrators { get; set; }

        public List<Activity> Activities { get; set; }

    }


    public class Address
    {
        public string Country { get; set; }

        public string Region { get; set; }

        public string Locality { get; set; }

        public int AdministrativeCode { get; set; }

        public string Street { get; set; }

        public string House { get; set; }

        public string Block { get; set; }

        public string Flat { get; set; }

    }



    public class Administrator
    {
        public string IDNP { get; set; }

        public string FamilyName { get; set; }

        public string GivenName { get; set; }

        public string Role { get; set; }

        public Address AdministratorAddress { get; set; }
    }

    public class Activity
    {
        public string Type { get; set; }
        public string Code { get; set; }
    }
}
