using Azure_Function_With_EFCore.Services;
using Microsoft.Azure.WebJobs;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Data;

namespace Azure_Function_With_EFCore
{
	public class FunctionEFCore
	{
		private readonly IAlbumService _albumService;
		private readonly IConfiguration _configuration;

		public FunctionEFCore(IAlbumService albumService, IConfiguration config)
		{
			_albumService = albumService;
			_configuration = config;
		}

		[FunctionName("FunctionEFCore")]
		public void Run([TimerTrigger("0/1 * * * * *")] TimerInfo myTimer, ILogger log)
		{
			log.LogInformation($"C# HTTP trigger function processed a request. {DateTime.Now}");
			log.LogInformation(_configuration.GetConnectionStringOrSetting("SqlConnectionString"));

			var albumes = _albumService.GetAsync().ConfigureAwait(true).GetAwaiter().GetResult();
			foreach (var item in albumes)
			{
				log.LogInformation(item.Titulo);
			}

			GetSmstexts();
		}


		private void GetSmstexts()
		{
			string connectionString = "Server=d-i9sports.database.windows.net,12;Initial Catalog=i9SportsTest;Persist Security Info=False;User ID=i9sportsadmin;Password=b7frF2CgHFmnk3VR;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30000;";

			// Provide the query string with a parameter placeholder.
			string queryString = "dbo.GetSMSTextData @top";

			// Specify the parameter value.
			int paramValue = 5;

			// Create and open the connection in a using block. This
			// ensures that all resources will be closed and disposed
			// when the code exits.
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				// Create the Command and Parameter objects.
				SqlCommand cmd = new SqlCommand(queryString, connection);
				//cmd.Parameters.AddWithValue("@top", paramValue);

				// Open the connection in a try/catch block.
				// Create and execute the DataReader, writing the result
				// set to the console window.
				try
				{
					cmd.CommandType = CommandType.StoredProcedure;

					cmd.Parameters.Add("@top", SqlDbType.Int).Value = paramValue;

					connection.Open();
					cmd.ExecuteNonQuery();
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
				}
				Console.ReadLine();
			}
		}

	}
}