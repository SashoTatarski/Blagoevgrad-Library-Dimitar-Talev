//using Library.Database;
//using Library.Models.Enums;
//using Library.Models.Models;
//using Microsoft.AspNetCore.Builder;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.DependencyInjection;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace Library.Web.Models
//{
//    public class SeedData
//    {
//        public static void EnsurePopulated(IApplicationBuilder app)
//        {
//            LibraryContext context = app.ApplicationServices.GetRequiredService<LibraryContext>();
//            context.Database.Migrate();
//            if (!context.Books.Any())
//            {
//                var genre1 = new Genre { GenreName = "Philosophy" };
//                context.Genres.Add(genre1);

//                var book1 = new Book
//                {
//                    Author = new Author { Name = "Jordan Peterson" },
//                    Title = "12 Rules For Life",
//                    ISBN = "345816021",
//                    Publisher = new Publisher { Name = "Random House of Canada" },
//                    Year = 2018,
//                    Rack = 11,
//                    Status = BookStatus.Available
//                };                
//                var bookGenre1 = context.BookGenre.Add(new BookGenre { BookId = book1.Id, GenreId = genre1.Id });

//                var genre2 = new Genre { GenreName = "Behavioural Psychology" };
//                context.Genres.Add(genre2);

//                var book2 = new Book
//                {
//                    Author = new Author { Name = "Jordan Peterson" },
//                    Title = "Maps of Meaning: The Architecture of Belief",
//                    ISBN = "B00BW54XVO",
//                    Publisher = new Publisher { Name = "Routledge" },
//                    Year = 1999,
//                    Rack = 22,
//                    Status = BookStatus.Available
//                };
//                var bookGenre2 = context.BookGenre.Add(new BookGenre { BookId = book2.Id, GenreId = genre2.Id });

//                var genre3 = new Genre { GenreName = "Political Humor" };
//                context.Genres.Add(genre3);

//                var book3 = new Book
//                {
//                    Author = new Author { Name = "Milo Yiannaopoulos" },
//                    Title = "Dangerous",
//                    ISBN = "069289344X",
//                    Publisher = new Publisher { Name = "Instagram" },
//                    Year = 2007,
//                    Rack = 33,
//                    Status = BookStatus.Available
//                };
//                var bookGenre3 = context.BookGenre.Add(new BookGenre { BookId = book3.Id, GenreId = genre3.Id });

//                var genre4 = new Genre { GenreName = "Religion" };
//                context.Genres.Add(genre4);

//                var book4 = new Book
//                {
//                    Author = new Author { Name = "Ben Shapiro" },
//                    Title = "The Right Side of History",
//                    ISBN = "62857908",
//                    Publisher = new Publisher { Name = "Broadside" },
//                    Year = 2019,
//                    Rack = 44,
//                    Status = BookStatus.Available
//                };
//                var bookGenre4 = context.BookGenre.Add(new BookGenre { BookId = book4.Id, GenreId = genre4.Id });


//                var genre5 = new Genre { GenreName = "Mysticism" };
//                context.Genres.Add(genre5);

//                var book5 = new Book
//                {
//                    Author = new Author { Name = "Sue Lilly" },
//                    Title = "Tree Essence",
//                    ISBN = "1861630840",
//                    Publisher = new Publisher { Name = "Capall Bann Publishing" },
//                    Year = 2001,
//                    Rack = 55,
//                    Status = BookStatus.Available
//                };
//                var bookGenre5 = context.BookGenre.Add(new BookGenre { BookId = book5.Id, GenreId = genre5.Id });

//                var genre6 = new Genre { GenreName = "Business & Money" };
//                context.Genres.Add(genre6);

//                var book6 = new Book
//                {
//                    Author = new Author { Name = "Steven Kotler" },
//                    Title = "The Rise of Superman: Decoding the Science of Ultimate Human Performance",
//                    ISBN = "B00BW54XVO",
//                    Publisher = new Publisher { Name = "Amazon Publishing" },
//                    Year = 2014,
//                    Rack = 66,
//                    Status = BookStatus.Available
//                };
//                var bookGenre6 = context.BookGenre.Add(new BookGenre { BookId = book6.Id, GenreId = genre6.Id });

//                var genre7 = new Genre { GenreName = "Mysticism" };
//                context.Genres.Add(genre7);

//                var book7 = new Book
//                {
//                    Author = new Author { Name = "Alan Abbadessa" },
//                    Title = "The Sync Book: Myths Magic Media and Mindscapes",
//                    ISBN = "1463764006",
//                    Publisher = new Publisher { Name = "Sync Book Press" },
//                    Year = 2011,
//                    Rack = 77,
//                    Status = BookStatus.Available
//                };
//                var bookGenre7 = context.BookGenre.Add(new BookGenre { BookId = book7.Id, GenreId = genre7.Id });

//                var genre8 = new Genre { GenreName = "Mysticism" };
//                context.Genres.Add(genre7);

//                var book8 = new Book
//                {
//                    Author = new Author { Name = "Alan Abbadessa" },
//                    Title = "The Sync Book 2",
//                    ISBN = "615724892",
//                    Publisher = new Publisher { Name = "Sync Book Press" },
//                    Year = 2012,
//                    Rack = 88,
//                    Status = BookStatus.Available
//                };
//                var bookGenre8 = context.BookGenre.Add(new BookGenre { BookId = book8.Id, GenreId = genre8.Id });

//                context.Genres.AddRange(genre1, genre2, genre3, genre4, genre5, genre6, genre7,genre8);
//                context.Books.AddRange(book1, book2, book3, book4, book5, book6, book7, book8);

//                context.SaveChanges();
//            }
//        }
//    }
//}

