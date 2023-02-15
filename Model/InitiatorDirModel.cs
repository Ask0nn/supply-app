using Microsoft.EntityFrameworkCore;
using PropertyChanged;
using SupplyApp.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SupplyApp.Model
{
    [AddINotifyPropertyChangedInterface]
    internal class InitiatorDirModel
    {
        private readonly ApplicationContext _context = new();

        public InitiatorDirModel() { }

        public List<Initiator> GetAll()
        {
            var data = _context.Initiators.ToList();
            return data;
        }

        public bool Add(Initiator initiator)
        {
            try
            {
                if (!_context.Initiators.Any(r => r.Name.Equals(initiator.Name)))
                {
                    _context.Initiators.Add(initiator);
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

        public bool Remove(Initiator initiator)
        {
            try
            {
                if (!_context.Requests.Include(p => p.Initiator).Any(r => r.Initiator.Equals(initiator)))
                {
                    _context.Initiators.Remove(initiator);
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

        public bool Update(Initiator initiator)
        {
            try
            {
                if (!_context.Initiators.Any(r => r.Name.Equals(initiator.Name)))
                {
                    _context.Initiators.Update(initiator);
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
