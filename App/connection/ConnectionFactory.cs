﻿using System.Data;
using System.Reflection;

namespace App.connection;

public abstract class ConnectionFactory
{
    protected ConnectionFactory()
    {
    }

    private static ConnectionFactory instance;

    public static ConnectionFactory getInstance()
    {
        if (instance == null)
        {

            var assem = Assembly.GetExecutingAssembly();
            Type[] types = assem.GetTypes();
            foreach (var type in types)
            {
                if (type.IsSubclassOf(typeof(ConnectionFactory)))
                    instance = (ConnectionFactory)Activator.CreateInstance(type);
            }
        }
        return instance;
    }

    public abstract  IDbConnection createConnection(IDictionary<string,string> props);
}