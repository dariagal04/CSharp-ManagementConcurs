using System;
using System.Data;
using System.Collections.Generic;
using App.connection;

namespace App.repository
{
	public static class DbUtils
	{
		

		private static IDbConnection _instance = null;


		/*public static IDbConnection GetConnection(IDictionary<string,string> props)
		{
			if (_instance != null && _instance.State != ConnectionState.Closed) return _instance;
			_instance = GetNewConnection(props);
			_instance.Open();
			return _instance;
		}

		private static IDbConnection GetNewConnection(IDictionary<string,string> props)
		{
			return ConnectionFactory.getInstance().createConnection(props);
		}*/
		
		public static IDbConnection GetConnection(IDictionary<string, string> props)
		{
			var connection = GetNewConnection(props);
			connection.Open();
			return connection;
		}

		private static IDbConnection GetNewConnection(IDictionary<string, string> props)
		{
			return ConnectionFactory.getInstance().createConnection(props);
		}
		
		public static void CloseConnection()
		{
			if (_instance != null && _instance.State != ConnectionState.Closed)
			{
				_instance.Close();
			}
		}
	}
}
