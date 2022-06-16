using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomeProjectOnPharmacies.DB.Models
{
    public class BatchModel
    {
        public Guid Id { get; set; }
        public Guid StoreId { get; set; }
        public Guid NomenclatureId { get; set; }
        public int Count { get; set; }
    }
}
