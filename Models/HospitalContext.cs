using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class HospitalContext : DbContext
    {
        public DbSet<Patient> patient { get; set; }
        public DbSet<Doctor> doctor { get; set; }
        public DbSet<Appointment> appointment { get; set; }
        public DbSet<Schedule> schedule { get; set; }

        internal void Add(Schedule schedule)
        {
            throw new NotImplementedException();
        }

        internal void Update(Schedule schedule)
        {
            throw new NotImplementedException();
        }

        internal void Add(Appointment appointment)
        {
            throw new NotImplementedException();
        }

        internal void Update(Appointment appointment)
        {
            throw new NotImplementedException();
        }

        internal void Add(Doctor doctor)
        {
            throw new NotImplementedException();
        }

        internal void Update(Doctor doctor)
        {
            throw new NotImplementedException();
        }

        internal void Add(Patient patient)
        {
            throw new NotImplementedException();
        }

        internal void Update(Patient patient)
        {
            throw new NotImplementedException();
        }
    }
}
