using System;
using PropertyChanged;
using SupplyApp.Entity;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SupplyApp.Model
{
    [AddINotifyPropertyChangedInterface]
    internal class ExecutorDirModel
    {
        private readonly ApplicationContext _context = new();

        public ExecutorDirModel() { }

        public List<Executor> GetAll()
        {
            var data = _context.Executors.ToList();
            return data;
        }

        public bool Add(Executor executor)
        {
            try
            {
                if (!_context.Executors.Any(r => r.Name.Equals(executor.Name)))
                {
                    _context.Executors.Add(executor);
                    _context.SaveChanges();
                }
                else return false;
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public bool Remove(Executor executor)
        {
            try
            {
                if (!_context.Requests.Include(p => p.Executor).Any(r => r.Executor.Equals(executor)))
                {
                    _context.Executors.Remove(executor);
                    _context.SaveChanges();
                }
                else return false;
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public bool Update(Executor executor)
        {
            try
            {
                if (!_context.Executors.Any(r => r.Name.Equals(executor.Name)))
                {
                    _context.Executors.Update(executor);
                    _context.SaveChanges();
                }
                else return false;
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
    }
}
