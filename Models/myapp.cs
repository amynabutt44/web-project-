using Microsoft.AspNetCore.Identity;

namespace WebApplication2.Models
{
    public class myapp : IdentityUser
    {

        public string firstName { get; set; }
        public string lastName { get; set; }
        public string city { get; set; }

        public string country { get; set; }


    }
}
