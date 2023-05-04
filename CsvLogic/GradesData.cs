using System.Globalization;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using CsvHelper;
using CsvLogic.model;

namespace CsvLogic
{
    public interface IGradesData
    {
        public Task<GetGradesResult> GetGrades();
    }

    public class GradesData : IGradesData
    {
        public async Task<GetGradesResult> GetGrades()
        {
            var result = new GetGradesResult();

            Stream csvData = await GetCSVData();
            result.Grades = TranslateCsvToJson(csvData);

            return result;
        }

        private async Task<Stream> GetCSVData()
        {
            using HttpClient client = new();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");
            var result = await client.GetAsync("https://interviewcsv.blob.core.windows.net/csv/grades.csv");

            return await result.Content.ReadAsStreamAsync();
        }

        private List<StudentGrade> TranslateCsvToJson(Stream csvData)
        {
            var result = new List<StudentGrade>();
            try
            {
                using (var reader = new StreamReader(csvData))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    result = csv.GetRecords<StudentGrade>().ToList();
                }

                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}