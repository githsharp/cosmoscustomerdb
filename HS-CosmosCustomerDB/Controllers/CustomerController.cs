using HS_CosmosCustomerDB.Data.Context;
using HS_CosmosCustomerDB.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
//using System.Configuration;
//using ConfigurationManager = System.Configuration.ConfigurationManager;

namespace HS_CosmosCustomerDB.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CustomerController : ControllerBase
    {
        //The Azure Cosmos DB endpoint for running this sample.
        //private static readonly string EndpointUri = ConfigurationManager.AppSettings["EndPointUri"];

        // The primary key for the Azure Cosmos account.
        //private static readonly string PrimaryKey = ConfigurationManager.AppSettings["PrimaryKey"];

        // The Cosmos client instance
        //private CosmosClient cosmosClient;

        // The database we will create
        //private Database database;

        // The container we will create.
        //private Container container;

        // The name of the database and container we will create
        //private string databaseId = "CosmosCustomerDB";
        //private string containerId = "salesrep";

        //


        // Cosmos DB details, In real use cases, these details should be configured in secure configuraion file.
        private readonly string CosmosDBAccountUri = "https://localhost:8081/";
        //private readonly string CosmosDBAccountUri = "https://localhost:5179/";
        private readonly string CosmosDBAccountPrimaryKey = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
        private readonly string CosmosDbName = "CosmosCustomerDB";
        private readonly string CosmosDbContainerName = "Customers";

        // Common Container Client, config parameter can also be passed dynamically.
        //returns ContainerClient

        private Container ContainerClient()
        {
            CosmosClient cosmosDbClient = new CosmosClient(CosmosDBAccountUri, CosmosDBAccountPrimaryKey);
            Container containerClient = cosmosDbClient.GetContainer(CosmosDbName, CosmosDbContainerName);
            return containerClient;
        }


        [HttpPost("1.AddCustomer")]
        public async Task <IActionResult> AddCustomer(Customer customer)
        {
            try
            {
                var container = ContainerClient();
                //var response = await container.CreateItemAsync(customer, new PartitionKey(customer.SalesRep));
                var response = await container.CreateItemAsync(customer, new Microsoft.Azure.Cosmos.PartitionKey(customer.salesrep));
                return Ok(Response);
            } 
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[HttpPost("AddItem")]

        //public async Task AddItemsToContainerAsync()
        //{
        //    Customer intersportCustomer = new Customer
        //    {
        //        Id = "Intersport.1",
        //        PartitionKey = "salesrep",
        //        Company = "Intersport",
        //        ContactName = "Ingvar Ivarsson",
        //        Title = "Inköpare",
        //        SalesReps = new Customer.SalesRep[]
        //        {
        //            new Customer.SalesRep
        //            {
        //                RepName = "Irene Ingves",
        //                RepPhone = "08-123456",
        //                RepEmail = "irene@gmail.com"
        //            }
        //        },
        //        Addresses = new Customer.Address[]
        //        {
        //            new Customer.Address
        //            {
        //                Street = "Storgatan 1",
        //                PostalCode = "12345",
        //                City = "Stockholm"
        //            }
        //        },

        //    };

        //    try
        //    {
        //        //Read the item to see if it exists.
        //        ItemResponse<Customer> intersportCustomerResponse = await this.container.ReadItemAsync<Customer>(intersportCustomer.Id, new PartitionKey(intersportCustomer.PartitionKey));
        //        Console.WriteLine("Item in database with id: {0} already exists\n", intersportCustomerResponse.Resource.Id);
        //    }
        //    catch(CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        //    {
        //        //Create an item in the container representing the Intersport company, and we provide the value of the partition key for this item, which is "Intersport"
        //        ItemResponse<Customer> intersportcustomerResponse = await this.container.CreateItemAsync<Customer>(intersportCustomer, new PartitionKey(intersportCustomer.PartitionKey));

        //        //Note that after creating the item, we can access the body of the item with the Resource property off the ItemResponse.
        //        Console.WriteLine("Created item in database with id: {0}\n", intersportcustomerResponse.Resource.Id, intersportcustomerResponse.RequestCharge);
        //    }

        //}

        [HttpGet("GetCustomer")]
        public async Task<IActionResult> GetCustomerDetails()
        {
            try
            {
                var container = ContainerClient();
                var sqlQuery = "SELECT * FROM c";
                QueryDefinition queryDefinition = new QueryDefinition(sqlQuery);
                FeedIterator<Customer> queryResultSetIterator = container.GetItemQueryIterator<Customer>(queryDefinition);
                List<Customer> customers = new List<Customer>();
                while (queryResultSetIterator.HasMoreResults)
                {
                    FeedResponse<Customer> currentResultSet = await queryResultSetIterator.ReadNextAsync();
                    foreach (Customer customer in currentResultSet)
                    {
                        customers.Add(customer);
                    }
                }
                return Ok(customers);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetCustomerById")]

        //public async Task<IActionResult> GetCustomersById(string CustomerId, string partitionKey)
        public async Task<IActionResult> GetCustomersById(string CustomerId)
        {
            try

            {
                var container = ContainerClient();
                var sqlQuery = "SELECT * FROM c WHERE c.id = '" + CustomerId + "'";
                QueryDefinition queryDefinition = new QueryDefinition(sqlQuery);
                FeedIterator<Customer> queryResultSetIterator = container.GetItemQueryIterator<Customer>(queryDefinition);
                List<Customer> customers = new List<Customer>();
                while (queryResultSetIterator.HasMoreResults)
                {
                    FeedResponse<Customer> currentResultSet = await queryResultSetIterator.ReadNextAsync();
                    foreach (Customer customer in currentResultSet)
                    {
                        customers.Add(customer);
                    }
                }
                return Ok(customers);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Get Customer by Name
        [HttpGet("2.GetCustomerByName")]
        public async Task<Customer> GetCustomerByName(string company)
        {
            var container = ContainerClient();
            var sqlQuery = "SELECT * FROM c WHERE c.Company = '" + company + "'";
            QueryDefinition queryDefinition = new QueryDefinition(sqlQuery);
            FeedIterator<Customer> queryResultSetIterator = container.GetItemQueryIterator<Customer>(queryDefinition);
            List<Customer> customers = new List<Customer>();
            while (queryResultSetIterator.HasMoreResults)
            {
                FeedResponse<Customer> currentResultSet = await queryResultSetIterator.ReadNextAsync();
                foreach (Customer customer in currentResultSet)
                {
                    customers.Add(customer);
                }
            }
            return customers.FirstOrDefault();

            //using (var db = new CustomerContext())
            //    {
            //        //var customer = await db.Customers.FirstOrDefaultAsync(c => c.Company == company);
            //        return await db.Customers.SingleOrDefaultAsync(c => c.Company == company);
            //    // if else
            //    //if (customer == null)
            //    //{
            //    //    return NotFound();
            //    //}
            //    //else
            //    //{
            //    //    return customer;
            //    //}
            //    }
        }

        //Get customer by SalesRep
        [HttpGet("3.GetCustomerBySalesRep")]

        public async Task<IActionResult> GetCustomerBySalesRep(string salesrep)
        {
            try
            {
                var container = ContainerClient();
                //var sqlQuery = "SELECT * FROM c WHERE c.SalesReps.RepName = '" + repName + "'";
                var sqlQuery = "SELECT * FROM c WHERE c.SalesReps.RepName = '" + salesrep + "'";
                QueryDefinition queryDefinition = new QueryDefinition(sqlQuery);
                FeedIterator<Customer> queryResultSetIterator = container.GetItemQueryIterator<Customer>(queryDefinition);
                List<Customer> customers = new List<Customer>();
                while (queryResultSetIterator.HasMoreResults)
                {
                    FeedResponse<Customer> currentResultSet = await queryResultSetIterator.ReadNextAsync();
                    foreach (Customer customer in currentResultSet)
                    {
                        customers.Add(customer);
                    }
                }
                return Ok(customers);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpPut("4.UpdateCustomer")]
        //public async Task <IActionResult>UpdateCustomer(Customer customer, string partitionKey)
        //public async Task <IActionResult>UpdateCustomer(string CustomerId)
       
        public async Task<IActionResult> UpdateCustomer(Customer customer, string CustomerId)
        {
            try
            {
                // a try statement updating a customer if possible
                var container = ContainerClient();

                ItemResponse<Customer> response = await container.ReadItemAsync<Customer>(CustomerId, new Microsoft.Azure.Cosmos.PartitionKey(customer.CustomerId));
                //ItemResponse<Customer> response = await container.ReadItemAsync<Customer>(customer.CustomerId, new Microsoft.Azure.Cosmos.PartitionKey(partitionKey));
                //Get Existing Customer/Item
                // i demon används existingItem osv istf existingCustomer
                var existingItem = response.Resource;
                if (existingItem != null)
                {
                    existingItem.CustomerId = customer.CustomerId;
                    existingItem.Company = customer.Company;
                    existingItem.ContactName = customer.ContactName;
                    existingItem.Title = customer.Title;
                    existingItem.Email = customer.Email;
                    existingItem.Phone = customer.Phone;
                    existingItem.POBox = customer.POBox;
                    existingItem.PostalCode = customer.PostalCode;
                    existingItem.City = customer.City;
                    existingItem.salesrep = customer.salesrep;
                    existingItem.RepPhone = customer.RepPhone;
                    existingItem.RepEmail = customer.RepEmail;
                    var updateResponse = await container.ReplaceItemAsync(existingItem, customer.CustomerId, new Microsoft.Azure.Cosmos.PartitionKey(customer.CustomerId));
                    return Ok(updateResponse.Resource);
                }
                else
                {
                    return BadRequest("Customer not found");
                }
            }   

                //SalesReps.Include(x => x.SalesReps.Select(c => c.RepName)).Include(x => x.SalesReps.Select(c => c.RepPhone)).Include(x => x.SalesReps.Select(c => c.RepEmail)).Include;
                //existingCustomer.Addresses = salesrep.Addresses.Include(x => x.Addresses.Selext(c => c.Street)).Include(x => x.Addresses.Select(c => c.PostalCode)).Include(x => x.Addresses.Select(c => c.City));
                //var existingAddress = existingCustomer.Addresses.FirstOrDefault(x => x.Street(a => a.Street)).Include(x => x.Addresses.Select(c => c.PostalCode)).Include(x => x.Addresses.Select(c => c.City));
                //add data to SalesRep and Address
                //existingSalesRep.RepName = cust.RepName;
                // use LINQ to find the SalesRep and Address

                //var existingSalesRep = existingCustomer.SalesReps.FirstOrDefault(x => x.RepName == salesrep.RepName);
                //var existingAddress = existingCustomer.Addresses.FirstOrDefault(x => x.Street == salesrep.Street);
                // update the SalesRep and Address

                //if (existingSalesRep != null)
                //{
                //    existingSalesRep.RepName = salesrep.RepName;
                //    existingSalesRep.Phone = salesrep.RepPhone;
                //    existingSalesRep.Email = salesrep.RepEmail;
                //}

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("5.DeleteCustomer")]
        public async Task <IActionResult> DeleteCustomer(string CustomerId, string partitionKey)
        {
            try
            {
                var container = ContainerClient();
                var response = await container.DeleteItemAsync<Customer>(CustomerId, new PartitionKey(partitionKey));
                return Ok(response.StatusCode);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }   

    }
}
