using System;
using System.Collections.Generic;

namespace DataLayer.Models
{
    public partial class TCommentVote
    {
        public Guid Id { get; set; }
        public int Vote { get; set; }
        public string Ip { get; set; }
        public Guid CommentId { get; set; }

        public virtual TComment Comment { get; set; }
    }
}
