using Threadly.UI.Configurations;
using Threadly.UI.Services.Abstracts;
using Threadly.UI.Services.Concretes;

var builder = WebApplication.CreateBuilder(args);

AppSettings.Configuration = builder.Configuration;


builder.Services.AddRazorPages().AddRazorRuntimeCompilation();




builder.Services.AddHttpClient<IApiService, ApiService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiClient:BaseUrl"]);
    //client.DefaultRequestHeaders.Add("Accept", "application/json");  // Ýstemcinin JSON formatýndaki bir yanýt beklediðini gösterir.
    //client.DefaultRequestHeaders.Add("Authorization", "Bearer your_token_here");
    //client.DefaultRequestHeaders.Add("User-Agent", "MyApp/1.0"); // Bu baþlýk, istemcinin (örneðin, bir tarayýcý, uygulama veya API istemcisi) sunucuya kendini tanýtmasýný saðlar.

});



// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddRazorOptions(options =>
    {
        
        options.ViewLocationFormats.Add("/{0}.cshtml");
    });






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
