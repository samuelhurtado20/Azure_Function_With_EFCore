using Azure_Function_With_EFCore.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Azure_Function_With_EFCore.Services
{
	public interface IAlbumService
	{
		Task<List<Album>> GetAsync();
	}
}
