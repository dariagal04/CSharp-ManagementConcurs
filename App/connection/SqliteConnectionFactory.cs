﻿using System.Data;
using System.Data.SQLite;

namespace App.connection;

public class SqliteConnectionFactory : ConnectionFactory
{
    public override IDbConnection createConnection(IDictionary<string,string> props)
    {
        //Mono Sqlite Connection

        //String connectionString = "URI=file:/D:/dari/an2sem2/MPP/Concurs.db,Version=3";
         //String connectionString = props["ConnectionString"];
        // Console.WriteLine("SQLite ---Se deschide o conexiune la  ... {0}", connectionString);
         //return new SQLiteConnection(connectionString);

        //Windows SQLite Connection, fisierul .db ar trebuie sa fie in directorul bin/debug
        const string connectionString = "Data Source=D:/dari/an2sem2/MPP/Concurs;Version=3;";
        return new SQLiteConnection(connectionString);
    }
}

