using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Reflection;

namespace DocRouter.Persistence
{
    /// <summary>
    /// Abstract class that implements <see cref="IDesignTimeDbContextFactory{TContext}"></see> to configure the DocRouter DbContext
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    public abstract class DocRouterContextFactoryBase<TContext> : IDesignTimeDbContextFactory<TContext> where TContext : DocRouterContext
    {
        private const string ConnectionStringName = "DocRouterDatabase";
        private const string AspNetCoreEnvironment = "ASPNETCORE_ENVIRONMENT";
        /// <summary>
        /// Creates the Context.
        /// </summary>
        /// <param name="args">An array of strings containing the arguments.</param>
        /// <returns></returns>
        public TContext CreateDbContext(string[] args)
        {
            var basePath = Directory.GetCurrentDirectory() + string.Format("{0}..{0}WebUI", Path.DirectorySeparatorChar);
            return Create(basePath, Environment.GetEnvironmentVariable(AspNetCoreEnvironment));
        }
        /// <summary>
        /// Creates an new instance of the Context.
        /// </summary>
        /// <param name="options">A <see cref="DbContextOptions"></see></param>
        /// <returns></returns>
        protected abstract TContext CreateNewInstance(DbContextOptions<TContext> options);
        private TContext Create(string basePath, string environmentName)
        {
            //Debugger.Launch();
            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.Local.json", optional: true)
                .AddJsonFile($"appsettings.{environmentName}.json", optional: true)
                .AddUserSecrets(Assembly.GetExecutingAssembly())
                .AddEnvironmentVariables()
                .Build();

            var connectionString = configuration.GetConnectionString(ConnectionStringName);

            return Create(connectionString);
        }
        private TContext Create(string connectionString)
        {

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentException($"Connection string '{connectionString}' is null or empty.", nameof(connectionString));
            }
            Console.WriteLine($"DesignTimeDbContextFactoryBase.Create(string): Connection string: '{connectionString}'");
            var optionsBuilder = new DbContextOptionsBuilder<TContext>();
            optionsBuilder.UseSqlServer(connectionString, b => b.MigrationsAssembly("DocRouter.Persistence"));
            return CreateNewInstance(optionsBuilder.Options);

        }
    }
}
