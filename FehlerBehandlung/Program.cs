

using FehlerBehandlung.Filter;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(options=>
{
    //Ohne das Definieren von ExceptionFilterAttribute k�nnen wir Fehlermeldungen zentral im AddControllersWithViews-Dienst rendern.
    options.Filters.Add(new BenutzerdefiniertesHandleAusnahmeFilterAttribut() { ErrorPage = "Fehler1" });
});

builder.Services.AddDatabaseDeveloperPageExceptionFilter();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    //Mit UseDeveloperExceptionPage k�nnen wir auch zus�tzliche Fehler sehen.
    app.UseDeveloperExceptionPage();

    app.UseDatabaseErrorPage();

    //app.UseStatusCodePages("text/plain", "Es gibt Fehler. StatusCode:{0}");

    app.UseStatusCodePages(async statusCodeContext =>
    {
        statusCodeContext.HttpContext.Response.ContentType = "text/plain";
        await statusCodeContext.HttpContext.Response.WriteAsync(
            $"Es gibt Fehler. StatusCode:{statusCodeContext.HttpContext.Response.StatusCode}");
    });


    
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//Ich habe diese Fehlermeldung aus dem if-Blog entfernt, da ich m�chte,
//dass sie au�erhalb der Entwicklungsumgebung ausgef�hrt wird.

//app.UseExceptionHandler("/Home/Error");

//app.UseExceptionHandler(context =>
//{
//    context.Run(async page =>
//    {
//        page.Response.StatusCode = 500;
//        page.Response.ContentType = "text/html";
//        await page.Response.WriteAsync($"<html><head></head><h1>Es gibt Fehler:{page.Response.StatusCode}</h1></html>");
//    });
//});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
