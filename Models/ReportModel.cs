using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pottymapbackend.Models
{
    public class ReportModel
    {
        public int ID { get; set; }
        public int UserId { get; set; }
        public int BathroomId { get; set; }
        public string Issue { get; set; }
        public string PriorityLevel { get; set; }
        public string Description { get; set; }
        public bool IsResolved { get; set; }

        public ReportModel()
        {
            
        }
    }
}