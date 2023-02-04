using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class AppUser : IdentityUser
    {
        public string Nickname { get; set; }
        public DateTime LastLogIn { get; set; }
        public DateTime CreationTime { get; set; }
        public int LanguageId { get; set; }
        [ForeignKey("LanguageId")]
        public Language Language { get; set; }
    }
}