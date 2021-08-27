using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRentManagement.Models
{
    public class HouseDetailsModel
    {
        public string Id { get; set; }
        public string HouseName { get; set; }
        [Required]
        public string HouseNo { get; set; }
        public string LandMark { get; set; }
        [Required]
        public string StreetName { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string PinCode { get; set; }
    }
}
