using MAIeDosar.API.ServicesModels.Org;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml;

namespace MAIeDosar.API.Services.MConnect
{
    public class OrganizationParser
    {

        public OrganizationParser()
        {
           

        }

        /// <summary>
        /// Solid - S - Ответственность метода - Распарсить XML документ, который должен содержать данные Organization
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<OrganizationServiceResult> GetParsedOrganization(XmlDocument document)
        {
            OrganizationServiceResult jurJson = new OrganizationServiceResult();
            try
            {
                //(, XmlNode node) = await GetResponse(search.IDNO);
                XmlNodeList faultStrings = document.GetElementsByTagName("faultstring");

                if (faultStrings.Count == 0)
                {

                    jurJson.IDNO = document.GetElementsByTagName("IDNO")[0].InnerText;
                    jurJson.UniqueIdentificationCode = document.GetElementsByTagName("UniqueIdentificationCode")[0].InnerText;
                    jurJson.FullNameOfLegalEntity = document.GetElementsByTagName("Name")[0].InnerText;
                    jurJson.ShortNameOfLegalEntity = document.GetElementsByTagName("ShortName")[0].InnerText;
                    jurJson.State = Convert.ToInt32(document.GetElementsByTagName("State")[0].InnerText);
                    jurJson.LegalForm = Convert.ToInt32(document.GetElementsByTagName("LegalForm")[0].InnerText);

                    XmlNode organizationAddress = document.GetElementsByTagName("Address")[0]; // Organization Address;

                    jurJson.OrganizationAddress.Country = organizationAddress.ChildNodes[0].InnerText;
                    jurJson.OrganizationAddress.Region = organizationAddress.ChildNodes[1].InnerText;
                    jurJson.OrganizationAddress.Locality = organizationAddress.ChildNodes[2].InnerText;
                    jurJson.OrganizationAddress.AdministrativeCode = Convert.ToInt32(organizationAddress.ChildNodes[3].InnerText);
                    jurJson.OrganizationAddress.Street = organizationAddress.ChildNodes[4].InnerText;
                    jurJson.OrganizationAddress.House = organizationAddress.ChildNodes[5].InnerText;


                    XmlNodeList activities = document.GetElementsByTagName("Activity"); // Activities
                    List<Activity> orgAtcivities = new List<Activity>();
                    for (int i = 0; i < activities.Count; i++)
                    {
                        Activity activity = new Activity
                        {
                            Type = activities[i].ChildNodes[0].InnerText,
                            Code = activities[i].ChildNodes[1].InnerText
                        };
                        orgAtcivities.Add(activity);
                    }
                    jurJson.Activities = orgAtcivities;


                    XmlNodeList administrators = document.GetElementsByTagName("Administrators"); // Administrators
                    for (int i = 0; i < administrators.Count; i++)
                    {
                        XmlNode administrator = administrators[i].ChildNodes[0];
                        Administrator admin = new Administrator
                        {
                            Role = administrator.ChildNodes[0].InnerText,
                            IDNP = administrator.ChildNodes[1].InnerText,
                            FamilyName = administrator.ChildNodes[2].InnerText,
                            GivenName = administrator.ChildNodes[3].InnerText,
                        };

                        XmlNode administratorAddress = administrator.ChildNodes[4];
                        Address adminAddress = new Address
                        {
                            Country = administratorAddress.ChildNodes[0].InnerText,
                            Region = administratorAddress.ChildNodes[1].InnerText,
                            Locality = administratorAddress.ChildNodes[2].InnerText,
                            AdministrativeCode = Convert.ToInt32(administratorAddress.ChildNodes[3].InnerText),
                            Street = administratorAddress.ChildNodes[4].InnerText,
                            Block = administratorAddress.ChildNodes[5].InnerText
                        };

                        admin.AdministratorAddress = adminAddress;
                        jurJson.Administrators.Add(admin);
                    }
                }
                else
                {
                    throw new ApplicationException("FaultStrings Error: " + faultStrings[0].ChildNodes[0].InnerText);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("GetLegalEntity Error: " + ex);
            }

            return jurJson;
        }

        



    }
}

