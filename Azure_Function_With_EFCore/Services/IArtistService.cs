using Azure_Function_With_EFCore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Azure_Function_With_EFCore.Services
{
	public interface IArtistService
	{
		Task<List<Artist>> GetAsync();
	}
}
