//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Text;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Design;
//using Microsoft.Extensions.Configuration;
//using PE.ApiHelper.Context;

//namespace brechtbaekelandt.entityFrameworkCoreClassLibrary
//{
//    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<PaylocityContext>
//    {
//        public PaylocityContext CreateDbContext(string[] args)
//        {
//            var configuration = new ConfigurationBuilder()
//                .SetBasePath(Directory.GetCurrentDirectory())
//                .AddJsonFile("appsettings.json")
//                .Build();

//            var builder = new DbContextOptionsBuilder<PaylocityContext>();

//            var connectionString = configuration.GetConnectionString("PaylocitySqlConn");

//            builder.UseSqlServer(connectionString);

//            return new PaylocityContext(builder.Options);
//        }
//    }
//}
