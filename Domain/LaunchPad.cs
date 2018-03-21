using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json.Linq;

namespace SmileDirectClub.CodingTest.Domain
{
    public class LaunchPad: IEquatable<LaunchPad>
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Status { get; set; }

        public LaunchPad(string id, string name, string status)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Status = status ?? throw new ArgumentNullException(nameof(status));
        }

        public bool Equals(LaunchPad other)
        {
            return other.Id == Id && other.Name == Name && other.Status == Status;
        }

    }

}
