using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medicure_Entity
{
    public class Supplier_Log
    {
        public int Supplier_Id { get; set; }
        public int Physician_ID { get; set; }
        public int Drug_id { get; set; }
        public int Qty { get; set; }
        public string Date { get; set; }
    }
}
