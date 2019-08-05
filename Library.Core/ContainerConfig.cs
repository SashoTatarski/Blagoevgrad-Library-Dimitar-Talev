﻿using Autofac;
using Library.Core.Commands;
using Library.Core.Contracts;
using Library.Core.Factory;
using Library.Database;

namespace Library.Core
{
    public class ContainerConfig
    {
        public IContainer Build()
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterType<Engine>().As<IEngine>().SingleInstance();
            containerBuilder.RegisterType<CatalogDatabase>().As<IDatabase>().SingleInstance();
            containerBuilder.RegisterType<BookFactory>().As<IBookFactory>();
            containerBuilder.RegisterType<ConsoleRenderer>().As<IRenderer>();
            containerBuilder.RegisterType<CommandProcessor>().As<ICommandProcessor>();
            containerBuilder.RegisterType<Json>().As<IJson>().SingleInstance();

            containerBuilder.RegisterType<AddBookCommand>().Named<ICommand>("addbook");

            return containerBuilder.Build();
        }
    }
}

