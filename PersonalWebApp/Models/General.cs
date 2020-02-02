using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalWebApp.Models
{
    public class General
    {
        public Guid Id { get; set; }
        [Required]
        public string SiteColor { get; set; }
        public string SiteLogoPath { get; set; }
        [Required]
        public string SiteOwnerJob { get; set; }
        [Required]
        public string SiteOwnerName { get; set; }
        [Required]
        public string SiteOwnerEmail { get; set; }
        [Required]
        public string SiteOwnerCountry { get; set; }
        [Required]
        public string SiteOwnerAddress { get; set; }

    }
}
