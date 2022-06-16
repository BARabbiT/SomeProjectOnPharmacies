using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomeProjectOnPharmacies.DB.Models
{
    public class StoreModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid ShopId { get; set; }
        public string ShopName { get; set; }
    }
}
