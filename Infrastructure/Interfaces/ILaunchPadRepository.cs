using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SmileDirectClub.CodingTest.Domain;

namespace SmileDirectClub.CodingTest.Infrastructure.Interfaces
{
    public interface ILaunchPadRepository
    {
        /// <summary>
        /// Gets the launch pad list
        /// </summary>
        /// <returns>Task getting the launchpad list</returns>
        /// <param name="status">Status of launchpad</param>
        /// <param name="name">Shortname of launchpad, not full name</param>
        /// <param name="region">Region location of launchpad</param>
        Task<List<LaunchPad>> GetAllAsync(string status=null, string name=null, string region=null);

        /// <summary>
        /// Gets a single launch pad matching 
        /// the specified id
        /// </summary>
        /// <returns>Task of getting single launchpad</returns>
        /// <param name="id">id parameter of launchpad</param>
        Task<LaunchPad> GetAsync(string id); 
    }
}
