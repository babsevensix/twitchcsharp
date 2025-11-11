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

builder.Services.AddScoped(typeof(IEntityBaseRepository<>), typeof(EntityBaseRepository<>));

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

}
app.UseSwagger();
app.UseSwaggerUI();
//app.UseHttpsRedirection();



app.MapGet("/rubrica", (IEntityBaseRepository<PersonaEntity> repository) =>
{
    // return dbContext.Persone
    //     .Include(p=>p.ListIndirizzi)
    //     .ThenInclude(i=>i.LinkCitta).ToList();

    return repository.All
       .Include(p => p.ListIndirizzi)
       .ThenInclude(i => i.LinkCitta).ToList();
})
.WithName("Rubrica");

app.MapGet("/indirizzi", (IEntityBaseRepository<IndirizzoEntity> repository) =>
{
    return repository.All.Select(i => i.LinkCitta.Nome)
        
        .Distinct().OrderBy(x=>x).ToList();
});

app.MapGet("/rubrica/{id}", (IEntityBaseRepository<PersonaEntity> repository, int id) =>
{
    //return repository.GetSingle(id);
    return repository.AllIncluding(p => p.ListIndirizzi)
        .Select(x=> new { via = x.ListIndirizzi.First().Via, x.Id }).FirstOrDefault(x => x.Id == id);
})
.WithName("Elemento rubrica");

app.MapGet("/rubrica/by/nome/{nome}", (IEntityBaseRepository<PersonaEntity> repository, string nome) =>
{
    return repository.All
        .OrderByDescending(p=>p.Cognome)
        .Where(p => p.Nome.Contains(nome))
        .ToList();
})
.WithName("Elemento rubrica by nome");

app.MapPost("/rubrica", (PersonaDTO nuovaPersonaReq, IEntityBaseRepository<PersonaEntity> repository) =>
{
    PersonaEntity nuovaPersonaEntity = new PersonaEntity();
    nuovaPersonaEntity.Cognome = nuovaPersonaReq.cognome;
    nuovaPersonaEntity.Nome = nuovaPersonaReq.nome;
    nuovaPersonaEntity.Telefono = nuovaPersonaReq.telefono;


    repository.Add(nuovaPersonaEntity);
    repository.SaveChanges();

    return Results.Created();
});

app.MapPut("/rubrica/{id}", (int id, PersonaDTO personaDtoReq,
    IEntityBaseRepository<PersonaEntity> personaRepository,
    IEntityBaseRepository<CittaEntity> cittaRepository) =>
{

    
    var personaEntity = personaRepository.All
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

            bool existCitta = cittaRepository.All.Any(c => c.Nome == personaDtoReq.nomeCitta && c.Cap == personaDtoReq.cap);
            if (existCitta)
            {
                ie.LinkCitta = cittaRepository.All.First(c => c.Nome == personaDtoReq.nomeCitta && c.Cap == personaDtoReq.cap);

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

        personaRepository.SaveChanges();
        return Results.Ok( personaRepository.All.ToList());
    }
    
});

app.MapDelete("/rubrica/{id}", (int id, IEntityBaseRepository<PersonaEntity> personeRepository) =>
{
    var elemento = personeRepository.AllIncluding(x=>x.ListIndirizzi)
        .FirstOrDefault(p => p.Id == id);
    //var elemento = elementiRubrica.FirstOrDefault(r => r.id == id);
    if (elemento == null)
    {
        return Results.NotFound();
    }
    else
    {
        personeRepository.Delete(elemento);
        personeRepository.SaveChanges();


    }
    return Results.Ok(personeRepository.All.ToList());
});


app.Use(async (context, next) =>
{
    Console.WriteLine($"Request : {context.Request.Method} {context.Request.Path}");
    await next();
    Console.WriteLine($"Response : {context.Response.StatusCode}");
});


app.Run();



record PersonaDTO(string cognome, string nome, string telefono, string? via, string? nomeCitta, string? cap);
