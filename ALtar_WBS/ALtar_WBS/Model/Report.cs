using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ALtar_WBS.Model
{
    public class Report
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReportID { get; set; }
        public string ReportName { get; set; }
        public string ReportType { get; set; }
        public string FilePath { get; set; }
    }
}
