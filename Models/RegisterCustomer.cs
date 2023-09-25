using System.ComponentModel.DataAnnotations;

namespace CashierApi.Models
{
    public class RegisterCustomer
    {
        public string FirstName { get; set; }   

        public string LastName { get; set; }

        [RegularExpression(@"^\S+$",ErrorMessage = "Please enter a valid email")]
        public string Email { get; set; }
        
        public string Password { get; set; }    

        public string PhoneNumber { get; set; }

    }
}
