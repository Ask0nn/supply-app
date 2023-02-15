using Microsoft.EntityFrameworkCore;
using PropertyChanged;
using SupplyApp.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SupplyApp.Model
{
    [AddINotifyPropertyChangedInterface]
    internal class SignificanceDirModel
    {
        private readonly ApplicationContext _context = new();

        public SignificanceDirModel() { }

        public List<Significance> GetAll()
        {
            var data = _context.Significances.ToList();
            return data;
        }

        public bool Add(Significance significance)
        {
            try
            {
                if (!_context.Significances.Any(r => r.Name.Equals(significance.Name)))
                {
                    _context.Significances.Add(significance);
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

        public bool Remove(Significance significance)
        {
            try
            {
                if (!_context.Requests.Include(p => p.Significance).Any(r => r.Significance.Equals(significance)))
                {
                    _context.Significances.Remove(significance);
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

        public bool Update(Significance significance)
        {
            try
            {
                if (!_context.Significances.Any(r => r.Name.Equals(significance.Name)))
                {
                    _context.Significances.Update(significance);
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
