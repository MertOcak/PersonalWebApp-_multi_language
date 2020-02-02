using Microsoft.EntityFrameworkCore;
using PersonalWebApp.Interfaces;
using PersonalWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalWebApp.Services
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        private DbSet<T> table;

        public GenericRepository(AppDbContext context)
        {
            this._context = context;
            table = _context.Set<T>();
        }


        public void Delete(object Id)
        {
            T exists = table.Find(Id);
            table.Remove(exists);
        }

        public List<T> GetAll()
        {
            return table.ToList();
        }

        public T GetById(object Id)
        {
            return table.Find(Id);
        }

        public void Insert(T obj)
        {
            table.Add(obj);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(T obj)
        {
            table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
        }
    }
}
