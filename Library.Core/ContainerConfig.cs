using Autofac;
using Library.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Core
{
    public class ContainerConfig
    {
        public IContainer Build()
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterType<Engine>().As<IEngine>().SingleInstance();
            containerBuilder.RegisterType<ConsoleRenderer>().As<IRenderer>();
            containerBuilder.RegisterType<CommandProcessor>().As<ICommandProcessor>();
            containerBuilder.RegisterType<CommandFactory>().As<ICommandFactory>().SingleInstance();

            return containerBuilder.Build();
        }
    }
}

