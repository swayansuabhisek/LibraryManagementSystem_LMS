using LMS.API.Middleware;
using LMS.Core.Services.Interface;
using LMS.Infrastructure;
using LMS.Infrastructure.Repositories;
using LMS.Infrastructure.Repositories.Interface;
using LMS.Service.Services;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using FluentValidation.AspNetCore;
using LMS.Shared.DtoValidators;

const string AllowedCORSPolicy = "AllowAll";
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssembly(typeof(CreateAuthorDtoValidator).Assembly);

builder.Services.AddDbContext<LibraryDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("LMSConnString")));

builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IBorrowerRepository, BorrowerRepository>();
builder.Services.AddScoped<IBorrowerService, BorrowerService>();
builder.Services.AddScoped<IBorrowBookRepository, BorrowBookRepository>();
builder.Services.AddScoped<ILibraryOperationService, LibraryOperationService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(AllowedCORSPolicy, policy =>
        policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod());
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseCors(AllowedCORSPolicy);

app.UseMiddleware<GlobalExceptionMiddleware>();

app.MapControllers();

app.Run();
