using AdsBoard.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdsBoard.WebUI.Models
{
    public class CommentListViewModel
    {
        public IEnumerable<Comment> Comments { get; set; }
    }
}