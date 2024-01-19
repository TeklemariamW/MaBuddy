using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace mabuddy.Models
{
    public class Tips
    {
        [Required] public int Id { get; set; }


        [Required] public string TipTitle { get; set; }

        [Required] public string TipText { get; set; }
    }
}