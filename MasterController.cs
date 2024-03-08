using Newtonsoft.Json;
using System;
using System.Linq;
using System.Web.Http;
using WebAPISolution.Builder;
using WebAPISolution.Repository;

namespace HRMS.Areas.Incident.Controllers
{
    public class MasterController : ApiController
    {
        GDHRMSEntities db = new GDHRMSEntities();
        MasterBuilder obj = new MasterBuilder();

        string CreateCountryID(string str)
        {
            var iid = "";
            var data = str.Split(' ').ToList();
            foreach (var item in data)
            {
                iid = iid + item[0].ToString().ToUpper() + item[1].ToString().ToUpper();
            }
            return iid;
        }

        string CreateCountryIDDuplicate(string NATY, int b)
        {
            NATY = NATY + b;
            return NATY;
        }

        [HttpPost]
        [Route("~/api/addCountry")]
        public dynamic AddCountry(C0001 c0001)
        {
            try
            {
                //int b = 0;
                //var NATY = "";
                var val = db.C0001.Where(a => a.ID == c0001.ID).FirstOrDefault();
                if (val != null)
                {
                    // b++;
                    //NATY = CreateCountryIDDuplicate(NATY, b);
                    //val.COUNTR = NATY;
                    val.COUNTR = c0001.COUNTR;
                    val.DESCR = c0001.DESCR;
                    val.USERS1 = "ADMIN";
                    val.DATES1 = DateTime.Now;
                    val.STATUS = "A";
                    db.SaveChanges();
                    return "Country Updated Successfully";
                }
                else
                {
                    var coun =db.C0001.Where(a => a.DESCR == c0001.DESCR && a.STATUS == "A").FirstOrDefault();
                    if (coun == null)
                    {
                        //NATY = CreateCountryID(c0001.DESCR);
                        //var dd = db.C0001.Where(x => x.DESCR == NATY).FirstOrDefault();
                        //c0001.COUNTR = NATY;
                        //c0001.DESCR = c0001.DESCR;
                        c0001.STATUS = "A";
                        c0001.USERS = "ADMIN";
                        c0001.DATES = DateTime.Now;
                        db.C0001.Add(c0001);
                        db.SaveChanges();
                        return "Country Added Successfully";
                    }
                    else
                    {
                        return "This Country already exists";
                    }
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        [HttpGet]
        [Route("~/api/getCountries")]
        public dynamic GetCountries()
        {
            try
            {
                    return db.C0001.Where(a => a.STATUS == "A").ToList();
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        [HttpGet]
        [Route("~/api/GetCountryById")]
        public dynamic GetCountryById(string COUNTR)
        {
            try
            {
                return db.C0001.Where(a => a.STATUS == "A" && a.COUNTR==COUNTR).FirstOrDefault();
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        [HttpPost]
        [Route("~/api/DeleteCountry")]
        public dynamic DeleteCountry(C0001 c0001)
        {
            try
            {
                C0001 c00011= db.C0001.Where(a => a.COUNTR == c0001.COUNTR && a.STATUS=="A").FirstOrDefault();
                if (c00011 != null)
                {
                    c00011.STATUS = "B";
                    db.SaveChanges();
                    return "Country Deleted Successfully..!";
                }
                return "Country Not Found..!";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        
        [HttpGet]
        [Route("~/api/getState")]
        public dynamic getState()
        {
            try
            {
                return JsonConvert.SerializeObject(obj.getStateList());
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }


    }
}
