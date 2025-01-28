using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net;
using System.Text.Json.Serialization;
using Tratament.Web.ViewModels.Recaptcha;

namespace Tratament.Web.Options
{
    public class RecaptchaHelper
    {
        private  RecaptchatOption _options { get; set; }

        public RecaptchaHelper(IOptions<RecaptchatOption> options)
        {
            this._options = options.Value;
        }


        public RecaptchaResponse ValidateCaptha(string response)
        {
            using (var client = new WebClient())
            {
                string secret = _options.SecretKey;
                string url = $"{_options.Url}secret={secret}&response={response}";

                var result = client.DownloadString(url);

                try
                {
                    var data = JsonConvert.DeserializeObject<RecaptchaResponse>(result.ToString());

                    return data;

                }
                catch (Exception ex)
                {
                    return default(RecaptchaResponse);
                }
            }

          
        }


    }
}
