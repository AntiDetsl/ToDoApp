using Microsoft.EntityFrameworkCore;
using ToDoApp.BLL;
using ToDoApp.BLL.Interfaces;
using ToDoApp.DAL;
using ToDoApp.DAL.Context;
using ToDoApp.DAL.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ToDoAppDbContext>(x => x.UseSqlServer(connection));

builder.Services.AddScoped<IListDao, ListDao>();
builder.Services.AddScoped<IToDoListLogic, ToDoListLogic>();

builder.Services.AddScoped<IToDoTaskDao, ToDoTaskDao>();
builder.Services.AddScoped<IToDoTaskLogic, ToDoTaskLogic>();

builder.Services.AddScoped<ITaskStepDao, TaskStepDao>();
builder.Services.AddScoped<IToDoTaskStepLogic, ToDoTaskStepLogic>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

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
