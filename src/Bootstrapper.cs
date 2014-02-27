using Nancy;
using Nancy.TinyIoc;
using Newtonsoft.Json;
using Mtg;
using Nancy.Bootstrapper;
//using Nancy.LightningCache.Extensions;
using Nancy.Routing;
using System.Configuration;

public class Bootstrapper : DefaultNancyBootstrapper
{
    protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
    {
        base.ApplicationStartup(container, pipelines);

        StaticConfiguration.DisableErrorTraces = false;
        StaticConfiguration.EnableRequestTracing = true;

    }

    protected override void ConfigureApplicationContainer(TinyIoCContainer container)
    {
        base.ConfigureApplicationContainer(container);
        IRepository repository = new MongoRepository (ConfigurationManager.AppSettings.Get("db"));
        container.Register<IRepository>(repository);
    }
}

