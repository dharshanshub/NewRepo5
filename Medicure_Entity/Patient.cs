using System.ComponentModel.DataAnnotations;

namespace Medicure_Entity
{
    public class Patient
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "This field must not be empty")]
        public string Name { get; set; }
        [Required(ErrorMessage = "This field must not be empty")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number should contain only 10 digits")]
        [DataType(DataType.PhoneNumber)]
        public string MobileNo { get; set; }
        public string DateOfReg  { get; set; }
        [Required(ErrorMessage = "This field must not be empty")]
        public string Username { get; set; }
        [Required(ErrorMessage = "This field must not be empty")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$", ErrorMessage = "password should contain Minimum eight characters, at least one uppercase letter, one lowercase letter and one number")]

        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
