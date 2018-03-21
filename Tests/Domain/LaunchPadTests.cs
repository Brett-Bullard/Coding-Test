using System;
using SmileDirectClub.CodingTest.Domain;
using Xunit;

namespace SmileDirectClub.CodingTest.Tests
{
    public class LaunchpadTests
    {

        /// <summary>
        /// Name the is null on launch pad constructor should throw argument null exception.
        /// </summary>
        [Fact]
        public void LaunchPad_NameIsNullOnLaunchPadConstructor_ShouldThrowArgumentNullException()
        {
            Assert.Throws(typeof(ArgumentNullException), () => new LaunchPad("2", null, "status")); 
        }

        /// <summary>
        /// Status the is null on launch pad constructor should throw argument null exception.
        /// </summary>
        [Fact]
        public void LaunchPad_StatusIsNullOnLaunchPadConstructor_ShouldThrowArgumentNullException()
        {
            Assert.Throws(typeof(ArgumentNullException), () => new LaunchPad("2", "name", null));
        }

        /// <summary>
        /// Id the is null on launch pad constructor should throw argument null exception.
        /// </summary>
        [Fact]
        public void LaunchPad_IdIsNullOnLaunchPadConstructor_ShouldThrowArgumentNullException()
        {
            Assert.Throws(typeof(ArgumentNullException), () => new LaunchPad(null, "name", "status"));
        }
    }
}
