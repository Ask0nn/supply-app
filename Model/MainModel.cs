using Microsoft.EntityFrameworkCore;
using PropertyChanged;
using SupplyApp.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace SupplyApp.Model
{
    [AddINotifyPropertyChangedInterface]
    internal class MainModel
    {
        public List<Request> GetData()
        {
            using ApplicationContext context = new();
            var data = context.Requests
                .Include(t => t.Executor)
                .Include(t => t.Initiator)
                .Include(t => t.Provider)
                .Include(t => t.Significance)
                .Include(t => t.Status)
                .Include(t => t.RequestItems)
                .ThenInclude(t => t.Item)
                .ToList();

            return data;
        }

        public string? GetRoute()
        {
            using ApplicationContext context = new();
            return context.Database.GetConnectionString();
        }

        public bool Add(Request request)
        {
            try
            {
                using ApplicationContext context = new();
                var items = request.RequestItems.ToList();
                request.RequestItems.Clear();
                request.Executor = context.Executors.Find(request.Executor.Id)!;
                request.Initiator = context.Initiators.Find(request.Initiator.Id)!;
                request.Provider = context.Providers.Find(request.Provider.Id)!;
                request.Significance = context.Significances.Find(request.Significance.Id)!;
                request.Status = context.Statuses.Find(request.Status.Id)!;
                request = context.Requests.Add(request).Entity;

                foreach (var item in items)
                {
                    if (!context.Items.Any(t => t.Name.Equals(item.Item.Name)))
                    {
                        context.Items.Add(item.Item);
                        context.SaveChanges();
                    }
                    item.Item = context.Items.Find(item.Item.Id)!;
                    item.Request = request;
                    context.RequestItems.Add(item);
                    context.SaveChanges();
                }

                context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                MessageBox.Show(e.Message);
                return false;
            }
        }

        public bool Update(Request request)
        {
            try
            {
                using ApplicationContext context = new();
                var items = request.RequestItems.ToList();
                request.RequestItems.Clear();
                request.Executor = context.Executors.Find(request.Executor.Id)!;
                request.Initiator = context.Initiators.Find(request.Initiator.Id)!;
                request.Provider = context.Providers.Find(request.Provider.Id)!;
                request.Significance = context.Significances.Find(request.Significance.Id)!;
                request.Status = context.Statuses.Find(request.Status.Id)!;
                request = context.Requests.Update(request).Entity;
                context.SaveChanges();

                foreach (var item in items)
                {
                    if (!context.Items.Any(t => t.Name.Equals(item.Item.Name)))
                    {
                        context.Items.Add(item.Item);
                        context.SaveChanges();
                    }
                    if (context.RequestItems.Any(t => t.Id.Equals(item.Id)))
                    {
                        var reqItem = context.RequestItems.Find(item.Id)!;
                        reqItem.Item = context.Items.Find(item.Item.Id)!;
                        reqItem.Count = item.Count;
                        reqItem.Request = request;
                        context.RequestItems.Update(reqItem);
                        context.SaveChanges();
                    }
                    else
                    {
                        item.Item = context.Items.Find(item.Item.Id)!;
                        item.Request = request;
                        context.RequestItems.Add(item);
                        context.SaveChanges();
                    }
                }

                items = context.RequestItems
                    .AsEnumerable()
                    .Where(p => p.RequestId.Equals(request.Id))
                    .Where(p => !items.Any(r => r.Id == p.Id && p.Id > 0))
                    .ToList();
                context.RequestItems.RemoveRange(items);
                context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                MessageBox.Show(e.Message);
                return false;
            }
        }

        public bool Remove(Request request)
        {
            try
            {
                using ApplicationContext context = new();
                context.Requests.Remove(request);
                context.SaveChanges();
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
