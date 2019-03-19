using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace TestingStuff.EFCore
{
    public class InMemoryTest
    {
        public async Task TestScenario1()
        {
            var sp1 = BuildServiceProvider();
            var sp2 = BuildServiceProvider();

            var sp1C1 = sp1.GetRequiredService<TestDbContext>();
            var sp2Context = sp2.GetRequiredService<TestDbContext>();

            var id = Guid.Parse("F2AAEDB4-4CB9-4D0A-8DE8-0C5C2F51D39C");
            sp1C1.TestModels.Add(new TestModel(id, 3));

            var id3 = Guid.Parse("48F03255-B290-4B14-B291-1EFD59583677");
            var toBeDeleted = new TestModel(id3, 22);
            sp1C1.TestModels.Add(toBeDeleted);

            await sp1C1.SaveChangesAsync();

            var sp1C2 = sp1.GetRequiredService<TestDbContext>();

            var id2 = Guid.Parse("8E879E0B-D2B2-4BB0-976E-CB7BCADF4586");
            sp2Context.TestModels.Add(new TestModel(id2, 10));
            var firstModel1 = await sp2Context.TestModels.FindAsync(id);
            firstModel1.Value += 1;
            sp2Context.TestModels.Remove(toBeDeleted);
            await sp2Context.SaveChangesAsync();

            var firstModel2 = await sp1C2.TestModels.FindAsync(id);
            var firstModel2Value = firstModel2.Value;

            var secondModel = await sp1C2.TestModels.FindAsync(id2);
            var secondModelValue = secondModel.Value;

            var thirdModel = await sp1C2.TestModels.FirstOrDefaultAsync(t => t.Id == id3);

            var sp1C3 = sp1.GetRequiredService<TestDbContext>();
            var secondModelLastContext = await sp1C3.TestModels.FindAsync(id2);
            var secondModelLastContextValue = secondModelLastContext.Value;
            var firstModelLastContext = await sp1C3.TestModels.FindAsync(id);
            var firstModelLastContextValue = firstModelLastContext.Value;

            await sp1C3.Entry(firstModelLastContext).ReloadAsync();
            firstModelLastContextValue = firstModelLastContext.Value;

            var thirdModelLastContext = await sp1C3.TestModels.FirstOrDefaultAsync(t => t.Id == id3);

            var scopeFactory = sp1.GetRequiredService<IServiceScopeFactory>();
            using (var scope = scopeFactory.CreateScope())
            using (var scopeContext = scope.ServiceProvider.GetRequiredService<TestDbContext>())
            {
                var firstScope = await scopeContext.TestModels.FindAsync(id);
                var secondScope = await scopeContext.TestModels.FindAsync(id2);
                var thirdScope = await scopeContext.TestModels.FindAsync(id3);
            }

            var options = new DbContextOptionsBuilder<TestDbContext>().UseInMemoryDatabase("Default", DatabaseRoot.Root).Options;
            var manuallyCreatedContext = new TestDbContext(options);
            var firstModelManualContext = await manuallyCreatedContext.TestModels.FindAsync(id);
            var firstModelManualContextValue = firstModelManualContext.Value;
            var secondModelManualContext = await manuallyCreatedContext.TestModels.FindAsync(id2);
            var secondModelManualContextValue = secondModelManualContext.Value;

            var thirdModelManualContext = await manuallyCreatedContext.TestModels.FirstOrDefaultAsync(t => t.Id == id3);
        }

        private IServiceProvider BuildServiceProvider()
        {
            var services = new ServiceCollection();

            RegisterServices(services);

            var serviceProvider = services.BuildServiceProvider();

            return serviceProvider;
        }

        private void RegisterServices(IServiceCollection services)
        {
            services.AddEntityFrameworkInMemoryDatabase();
            services.AddDbContextPool<TestDbContext>(builder =>
            {
                builder.UseInMemoryDatabase("Default", DatabaseRoot.Root);
            });
        }
    }
}
