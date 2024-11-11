namespace PlatformService.Data
{
    public class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using (var servicesScoped = app.ApplicationServices.CreateScope())
            {
                SeedData(servicesScoped.ServiceProvider.GetService<PlatformDbContext>());
            }
        }

        private static void SeedData(PlatformDbContext context)
        {
            if (!context.Platforms.Any())
            {
                context.Platforms.AddRange(
                    new Models.Platform() { Name = "Dot Net", Publisher = "Microsoft", Cost = "Free" },
                    new Models.Platform() { Name = "Sql Server Express", Publisher = "Microsoft", Cost = "Free" },
                    new Models.Platform() { Name = "Kubernetes", Publisher = "Cloud Native Computing Foundation", Cost = "Free" }
                    );

                context.SaveChanges();
            }
        }
    }
}
