using System.Net.Http.Headers;

using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;



namespace BlazorStaticSiteGeneratorExperiment.BlazorApp.Extensions
{
	public static class ContentFulClientHostBuilderExtensions
	{
		public static void AddContentfulGraphQLClient(this IServiceCollection services)
		{
			//Note: Ideally this could do with pulling out into configuration, blah blah, but this is all good for purposes of demo
			var graphQlClient = new GraphQLHttpClient(
					"https://graphql.contentful.com/content/v1/spaces/5gi0c2vvptsm/environments/master",
				new NewtonsoftJsonSerializer());

			//Note:  this credential [to a demo Contentful CMS] is OK to be shared in a public GitHub Repo
			graphQlClient.HttpClient.DefaultRequestHeaders.Authorization
			= new AuthenticationHeaderValue("Bearer", "Tk0jMX-Z6addZMiJpd0WR_wN57LBUv6oQOScfU2n3qA");

			services.AddScoped(sp => graphQlClient);

		}
	}
}
