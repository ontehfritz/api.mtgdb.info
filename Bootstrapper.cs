namespace Magic
{
    using Nancy;
	using Nancy.TinyIoc;
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        // The bootstrapper enables you to reconfigure the composition of the framework,
        // by overriding the various methods and properties.
        // For more information https://github.com/NancyFx/Nancy/wiki/Bootstrapper

//		protected override byte[] FavIcon
//		{
//			get { return null; }
//		}

//		protected override void ApplicationStartup(TinyIoCContainer container, 
//		                                           Nancy.Bootstrapper.IPipelines pipelines)
//		{
//			base.ApplicationStartup(container, pipelines);
//			Elmahlogging.Enable(pipelines, "elmah");
//		}
    }
}