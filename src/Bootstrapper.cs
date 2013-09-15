using System;

namespace Magic
{
    using Nancy;
	using Nancy.TinyIoc;
    using Nancy.Routing;
    using Nancy.LightningCache.Extensions;

    public class Bootstrapper : DefaultNancyBootstrapper
    {
        // The bootstrapper enables you to reconfigure the composition of the framework,
        // by overriding the various methods and properties.
        // For more information https://github.com/NancyFx/Nancy/wiki/Bootstrapper

        protected override void ApplicationStartup(Nancy.TinyIoc.TinyIoCContainer container, 
                                                   Nancy.Bootstrapper.IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);
            /*enable lightningcache, vary by url params id,query,take and skip*/
            this.EnableLightningCache(container.Resolve<IRouteResolver>(), ApplicationPipelines, new[] { "id", 
                "query", "take", "skip", "type","card_set_id","artist", "rarity", "loyalty","toughness",
                "power", "subtype", "card_set_name","convertedmanacost", "set_number", "manacost", "colors",
                "name", "limit"});
        }
    }
}