using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealerManagement.Models
{
    public class BookingView
    {
        public int Id { get; set; }
        public Nullable<int> ServiceId { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<int> VehicleId { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm}", ApplyFormatInEditMode = true)]
        public System.DateTime StartTime { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm}", ApplyFormatInEditMode = true)]
        public System.DateTime EndTime { get; set; }
        public string Status { get; set; }
    }
}
