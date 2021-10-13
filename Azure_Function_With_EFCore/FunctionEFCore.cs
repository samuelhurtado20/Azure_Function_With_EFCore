using Azure_Function_With_EFCore.Services;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;

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
		public void Run([TimerTrigger("0/5 * * * * *")] TimerInfo myTimer, ILogger log)
		{
			log.LogInformation($"C# HTTP trigger function processed a request. {DateTime.Now}");
			log.LogInformation(_configuration.GetConnectionStringOrSetting("SqlConnectionString"));

			var albumes = _albumService.GetAsync().ConfigureAwait(true).GetAwaiter().GetResult();
			foreach (var item in albumes)
			{
				log.LogInformation(item.Titulo);
			}
		}
	}
}