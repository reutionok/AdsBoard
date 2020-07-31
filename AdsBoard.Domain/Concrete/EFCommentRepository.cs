using AdsBoard.Domain.Abstract;
using AdsBoard.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdsBoard.Domain.Concrete
{
    public class EFCommentRepository : ICommentRepository
    {
        EFDbContext context = new EFDbContext();
        public IEnumerable<Comment> Comments
        {
            get { return context.Comments; }
        }
        public void SaveComment(Comment comment)
        {
            if (comment.CommentId == 0)
                context.Comments.Add(comment);
            else
            {
                Comment dbEntry = context.Comments.Find(comment.CommentId);
                if (dbEntry != null)
                {
                    dbEntry.Name = comment.Name;
                    dbEntry.Text = comment.Text;
                    dbEntry.adId = comment.adId;
                   
                }
            }
            context.SaveChanges();
        }
    }
}
