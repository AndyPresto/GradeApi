using CsvLogic;
using Microsoft.AspNetCore.Mvc;

namespace GradeApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GradeController : ControllerBase
    {
        private readonly IGradesData _gradesData;

        public GradeController(IGradesData gradesData)
        {
            _gradesData = gradesData;
        }

        [HttpGet(Name = "GetGrades")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _gradesData.GetGrades());
        }
    }
}