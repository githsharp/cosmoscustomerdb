using System;
using System.Collections.Generic;
using System.Configuration;
using HS_CosmosCustomerDB.Data.Entities;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CosmosCustomerFunctionApp
{
    public class Function1
    {
        private readonly ILogger _logger;

        public Function1(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<Function1>();
        }

        [Function("Function1")]
        public async Task Run([CosmosDBTrigger(
            databaseName: "CosmosCustomerDB",
            containerName: "Customer",
            Connection = "https:::localhost:8081",
            LeaseContainerName = "leases",
            CreateLeaseContainerIfNotExists = true)] IReadOnlyList<Customer> input)
        {
            try
            {
                if (input != null && input.Count > 0)
                {
                    foreach (var item in input)
                    {
                        var customer = JsonConvert.DeserializeObject<Customer>(item.ToString());
                        using (var db = new CustomerContext())
                        {
                            db.Customers.Add(customer);
                            await db.SaveChangesAsync();
                        }
                        {
                            _logger.LogInformation("Documents modified: " + input.Count);
                            _logger.LogInformation("First document Id: " + input[0].ContractId);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex.Message);
            }
        }
    }

    public class CustomerContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // här anges secrets som egentligen bör ligga tex i en keyvault
            //base.OnConfiguring(optionsBuilder); 
            optionsBuilder.UseCosmos(
                "https://localhost:5179/",
                "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==",
                "CosmosCustomerDB");
        }

        // Detta är en collection som mappar containern Customers i Cosmos DB
        public DbSet<Customer> Customers { get; set; }

        // Detta är en collection som mappar containern SalesRep i Cosmos DB
        //public DbSet<SalesRep> SalesReps { get; set; }
        // Detta är en collection som mappar containern Address i Cosmos DB
        //public DbSet<Address> Addresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // mappning mellan entiteten och container
            modelBuilder.Entity<Customer>()
                .ToContainer("SignedCustomers") // ToContainer
                .HasPartitionKey(p => p.ContractId); // HasPartitionKey
        }
    }

    //public class Contract
    public class Customer
    {
        //public string CustomerId { get; set; }
        public string ContractId { get; set; }

        public string CustomerName { get; set; }

    }

    public class MyDocument
    {
        public string Id { get; set; }

        public string Text { get; set; }

        public int Number { get; set; }

        public bool Boolean { get; set; }
    }
}
