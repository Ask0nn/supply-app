using Microsoft.EntityFrameworkCore;
using PropertyChanged;
using SupplyApp.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SupplyApp.Model
{
    [AddINotifyPropertyChangedInterface]
    internal class ProviderDirModel
    {
        private readonly ApplicationContext _context = new();

        public ProviderDirModel() { }

        public List<Provider> GetAll()
        {
            var data = _context.Providers.ToList();
            return data;
        }

        public bool Add(Provider provider)
        {
            try
            {
                if (!_context.Providers.Any(r => r.Name.Equals(provider.Name)))
                {
                    _context.Providers.Add(provider);
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

        public bool Remove(Provider provider)
        {
            try
            {
                if (!_context.Requests.Include(p => p.Provider).Any(r => r.Provider.Equals(provider)))
                {
                    _context.Providers.Remove(provider);
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

        public bool Update(Provider provider)
        {
            try
            {
                if (!_context.Providers.Any(r => r.Name.Equals(provider.Name)))
                {
                    _context.Providers.Update(provider);
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
