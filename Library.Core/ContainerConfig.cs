using Autofac;
using Library.Core.Commands;
using Library.Core.Contracts;
using Library.Core.Factory;
using Library.Database;
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
            containerBuilder.RegisterType<Service>().As<IService>().SingleInstance();
            containerBuilder.RegisterType<BookFactory>().As<IBookFactory>();
            containerBuilder.RegisterType<ConsoleRenderer>().As<IRenderer>();
            containerBuilder.RegisterType<CommandParser>().As<ICommandParser>();
            containerBuilder.RegisterType<Database.Database>().As<IDatabase>().SingleInstance();
            containerBuilder.RegisterType<AccountManager>().As<IAccountManager>().SingleInstance();
            containerBuilder.RegisterType<MenuFactory>().As<IMenuFactory>();

            containerBuilder.RegisterType<AddBookCommand>().Named<ICommand>("addbook");
            containerBuilder.RegisterType<LoginCommand>().Named<ICommand>("login");
            containerBuilder.RegisterType<LogoutCommand>().Named<ICommand>("logout");


            return containerBuilder.Build();
        }
    }
}

