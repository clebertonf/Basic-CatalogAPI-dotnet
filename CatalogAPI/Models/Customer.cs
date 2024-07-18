using System.ComponentModel.DataAnnotations;

namespace CatalogAPI.Models
{
    public class Customer
    {
        [Key]
        public int CustmoerId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int CustomerCode { get; set; }
        public string? PhoneNumber { get; set; }
        public CustomerAddress? Address { get; set; }
    }
}
