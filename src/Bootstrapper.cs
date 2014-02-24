using Nancy;
using Nancy.TinyIoc;
using Newtonsoft.Json;
using Mtg;
using Nancy.Bootstrapper;
using Nancy.LightningCache.Extensions;
using Nancy.Routing;

public class Bootstrapper : DefaultNancyBootstrapper
{
    protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
    {
        base.ApplicationStartup(container, pipelines);
        this.EnableLightningCache(container.Resolve<IRouteResolver>(), ApplicationPipelines, 
            new[] { "id", "query", "fields", "colors", "setNumber", "name","searchName",          
                "description", "flavor", "colors", "manacost", "convertedManaCost","cardSetName",         
                "type","subType","power","toughness", "loyalty", "rarity","artist","cardImage", "cardSetId", "rulings",             
                "formats", "releasedAt" });

        StaticConfiguration.DisableErrorTraces = false;
        StaticConfiguration.EnableRequestTracing = true;

    }
}

