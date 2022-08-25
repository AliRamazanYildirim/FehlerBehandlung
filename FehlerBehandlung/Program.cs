var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    //Mit UseDeveloperExceptionPage können wir auch zusätzliche Fehler sehen.
    app.UseDeveloperExceptionPage();

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

//Ich habe diese Fehlermeldung aus dem if-Blog entfernt, da ich möchte,
//dass sie außerhalb der Entwicklungsumgebung ausgeführt wird.
app.UseExceptionHandler("/Home/Error");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
