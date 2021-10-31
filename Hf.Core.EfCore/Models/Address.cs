using System.ComponentModel.DataAnnotations;

namespace Hf.Core.EfCore.Models
{
    public class Address
    {
        [Key]
        public long Id { get; set; }
        public long CustomerId { get; set; }
        public string AddressName { get; set; }
        public bool IsActive { get; set; }

        public Customer Customer { get; set; }
    }
}
