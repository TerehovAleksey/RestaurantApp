using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DAL
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            if (Database.EnsureCreated())
            {
                #region создание ролей пользователей и администратора

                // роли пользователей
                var roleStore = new RoleStore<IdentityRole>(this);
                var roleManager = new RoleManager<IdentityRole>
                    (roleStore,
                    new IRoleValidator<IdentityRole>[] { },
                    new UpperInvariantLookupNormalizer(),
                    new IdentityErrorDescriber(), null);
                roleManager.CreateAsync(new IdentityRole { Name = Domain.UserRoles.ADMIN }).GetAwaiter().GetResult();
                roleManager.CreateAsync(new IdentityRole { Name = Domain.UserRoles.USER }).GetAwaiter().GetResult();

                // учётная запись администратора
                var userStore = new UserStore<ApplicationUser>(this);
                var userManager = new UserManager<ApplicationUser>
                    (userStore,
                    new OptionsManager<IdentityOptions>(new OptionsFactory<IdentityOptions>(new IConfigureOptions<IdentityOptions>[] { },
                    new IPostConfigureOptions<IdentityOptions>[] { })),
                    new PasswordHasher<ApplicationUser>(),
                    new IUserValidator<ApplicationUser>[] { },
                    new IPasswordValidator<ApplicationUser>[] { },
                    new UpperInvariantLookupNormalizer(),
                    new IdentityErrorDescriber(),
                    null, null
                    );
                var user = new ApplicationUser() { UserName = "Admin", Email = "admin@mail.com", EmailConfirmed = true };
                var result = userManager.CreateAsync(user, "secret").GetAwaiter().GetResult();
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, Domain.UserRoles.ADMIN).GetAwaiter().GetResult();
                    userManager.AddToRoleAsync(user, Domain.UserRoles.USER).GetAwaiter().GetResult();
                }

                #endregion
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            #region заполнение базы данных начальными данными

            builder.Entity<PaymentMethod>().HasData(new PaymentMethod { Id = 1, PaymentName = "Наличные" }, new PaymentMethod { Id = 2, PaymentName = "Карта" });
            builder.Entity<Category>().HasData(new Category { Id = 1, Name = "Пицца" }, new Category { Id = 2, Name = "Напитки" });
            builder.Entity<Product>().HasData(new Product { Id = 1, CategoryId = 2, Name = "Кола", Price = 1.5m },
                new Product { Id = 2, CategoryId = 2, Name = "Фанта", Price = 1.4m });

            #endregion

        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            //организация отметки об удалении вместо физического удаления из базы данных
            foreach (var entry in ChangeTracker.Entries().Where(e => e.State == EntityState.Deleted))
            {
                if (entry.Entity is IMarkDeleted mark)
                {
                    mark.IsDeleted = true;
                    entry.State = EntityState.Modified;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderProduct> OrderProducts { get; set; }

        public DbSet<PaymentMethod> PaymentMethods { get; set; }

        public DbSet<Product> Products { get; set; }
    }
}
