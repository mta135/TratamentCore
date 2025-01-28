using MAIeDosar.API.ApiViewModels.ExternalServices;
using System;
using System.Threading.Tasks;
using System.Xml;

namespace MAIeDosar.API.ServicesModels.PhisicalPerson
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

                    personAPI.Name = person.ChildNodes[1].InnerText; // Имя
                    personAPI.Surname = person.ChildNodes[0].InnerText;// Фамилия
                    personAPI.DateOfBirth = Convert.ToDateTime(person.ChildNodes[2].InnerText);
                    personAPI.Citizenship = person.ChildNodes[3].InnerText;

                    //var Photo = person.ChildNodes[5].InnerText;
                    XmlNode identityDocument = person.ChildNodes[6]; // IdentityDocument 

                    personAPI.Type = Convert.ToInt32(identityDocument.ChildNodes[0].InnerText);
                    personAPI.Series = identityDocument.ChildNodes[1].InnerText;
                    personAPI.DocNumber = identityDocument.ChildNodes[2].InnerText;
                    personAPI.IssueDate = Convert.ToDateTime(identityDocument.ChildNodes[3].InnerText);
                    personAPI.IssuedBy = identityDocument.ChildNodes[4].InnerText;
                    personAPI.Status = Convert.ToInt32(identityDocument.ChildNodes[5].InnerText);

                    XmlNode registration = person.ChildNodes[7].ChildNodes[0]; // Registrations
                    if (registration != null)
                    {
                        personAPI.RegistrationType = Convert.ToInt32(registration.ChildNodes[0].InnerText);
                        personAPI.RegistrationStatus = Convert.ToInt32(registration.ChildNodes[1].InnerText);

                        XmlNode address = registration.ChildNodes[2]; // Address
                        if (address != null)
                        {
                            personAPI.Country = address.ChildNodes[0].InnerText;
                            personAPI.Region = address.ChildNodes[1].InnerText;
                            personAPI.Locality = address.ChildNodes[2].InnerText;
                            personAPI.Street = address.ChildNodes[4].InnerText;
                            personAPI.House = address.ChildNodes[5].InnerText;
                            personAPI.Block = address.ChildNodes[6].InnerText;
                            personAPI.Flat = address.ChildNodes[7].InnerText;
                        }
                    }
                }
                else
                {
                    throw new ApplicationException("FaultStrings Error: " + faultStrings[0].ChildNodes[0].InnerText);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("GetPerson Error: " + ex);
            }

            return personAPI;
        }
    }
}
