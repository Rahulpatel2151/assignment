using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace HMS.Models.Models
{
    public class BookingsModel
    {
        public int Id { get; set; }
        [Required]
        public int RoomId { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public System.DateTime BookingDate { get; set; }

        public string Status { get; set; }
    }
}
