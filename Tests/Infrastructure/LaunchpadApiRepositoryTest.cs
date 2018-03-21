using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SmileDirectClub.CodingTest.Domain;
using SmileDirectClub.CodingTest.Infrastructure.Repositories;
using Xunit;

namespace SmileDirectClub.CodingTest.Tests.Infrastructure
{
    public class LaunchpadApiRepositoryTest
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<LaunchpadApiRepository> _logger; 

        public LaunchpadApiRepositoryTest()
        {
            _configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            _logger =  new LoggerFactory().AddConsole().CreateLogger<LaunchpadApiRepository>();
        }

        /// <summary>
        /// Call launch pad get all 
        /// and verfies that the result set is not 
        /// null and the count is greater than 0
        /// </summary>
        [Fact]
        public async Task Repo_GetAll_ReturnsNonNullResultSet()
        {
            LaunchpadApiRepository repo = new LaunchpadApiRepository(_configuration, _logger);
            List<LaunchPad> launchPadList= await  repo.GetAllAsync();
            Assert.NotNull(launchPadList);
            Assert.True(launchPadList.Count > 0);
        }

        /// <summary>
        /// Call launch pad get all 
        /// then takes the first one from that list
        /// and calls GetAsync and verifies the launch pad 
        /// matches what is in teh list
        /// </summary>
        [Fact]
        public async Task Repo_GetListThenIndividualLaunchPad_ReturnsMatchingLaunchpad()
        {
            LaunchpadApiRepository repo = new LaunchpadApiRepository(_configuration, _logger);
            List<LaunchPad> launchPadList = await repo.GetAllAsync();
            LaunchPad launchpad = await repo.GetAsync(launchPadList[0].Id);
            Assert.NotNull(launchpad);
            Assert.True(launchPadList[0].Equals(launchpad));                             
        }

    }
}
