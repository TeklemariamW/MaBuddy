using System.Collections;
using System.Collections.Generic;

namespace mabuddy.Models
{
    public class Progress
    {
        public int Id { get; set; }
        public int CountCorrect { get; set; }
        public float Percent { get; set; }
        public int level { get; set; }
        public string subject { get; set; }
        public string userEmail { get; set; }
        public Quiz Quiz { get; set; }
    }
}