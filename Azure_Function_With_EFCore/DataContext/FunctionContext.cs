using Azure_Function_With_EFCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;

namespace Azure_Function_With_EFCore.DataContext
{
	public class FunctionContext : DbContext
	{
		public FunctionContext(DbContextOptions<FunctionContext> options)
			: base(options)
		{ }

		public DbSet<Artist> Artists { get; set; }
		public DbSet<Album> Albumes { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			//modelBuilder.Entity<Artist>()
			//	.HasData(
			//		new Artist() { ArtistaID = 1, Nombre = "Ricardo Arjona" },
			//		new Artist() { ArtistaID = 2, Nombre = "Luis Miguel" },
			//		new Artist() { ArtistaID = 3, Nombre = "Kalimba" });

			//modelBuilder.Entity<Album>()
			//	.HasData(
			//		new Album()
			//		{
			//			AlbumID = 1,
			//			ArtistaID = 2,
			//			Titulo = "Romance",
			//			Anio = 1991,
			//			Precio = 180
			//		},
			//		new Album()
			//		{
			//			AlbumID = 2,
			//			ArtistaID = 1,
			//			Titulo = "Circo Soledad",
			//			Anio = 2017,
			//			Precio = 190
			//		},
			//		new Album()
			//		{
			//			AlbumID = 3,
			//			ArtistaID = 3,
			//			Titulo = "Aerosoul",
			//			Anio = 2004,
			//			Precio = 210
			//		});
		}
	}

	public class FunctionContextFactory : IDesignTimeDbContextFactory<FunctionContext>
	{
		public FunctionContext CreateDbContext(string[] args)
		{
			//$Env: ConnectionString = "Data Source=(local)\\sqlexpress;Initial Catalog=DbTest;Integrated security=True;"
			//setx ConnectionString "Data Source=(local)\\sqlexpress;Initial Catalog=DbTest;Integrated security=True;"
			//Environment.SetEnvironmentVariable("ConnectionString", "Data Source=(local)\\sqlexpress;Initial Catalog=DbTest;Integrated security=True;");
			string cs = Environment.GetEnvironmentVariable("ConnectionString");
			var optionsBuilder = new DbContextOptionsBuilder<FunctionContext>();
			//optionsBuilder.UseSqlServer(_configuration.GetConnectionStringOrSetting("SqlConnectionString"));
			optionsBuilder.UseSqlServer(cs);

			return new FunctionContext(optionsBuilder.Options);
		}
	}
}