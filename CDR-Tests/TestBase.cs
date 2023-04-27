using CDR_BIP.Model;
using CsvHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Globalization;
using System.IO;

namespace CDR_Tests
{
    public class TestBase
    {
        protected CDRContext context;

        public TestBase()
        {
           context = GetInMemoryDBContext();
        }

        protected static CDRContext GetInMemoryDBContext()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<CDRContext>();
            var options = builder.UseInMemoryDatabase("CDR")
                .UseInternalServiceProvider(serviceProvider).Options;

            CDRContext dbContext = new(options);
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

            var reader = File.OpenText(Path.Combine(Environment.CurrentDirectory, @"Data\", "techtest_cdr.csv"));

            var csv = new CsvReader(reader, CultureInfo.GetCultureInfo("en-GB"));
            var records = csv.GetRecords<CDR>();
            
            dbContext.CDRs.AddRange(records);
            dbContext.SaveChanges();

            return dbContext;
        }

        protected static IFormFile GetFormFile()
        {
            var file = new FileInfo(Path.Combine(Environment.CurrentDirectory, @"Data\", "techtest_cdr.csv"));
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(file.OpenRead());
            writer.Flush();
            stream.Position = 0;

            return new FormFile(stream, 0, stream.Length, "test", file.Name);
        }
    }
}
