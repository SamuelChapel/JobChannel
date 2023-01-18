using System;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;

namespace JobChannel.Doc
{
    public class ConfigDocFxUI
    {
        public string Path { get; set; }
    }

    public static class DocFxExtension
    {
        public static void UseDocFxUI(this IApplicationBuilder app, Action<ConfigDocFxUI> settings)
        {
            ConfigDocFxUI configDocFxUI = new ConfigDocFxUI();

            settings.Invoke(configDocFxUI);

            configDocFxUI.Path ??= "/doc";

            app.UseFileServer(new FileServerOptions()
            {
                RequestPath = new PathString(configDocFxUI.Path),
                FileProvider = new EmbeddedFileProvider(Assembly.GetExecutingAssembly(), "JobChannel.DocFx._site")
            });
        }
    }
}
