using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace HSPAWebAPI.Models
{
    public class City
    {
        [Key]
        public int id { get; set; }
        public string Name { get; set; }

        public string? Country { get; set; }

        public DateTime LastUpdatedOn { get; set; }

        public int LastUpdatedBy { get; set; }
    }
}
