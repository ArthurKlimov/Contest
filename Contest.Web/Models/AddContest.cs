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
        [StringLength(140, MinimumLength = 1)]
        public string Title { get; set; }

        [Required]
        [StringLength(75, MinimumLength = 1)]
        public string Link { get; set; }

        [StringLength(75)]
        public string City { get; set; }

        public IFormFile CoverImage { get; set; }

        public bool AcrossCountry { get; set; }
    }
}
