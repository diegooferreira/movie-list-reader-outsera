namespace Outsera.MovieListReader.Api.Configurations
{
    public static class SwaggerConfigurations
    {
        public static void ConfigureSwagger(this WebApplication app)
        {

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                    options.RoutePrefix = string.Empty;
                });
            }
        }
    }
}
