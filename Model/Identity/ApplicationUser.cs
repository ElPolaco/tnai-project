using Microsoft.AspNetCore.Identity;
using Model.Entities;

namespace Model.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Comment> Comments { get; set; }
    }
}
