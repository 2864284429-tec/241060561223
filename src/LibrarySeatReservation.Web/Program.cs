using LibrarySeatReservation.Web.DataAccess;
using LibrarySeatReservation.Web.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// EF Core - 优先 SQL Server，不可用时回退 SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
{
    var sqliteConn = builder.Configuration.GetConnectionString("SqliteConnection");
    if (!string.IsNullOrEmpty(sqliteConn))
    {
        options.UseSqlite(sqliteConn);
    }
    else
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    }
});

// Session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// DI 注册
builder.Services.AddScoped<ISeatService, SeatService>();
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<IAdminAuthService, AdminAuthService>();

// MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// 自动迁移 + 种子数据
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    // SQLite 使用 EnsureCreated；SQL Server 使用 Migrate
    if (db.Database.IsSqlite())
    {
        db.Database.EnsureCreated();
    }
    else
    {
        db.Database.Migrate();
    }
}

// 开发环境种子数据（EF Core HasData 已包含种子数据，Migrate 自动应用）

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
