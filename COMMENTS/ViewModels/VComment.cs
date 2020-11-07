using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PresentationLayer.ViewModels
{
    public class VComment
    {
        public Guid Id { get; set; }
        public string DomainId { get; set; }
        public string Path { get; set; }
        public string Ip { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public Guid? ParentId { get; set; }
        public DateTime DatePosted { get; set; }
        public string QueryString { get; set; }

        public List<VComment> Children { get; set; }

    }
}
