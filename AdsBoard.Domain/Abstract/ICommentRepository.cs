using AdsBoard.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdsBoard.Domain.Abstract
{
    public interface ICommentRepository
    {
        
            IEnumerable<Comment> Comments { get; }
            void SaveComment(Comment comment);
            
        
    }
}
