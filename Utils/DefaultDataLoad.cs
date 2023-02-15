using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows;
using SupplyApp.Entity;

namespace SupplyApp.Utils;

internal class DefaultDataLoad
{
    private const string DefaultData = "DefaultData.json";
    private const string DbConnections = "DataSourceConnections.json";

    private static DefaultDataLoad? _instance;

    public DefaultsModel? Data { get; private set; }
    public DataSourceConnectionsModel? Connections { get; private set; }
    public static DefaultDataLoad GetInstance => _instance ??= new DefaultDataLoad();

    private DefaultDataLoad()
    {
        if (File.Exists(DbConnections))
            Connections = JsonSerializer.Deserialize<DataSourceConnectionsModel>(File.ReadAllText(DbConnections));
        if (File.Exists(DefaultData))
            Data = JsonSerializer.Deserialize<DefaultsModel>(File.ReadAllText(DefaultData));
    }

    public void LoadDirs()
    {
        if (Data is null)
            return;

        var context = new ApplicationContext();

        try
        {
            Data.Executor?.Where(p => !context.Executors.Any(t => t.Name.Equals(p)))
                .ToList()
                .ForEach(p => context.Executors.Add(new Executor { Name = p }));
            Data.Initiator?.Where(p => !context.Initiators.Any(t => t.Name.Equals(p)))
                .ToList()
                .ForEach(p => context.Initiators.Add(new Initiator { Name = p }));
            Data.Item?.Where(p => !context.Items.Any(t => t.Name.Equals(p)))
                .ToList()
                .ForEach(p => context.Items.Add(new Item { Name = p }));
            Data.Provider?.Where(p => !context.Providers.Any(t => t.Name.Equals(p)))
                .ToList()
                .ForEach(p => context.Providers.Add(new Provider { Name = p }));
            Data.Significance?.Where(p => !context.Significances.Any(t => t.Name.Equals(p)))
                .ToList()
                .ForEach(p => context.Significances.Add(new Significance { Name = p }));
            Data.Status?.Where(p => !context.Statuses.Any(t => t.Name.Equals(p.Key)))
                .ToList()
                .ForEach(p => context.Statuses.Add(new Status { Name = p.Key, Color = p.Value}));

            context.SaveChanges();
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message);
            throw;
        }
    }

    public bool AddCon(ConnectionModel connection)
    {
        try
        {
            if (!Connections!.Connections.Any(r => r.Name.Equals(connection.Name)))
            {
                Connections!.Connections.Add(connection);
                SaveConnections();
            }
            else
            {
                return false;
            }

            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public bool RemoveCon(ConnectionModel connection)
    {
        try
        {
            if (Connections!.Connections.Contains(connection))
            {
                Connections!.Connections.Remove(connection);
                SaveConnections();
            }
            else
            {
                return false;
            }

            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public bool SetCon(ConnectionModel connection)
    {
        try
        {
            if (Connections!.Connections.Contains(connection))
            {
                Connections!.Active = connection;
                SaveConnections();
            }
            else
            {
                return false;
            }

            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public void SaveConnections()
    {
        var json = JsonSerializer.Serialize(Connections);
        File.WriteAllText(DbConnections, json);
    }
}