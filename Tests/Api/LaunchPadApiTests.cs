using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SmileDirectClub.CodingTest.Infrastructure.Repositories;
using System.Threading.Tasks;
using System.Net;
using SmileDirectClub.CodingTest.Domain;
using System.Collections.Generic;
using Xunit;
using System.Net.Http;
using Newtonsoft.Json;

namespace SmileDirectClub.CodingTest.Tests.Api
{
    public class LaunchPadApiTests
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<LaunchpadApiRepository> _logger;
        private readonly TestServer _server;
        private readonly HttpClient _client; 

        public LaunchPadApiTests()
        {
            _configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            _logger = new LoggerFactory().AddConsole().CreateLogger<LaunchpadApiRepository>();
            _server = new TestServer(new WebHostBuilder().UseStartup<CodingTest.Api.Startup>().UseConfiguration(_configuration));
            _client = _server.CreateClient();
        }

        public static IEnumerable<object[]> GetLaunchPadStatuses()
        {
            yield return new object[] { "active" };
            yield return new object[] { "under construction" };
            yield return new object[] { "retired" };
        }

        public static IEnumerable<object[]> GetLaunchPadNames()
        {
            yield return new object[] { "Boca Chica Village" };
            yield return new object[] { "Cape Canaveral" };
            yield return new object[] { "Omelek Island" };
        }

        public static IEnumerable<object[]> GetLaunchPadRegions()
        {
            yield return new object[] { "California" };
            yield return new object[] { "Florida" };
            yield return new object[] { "Marshall Islands" };
        }

        /// <summary>
        /// This tests calls the get all service and verifies that it returns at least one launch pad
        /// </summary>
        [Fact]
        public async Task GetAllService_ReturnsAtLeastOneLaunchPad()
        {
            HttpResponseMessage response = await _client.GetAsync("api/launchpad");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            List<LaunchPad> launchPadList = JsonConvert.DeserializeObject<List<LaunchPad>>(await response.Content.ReadAsStringAsync());
            Assert.False(launchPadList == null);
            Assert.True(launchPadList.Count > 0);

        }

        /// <summary>
        /// This tests calls the get all service, filters on status and verifies
        /// that  each launchpad returned has a status
        /// that matches the status in the request query string
        /// </summary>
        [Theory]
        [MemberData(nameof(GetLaunchPadStatuses))]
        public async Task GetAllServiceFilterByStatus_ListResultsHaveMatchingStatus(string status)
        {
            
            HttpResponseMessage response = await _client.GetAsync("api/launchpad?status="+status);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            List<LaunchPad> launchPadList = JsonConvert.DeserializeObject<List<LaunchPad>>(await response.Content.ReadAsStringAsync());
            foreach(LaunchPad result in launchPadList)
            {
                Assert.Equal(status, result.Status);
            }

        }

        /// <summary>
        /// This tests calls the get all service, filters on status and verifies
        /// that the result set is smaller than the get all result set and that
        /// at least one filtered results was returned.
        /// The test cannot verify that the filtered data matches since
        /// name is not a parameter returned. 
        /// </summary>
        [Theory]
        [MemberData(nameof(GetLaunchPadNames))]
        public async Task GetAllServiceFilterByName_VerifyResultsAreLessThanNoFilterResults(string name)
        {
            HttpResponseMessage getAllResponse = await _client.GetAsync("api/launchpad");
            List<LaunchPad> launchPadList = JsonConvert.DeserializeObject<List<LaunchPad>>(await getAllResponse.Content.ReadAsStringAsync()); 

            HttpResponseMessage filterdResponse = await _client.GetAsync("api/launchpad?name=" + name);
            Assert.Equal(HttpStatusCode.OK, filterdResponse.StatusCode);
            List<LaunchPad> filterLaunchPadList = JsonConvert.DeserializeObject<List<LaunchPad>>(await filterdResponse.Content.ReadAsStringAsync());
            Assert.True(launchPadList.Count > filterLaunchPadList.Count);
            Assert.True(filterLaunchPadList.Count > 0);

        }

        /// <summary>
        /// This tests calls the get all service, filters on regopm and verifies
        /// that the result set is smaller than the get all result set and 
        /// that at least one filted results was returned. 
        /// The test cannot verify that the filtered data matches since
        /// region is not a parameter returned. 
        /// </summary>
        [Theory]
        [MemberData(nameof(GetLaunchPadRegions))]
        public async Task GetAllServiceFilterByRegion_VerifyResultsAreLessThanNoFilterResults(string region)
        {
            HttpResponseMessage getAllResponse = await _client.GetAsync("api/launchpad");
            List<LaunchPad> launchPadList = JsonConvert.DeserializeObject<List<LaunchPad>>(await getAllResponse.Content.ReadAsStringAsync());

            HttpResponseMessage filterdResponse = await _client.GetAsync("api/launchpad?region=" + region);
            Assert.Equal(HttpStatusCode.OK, filterdResponse.StatusCode);
            List<LaunchPad> filterLaunchPadList = JsonConvert.DeserializeObject<List<LaunchPad>>(await filterdResponse.Content.ReadAsStringAsync());
            Assert.True(launchPadList.Count > filterLaunchPadList.Count);
            Assert.True(filterLaunchPadList.Count > 0);

        }

        /// <summary>
        /// This test calls the get all service, takes the first results and 
        /// calls the get service. It verifies the get service returns a non null
        /// result and that the get services's result matches the 
        /// first launch pad result from the original get all service call. 
        /// </summary>
        /// <returns>The single launch pad request identifier matches response identifier.</returns>
        [Fact]
        public async Task GetSingleLaunchPad_RequestIdMatchesResponseId()
        {
            HttpResponseMessage listResponse = await _client.GetAsync("api/launchpad");
            List<LaunchPad> launchPadList = JsonConvert.DeserializeObject<List<LaunchPad>>(await listResponse.Content.ReadAsStringAsync());
            LaunchPad firstLaunchPad = launchPadList[0];
            HttpResponseMessage singleLaunchPadResponse = await _client.GetAsync("api/launchpad/" + firstLaunchPad.Id);
            LaunchPad singleLaunchPad = JsonConvert.DeserializeObject<LaunchPad>(await singleLaunchPadResponse.Content.ReadAsStringAsync());
            Assert.False(singleLaunchPad == null);
            Assert.True(firstLaunchPad.Equals(singleLaunchPad));

        }




    }
}
