using Autofac;
using Library.Core;
using Library.Core.Contracts;
using System;

namespace Library.CLI
{
    public class Startup
    {
        public static void Main()
        {
            var containerConfig = new ContainerConfig();
            var container = containerConfig.Build();

            var engine = container.Resolve<IEngine>();
            engine.Start();

            //addbook,Sasho Tatarski, My First Title,383838383, Some Category, Retard Sensation Publishing,2019,100
        }
    }
}
