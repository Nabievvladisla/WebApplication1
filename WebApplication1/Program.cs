

using Microsoft.AspNetCore.Http;
using System.Net;
using System.Numerics;
using System.Xml.Linq;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.UseMiddleware<CompanyMiddleware>();
app.Run();
public class Company
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
}
public class CompanyMiddleware
{
    private readonly RequestDelegate next;

    public CompanyMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        string path = context.Request.Path;
        if (path == "/")
        {
            await context.Response.WriteAsync("Hello teacher");
        }
        else if (path == "/company")
        {
            var company = new Company()
            {
                Name = "Lenovo",
                Address = " Kyiv, Ukraine",
                Phone = "0993402295"
            };
            context.Items["Company"] = company;

            await context.Response.WriteAsync($"Company: {company.Name}, Address: {company.Address}, Phone: {company.Phone}");
        }
        else if (path=="/random")
        {
            Random random = new Random();
            int randomNumber = random.Next(0, 101);
            await context.Response.WriteAsync(randomNumber.ToString());
        }
        else
        {
            context.Response.StatusCode = 404;
            await context.Response.WriteAsync("Not found");
        }
    }
}
       








