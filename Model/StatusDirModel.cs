using Microsoft.EntityFrameworkCore;
using PropertyChanged;
using SupplyApp.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SupplyApp.Model
{
    [AddINotifyPropertyChangedInterface]
    internal class StatusDirModel
    {
        private readonly ApplicationContext _context = new();

        public StatusDirModel() { }

        public List<Status> GetAll()
        {
            var data = _context.Statuses.ToList();
            return data;
        }

        public bool Add(Status status)
        {
            try
            {
                if (!_context.Statuses.Any(r => r.Name.Equals(status.Name)))
                {
                    _context.Statuses.Add(status);
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

        public bool Remove(Status status)
        {
            try
            {
                if (!_context.Requests.Include(p => p.Status).Any(r => r.Status.Equals(status)))
                {
                    _context.Statuses.Remove(status);
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

        public bool Update(Status status)
        {
            try
            {
                if (!_context.Statuses.Any(r => r.Name.Equals(status.Name) && !r.Id.Equals(status.Id)))
                {
                    _context.Statuses.Update(status);
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
