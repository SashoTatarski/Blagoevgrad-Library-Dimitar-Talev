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
        }
    }
}
