using Infrastructure.Settings;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Services.Interfaces;

namespace Infrastructure.Persistence.Files;

public static class Extensions
{
    public static void UseFilesConfiguration(this WebApplication app, IConfiguration config)
    {
        var filesOptions = app.Services.GetRequiredService<IOptions<FilesSettings>>().Value;
        filesOptions.AppDirectory = new DirectoryInfo(app.Environment.ContentRootPath!).Parent!.Parent!.ToString();
        
        if (!Directory.Exists(filesOptions.ImagesRoute())) 
            Directory.CreateDirectory(filesOptions.ImagesRoute());

       // app.UseDefaultFiles();
        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(filesOptions.FilesRoute()),
            RequestPath = filesOptions.RequestPath(),
        });
    }
}