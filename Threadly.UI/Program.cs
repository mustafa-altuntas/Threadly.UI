using Microsoft.AspNetCore.Authentication.Cookies;
using Threadly.UI.Configurations;
using Threadly.UI.Constants;
using Threadly.UI.HttpClients;
using Threadly.UI.Middlewares;
using Threadly.UI.Services.Abstracts;
using Threadly.UI.Services.Abstracts.AuthService;
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

})
    .AddHttpMessageHandler<BearerTokenHandler>();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddTransient<BearerTokenHandler>();





//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//})
//    .AddJwtBearer(CookieAuthenticationDefaults.AuthenticationScheme, options =>
//    {
//        options.TokenValidationParameters = new TokenValidationParameters()
//        {
//            ValidateAudience = true,
//            ValidateIssuer = true,
//            ValidateLifetime = true,
//            ValidateIssuerSigningKey = true,

//            ValidAudience = builder.Configuration["JwtToken:Audience"],
//            ValidIssuer = builder.Configuration["JwtToken:Issuer"],
//            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtToken:SecretKey"])),
//            ClockSkew = TimeSpan.Zero,

//            NameClaimType = ClaimTypes.Name //Jwt üzerinde Name Claim 'e karþýlýk gelen deðeri User.Indentity.Name üzerinden elde edebiliriz.

//        };
//    });







builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
.AddCookie(options =>
{
    options.Cookie.Name = CookieConstant.CookieName;
    options.LoginPath = "/Auth/Login";
    options.LogoutPath = "/Home/CikisYapti";
    options.AccessDeniedPath = "/Account/Yetkisiz"; // Login olmuþ ama istek attýðý sayfaya yetkisi yoksa yönlendirileceði sayfa
    options.Cookie.HttpOnly = true; // Sadece sunucu tarafýndan okunabilir
    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest; // Ýstek https veya http

    // Default expire time
    options.ExpireTimeSpan = TimeSpan.FromMicroseconds(1);
    options.SlidingExpiration = false;

    // Dinamik olarak expire süresini belirlemek için OnSigningIn eventi
    options.Events = new CookieAuthenticationEvents
    {
        OnSigningIn = async context =>
        {
            Console.WriteLine($"\n Oturum açma iþlemi sýrasýnda mevcut zaman: {DateTime.UtcNow} \n ");

            // Token 'dan gelen expire süresini alýnýr.
            // Kullanýcýnýn token expire bilgisi "claim" içinde çaðrýlýr.
            var accessTokenExpiration = context.Principal?.FindFirst(CookieConstant.AccessTokenExpiration)?.Value;
            if (accessTokenExpiration != null && DateTime.TryParse(accessTokenExpiration, out var accessTokenDateTime))
            {
                // Çerez süresi olarak token expire süresi atanýr.
                context.Properties.ExpiresUtc = accessTokenDateTime.ToUniversalTime(); // UTC'ye dönüþtür
            }

            await Task.CompletedTask;
        },

        OnSigningOut = async context =>
        {
            var signInService = context.HttpContext.RequestServices.GetRequiredService<ISignInService>();
            await signInService.LoginRefreshTokenAsync("", context.HttpContext);
        }
    };
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
//app.UseMiddleware<TokenMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
