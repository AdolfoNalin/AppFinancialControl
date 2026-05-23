using System;
using System.Collections.Generic;
using System.Text;

namespace AppFinancialControl.Models
{
    public class Client
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public decimal Salary { get; set; }
    }
}
