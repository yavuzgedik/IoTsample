using Entity.DBContext;
using Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class SwitchRepo
    {
        public static List<Switch> GetAll()
        {
            using (IoTdbContext db = new IoTdbContext())
            {
                return db.Switch.ToList();
            }
        }

        public static Switch Last()
        {
            using (IoTdbContext db = new IoTdbContext())
            {
                return db.Switch.OrderByDescending(s => s.ID).FirstOrDefault();
            }
        }

        public static void Add(Switch sw)
        {
            using (IoTdbContext db = new IoTdbContext())
            {
                sw.Date = DateTime.Now;
                db.Switch.Add(sw);
                db.SaveChanges();
            }
        }
    }
}
