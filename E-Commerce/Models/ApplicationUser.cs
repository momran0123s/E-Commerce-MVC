
using Microsoft.AspNetCore.Identity;

namespace E_Commerce.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? Address { get; set; }

        public virtual List<Order>? Orders { get; set; } = new List<Order>();
        public virtual Cart Cart { get; set; }

    }
}
