using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ALtar_WBS.Model
{
	public class SubjectCategories
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int CategoryID { get; set; }
		public string CategoryName { get; set; }
		public ICollection<Subjects> Subjects { get; set; }
	}
}
