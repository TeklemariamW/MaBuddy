namespace mabuddy.Models
{
    public class AddLesson
    {
        public int Id { get; set; }
        public string LessonTitle { get; set; }
        public string LessonUrl { get; set; }
        public int Level { get; set; }
        public string Subject { get; set; }
    }
}