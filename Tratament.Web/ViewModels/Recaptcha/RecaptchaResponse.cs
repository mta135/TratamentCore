using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Tratament.Web.ViewModels.Recaptcha
{
    public class RecaptchaResponse
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("error-codes")]
        public List<string> ErrorMessage { get; set; }
    }
}
