using Nancy;
using Nancy.TinyIoc;
using Newtonsoft.Json;
using Mtg;
using Nancy.Bootstrapper;

public class Bootstrapper : DefaultNancyBootstrapper
{
    protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
    {
        base.ApplicationStartup(container, pipelines);
        StaticConfiguration.DisableErrorTraces = false;
        StaticConfiguration.EnableRequestTracing = true;
    }
//    protected override byte[] FavIcon
//    {
//        get { return null; }
//    }
}

