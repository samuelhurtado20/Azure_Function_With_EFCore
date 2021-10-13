using Azure_Function_With_EFCore.DataContext;
using Azure_Function_With_EFCore.Services;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

[assembly: FunctionsStartup(typeof(Azure_Function_With_EFCore.Startup))]
namespace Azure_Function_With_EFCore
{
	public class Startup : FunctionsStartup
	{
		public override void Configure(IFunctionsHostBuilder builder)
		{
			string cs = Environment.GetEnvironmentVariable("SqlConnectionString");
			builder.Services.AddDbContext<FunctionContext>(options => options.UseSqlServer(cs));
			builder.Services.AddTransient<IAlbumService, AlbumService>();
			builder.Services.AddTransient<IArtistService, ArtistService>();
		}


	}
}