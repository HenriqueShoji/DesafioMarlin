using DesafioMarlin.Repositories;
using DesafioMarlin.Services;
using DesafioMarlin.Services.AlunoServices;
using DesafioMarlin.Services.TurmaServices;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<Context>();
builder.Services.AddTransient<AlunoService, AlunoService>();
builder.Services.AddScoped<AlunoService, AlunoService>();
builder.Services.AddTransient<TurmaService, TurmaService>();
builder.Services.AddScoped<TurmaService, TurmaService>();
builder.Services.AddTransient<VerificacaoService, VerificacaoService>();
builder.Services.AddScoped<VerificacaoService, VerificacaoService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();