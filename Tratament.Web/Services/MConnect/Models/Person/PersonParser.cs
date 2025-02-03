using System.Xml;
using Tratament.Model.Models.ExternalServices;

namespace Tratament.Web.ServicesModels.PhisicalPerson
{
    public class PersonParser
    {
        public PersonParser()
        {

        }


        /// <summary>
        /// Solid - S - Ответственность метода - Распарсить XML документ, который должен содержать данные Person
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<PersonAPIModel> GetParsedPerson(XmlDocument document)
        {
            PersonAPIModel personAPI = new PersonAPIModel();
            try
            {
                XmlNodeList faultStrings = document.GetElementsByTagName("faultstring");

                if (faultStrings.Count == 0)
                {
                    XmlNode person = document.GetElementsByTagName("Person")[0];

                    personAPI.IDNP = person.ChildNodes[0].InnerText;

                    personAPI.Name = person.ChildNodes[1].InnerText; 

                    personAPI.Surname = person.ChildNodes[2].InnerText;
                    personAPI.Patronymic = person.ChildNodes[3].InnerText;

                    personAPI.DateOfBirth = Convert.ToDateTime(person.ChildNodes[4].InnerText);

                    XmlNode address = person.ChildNodes[14];

                    if(address != null)
                    {
                        personAPI.PersoneAddress.AdministrativeCode = address.ChildNodes[0].InnerText;

                        personAPI.PersoneAddress.Block = address.ChildNodes[1].InnerText;
                        personAPI.PersoneAddress.Country = address.ChildNodes[2].InnerText;

                        personAPI.PersoneAddress.CountyCode = address.ChildNodes[3].InnerText;

                        personAPI.PersoneAddress.Flat = address.ChildNodes[4].InnerText;

                        personAPI.PersoneAddress.House = address.ChildNodes[5].InnerText;

                        personAPI.PersoneAddress.Locality = address.ChildNodes[6].InnerText;

                        personAPI.PersoneAddress.Region = address.ChildNodes[7].InnerText;
                        personAPI.PersoneAddress.Street = address.ChildNodes[8].InnerText;
                    }
                }
                else
                {
                    throw new ApplicationException("FaultStrings Error: " + faultStrings[0].ChildNodes[0].InnerText);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("GetPersonDataForActualization  Error: " + ex);
            }

            return personAPI;
        }
    }
}
