using Microsoft.AspNetCore.Identity;
using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Zadatak1.Models
{
    public class User : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public bool UserRole { get; set; }
        public bool LoginPermission { get; set; }
    }
}
