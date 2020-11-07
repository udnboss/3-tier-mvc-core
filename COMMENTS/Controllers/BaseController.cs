using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.ComponentModel;

namespace PresentationLayer.Controllers
{
    public class BaseController : Controller
    {
        //protected DatabaseLibrary.COMMENTSContext db;

        public IConfiguration Configuration;
        public BaseController(IConfiguration _configuration)
        {
            Configuration = _configuration;
           // db = new DatabaseLibrary.COMMENTSContext(Configuration);
        }

        public List<TResult> QuickMapper<TSource, TResult>(IList<TSource> data) where TResult : new()
        {
            /*


             N.B. no DEEP copy - good for simple dto to View Model transfer etc ...
             classes will need to have a parameterless constructor  'where TResult : new()' 
             by default - this will ignore cases where destination object does not have one of the source object's fields- common in ViewModels ...
             you could use a Dictionary<String,string> param to handle cases  where property names don't marry up..

            to use :   List<Class2> lst2 = Helper.QuickMapper<Class1, Class2>(lst1).ToList();

            */

            var result = new List<TResult>(data.Count);


            PropertyDescriptorCollection propsSource = TypeDescriptor.GetProperties(typeof(TSource));
            PropertyDescriptorCollection propsResult = TypeDescriptor.GetProperties(typeof(TResult));


            TResult obj;
            Object colVal;
            string sResultFieldName = "";
            string sSourceFieldName = "";

            foreach (TSource item in data)
            {
                obj = new TResult();

                for (int iResult = 0; iResult < propsResult.Count; iResult++)
                {
                    PropertyDescriptor propResult = propsResult[iResult];
                    sResultFieldName = propResult.Name;

                    for (int iSource = 0; iSource < propsResult.Count; iSource++)
                    {
                        PropertyDescriptor propSource = propsSource[iSource];

                        sSourceFieldName = propSource.Name;

                        if (sResultFieldName == sSourceFieldName)
                        {
                            try
                            {
                                colVal = propSource.GetValue(item) ?? null;
                                propResult.SetValue(obj, colVal);
                            }
                            catch (Exception ex)
                            {
                                string ss = "sResultFieldName = " + sResultFieldName + "\r\nsSourceFieldName = " + sSourceFieldName + "\r\n" + ex.Message + "\r\n" + ex.StackTrace;
                                // do what you want here ...
                            }
                        }
                    }

                }

                result.Add(obj);
            }
            return result;
        }
    }

}
