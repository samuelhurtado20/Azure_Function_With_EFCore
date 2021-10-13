using Azure_Function_With_EFCore.DataContext;
using Azure_Function_With_EFCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Azure_Function_With_EFCore.Services
{

	public class ArtistService : IArtistService
    {
        private readonly FunctionContext _context;
        public ArtistService(FunctionContext context)
		{
			_context = context;
		}

        public async Task<List<Artist>> GetAsync()
        {
            var artistas = new List<Artist>();

            try
            {
                artistas = await _context.Artists.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return artistas;
        }
    }
}
