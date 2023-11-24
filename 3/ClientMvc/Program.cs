using ClientHTML;
using ClientMvc.Controllers;
using ClientMvc.Services;
using Grpc.Net.Client;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

using var channel = GrpcChannel.ForAddress("http://localhost:5117");

var client = new ToDoServiceProto.ToDoServiceProtoClient(channel);

builder.Services.AddScoped<TodoService>(x=> new TodoService(channel));
builder.Services.AddScoped<ToDoServiceProto.ToDoServiceProtoClient>(x =>
    new ToDoServiceProto.ToDoServiceProtoClient(channel));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
