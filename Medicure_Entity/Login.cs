using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medicure_Entity
{
    public class Login
    {
        public string Type { get; set; }
        [Required(ErrorMessage ="This field must not be empty")]
        public string Username { get; set; }

        [Required(ErrorMessage = "This field must not be empty")]

        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
