using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;

namespace ModbusRecorder.Service
{
    public class DbRecordService:IDbRecordService
    {
        public DbRecordService()
        {
            using(var db = new LiteDatabase(@"ModbusRecorder.db"))
            {
                // Get customer collection
                var col = db.GetCollection<Customer>("customers");
                // Create your new customer instance
                //var customer = new Customer
                //{ 
                //    Name = "Atakan Doe", 
                //    Phones = new string[] { "8000-0000", "9000-0000" }, 
                //    Age = 25,
                //    IsActive = true
                //};

                //// Create unique index in Name field
                //col.EnsureIndex(x => x.Name, true);

                //// Insert new customer document (Id will be auto-incremented)
                //col.Insert(customer);

                // Update a document inside a collection
                //customer.Name = "Joana Doe";

                //col.Update(customer);

                // Use LINQ to query documents (with no index)
                List<Customer> results = col.Find(x=>x.Age>20).ToList();
            }
        }
    }

    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string[] Phones { get; set; }
        public bool IsActive { get; set; }
    }

   
}
