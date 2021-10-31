using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hf.Core.EfCore.Models
{
    public class Customer
    {

        public Customer()
        {
            Addresses = new HashSet<Address>();
        }

        [Key]
        public long Id { get; set; }

        public string FName { get; set; }
        public string LName { get; set; }
        public string NationalCode { get; set; }

        public ICollection<Address> Addresses { get; set; }

    }
}
