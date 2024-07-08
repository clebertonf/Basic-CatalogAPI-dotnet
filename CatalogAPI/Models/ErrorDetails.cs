using System.Text.Json;

namespace CatalogAPI.Models
{
    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string? ErrorMessage { get; set; }
        public string? StackTrace { get; set; }
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
