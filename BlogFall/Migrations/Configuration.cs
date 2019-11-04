namespace BlogFall.Migrations
{
    using BlogFall.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BlogFall.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(BlogFall.Models.ApplicationDbContext context)
        {
            #region Admin Rolünü ve Kullanýcýsýný oluþtur
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Admin" };

                manager.Create(role);
            }

            if (!context.Users.Any(u => u.UserName == "saroglu.erol@gmail.com"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser { UserName = "saroglu.erol@gmail.com", Email= "saroglu.erol@gmail.com" };

                manager.Create(user, "Erol1.");
                manager.AddToRole(user.Id, "Admin");

                // Oluþturulan kullanýcýya ait yazýlar ekleyelim

                #region Kategoriler ve Yazýlar

                if (!context.Categories.Any())
                {
                    Category cat1 = new Category
                    {
                        CategoryName = "Gezi Yazýlarý"
                    };
                    cat1.Posts = new List<Post>();
                    cat1.Posts.Add(new Post
                    {
                        Title = "Gezi Yazýlarý 1",
                        AuthorId = user.Id,
                        Content = @"<p>Tincidunt integer eu augue augue nunc elit dolor, luctus placerat scelerisque euismod, iaculis eu lacus nunc mi elit, vehicula ut laoreet ac, aliquam sit amet justo nunc tempor, metus vel.</p>
<p>Placerat suscipit, orci nisl iaculis eros, a tincidunt nisi odio eget lorem nulla condimentum tempor mattis ut vitae feugiat augue cras ut metus a risus iaculis scelerisque eu ac ante.</p>
<p>Fusce non varius purus aenean nec magna felis fusce vestibulum velit mollis odio sollicitudin lacinia aliquam posuere, sapien elementum lobortis tincidunt, turpis dui ornare nisl, sollicitudin interdum turpis nunc eget.</p>",
                        CreateTime = DateTime.Now
                    });
                    cat1.Posts.Add(new Post
                    {
                        Title = "Gezi Yazýlarý 2",
                        AuthorId = user.Id,
                        Content = @"<p>Tincidunt integer eu augue augue nunc elit dolor, luctus placerat scelerisque euismod, iaculis eu lacus nunc mi elit, vehicula ut laoreet ac, aliquam sit amet justo nunc tempor, metus vel.</p>
<p>Placerat suscipit, orci nisl iaculis eros, a tincidunt nisi odio eget lorem nulla condimentum tempor mattis ut vitae feugiat augue cras ut metus a risus iaculis scelerisque eu ac ante.</p>
<p>Fusce non varius purus aenean nec magna felis fusce vestibulum velit mollis odio sollicitudin lacinia aliquam posuere, sapien elementum lobortis tincidunt, turpis dui ornare nisl, sollicitudin interdum turpis nunc eget.</p>",
                        CreateTime = DateTime.Now
                    });
                    Category cat2 = new Category
                    {
                        CategoryName = "Ýþ Yazýlarý"
                    };
                    cat2.Posts = new List<Post>();
                    cat2.Posts.Add(new Post
                    {
                        Title = "Ýþ Yazýlarý 1",
                        AuthorId = user.Id,
                        Content = @"<p>Tincidunt integer eu augue augue nunc elit dolor, luctus placerat scelerisque euismod, iaculis eu lacus nunc mi elit, vehicula ut laoreet ac, aliquam sit amet justo nunc tempor, metus vel.</p>
<p>Placerat suscipit, orci nisl iaculis eros, a tincidunt nisi odio eget lorem nulla condimentum tempor mattis ut vitae feugiat augue cras ut metus a risus iaculis scelerisque eu ac ante.</p>
<p>Fusce non varius purus aenean nec magna felis fusce vestibulum velit mollis odio sollicitudin lacinia aliquam posuere, sapien elementum lobortis tincidunt, turpis dui ornare nisl, sollicitudin interdum turpis nunc eget.</p>",
                        CreateTime = DateTime.Now
                    });
                    cat2.Posts.Add(new Post
                    {
                        Title = "Ýþ Yazýlarý 2",
                        AuthorId = user.Id,
                        Content = @"<p>Tincidunt integer eu augue augue nunc elit dolor, luctus placerat scelerisque euismod, iaculis eu lacus nunc mi elit, vehicula ut laoreet ac, aliquam sit amet justo nunc tempor, metus vel.</p>
<p>Placerat suscipit, orci nisl iaculis eros, a tincidunt nisi odio eget lorem nulla condimentum tempor mattis ut vitae feugiat augue cras ut metus a risus iaculis scelerisque eu ac ante.</p>
<p>Fusce non varius purus aenean nec magna felis fusce vestibulum velit mollis odio sollicitudin lacinia aliquam posuere, sapien elementum lobortis tincidunt, turpis dui ornare nisl, sollicitudin interdum turpis nunc eget.</p>",
                        CreateTime = DateTime.Now
                    });

                    context.Categories.Add(cat1);
                    context.Categories.Add(cat2);

                }

                #endregion
            }
            #endregion

            #region Admin Kullanýcýsýna 77 yazý eklenmesi
            if (!context.Categories.Any(x => x.CategoryName =="Diðer"))
            {
                ApplicationUser admin = context.Users.Where(x => x.UserName == "saroglu.erol@gmail.com").FirstOrDefault();

                if (admin != null)
                {
                    if (!context.Categories.Any(x => x.CategoryName =="Diðer"))
                    {
                        Category diger = new Category
                        {
                            CategoryName = "Diðer",
                            Posts = new HashSet<Post>()
                        };
                        for (int i = 0; i < 77; i++)
                        {
                            diger.Posts.Add(new Post
                            {
                                Title = "Diðer Yazý " + i,
                                AuthorId = admin.Id,
                                Content = @"<p>Tincidunt integer eu augue augue nunc elit dolor, luctus placerat scelerisque euismod, iaculis eu lacus nunc mi elit, vehicula ut laoreet ac, aliquam sit amet justo nunc tempor, metus vel.</p>
<p>Placerat suscipit, orci nisl iaculis eros, a tincidunt nisi odio eget lorem nulla condimentum tempor mattis ut vitae feugiat augue cras ut metus a risus iaculis scelerisque eu ac ante.</p>
<p>Fusce non varius purus aenean nec magna felis fusce vestibulum velit mollis odio sollicitudin lacinia aliquam posuere, sapien elementum lobortis tincidunt, turpis dui ornare nisl, sollicitudin interdum turpis nunc eget.</p>",
                                CreateTime = DateTime.Now.AddMinutes(i)
                            });
                        }
                        context.Categories.Add(diger);
                    }
                }
            }
            #endregion

        }
    }
}
