using System.Collections.Generic;
using System.Linq;

namespace mabuddy.Models
{
    public class Quiz
    {
      public  int Id { get; set; }
      public string userEmail { get; set; }
      public string ApplicationUserId { get; set; }
      
      public ApplicationUser ApplicationUser { get; set; }     
      public List<Questions> Questions { get; set; }
    }
}