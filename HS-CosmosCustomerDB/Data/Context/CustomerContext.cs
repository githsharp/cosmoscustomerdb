using HS_CosmosCustomerDB.Data.Entities;
using Microsoft.EntityFrameworkCore;
using static HS_CosmosCustomerDB.Data.Entities.Customer;

namespace HS_CosmosCustomerDB.Data.Context
{
    //public class CustomerContext:DbContext
    //{
    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //    {
    //        // här anges secrets som man gärna kan lägga i tex Azure Key Vault  
    //        optionsBuilder.UseCosmos(
    //            "https://localhost:5179/",
    //            // "https://localhost:8081", borde oftast vara denna men inte just idag
    //            "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==",
    //            databaseName: "CosmosCustomerDB");
    //    }

    //    // Detta är den collection som mappar containern Customers i Cosmos DB
    //    public DbSet<Customer> Customers { get; set; }

    //    // Detta är den collection som mappar containern SalesRep i Cosmos DB
    //    public DbSet<SalesRep> SalesReps { get; set; }

    //    // Detta är den collection som mappar containern Address i Cosmos DB
    //    public DbSet<Address> Addresses { get; set; }

    //    protected override void OnModelCreating(ModelBuilder modelbuilder)
    //    {
    //        //Mappning mellan klass och container
    //        modelbuilder.Entity<Customer>().ToContainer("Customers");
    //        modelbuilder.Entity<SalesRep>().ToContainer("SalesReps");
    //        modelbuilder.Entity<Address>().ToContainer("Addresses");
    //        //mappning till partitionkey
    //        modelbuilder.Entity<Customer>().HasPartitionKey(p => p.PartitionKey);
    //        //det finns ingen partitionkey i SalesRep och Address
    //    }

    //}
}
