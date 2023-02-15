using PropertyChanged;
using SupplyApp.Entity;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace SupplyApp.Model
{
    [AddINotifyPropertyChangedInterface]
    internal class FilesModel
    {
        public ObservableCollection<Document> GetDocumentsByRequestId(long requestId)
        {
            using ApplicationContext context = new ApplicationContext();
            return new ObservableCollectionListSource<Document>(
                context.Documents.Where(p => p.RequestId.Equals(requestId)).ToList());
        }

        public bool Add(Document document)
        {
            try
            {
                using ApplicationContext context = new ApplicationContext();
                if (!context.Documents.Any(r => r.Name.Equals(document.Name)))
                {
                    context.Documents.Add(document);
                    context.SaveChanges();
                }
                else return false;
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                MessageBox.Show(e.Message);
                return false;
            }
        }

        public bool Remove(Document document)
        {
            try
            {
                using ApplicationContext context = new ApplicationContext();
                context.Documents.Remove(document);
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
