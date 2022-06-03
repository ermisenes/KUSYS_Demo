using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KUSYS_DEMO.Domain.Entities
{
    public class StudentCourse
    {
        [Key]
        public int Id { get; set; }
        public string StudentId { get; set; }
        public string CourseId { get; set; }

        [ForeignKey(nameof(StudentId))]
        public virtual Student Student { get; set; }
        [ForeignKey(nameof(CourseId))]
        public virtual Course Course { get; set; }

    }
}
