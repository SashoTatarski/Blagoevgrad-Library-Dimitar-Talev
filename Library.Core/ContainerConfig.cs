using Autofac;
using Library.Core.Commands;
using Library.Core.Contracts;
using Library.Database;
using Library.Database.Contracts;
using Library.Database.JsonHandler;
using Library.Services;
using Library.Services.Contracts;
using Library.Services.Factories;
using Library.Services.Factories.Contracts;
using Services;
using Services.Contracts;

namespace Library.Core
{
    public class ContainerConfig
    {
        public IContainer Build()
        {
            // ASK: Should we/how to impore this method so that it's not so big?
            // SOLID: Single Reponsibility
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterType<Engine>().As<IEngine>().SingleInstance();
            //DataBase
            containerBuilder.RegisterType<LibraryContext>().AsSelf().SingleInstance();
            containerBuilder.RegisterGeneric(typeof(BookDatabase<>)).As(typeof(IDataBase<>)).SingleInstance();
            containerBuilder.RegisterGeneric(typeof(LibrarianDataBase<>)).As(typeof(IDataBase<>)).SingleInstance();
            containerBuilder.RegisterGeneric(typeof(UserDataBase<>)).As(typeof(IDataBase<>)).SingleInstance();
            containerBuilder.RegisterGeneric(typeof(AuthorDataBase<>)).As(typeof(IDataBase<>)).SingleInstance();
            containerBuilder.RegisterGeneric(typeof(GenreDataBase<>)).As(typeof(IDataBase<>)).SingleInstance();
            containerBuilder.RegisterGeneric(typeof(PublisherDataBase<>)).As(typeof(IDataBase<>)).SingleInstance();

            // Factories
            containerBuilder.RegisterType<BookFactory>().As<IBookFactory>();
            containerBuilder.RegisterType<UserFactory>().As<IUserFactory>();
            containerBuilder.RegisterType<MenuFactory>().As<IMenuFactory>();
            containerBuilder.RegisterType<LibrarianFactory>().As<ILibrarianFactory>();
            containerBuilder.RegisterType<AuthorFactory>().As<IAuthorFactory>();
            containerBuilder.RegisterType<GenreFactory>().As<IGenreFactory>();
            containerBuilder.RegisterType<PublisherFactory>().As<IPublisherFactory>();

            // Providers
            containerBuilder.RegisterType<ConsoleRenderer>().As<IConsoleRenderer>();
            containerBuilder.RegisterType<CommandParser>().As<ICommandParser>();
            containerBuilder.RegisterType<ConsoleFormatter>().As<IConsoleFormatter>();
            containerBuilder.RegisterGeneric(typeof(UsersJsonHandler<>)).As(typeof(IJsonHandler<>));
            containerBuilder.RegisterGeneric(typeof(LibrariansJsonHandler<>)).As(typeof(IJsonHandler<>));
            containerBuilder.RegisterGeneric(typeof(BooksJsonHandler<>)).As(typeof(IJsonHandler<>));
            // Service Managers
            containerBuilder.RegisterType<AuthenticationManager>().As<IAuthenticationManager>().SingleInstance();
            containerBuilder.RegisterType<AccountManager>().As<IAccountManager>();
            containerBuilder.RegisterType<BookManager>().As<IBookManager>();
            containerBuilder.RegisterType<LibrarySystem>().As<ILibrarySystem>();
            containerBuilder.RegisterType<DataService>().As<IDataService>();
            // Commands
            containerBuilder.RegisterType<AddBookCommand>().Named<ICommand>("addbook");
            containerBuilder.RegisterType<RemoveBookCommand>().Named<ICommand>("removebook");
            containerBuilder.RegisterType<EditBookCommand>().Named<ICommand>("editbook");
            containerBuilder.RegisterType<SearchBookCommand>().Named<ICommand>("search");
            containerBuilder.RegisterType<RegisterUserCommand>().Named<ICommand>("registeruser");
            containerBuilder.RegisterType<RemoveUserCommand>().Named<ICommand>("removeuser");
            containerBuilder.RegisterType<RegisterLibrarianCommand>().Named<ICommand>("registerlibrarian");
            containerBuilder.RegisterType<LoginCommand>().Named<ICommand>("login");
            containerBuilder.RegisterType<LogoutCommand>().Named<ICommand>("logout");
            containerBuilder.RegisterType<CheckOutBookCommand>().Named<ICommand>("checkoutbook");
            containerBuilder.RegisterType<ReturnBookCommand>().Named<ICommand>("returnbook");
            containerBuilder.RegisterType<ReserveBookCommand>().Named<ICommand>("reservebook");
            containerBuilder.RegisterType<TravelInTimeCommand>().Named<ICommand>("travelintime");
            containerBuilder.RegisterType<ExitCommand>().Named<ICommand>("exit");

            return containerBuilder.Build();
        }
    }
}

