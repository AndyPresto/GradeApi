using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvLogic.model
{
    public class GetGradesResult
    {
        public bool Success { get; set; }
        public List<StudentGrade> Grades { get; set; }
    }
}
