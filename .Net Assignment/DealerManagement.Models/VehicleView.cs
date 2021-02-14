using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealerManagement.Models
{
    public class VehicleView
    {

        public int Id { get; set; }
        public int CustomerId { get; set; }
        [Required]
        public string LicensePlate { get; set; }
        [Required]
        public string ChassisNo { get; set; }
        [Required]
        public Nullable<System.DateTime> RegistrationDate { get; set; }
        [Required]
        public string Make { get; set; }
        [Required]
        public string Model { get; set; }
    }
}
