using MAIeDosar.API.ServicesModels.Civil;
using MAIeDosar.API.ServicesModels.Org;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MAIeDosar.API.Services.MConnect
{
    public class InputValidator
    {
        public static Tuple<bool, string> PersonFilterValidation(PersonFilter personFilter)
        {
            bool isValid = false;
            string message = string.Empty;
            Regex regex = new Regex(@"^[a-zA-Z0-9]*$");

            if (string.IsNullOrEmpty(personFilter.IDNP))
                message = "Nu a fost introdus IDNP-ul... ";

            else if (personFilter.IDNP.Length < 13)
                message = "Tipul IDNP-ului nu este corect. Are mai putin de 13 caractere. IDNP: " + personFilter.IDNP;

            else if (!regex.IsMatch(personFilter.IDNP))
                message = "IDNP-ul cotine caratere interzise. IDNP: " + personFilter.IDNP;
            else
                isValid = true;
            return new Tuple<bool, string>(isValid, message);
        }

        public static Tuple<bool, string> OrganizationFilterValidation(OrganizationFilter personFilter)
        {
            bool isValid = false;
            string message = string.Empty;
            Regex regex = new Regex(@"^[a-zA-Z0-9]*$");

            if (string.IsNullOrEmpty(personFilter.IDNO))
                message = "Nu a fost introdus IDNO... ";

            else if (!regex.IsMatch(personFilter.IDNO))
                message = "IDNO cotine caratere interzise. IDNO: " + personFilter.IDNO;
            else
                isValid = true;
            return new Tuple<bool, string>(isValid, message);
        }


    }
}
