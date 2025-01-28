using MAIeDosar.API.ServicesModels.Org;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MAIeDosar.API.ApiViewModels.ExternalServices
{
    /// <summary>
    /// OrgApiModel -> ApiViewModels / ExternalServices
    /// OrganizationAPIModel -> OrganizationServiceResult
    /// 
    /// Naming Recommendations
    /// APIModel - OrgAPIResult
    /// ServiceModel - OrganizationServiceResult
    /// 
    /// MConnectConfig - core - 
    /// </summary>
    public class Organization
    {
        [JsonProperty("IDNO")]
        public string IDNO { get; set; }


        [JsonProperty("UniqueIdentificationCodePJ")]
        public string UniqueIdentificationCodePJ { get; set; }


        [JsonProperty("EntityFullNamPJ")]
        public string EntityFullNamPJ { get; set; }


        [JsonProperty("EntityShortNamePJ")]
        public string EntityShortNamePJ { get; set; }


        [JsonProperty("StatePJ")]
        public int StatePJ { get; set; }


        [JsonProperty("LegalFormPJ")]
        public int LegalFormPJ { get; set; }


        [JsonProperty("CountryPJ")]
        public string CountryPJ { get; set; }


        [JsonProperty("RegionPJ")]
        public string RegionPJ { get; set; }


        [JsonProperty("LocalityPJ")]
        public string LocalityPJ { get; set; }


        [JsonProperty("AdministrativeCodePJ")]
        public int AdministrativeCodePJ { get; set; }


        [JsonProperty("StreetPJ")]
        public string StreetPJ { get; set; }


        [JsonProperty("HousePJ")]
        public string HousePJ { get; set; }


        [JsonProperty("BlockPJ")]
        public string BlockPJ { get; set; }


        [JsonProperty("FlatPJ")]
        public string FlatPJ { get; set; }


        //[JsonProperty("TypePJ")]
        //public int TypePJ { get; set; }


        //[JsonProperty("CodePJ")]
        //public int CodePJ { get; set; }


        [JsonProperty("IDNPAdmin")]
        public string IDNPAdmin { get; set; }


        [JsonProperty("LastNameAdmin")]
        public string LastNameAdmin { get; set; }


        [JsonProperty("FirstNameAdmin")]
        public string FirstNameAdmin { get; set; }


        [JsonProperty("RoleAdmin")]
        public string RoleAdmin { get; set; }


        [JsonProperty("RegionAdmin")]
        public string RegionAdmin { get; set; }


        [JsonProperty("LocalityAdmin")]
        public string LocalityAdmin { get; set; }


        [JsonProperty("AdministrativeCodeAdmin")]
        public int AdministrativeCodeAdmin { get; set; }


        [JsonProperty("StreetAdmin")]
        public string StreetAdmin { get; set; }


        [JsonProperty("HouseAdmin")]
        public string HouseAdmin { get; set; }


        [JsonProperty("BlockAdmin")]
        public string BlockAdmin { get; set; }


        [JsonProperty("FlatAdmin")]
        public string FlatAdmin { get; set; }


        public static Organization ConvertToOrganization(OrganizationServiceResult orgApiModel)
        {
            Organization org = new Organization();

            org.IDNO = orgApiModel.IDNO;
            org.UniqueIdentificationCodePJ = orgApiModel.UniqueIdentificationCode;
            org.EntityFullNamPJ = orgApiModel.FullNameOfLegalEntity;
            org.EntityShortNamePJ = orgApiModel.ShortNameOfLegalEntity;
            org.StatePJ = orgApiModel.State;
            org.LegalFormPJ = orgApiModel.LegalForm;

            // Organisation Address 
            org.CountryPJ = orgApiModel.OrganizationAddress.Country;
            org.RegionPJ = orgApiModel.OrganizationAddress.Region;
            org.LocalityPJ = orgApiModel.OrganizationAddress.Locality;
            org.AdministrativeCodePJ = orgApiModel.OrganizationAddress.AdministrativeCode;
            org.StreetPJ = orgApiModel.OrganizationAddress.Street;
            org.HousePJ = orgApiModel.OrganizationAddress.House;
            org.BlockPJ = orgApiModel.OrganizationAddress.Block;
            org.FlatPJ = orgApiModel.OrganizationAddress.Flat;

            // Administrator
            var orgAdmin = orgApiModel.Administrators.FirstOrDefault();
            org.IDNPAdmin = orgAdmin.IDNP;
            org.LastNameAdmin = orgAdmin.FamilyName;
            org.FirstNameAdmin = orgAdmin.GivenName;
            org.RoleAdmin = orgAdmin.Role;

            // Administrator Address
            org.RegionAdmin = orgAdmin.AdministratorAddress.Region;
            org.LocalityAdmin = orgAdmin.AdministratorAddress.Locality;
            org.AdministrativeCodeAdmin = orgAdmin.AdministratorAddress.AdministrativeCode;
            org.StreetAdmin = orgAdmin.AdministratorAddress.Street;
            org.HouseAdmin = orgAdmin.AdministratorAddress.House;
            org.BlockAdmin = orgAdmin.AdministratorAddress.Block;
            org.FlatAdmin = orgAdmin.AdministratorAddress.Flat;

            return org;
        }
    }
}
