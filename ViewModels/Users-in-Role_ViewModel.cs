using System.Collections;
using mabuddy.Models;

namespace mabuddy.ViewModels
{
    public class Users_in_Role_ViewModel
    {
        public ApplicationUser User { get; set; }

        public string Role { get; set; }

        public string SelectedRole { get; set; }
    }
}