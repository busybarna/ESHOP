using EShop.Extensions;
using EShop.Middleware.CustomExceptionMiddleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllersWithViews();  
builder.Services.AddApplicationServices();
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
// Enable Swagger   
builder.Services.AddSwaggerGen(swagger =>  
{  
    //This is to generate the Default UI of Swagger Documentation  
    swagger.SwaggerDoc("v1", new OpenApiInfo  
    {   
        Version= "v1",   
        Title = "ESHOP API",  
        Description="ESHOP applicaton API" });
      // Include the XML comments file
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    swagger.IncludeXmlComments(xmlPath);
});  

// builder.Services.AddAuthentication(option =>  
// {  
//     option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;  
//     option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;  

// }).AddJwtBearer(options =>  
// {  
//     options.TokenValidationParameters = new TokenValidationParameters  
//     {  
//         ValidateIssuer = true,  
//         ValidateAudience = true,  
//         ValidateLifetime = false,  
//         ValidateIssuerSigningKey = true,  
//         ValidIssuer = builder.Configuration["Jwt:Issuer"],  
//         ValidAudience = builder.Configuration["Jwt:Audience"],  
//         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]))
//     };  
// });  
builder.Logging.AddLog4Net("log4net.config");

var app = builder.Build();
app.UseExceptionHandlerMiddleware();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>{
        options.SwaggerEndpoint("swagger/v1/swagger.json","v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();
app.UseRouting();  

app.UseAuthorization();
app.UseEndpoints(endpoints =>  
{  
    endpoints.MapControllers();  
});  
app.UseAuthentication();
app.Run();
