using Newtonsoft.Json;

namespace MAIeDosar.API.ApiViewModels.ExternalServices
{
    public class PersonAPIModel
    {
        public PersonAPIModel()
        {
            PersoneAddress = new PersoneAddress();
        }

        public string IDNP { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string Patronymic { get; set; }

        public string Citizenship { get; set; }

        public string Country { get; set; }

        public string Region { get; set; }

        public string Locality { get; set; }

        public string Street { get; set; }

        public string House { get; set; }

        public string Block { get; set; }

        public string Flat { get; set; }

        public byte? Photo { get; set; }

        public int RegistrationType { get; set; }

        public int RegistrationStatus { get; set; }

        public int Type { get; set; }

        public string Series { get; set; }

        public string DocNumber { get; set; }

        public DateTime IssueDate { get; set; }

        public string IssuedBy { get; set; }

        public int Status { get; set; }

        public PersoneAddress PersoneAddress { get; set; }
    }


    public class IdentityDocument
    {
        public int Type { get; set; }

        public string Series { get; set; }
        public string Number { get; set; }

        public DateTime IssueDate { get; set; }
        public string IssuedBy { get; set; }

        public int Status { get; set; }

    }


    public class PersoneAddress
    {
        public string Country { get; set; }

        public string Region { get; set; }

        public string Locality { get; set; }

        public string Street { get; set; }

        public string House { get; set; }

        public string Block { get; set; }

        public string Flat { get; set; }

        public string CountyCode { get; set; }

        public string AdministrativeCode { get; set; }

    }
}
