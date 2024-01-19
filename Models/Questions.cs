using System.Collections.Generic;

namespace mabuddy.Models
{
    public class Questions
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public string FormulaImage { get; set; }
        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        public string Option4 { get; set; }
        public string CorrectAnswer { get; set; }

        public int Level { get; set; }
        public string Subject { get; set; }
    }
}