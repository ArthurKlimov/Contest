using Contest.Web.Attributes;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace Contest.Web.Models
{
    public class AddContest
    {
        [Required]
        [EndDate]
        public DateTime EndDate { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Title { get; set; }

        [Required]
        [StringLength(75, MinimumLength = 1)]
        public string Link { get; set; }

        [StringLength(75)]
        public string City { get; set; }

        public bool AcrossCountry { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 1)]
        public string Organizator { get; set; }
    }
}
