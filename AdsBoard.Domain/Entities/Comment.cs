using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdsBoard.Domain.Entities
{
    public class Comment
    {
        public int CommentId { get; set; }
        public int adId { get; set; }
        
        public string Name { get; set; }
        public string Text { get; set; }
    }
}
