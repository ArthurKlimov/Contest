using Microsoft.AspNetCore.Http;
using System;

namespace Contest.Web.Models
{
    public class AddContest
    {
        public DateTime EndDate { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public IFormFile FormFile { get; set; }
    }
}
