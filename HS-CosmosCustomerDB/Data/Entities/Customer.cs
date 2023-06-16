using Newtonsoft.Json;

namespace HS_CosmosCustomerDB.Data.Entities
{
    public class Customer
    {
        // ****
        //NOTE:  This is the Cosmos DB ID, not the Customer ID
        // I tutorial använder man gemener men jag har använt versaler och får 7 referenser, med gemener blir det inga referenser.

        //[JsonProperty(PropertyName = "id")]

        [JsonProperty("id")]

        public string? CustomerId { get; set; }

        //[JsonProperty(PropertyName = "partitionKey")]

        //public string? id { get; init; }

        //public string? PartitionKey { get; set; }
        //public string? PartitionKey { get; init; }
        public string? Company { get; set; }
        public string? ContactName { get; set; }
        public string? Title { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? POBox { get; set; }
        public string? PostalCode { get; set; }
        public string? City { get; set; }
        public string? salesrep { get; set; }

        //public string? SalesRepId { get; set; }

        //public string? Repname { get; set; }    
        public string? RepPhone { get; set; }
        public string? RepEmail { get; set; }   
        //
        //
        //
        //

        ////public Address[]? Addresses { get; set; }
        ////public SalesRep[]? SalesReps { get; set; }

        //public override string ToString()
        //{
        //    return JsonConvert.SerializeObject(this);
        //}
        //public class SalesRep
        //{
        //    public string? RepName { get; set; }
        //    public string? RepPhone { get; set; }
        //    public string? RepEmail { get; set; }
        //}

        //public class Address
        //{
        //    public string? Street { get; set; }
        //    public string? PostalCode { get; set; }
        //    public string? City { get; set; }
        //}
    }
}
