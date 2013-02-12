using System;
using System.Configuration;
using System.IO;
using DatabaseMigraine;

namespace DatabaseCreator
{
	class Program
	{
		//TODO: change this to use DbParams instead of an app.config file:
		static readonly string DbCreationPath = ConfigurationManager.AppSettings["DbCreationPath"];
		static readonly string DisposableDbConnString = ConfigurationManager.AppSettings["DisposableDbConnString"];
		static readonly string DisposableDbHostname = ConfigurationManager.AppSettings["DisposableDbHostname"];

		static void Main(string[] args)
		{
			try
			{
				if(args.Length<3 || args.Length>3)
					Console.WriteLine("Usage: DatabaseCreator DatabaseName DatabaseConnectionString");				


				Environment.Exit(0);

			}
			catch (Exception exception)
			{
				Console.WriteLine(exception.Message);
				Environment.Exit(1);
			}
		}

		private static string CreateDatabase(string dbNameInVcs,string fixedDatabaseName)
		{
			var disposableDbServer = ConnectionHelper.Connect(DisposableDbHostname, DisposableDbConnString);

			var disposableDbCreator = new DisposableDbManager(DbCreationPath, disposableDbServer, dbNameInVcs);
			if(!String.IsNullOrEmpty(fixedDatabaseName))
			{
				DisposableDbManager.KillDb(disposableDbServer,fixedDatabaseName);
				disposableDbCreator.FixedDatabaseName = fixedDatabaseName;	
			}

			

			string disposableDbName = disposableDbCreator.CreateCompleteDisposableDb();
			Console.WriteLine(disposableDbName);
			return disposableDbName;
		}
	}
}
