using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ALtar_WBS.Model
{
	public class Subjects
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int SubjectID { get; set; }
		public string SubjectName { get; set; }
		public string Level {  get; set; }
		public int CategoryID { get; set; }
		public SubjectCategories SubjectCategories { get; set; }
		public ICollection<CourseSubject> CourseSubjects { get; set; }
	}
}
