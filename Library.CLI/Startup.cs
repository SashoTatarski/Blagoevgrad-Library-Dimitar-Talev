﻿using Autofac;
using Library.Core;
using Library.Core.Contracts;
using System;

namespace Library.CLI
{
    public static class Startup
    {
        public static void Main()
        {
            ProgramStart();
        }

        private static void ProgramStart()
        {
            var containerConfig = new ContainerConfig();
            var container = containerConfig.Build();

            var engine = container.Resolve<IEngine>();
            engine.Start();
        }
    }
}
