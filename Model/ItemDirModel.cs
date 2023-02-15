using Microsoft.EntityFrameworkCore;
using PropertyChanged;
using SupplyApp.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SupplyApp.Model
{
    [AddINotifyPropertyChangedInterface]
    internal class ItemDirModel
    {
        private readonly ApplicationContext _context = new();

        public ItemDirModel() { }

        public List<Item> GetAll()
        {
            var data = _context.Items.ToList();
            return data;
        }

        public bool Add(Item item)
        {
            try
            {
                if (!_context.Items.Any(r => r.Name.Equals(item.Name)))
                {
                    _context.Items.Add(item);
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

        public bool Remove(Item item)
        {
            try
            {
                if (!_context.Requests
                        .Include(p => p.RequestItems)
                        .Any(r => r.RequestItems
                            .Any(p => p.Item.Equals(item))))
                {
                    _context.Items.Remove(item);
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

        public bool Update(Item item)
        {
            try
            {
                if (!_context.Items.Any(r => r.Name.Equals(item.Name)))
                {
                    _context.Items.Update(item);
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
