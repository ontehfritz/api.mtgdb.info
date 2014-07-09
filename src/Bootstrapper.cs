using Nancy;
using Nancy.TinyIoc;
using Newtonsoft.Json;
using Mtg;
using Nancy.Bootstrapper;
using Nancy.Routing;
using System.Configuration;
using Nancy.Conventions;
using Newtonsoft.Json.Serialization;

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
            

        container.Register(typeof(JsonSerializer), typeof(CustomJsonSerializer));

       
        container.Register<IWriteRepository>(wrepository);
        container.Register<IRepository>(repository);
    }

    protected override void ConfigureConventions(NancyConventions nancyConventions)
    {
        base.ConfigureConventions(nancyConventions);
       

        nancyConventions.StaticContentsConventions.Add(
            StaticContentConventionBuilder.AddDirectory("/", "/Static", new[] { ".xml", ".txt" })
        );
    }
}

public class CustomJsonSerializer : JsonSerializer
{
    public CustomJsonSerializer()
    {
        this.ContractResolver = new CamelCasePropertyNamesContractResolver();
       // this.Formatting = Formatting.Indented;
    }
}
