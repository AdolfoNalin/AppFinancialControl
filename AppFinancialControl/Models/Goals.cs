using System;
using System.Collections.Generic;
using System.Text;

namespace AppFinancialControl.Models
{
    public class Goals
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
}
