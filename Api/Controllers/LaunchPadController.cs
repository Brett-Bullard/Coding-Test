using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmileDirectClub.CodingTest.Infrastructure.Interfaces;
using SmileDirectClub.CodingTest.Domain;

namespace SmileDirectClub.CodingTest.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class LaunchPadController : Controller
    {
        private readonly ILogger<LaunchPadController> _logger;
        private readonly ILaunchPadRepository _launchPadRepository; 
        public LaunchPadController(ILogger<LaunchPadController> logger, ILaunchPadRepository launchPadRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _launchPadRepository = launchPadRepository ?? throw new ArgumentNullException(nameof(launchPadRepository));


        }
         


        /// <summary>
        /// Get entire list of launch pads. Service returns a 204 
        /// if no results or found. A 500 is returned if there is an error
        /// getting all launch pads. 
        /// </summary>
        /// <returns>List of launch pads matching specified parameters</returns>
        /// <param name="name">Short name of launch pad, not full name</param>
        /// <param name="status">Status of launch pad. Choices are active, under constuction or retired</param>
        /// <param name="region">Region of launch pad</param>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string name, [FromQuery]string status, [FromQuery] string region)
        {
            try
            {
                List<LaunchPad> launchPadList = await _launchPadRepository.GetAllAsync(status, name, region);
                if (launchPadList == null || launchPadList.Count == 0)
                    return StatusCode(204, "No results found");
                else
                    return Ok(launchPadList); 
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }

        }

        /// <summary>
        /// Returns launch pad with matching ID. If not result is found a 
        /// 204 is returned. If an error occurs a 500 is returned.
        /// </summary>
        /// <returns>Launch pad matching ID</returns>
        /// <param name="id">String Id of launch pad</param>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            try
            {
                LaunchPad launchPad = await _launchPadRepository.GetAsync(id);
                if (launchPad == null)
                    return StatusCode(204, "No results found");
                else
                    return Ok(launchPad);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
        }

    }
}
