using Nancy;
using Nancy.TinyIoc;
using Newtonsoft.Json;
using Mtg;
using Nancy.Bootstrapper;
using System.Web.Caching;
using Nancy.Routing;
using System.Configuration;
using Nancy.Conventions;

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
        IRepository repository = 
            new MongoRepository (ConfigurationManager.AppSettings.Get("db"));

        IWriteRepository wrepository = 
            new MongoRepository (ConfigurationManager.AppSettings.Get("db"));

        Cache cache = new Cache();

        container.Register<IWriteRepository>(wrepository);
        container.Register<IRepository>(repository);
        container.Register<Cache>(cache);

    }

    protected override void ConfigureConventions(NancyConventions nancyConventions)
    {
        base.ConfigureConventions(nancyConventions);

        nancyConventions.StaticContentsConventions.Add(
            StaticContentConventionBuilder.AddDirectory("/", "/Static", new[] { ".xml", ".txt" })
        );
    }
}

