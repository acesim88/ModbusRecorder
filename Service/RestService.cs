using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;

namespace ModbusRecorder
{
    public class RestService : IRestService
    {
        private RestClient _client;
        private RestRequest _request;

        public RestService()
        {

        }
        public async Task<T> GetData<T>(string url, string requestString)
        {
            _client = new RestClient(url);

            _request = new RestRequest(requestString, DataFormat.Json);

            CancellationToken cancellationToken = new CancellationToken();

            var response = await _client.ExecuteGetAsync(_request, cancellationToken);

            return JsonConvert.DeserializeObject<T>(response.Content);


            //string apiKey = "ABnLWVga2jJxvS2ybF5iklrFh";
            //string apiKeySecret = "PFT84QDwaINeeiYuz8tIhnf1kUaomCsHfh26wYu2XpPeNtEr3r";
            //string accessToken = "1277490637047312384-IstXswNci6Fs2IwhHNiVCI4yhQhdJY";
            //string accessTokenSecret = "EpDmSfcp4kkuW0nBi01fivhVnBYSzjaj934Atzp5qFQDO";

            //_client = new RestClient("https://api.twitter.com/1.1");

            //_client.Authenticator = OAuth1Authenticator.ForProtectedResource(apiKey, apiKeySecret, accessToken, accessTokenSecret);

            //_request = new RestRequest("statuses/home_timeline.json", DataFormat.Json);

            //CancellationToken cancellationToken = new CancellationToken();

            //var response = await _client.ExecuteGetAsync(_request, cancellationToken);

            //return JsonConvert.DeserializeObject<T>(response.Content);

        }
    }
}