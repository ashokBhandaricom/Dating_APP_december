using System.Text;
using API.Data;
using API.Extensions;
using API.Interfaces;
using API.Middleware;
using API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddApplicationServices(builder.Configuration)  ; 

builder.Services.AddIdentityServices(builder.Configuration) ;
var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>() ; 

// if( builder.Environment.IsDevelopment())
// {
//     app.UseDeveloperExceptionPage(); 
// }
// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseMiddleware<ExceptionMiddleware>( ) ; 
app.UseAuthorization();
app.UseCors(builder=> builder.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200") ) ; 

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();

// just to  check if git is working or not 