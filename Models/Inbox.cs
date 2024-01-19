using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace mabuddy.Models
{
    public class Inbox
    {
        [Required] public int Id { get; set; }

        [Display(Name = "Title")] [Required] public string Title { get; set; }

        [Display(Name = "Time")] [Required] public string Time { get; set; } = DateTime.Now.ToString("dd.MM.yyyy");

        [Display(Name = "Content")] [Required] public string Content { get; set; }

        [Display(Name = "Link")] public string Link { get; set; }

        public string Math { get; set; }

        public string ApplicationUserID { get; set; }


        public string To { get; set; }

        [Display(Name = "Nickname")] public ApplicationUser ApplicationUser { get; set; }
    }
}