using System;
using System.Collections.Generic;
using System.Configuration;
using HS_CosmosCustomerDB.Data.Entities;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
//using Microsoft.Azure.WebJobs;
//using Microsoft.Azure.WebJobs.Extensions.CosmosDB;
using System.Threading.Tasks;
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
            containerName: "Customers",
            // *** KOMMER INTE �T AT L�GGA IN DENNA CONNSTRING i ms AZURE WEBJOBS EXTENSIONS.COSMOSDB
            //ConnectionStringSetting = "CosmosDbConnString",
            LeaseContainerName = "leases",
            CreateLeaseContainerIfNotExists = true)] IReadOnlyList<Customer> input,
            ILogger log)
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
                            _logger.LogInformation("First document id: " + input[0].ContractId);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }

            // if (input != null && input.Count > 0)
            //{
            // - logger.LogInformation("Documents modified: " + input.Count);
            // _logger.LogInformation("First document Id: " + input[0].Id;
            //}
        }
    }

    public class CustomerContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // h�r anges secrets som egentligen b�r ligga tex i en keyvault
            //base.OnConfiguring(optionsBuilder); 
            optionsBuilder.UseCosmos(
                "https://localhost:5179/",
                "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==",
                "CosmosCustomerDB");
        }

        // Detta �r en collection som mappar containern Customers i Cosmos DB
        public DbSet<Customer> Customers { get; set; }

        // Detta �r en collection som mappar containern SalesRep i Cosmos DB
        //public DbSet<SalesRep> SalesReps { get; set; }
        // Detta �r en collection som mappar containern Address i Cosmos DB
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
