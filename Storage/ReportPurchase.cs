using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage
{
    class ReportPurchase
    {
        public string Date { get; set; }
        public int IdPurchase { get; set; }
        public int IdCounteragent { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string MiddleName { get; set; }
        public string INN { get; set; }
    }
}
