using Newtonsoft.Json;

namespace MAIeDosar.API.ApiViewModels.ExternalServices
{
    public class PersonAPIModel
    {

        // NameTime -> nameTime
        [JsonProperty("IDNP")]
        public string IDNP { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Surname")]
        public string Surname { get; set; }

        [JsonProperty("DateOfBirth")]
        public DateTime DateOfBirth { get; set; }

        [JsonProperty("Patronymic")]
        public string Patronymic { get; set; }

        [JsonProperty("Citizenship")]
        public string Citizenship { get; set; }

        [JsonProperty("Country")]
        // Address
        public string Country { get; set; }

        [JsonProperty("Region")]
        public string Region { get; set; }

        [JsonProperty("Locality")]
        public string Locality { get; set; }

        [JsonProperty("Street")]
        public string Street { get; set; }

        [JsonProperty("House")]
        public string House { get; set; }

        [JsonProperty("Block")]
        public string Block { get; set; }

        [JsonProperty("Flat")]
        public string Flat { get; set; }

        [JsonProperty("Photo")]
        public byte? Photo { get; set; }

        [JsonProperty("RegistrationType")]
        public int RegistrationType { get; set; }

        [JsonProperty("RegistrationStatus")]
        public int RegistrationStatus { get; set; }

        [JsonProperty("Type")]
        public int Type { get; set; }

        [JsonProperty("Series")]
        public string Series { get; set; }

        [JsonProperty("DocNumber")]
        public string DocNumber { get; set; }

        [JsonProperty("IssueDate")]
        public DateTime IssueDate { get; set; }

        [JsonProperty("IssuedBy")]
        public string IssuedBy { get; set; }

        [JsonProperty("Status")]
        public int Status { get; set; }

        /// <summary>
        /// Тип массива - определяется по типу поля.
        ///1	String
        ///2	Integer
        ///3	Decimal
        ///4	Date
        ///5	DateTime
        ///6	Time
        ///7	CommonReference
        ///8	Boolean
        /// </summary>
        [JsonProperty("TestArray")]
        public List<string> TestArray { get; set; }

        [JsonProperty("TestReference")]
        public CommonReferenceAPIModel TestReference { get; set; }

        [JsonProperty("TestReferenceArray")]
        public List<CommonReferenceAPIModel> TestReferenceArray { get; set; }



        #region Наверное не понадобится...

        //public string FirstName { get; set; }

        ///// <summary>
        ///// Last Name
        ///// </summary>

        //public string LastName { get; set; }

        ///// <summary>
        ///// Second Name
        ///// </summary>

        //public string SecondName { get; set; }

        ///// <summary>
        ///// Birth Date
        ///// </summary>

        //public string BirthDate { get; set; }

        ///// <summary>
        ///// Citizenship
        ///// </summary>

        //public string Citizenship { get; set; }

        ///// <summary>
        ///// Documents
        ///// </summary>

        //public List<string> Documents { get; set; }

        ///// <summary>
        ///// Permanent Address
        ///// </summary>

        //public string PermanentAddress { get; set; }

        ///// <summary>
        ///// Temporal Address
        ///// </summary>

        //public string TemporalAddress { get; set; }

        ///// <summary>
        ///// Civil Status
        ///// </summary>
        //public string CivilStatus { get; set; }

        ///// <summary>
        ///// Civil Status Date
        ///// </summary>
        //public string CivilStatusDate { get; set; }

        #endregion





        public PersonAPIModel GetTestRegistruPshisicalPersone()
        {
            PersonAPIModel viewModel = new PersonAPIModel
            {
                IDNP = "2564789654123",
                Name = "Ion",
                Surname = "Vasile",
                Country = "Moldova",
                Region = "Chisinau",
                Locality = "Chisinau",
                Street = "Stefac Cel Mare",
                House = "70",
                Block = "2",
                Flat = "34",
                DateOfBirth = DateTime.Now,
                Citizenship = "Moldavan",
                Photo = null,
                RegistrationType = 1,
                RegistrationStatus = 2,
                Patronymic = "Petru",
                TestArray = new List<string>() {"1234567890123", "1234567890124", "1234567890125", "1234567890126", "1234567890127" },
                TestReference = new CommonReferenceAPIModel { ReferenceId = 8, ReferenceCode = 145},
                TestReferenceArray = new List<CommonReferenceAPIModel> { 
                                                                        new CommonReferenceAPIModel(4,14), 
                                                                        new CommonReferenceAPIModel(4,15), 
                                                                        new CommonReferenceAPIModel(4,16), 
                                                                        new CommonReferenceAPIModel(4,17), 
                                                                        new CommonReferenceAPIModel(4,18),
                                                                        new CommonReferenceAPIModel(4,19)
                                                                        }

            };

            return viewModel;

        }
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


    public class PhisicalPersoneAddress
    {
        public string Country { get; set; }

        public string Region { get; set; }

        public string Locality { get; set; }

        public string Street { get; set; }

        public string House { get; set; }

        public string Block { get; set; }

        public string Flat { get; set; }

    }
}
