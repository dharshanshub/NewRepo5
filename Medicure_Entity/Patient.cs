using System.ComponentModel.DataAnnotations;

namespace Medicure_Entity
{
    public class Patient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string MobileNo { get; set; }
        public string DateOfReg  { get; set; }
        [Required(ErrorMessage = "This field must not be empty")]
        public string Username { get; set; }
        [Required(ErrorMessage = "This field must not be empty")]

        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
