using System.ComponentModel.DataAnnotations;

namespace Medicure_Entity
{
    public class Supplier
    {
       public int SupplierId{get; set;}
        [Required(ErrorMessage = "This field must not be empty")]
        public string Username { get; set; }
        [Required(ErrorMessage = "This field must not be empty")]

        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
