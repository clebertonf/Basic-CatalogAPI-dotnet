using System.ComponentModel.DataAnnotations;

namespace CatalogAPI.Models
{
    public class Customer
    {
        [Key]
        public int CustmoerId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
    }
}
