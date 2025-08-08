using BattleGame.Api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ► Add services
builder.Services.AddDbContext<GameDb>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ► HTTP pipeline
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();         // Swagger JSON
    app.UseSwaggerUI();       // Swagger UI
}
app.UseHttpsRedirection();

// ► Minimal-API endpoints (ví dụ CRUD Player)
app.MapGet("/players", async (GameDb db) =>
    await db.Players
            .Include(p => p.PlayerAssets).ThenInclude(pa => pa.Asset)
            .Select(p => new {
                p.PlayerId, p.PlayerName, p.Level, p.Age,
                Assets = p.PlayerAssets.Select(pa => pa.Asset.AssetName)
            }).ToListAsync());

app.MapPost("/players", async (Player dto, GameDb db) =>
{
    db.Players.Add(dto);
    await db.SaveChangesAsync();
    return Results.Created($"/players/{dto.PlayerId}", dto);
});

app.MapPut("/players/{id}", async (Guid id, Player dto, GameDb db) =>
{
    var p = await db.Players.FindAsync(id);
    if (p is null) return Results.NotFound();
    db.Entry(p).CurrentValues.SetValues(dto);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/players/{id}", async (Guid id, GameDb db) =>
{
    var p = await db.Players.FindAsync(id);
    if (p is null) return Results.NotFound();
    db.Players.Remove(p);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

// TODO: lặp lại cho /assets và /playerassets

app.Run();
