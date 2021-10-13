using Azure_Function_With_EFCore.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
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
			return new OkObjectResult(albumes);
		}
	}
}
