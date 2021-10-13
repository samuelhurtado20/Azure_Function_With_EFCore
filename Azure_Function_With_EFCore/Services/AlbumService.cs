using Azure_Function_With_EFCore.DataContext;
using Azure_Function_With_EFCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Azure_Function_With_EFCore.Services
{
	public class AlbumService : IAlbumService
    {
        private readonly FunctionContext _context;
        public AlbumService(FunctionContext context)
		{
			_context = context;
		}

        public async Task<List<Album>> GetAsync()
        {
            var albumes = new List<Album>();

            try
            {
                albumes = await _context.Albumes.Include(a => a.Artista).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return albumes;
        }
    }
}
