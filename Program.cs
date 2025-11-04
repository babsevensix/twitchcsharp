using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger; 


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});

builder.Services.AddDbContext<WebApiDbContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
    );

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

}
app.UseSwagger();
app.UseSwaggerUI();
//app.UseHttpsRedirection();



app.MapGet("/rubrica", (WebApiDbContext dbContext) =>
{
    return dbContext.Persone
        .Include(p=>p.ListIndirizzi)
        .ThenInclude(i=>i.LinkCitta).ToList();
})
.WithName("Rubrica");

app.MapGet("/rubrica/{id}", (WebApiDbContext dbContext, int id) =>
{
    return dbContext.Persone.SingleOrDefault(p => p.Id == id);
})
.WithName("Elemento rubrica");

app.MapGet("/rubrica/by/nome/{nome}", (WebApiDbContext dbContext, string nome) =>
{
    return dbContext.Persone
        .OrderByDescending(p=>p.Cognome)
        .Where(p => p.Nome.Contains(nome))
        .ToList();
})
.WithName("Elemento rubrica by nome");

app.MapPost("/rubrica", (PersonaDTO nuovaPersonaReq, WebApiDbContext dbContext) =>
{
    PersonaEntity nuovaPersonaEntity = new PersonaEntity();
    nuovaPersonaEntity.Cognome = nuovaPersonaReq.cognome;
    nuovaPersonaEntity.Nome = nuovaPersonaReq.nome;
    nuovaPersonaEntity.Telefono = nuovaPersonaReq.telefono;


    dbContext.Persone.Add(nuovaPersonaEntity);
    dbContext.SaveChanges();

    return Results.Created();
});

app.MapPut("/rubrica/{id}", (int id, PersonaDTO personaDtoReq, WebApiDbContext dbContext) =>
{

    
    var personaEntity = dbContext.Persone
        .Include(p=>p.ListIndirizzi)
        .ThenInclude(i=>i.LinkCitta)
        .FirstOrDefault(p => p.Id == id);
    if (personaEntity == null)
    {
        return Results.NotFound();
    }
    else
    {
        personaEntity.Cognome = personaDtoReq.cognome;
        personaEntity.Nome = personaDtoReq.nome;
        personaEntity.Telefono = personaDtoReq.telefono;

        if (personaDtoReq.nomeCitta != null && personaDtoReq.via != null && personaDtoReq.cap != null)
        {
            // Inseriamo l'indirizzo sempre
            IndirizzoEntity ie = new IndirizzoEntity();
            ie.Default = false;
            ie.Via = personaDtoReq.via;

            bool existCitta = dbContext.Citta.Any(c => c.Nome == personaDtoReq.nomeCitta && c.Cap == personaDtoReq.cap);
            if (existCitta)
            {
                ie.LinkCitta = dbContext.Citta.First(c => c.Nome == personaDtoReq.nomeCitta && c.Cap == personaDtoReq.cap);

            }
            else
            {
                // var cittaNew = new CittaEntity
                // {
                //     Cap = personaDtoReq.cap,
                //     Nome = personaDtoReq.nomeCitta
                // };
                // dbContext.Add(cittaNew);
                // ie.LinkCitta = cittaNew;

                ie.LinkCitta = new CittaEntity
                {
                    Nome = personaDtoReq.nomeCitta,
                    Cap = personaDtoReq.cap
                };
            }

            personaEntity.ListIndirizzi.Add(ie);

        }

        dbContext.SaveChanges();
        return Results.Ok( dbContext.Persone.ToList());
    }
    
});

app.MapDelete("/rubrica/{id}", (int id, WebApiDbContext dbContext) =>
{
    var elemento = dbContext.Persone.FirstOrDefault(p => p.Id == id);
    //var elemento = elementiRubrica.FirstOrDefault(r => r.id == id);
    if (elemento == null)
    {
        return Results.NotFound();
    }
    else
    {
        dbContext.Persone.Remove(elemento);
        dbContext.SaveChanges();


    }
    return Results.Ok(dbContext.Persone.ToList());
});


app.Use(async (context, next) =>
{
    Console.WriteLine($"Request : {context.Request.Method} {context.Request.Path}");
    await next();
    Console.WriteLine($"Response : {context.Response.StatusCode}");
});


app.Run();



record PersonaDTO(string cognome, string nome, string telefono, string? via, string? nomeCitta, string? cap);
