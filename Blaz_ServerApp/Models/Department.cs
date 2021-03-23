using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Blaz_ServerApp.Models
{
    public partial class Department
    {
        public Department()
        {
            Employees = new HashSet<Employee>();
        }
        [Required(ErrorMessage ="DeptNo is must")]
        public int DeptNo { get; set; }
        [Required(ErrorMessage = "DeptName is must")]
        public string DeptName { get; set; }
        [Required(ErrorMessage = "Location is must")]
        public string Location { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
