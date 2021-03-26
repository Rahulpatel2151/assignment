using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HRM.Models
{
    public class DepartmentView
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
