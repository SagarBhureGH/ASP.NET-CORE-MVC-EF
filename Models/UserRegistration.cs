using System.CodeDom.Compiler;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_Project2024.Models
{
    public class UserRegistration
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }


        public string NameId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string BloodGroup { get; set; }



        [Required]
        [DataType(DataType.EmailAddress)] 
        public string Email { get; set; }


        [Required]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
    }
}
