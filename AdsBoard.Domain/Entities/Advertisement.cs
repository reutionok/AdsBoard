using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdsBoard.Domain.Entities
{
    public class Advertisement
    {
        [Key]
        public int AdId { get; set; }
        public string Headline { get; set; }
        public string Details { get; set; } 
    }
}
