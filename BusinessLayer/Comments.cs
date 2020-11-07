using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLayer
{
    public class Comments
    {
        DataLayer.Comments db;
        public Comments(IConfiguration configuration)
        {
            db = new DataLayer.Comments(configuration);            
        }

        public List<DataLayer.Models.TComment> GetListByPath(string path)
        {
            return db.GetListByPath(path).ToList();
        }

        public bool PostComment(DataLayer.Models.TComment c)
        {
            return db.PostComment(c);
        }
    }
}
