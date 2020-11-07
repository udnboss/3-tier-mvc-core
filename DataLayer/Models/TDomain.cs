using System;
using System.Collections.Generic;

namespace DataLayer.Models
{
    public partial class TDomain
    {
        public TDomain()
        {
            TComment = new HashSet<TComment>();
        }

        public string Id { get; set; }
        public string Host { get; set; }
        public int? Port { get; set; }

        public virtual ICollection<TComment> TComment { get; set; }
    }
}
