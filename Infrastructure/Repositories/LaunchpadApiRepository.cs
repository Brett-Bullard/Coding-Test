using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using SmileDirectClub.CodingTest.Domain;
using SmileDirectClub.CodingTest.Infrastructure.Interfaces;

namespace SmileDirectClub.CodingTest.Infrastructure.Repositories
{
    public class LaunchpadApiRepository:ILaunchPadRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger; 

        public LaunchpadApiRepository(IConfiguration configuration, ILogger<LaunchpadApiRepository> logger)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger)); 
        }

        /// <summary>
        /// Gets the launch pad list
        /// </summary>
        /// <returns>Task getting the launchpad list</returns>
        /// <param name="status">Status of launchpad</param>
        /// <param name="name">Shortname of launchpad, not full name</param>
        /// <param name="region">Region location of launchpad</param>
        public async Task<List<LaunchPad>> GetAllAsync(string status=null, string name=null, string region=null)
        {
            JToken responseJtoken = await GetApiResponse(_configuration["LAUNCHPAD_URL"]);
            List<LaunchPad> launchpads = new List<LaunchPad>();
            foreach (JToken child in responseJtoken)
            {
                if((status==null || child["status"].ToString().Equals(status, StringComparison.InvariantCultureIgnoreCase)) 
                   && (name == null || child["location"]["name"].ToString().Equals(name, StringComparison.InvariantCultureIgnoreCase))
                   &&(region == null || child["location"]["region"].ToString().Equals(region, StringComparison.InvariantCultureIgnoreCase)))
                    launchpads.Add(new LaunchPad(child["id"].ToString(), child["full_name"].ToString(), child["status"].ToString()));
            }
            return launchpads;
        }

        private async Task<JToken> GetApiResponse(string url)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage responseMessage = await client.GetAsync(_configuration["LAUNCHPAD_URL"]);
            string responseString = await responseMessage.Content.ReadAsStringAsync();
            _logger.LogInformation("Response string from " + _configuration["LAUNCHPAD_URL"] + ": " + responseString);
            JToken responseJtoken = JToken.Parse(responseString);
            client.Dispose();
            return responseJtoken;
        }

        /// <summary>
        /// Gets a single launch pad matching 
        /// the specified id
        /// </summary>
        /// <returns>Task of getting single launchpad</returns>
        /// <param name="id">id parameter of launchpad</param>
        public async Task<LaunchPad> GetAsync(string id)
        {
            JToken responseToken = await GetApiResponse(_configuration["LAUNCHPAD_URL"] + "/" + id);
            return new LaunchPad(responseToken[0]["id"].ToString(), responseToken[0]["full_name"].ToString(), responseToken[0]["status"].ToString());
                
        }

    }
}
