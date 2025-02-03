namespace Tratament.Model.Models.ExternalServices
{
    public class PersonModel
    {
        public PersonModel()
        {
            PersoneAddress = new PersoneAddress();
        }

        public string IDNP { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string Patronymic { get; set; }

        public PersoneAddress PersoneAddress { get; set; }
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
