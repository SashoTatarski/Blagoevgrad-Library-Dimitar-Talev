using Autofac;
using Library.Core.Commands;
using Library.Core.Contracts;
using Library.Core.Factory;
using Library.Database;
using Library.Services;
using Library.Services.Contracts;
using Services;
using Services.Contracts;

namespace Library.Core
{
    public class ContainerConfig
    {
        public IContainer Build()
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterType<Engine>().As<IEngine>().SingleInstance();
            containerBuilder.RegisterType<DatabaseService>().As<IDatabaseService>().SingleInstance();
            containerBuilder.RegisterType<BookFactory>().As<IBookFactory>();
            containerBuilder.RegisterType<UserFactory>().As<IUserFactory>();
            containerBuilder.RegisterType<LibrarianFactory>().As<ILibrarianFactory>();

            containerBuilder.RegisterType<ConsoleRenderer>().As<IConsoleRenderer>();
            containerBuilder.RegisterType<CommandParser>().As<ICommandParser>();
            containerBuilder.RegisterType<Database.Database>().As<IDatabase>().SingleInstance();
            containerBuilder.RegisterType<AccountManager>().As<IAccountManager>().SingleInstance();
            containerBuilder.RegisterType<MenuFactory>().As<IMenuFactory>();
            containerBuilder.RegisterType<Search>().As<ISearch>();

            containerBuilder.RegisterType<AddBookCommand>().Named<ICommand>("addbook");
            containerBuilder.RegisterType<RemoveBookCommand>().Named<ICommand>("removebook");
            containerBuilder.RegisterType<EditBookCommand>().Named<ICommand>("editbook");
            containerBuilder.RegisterType<ViewAccountsCommand>().Named<ICommand>("viewaccounts");
            containerBuilder.RegisterType<SearchBookCommand>().Named<ICommand>("search");
            containerBuilder.RegisterType<RegisterUserCommand>().Named<ICommand>("registeruser");
            containerBuilder.RegisterType<RemoveUserCommand>().Named<ICommand>("removeuser");
            containerBuilder.RegisterType<RegisterLibrarianCommand>().Named<ICommand>("registerlibrarian");




            containerBuilder.RegisterType<LoginCommand>().Named<ICommand>("login");
            containerBuilder.RegisterType<LogoutCommand>().Named<ICommand>("logout");
            containerBuilder.RegisterType<CheckOutBookCommand>().Named<ICommand>("checkoutbook");
            containerBuilder.RegisterType<ReturnBookCommand>().Named<ICommand>("returnbook");


            return containerBuilder.Build();
        }
    }
}

