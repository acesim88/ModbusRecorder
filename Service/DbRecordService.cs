using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LiteDB;
using ModbusRecorder.Model;

namespace ModbusRecorder.Service
{
    public class DbRecordService : IDbRecordService
    {
        private readonly string _dbName;

        public DbRecordService(string dbName)
        {
            _dbName = dbName;
        }

        public bool AddRecord(IDbModel dbModel)
        {
            try
            {
                using (var db = new LiteDatabase(@"ModbusRecorder.db"))
                {
                    var col = db.GetCollection<IDbModel>(_dbName);
                    col.Insert(dbModel);
                    col.EnsureIndex(x => x.Id);
                }

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool UpdateRecord(IDbModel dbModel)
        {
            try
            {
                using (var db = new LiteDatabase(@"ModbusRecorder.db"))
                {
                    var col = db.GetCollection<IDbModel>(_dbName);
                    col.Update(dbModel);
                }

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public void DeleteRecord(IDbModel dbModel)
        {
            try
            {
                using (var db = new LiteDatabase(@"ModbusRecorder.db"))
                {
                    var col = db.GetCollection<IDbModel>(_dbName);
                    col.Delete(dbModel.Id);
                }
            }
            catch (Exception e)
            {

            }
        }

        public bool ReadRecord(IDbModel dbModel)
        {
            throw new NotImplementedException();
        }

        public List<IDbModel> GetRecords()
        {
            try
            {
                using (var db = new LiteDatabase(@"ModbusRecorder.db"))
                {
                    var col = db.GetCollection<IDbModel>(_dbName);
                    return col.FindAll().ToList();
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
