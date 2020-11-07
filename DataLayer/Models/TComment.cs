using System;
using System.Collections.Generic;

namespace DataLayer.Models
{
    public partial class TComment
    {
        public TComment()
        {
            InverseParent = new HashSet<TComment>();
            TCommentVote = new HashSet<TCommentVote>();
        }

        public Guid Id { get; set; }
        public string DomainId { get; set; }
        public string Path { get; set; }
        public string Ip { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public Guid? ParentId { get; set; }
        public DateTime DatePosted { get; set; }
        public string QueryString { get; set; }

        public virtual TDomain Domain { get; set; }
        public virtual TComment Parent { get; set; }
        public virtual ICollection<TComment> InverseParent { get; set; }
        public virtual ICollection<TCommentVote> TCommentVote { get; set; }
    }
}
