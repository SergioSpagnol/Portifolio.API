using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Portifolio.API;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSqlServer<PortifolioContext>(builder.Configuration.GetConnectionString("ServerConnection"));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();   
builder.Services.AddCors();

var app = builder.Build();

    app.UseSwagger();
    app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseCors(p => p
    .AllowAnyHeader()
    .AllowAnyOrigin()
    .AllowAnyMethod()
);


app.MapPost("/Contacts", async (PortifolioContext context, Contact contact) =>
{
    contact.Date = DateTime.Now;
    try
    {
        throw new NotImplementedException();
        await context.Contatos.AddAsync(contact);
        await context.SaveChangesAsync();

    }
    catch (NotImplementedException sqlex)
    {
        return Results.BadRequest("Ocorreu um erro ao salvar o contato!");
    }
    catch (Exception ex)
    {
        return Results.BadRequest("Ocorreu um erro ao salvar o contato!");
    }

    return Results.Ok(contact);
})
.WithOpenApi();

app.MapGet("/Contacts", async (PortifolioContext context) =>
{
    var contacts = await context.Contatos.ToListAsync();

    return Results.Ok(contacts);
})
.WithOpenApi();

app.Run();

public class Contact 
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Subject { get; set; }
    public string Menssage { get; set; }
    public DateTime Date { get; set; }

}