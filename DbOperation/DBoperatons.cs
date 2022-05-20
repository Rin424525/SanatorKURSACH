
using Microsoft.EntityFrameworkCore;
using Sanator.ModelDb;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanator
{
    public class DbOperations
    {
        static Model1 db;

        public static List<Client> Client { get; protected set; }

        public DbOperations()
        {
            if (db == null)
                db = new Model1();
        }
        public DbSet<Client> GetAllClient()
        {
            return db.Clients;
        }

        public DbSet<Kategory> GetAllKategory()
        {
            return db.Kategory;
        }
        public DbSet<Log> GetAllLog()
        {
            return db.Log;
        }
        public DbSet<Number> GetAllNumber()
        {
            return db.Number;
        }

        public DbSet<Pay> GetAllPay()
        {
            return db.Pay;
        }
        public DbSet<Service> GetAllService()
        {
            return db.Service;
        }
        public DbSet<Status> GetAllStatus()
        {
            return db.Status;
        }
        public DbSet<Uchet> GetAllUchet()
        {
            return db.Uchet;
        }
        public DbSet<Worker> GetAllWorker()
        {
            return db.Worker;
        }
        public Status FindStatus(int id)
        {
            return db.Status.Find(id);
        }

        public Client FindClient(int id)
        {
            return db.Clients.Find(id);
        }
        public Kategory FindKategory(int id)
        {
            return db.Kategory.Find(id);
        }
        public Worker FindWorker(int id)
        {
            return db.Worker.Find(id);
        }
        public Log FindLog(int id)
        {
            return db.Log.Find(id);
        }
        
        public Pay FindPay(int id)
        {
            return db.Pay.Find(id);
        }
        public Uchet FindUchet(int id)
        {
            return db.Uchet.Find(id);
        }
        public Service FindService(int id)
        {
            return db.Service.Find(id);
        }
        public void RemoveKategory(Kategory kategory)
        {
            db.Kategory.Remove(kategory);
        }
        public void RemoveWorker(Worker worker)
        {
            db.Worker.Remove(worker);
        }

        internal Number FindNumber(int iD)
        {
            return db.Number.Find(iD);
        }

        public void RemoveLog(Log log)
        {
            db.Log.Remove(log);
        }

        internal void RemoveNumber(Number selectedNumber)
        {
            db.Number.Remove(selectedNumber);
        }

        public void RemovePay(Pay pay)
        {
            db.Pay.Remove(pay);
        }
        public void RemoveClient(Client client)
        {
            db.Clients.Remove(client);
        }
        public void RemoveUchet(Uchet uchet)
        {
            db.Uchet.Remove(uchet);
        }
        public void RemoveService(Service service)
        {
            db.Service.Remove(service);
        }
        public void AddClient(Client client)
        {
            db.Clients.Add(client);
        }
        public void AddKategory(Kategory kategory)
        {
            db.Kategory.Add(kategory);
        }
        public void AddWorker(Worker worker)
        {
            db.Worker.Add(worker);
        }
        public void AddLog(Log log)
        {
            db.Log.Add(log);
        }
        
        public void AddUchet(Uchet uchet)
        {
            db.Uchet.Add(uchet);
        }

        internal void AddNumber(Number selectedNumber)
        {
            throw new NotImplementedException();
        }

        public void AddPay(Pay pay)
        {
            db.Pay.Add(pay);
        }
        public void AddService(Service service)
        {
            db.Service.Add(service);
        }
        public void Save()
        {
            db.SaveChanges();
        }

       
    }
}
