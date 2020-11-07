using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataLayer
{
    public class COMMENTSContext : Models.CommentsGeneratedContext
    {
        string _defaultConnectionString = "Server=localhost\\sql2019;Database=COMMENTS;Trusted_Connection=True;"; //for wizards etc..
        IConfiguration _conf;

        public COMMENTSContext()
        {

        }

        public COMMENTSContext(IConfiguration conf)
        {
            _conf = conf;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var cs = _conf != null ? _conf.GetConnectionString("COMMENTSConnection") : _defaultConnectionString;//read from appsettings.json
                optionsBuilder.UseSqlServer(cs);
            }
        }
    }
}
