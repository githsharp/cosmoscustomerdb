using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Add services to the container - för att include ska fungera
//
//builder.Services.AddControllers()
//    .AddJsonOptions(options =>
//    {
//        // Ignore some of the nulls, but not nested in lists
//        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
//        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve; // To handle circular references
//        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles; // Ignores the $id and $ref properties
//    });


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//app.UseCors();

//app.UseCors(builder =>
//                builder.AllowAnyMethod()
//                .AllowAnyHeader()
//                .SetIsOriginAllowed(origin => true)
//                .AllowCredentials());


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
