using DataLayer.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace DataLayer
{
    public class Comments
    {
        COMMENTSContext db;

        public Comments(IConfiguration configuration)
        {
            db = new COMMENTSContext(configuration);
        }
        public IQueryable<TComment> GetListByPath(string path)
        {
            var query = GetList(x => x.Path == path).OrderByDescending(x => x.DatePosted);
            return query;
        }

        public bool PostComment(TComment c)
        {
            db.TComment.Add(c);
            try
            {
                db.SaveChanges();
                return true;
            }
            catch( Exception e)
            {
                //throw e;
                return false;
            }

            
            
        }

        public IQueryable<TComment> GetList(Func<TComment, bool> filterExpr)
        {
            var query = db.TComment.Where(filterExpr).AsQueryable();
            return query;
        }


    }

    public static class LinqExtensions
    {
        public static IOrderedQueryable<TSource> CustomOrderBy<TSource, TKey>(this IQueryable<TSource> source,
        System.Linq.Expressions.Expression<Func<TSource, TKey>> keySelector,
        System.ComponentModel.ListSortDirection sortOrder
        )
        {
            if (sortOrder == System.ComponentModel.ListSortDirection.Ascending)
                return source.OrderBy(keySelector);
            else
                return source.OrderByDescending(keySelector);
        }
    }
}
