using System;
using NUnit.Framework;
using Nancy.Testing;
using mtgapi_test;
using Nancy;

namespace mtgapi_test
{
	[TestFixture()]
	public class TestMtgApi
	{
		[Test()]
		public void test_intro_page ()
		{
			// Given
			var bootstrapper = new DefaultNancyBootstrapper();
			var browser = new Browser(bootstrapper);

			// When
			var result = browser.Get("/", with => {
				with.HttpRequest();
			});

			// Then
			Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
		}
	}
}

