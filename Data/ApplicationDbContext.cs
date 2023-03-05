using Assignment.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set;}
        public DbSet<Order> Orders { get; set; }
        public DbSet<Request> Requests { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            SeedAuthor(builder);
            SeedBook(builder);
            SeedCategory(builder);
            SeedUersRole(builder);
            SeedRole(builder);
            SeedUser(builder);
        }
        private void SeedAuthor(ModelBuilder builder)
        {
            builder.Entity<Author>().HasData(
                new Author
                {
                    Id = 1,
                    Name = "Gosho AOYAMA",
                    Country = "Japan"
                },
                new Author
                {
                    Id = 2,
                    Name = "Fujiko F Fujio",
                    Country = "Japan"
                },
                new Author
                {
                    Id = 3,
                    Name = "Vu Trong Phung",
                    Country = "Viet Nam"
                },
                new Author
                {
                    Id = 4,
                    Name = "J. K. Rowling",
                    Country = "United States"
                }
                );
        }
        private void SeedBook(ModelBuilder builder)
        {
            builder.Entity<Book>().HasData(
                new Book
                {
                    Id = 1,
                    Title = "Conan",
                    Price = 5,
                    Image = "https://tuoitho.mobi/upload/doc-truyen/conan/anh-dai-dien.jpg",
                    Quantity = 30,
                    CategoryId = 1,
                    AuthorId = 1
                },
                new Book
                {
                    Id = 2,
                    Title = "Doremon",
                    Price = 3,
                    Image = "https://tuoitho.mobi/upload/truyen/doremon-truyen-ngan-tap-3/anh-bia.jpg",
                    Quantity = 30,
                    CategoryId = 1,
                    AuthorId = 2
                },
                new Book
                {
                    Id = 3,
                    Title = "C# book",
                    Price = 500,
                    Image = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRhN3A-fGPBHEBNaFFdpeDY9rr3n34jWylMEg&usqp=CAU",
                    Quantity = 30,
                    CategoryId = 3,
                    AuthorId = 2
                },
                new Book
                {
                    Id = 4,
                    Title = "Best english book",
                    Price = 200,
                    Image = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTpFrRPIX8FeWA8QgN9bH8aDAgXKsg38maanA&usqp=CAU",
                    Quantity = 30,
                    CategoryId = 4,
                    AuthorId = 3

                },
                new Book
                {
                    Id = 5,
                    Title = "Harry Potter",
                    Price = 120,
                    Image = "http://prodimage.images-bn.com/pimages/9780545139700_p0_v5_s1200x630.jpg",
                    Quantity = 30,
                    CategoryId = 2,
                    AuthorId = 4
                },
                new Book
                {
                    Id = 6,
                    Title = "The Shining",
                    Price = 120,
                    Image = "http://prodimage.images-bn.com/pimages/9780345806789_p0_v2_s1200x630.jpg",
                    Quantity = 30,
                    CategoryId = 3,
                    AuthorId = 1
                },
                new Book
                {
                    Id = 7,
                    Title = "Cormoran Strike",
                    Price = 120,
                    Image = "https://salt.tikicdn.com/ts/product/17/22/0b/da2ced7d44ce084dd6b6dde1ce00b97a.jpg",
                    Quantity = 30,
                    CategoryId = 4,
                    AuthorId = 4
                }
                );
        }
        private void SeedCategory(ModelBuilder builder)
        {
            builder.Entity<Category>().HasData(
                new Category
                {
                    Id = 1,
                    Name = "Romance"
                },
                new Category
                {
                    Id = 2,
                    Name = "Comic"
                },
                new Category
                {
                    Id = 3,
                    Name = "Adventure"
                },
                new Category
                {
                    Id = 4,
                    Name = "Mystery"
                },
                new Category
                {
                    Id = 5,
                    Name = "Horror"
                },
                new Category
                {
                    Id = 6,
                    Name = "Since Fiction"
                }
                );
        }
        private void SeedUersRole(ModelBuilder builder)
        {
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    UserId = "1",
                    RoleId= "1"
                },
                new IdentityUserRole<string>
                {
                    UserId = "2",
                    RoleId= "2"
                },
                new IdentityUserRole<string>
                {
                    UserId = "3",
                    RoleId= "3"
                }
                );
        }
        private void SeedRole(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id= "1",
                    Name = "Admin",
                    NormalizedName="Admin"
                },
                new IdentityRole
                {
                    Id= "2",
                    Name="Customer",
                    NormalizedName="Customer"
                },
                new IdentityRole
                {
                    Id= "3",
                    Name="StoreOwner",
                    NormalizedName="StoreOwner"
                }
                );
        }
        private void SeedUser(ModelBuilder builder)
        {
            //tạo tài khoản test cho admin, customer & storeowner
            var admin = new IdentityUser
            {
                Id = "1",
                Email = "admin@gmail.com",
                UserName = "admin@gmail.com",
                NormalizedUserName = "admin@gmail.com"
            };
            var customer = new IdentityUser
            {
                Id = "2",
                Email = "customer@gmail.com",
                UserName = "customer@gmail.com",
                NormalizedUserName = "customer@gmail.com"
            };
            var storeOwner = new IdentityUser
            {
                Id = "3",
                Email = "storeowner@gmail.com",
                UserName = "storeowner@gmail.com",
                NormalizedUserName = "storeowner@gmail.com"
            };
            //khai báo thư viện để mã hoá mật khẩu
            var hasher = new PasswordHasher<IdentityUser>();
            //set mật khẩu đã mã hoá
            admin.PasswordHash = hasher.HashPassword(admin, "P@ssw0rd");
            customer.PasswordHash = hasher.HashPassword(customer, "P@ssw0rd");
            storeOwner.PasswordHash = hasher.HashPassword(storeOwner, "P@ssw0rd");
            builder.Entity<IdentityUser>().HasData(admin,customer, storeOwner);
        }
    }

}
