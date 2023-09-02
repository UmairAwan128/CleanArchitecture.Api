namespace CleanArchitecture.Api.Extensions
{
    public static class CorsPolicyExtensions
    {
        public static void ConfigureCorsPolicy(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });

                //options.AddPolicy(name: MyAllowSpecificOrigins,
                //                  policy =>
                //                  {
                //                      policy.WithOrigins(_clientUrl);
                //                  });
            });

        }
    }
}
