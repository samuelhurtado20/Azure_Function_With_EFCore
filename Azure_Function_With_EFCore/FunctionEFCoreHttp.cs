using Azure_Function_With_EFCore.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Data;
using System.Threading.Tasks;

namespace Azure_Function_With_EFCore
{
	public class FunctionEFCoreHttp
	{
		private readonly IAlbumService _albumService;
		private readonly IConfiguration _configuration;

		public FunctionEFCoreHttp(IAlbumService albumService, IConfiguration config)
		{
			_albumService = albumService;
			_configuration = config;
		}

		[FunctionName("FunctionEFCoreHttp")]
		public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req, ILogger log)
		{
			log.LogInformation($"C# HTTP trigger function processed a request. {DateTime.Now}");
			log.LogInformation(_configuration.GetConnectionStringOrSetting("SqlConnectionString"));

			var albumes = await _albumService.GetAsync();

			GetSmstexts();

			return new OkObjectResult(albumes);
		}


		private void GetSmstexts()
		{
			string connectionString = "Server=d-i9sports.database.windows.net;Initial Catalog=i9SportsTest;Persist Security Info=False;User ID=i9sportsadmin;Password=b7frF2CgHFmnk3VR;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=3000;";

			// Provide the query string with a parameter placeholder.
			string queryString = "GetSMSTextData";

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

					cmd.Parameters.Add("@BatchSize", SqlDbType.Int).Value = paramValue;

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


		//private void GetSmstexts()
		//{
		//	string connectionString = "Server=d-i9sports.database.windows.net;Initial Catalog=i9SportsTest;Persist Security Info=False;User ID=i9sportsadmin;Password=b7frF2CgHFmnk3VR;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30000;";

		//	// Provide the query string with a parameter placeholder.
		//	string queryString = "GetSMSTextData";

		//	// Specify the parameter value.
		//	int paramValue = 5;

		//	// Create and open the connection in a using block. This
		//	// ensures that all resources will be closed and disposed
		//	// when the code exits.
		//	using (SqlConnection connection = new SqlConnection(connectionString))
		//	{
		//		// Create the Command and Parameter objects.
		//		SqlCommand cmd = new SqlCommand(queryString, connection);
		//		//cmd.Parameters.AddWithValue("@top", paramValue);

		//		// Open the connection in a try/catch block.
		//		// Create and execute the DataReader, writing the result
		//		// set to the console window.
		//		try
		//		{
		//			cmd.CommandType = CommandType.StoredProcedure;

		//			cmd.Parameters.Add("@BatchSize", SqlDbType.Int).Value = paramValue;

		//			connection.Open();
		//			cmd.ExecuteNonQuery();
		//		}
		//		catch (Exception ex)
		//		{
		//			Console.WriteLine(ex.Message);
		//		}
		//		Console.ReadLine();
		//	}
		//}

	}
}

//https://github.com/brminnick/GitTrends/blob/bae0c4c414e15badd035ce5fe2195940d02d0bd7/GitTrends.Functions/Functions/GenerateGitHubOAuthToken.cs
//https://codetraveler.io/2021/05/28/creating-azure-functions-using-net-5/
//_context.Database.ExecuteSqlRawAsync($"EXECUTE dbo.GetSMSTextData {batchSize}");
//var smstext = _context.Smstext.FromSqlInterpolated("GetStudents @FirstName", param).ToList();

//List<SMSTextData> result = new();

//_context.Smstext.FromStoredProc($"EXECUTE dbo.GetSMSTextData {batchSize}");
//_context.Smstext.FromSqlInterpolated($"Exec GetSMSTextData 10");

//System.FormattableString s1 = $"dbo.GetSMSTextData @BatchSize = {10}";
//var userType = _context.Database.ExecuteSqlInterpolated(s1);