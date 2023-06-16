#HS-CosmosCustomerDB

# Inlämningsuppgift 2: Kunddata i en Cosmos DB databas med Azure Functions och CosmosDBTrigger
## Helena Sveding @Jensen Yrkeshögskola
## Moln-databaser

## Beskrivning
- Skapa en databas med möjlighet att hantera data om kunder och kundansvariga säljare.
- All information skall sparas i en databas som du själv skapar i Cosmos DB. 
- Följande information skall kunna registreras och sparas i databasen för en Kund:
- namn, titel, telefon, email, adress.
- Ansvarig säljare lagras med:
- namn, telefon och email.
- Varje kund måste ha en ansvarig säljare.
- Det skall gå att lägga till nya kunder. Dessa skall även kunna uppdateras och tas bort.
- Gränssnittet till applikationen skall skapas som ett web api.
- Det skall finnas en sökfunktionalitet för att söka på kunder dels på kundens namn men
även på ansvarig säljare.
- Det skall finnas en Azure function med en CosmosDBtrigger. Den skall meddela en
säljare när en ny kund lagts till som säljaren är ansvarig för.