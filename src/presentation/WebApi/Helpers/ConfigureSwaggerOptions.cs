using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WebApi.Helpers
{
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) => _provider = provider;

        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in _provider.ApiVersionDescriptions)
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
        }

        private static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
        {
            var info = new OpenApiInfo
            {
                Title = "CleanArchitecture",
                Version = description.ApiVersion.ToString(),
                Description = "Web Service for CleanArchitecture.",
                Contact = new OpenApiContact
                {
                    Name = "lei zhao",
                    Email = "17630367972@163.com",
                    Url = new Uri("https://www.baidu.com")
                }
            };

            if (description.IsDeprecated)
                info.Description += " <strong>This API version of CleanArchitecture has been deprecated.</strong>";

            return info;
        }
    }
}
