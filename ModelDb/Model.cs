using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;

using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Sanator.ModelDb
{
    public partial class Model1 : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            MySqlConnectionStringBuilder b = new MySqlConnectionStringBuilder();
            b.Database = "host1125";
            b.UserID = "student";
            b.Password = "student";
            b.Server = "192.168.200.13";
            b.CharacterSet = "utf8";

            optionsBuilder.UseMySQL(b.ToString());
            base.OnConfiguring(optionsBuilder); 
        }

        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Kategory> Kategory { get; set; }
        public virtual DbSet<Log> Log { get; set; }
        public virtual DbSet<Number> Number { get; set; }
        public virtual DbSet<Pay> Pay { get; set; }
        public virtual DbSet<Service> Service { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<Uchet> Uchet { get; set; }
        public virtual DbSet<Worker> Worker { get; set; }
        public DbSet<Number> NumberPhone { get; internal set; }

        public Model1()
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            

/*            modelBuilder.Entity<Client>()
                .Property(e => e.FIO)
                .IsFixedLength();

            modelBuilder.Entity<Client>()
                .Property(e => e.passport)
                .IsFixedLength();

            modelBuilder.Entity<Client>()
               .Property(e => e.Number)
               .IsFixedLength();

            modelBuilder.Entity<Client>()
                .HasMany(e => e.Log)
                .WithOne(e => e.Client);
               /* .WithRequired(e => e.Client)
                .HasForeignKey(e => e.ID_client_FK);*/
/*
            modelBuilder.Entity<Client>()
                .HasMany(e => e.Pay)
                .WithOne(e => e.Client);*/
            /* .WithRequired(e => e.Client)
             .HasForeignKey(e => e.ID_client_FK);*/
            /*
            modelBuilder.Entity<Client>()
                .HasMany(e => e.Uchet)
                .WithOne(e => e.Client);*/
                //.WithRequired(e => e.Client)
                //.HasForeignKey(e => e.ID_client_FK);
                /*
            modelBuilder.Entity<Kategory>()
                .Property(e => e.name)
                .IsFixedLength();

            modelBuilder.Entity<Kategory>()
                .Property(e => e.description)
                .IsFixedLength();

            modelBuilder.Entity<Kategory>()
                .HasMany(e => e.Number)
                .WithOne(e => e.Kategory);*/
            //.HasForeignKey(e => e.ID_type_FK);
            /*
            modelBuilder.Entity<Number>()
          .HasMany(e => e.Uchet)
          .WithOne(e => e.Number);*/
          //.HasForeignKey(e => e.ID_number_FK)
          //.WillCascadeOnDelete(false);
          /*
            modelBuilder.Entity<Pay>()
                .HasMany(e => e.Log)
                .WithOne(e => e.Pay);*/
                //.HasForeignKey(e => e.ID_pay_FK);
/*
            modelBuilder.Entity<Pay>()
                .HasMany(e => e.Uchet)
               .WithOne(e => e.Pay);*/
                //.HasForeignKey(e => e.ID_pay_FK);

        /*    modelBuilder.Entity<Service>()
                .Property(e => e.name)
                .IsFixedLength();

            modelBuilder.Entity<Service>()
                .Property(e => e.description)
                .IsFixedLength();

            modelBuilder.Entity<Service>()
                .HasMany(e => e.Log)
               .WithOne(e => e.Service);*/
                //.HasForeignKey(e => e.ID_service_FK);
                /*
            modelBuilder.Entity<Status>()
                .Property(e => e.name)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Status>()
              .HasMany(e => e.Number)
                .WithOne(e => e.Status);*/
                //.HasForeignKey(e => e.ID_status_FK);
            
            /*
            modelBuilder.Entity<Worker>()
                .Property(e => e.FIO)
                .IsFixedLength();

            modelBuilder.Entity<Worker>()
                .Property(e => e.Number)
                .IsFixedLength();

            modelBuilder.Entity<Worker>()
                .Property(e => e.position)
                .IsFixedLength();

            modelBuilder.Entity<Worker>()
                .Property(e => e.position)
                .IsFixedLength();

            modelBuilder.Entity<Worker>()
                .Property(e => e.passport)
                .IsFixedLength();

            modelBuilder.Entity<Worker>()
                .Property(e => e.login)
                .IsFixedLength();

            modelBuilder.Entity<Worker>()
                .Property(e => e.parol)
                .IsFixedLength();

            modelBuilder.Entity<Worker>()
                .HasMany(e => e.Uchet)
                .WithOne(e => e.Worker);*/
                //.HasForeignKey(e => e.ID_worker_FK)
                //.WillCascadeOnDelete(false);

        }

        internal void SaveChanges(Worker worker)
        {
            throw new NotImplementedException();
        }
    }
}
