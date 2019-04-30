﻿using System;
using System.Threading;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Extensions;
using ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Generators.NSwagStudio;
using EnvDTE;
using Microsoft.VisualStudio.Shell;
using Task = System.Threading.Tasks.Task;

namespace ChristianHelle.DeveloperTools.CodeGenerators.ApiClient.Commands.NSwagStudio
{
    public class NSwagStudioCommand : ICommandInitializer
    {
        public const string ContextGuid = "65B3A74F-CD47-476A-A992-0C3DE31455FD";
        public const string Name = "NSwag Studio Context";
        public const string Expression = "nswag";
        public const string TermValue = "HierSingleSelectionName:.nswag$";

        protected int CommandId { get; } = 0x100;
        protected Guid CommandSet { get; } = new Guid("F76783DA-7AE3-4EDB-BDF4-B580CAB1BA90");

        public Task InitializeAsync(AsyncPackage package, CancellationToken token)
            => package.SetupCommandAsync(
                CommandSet,
                CommandId,
                OnExecute,
                token);

        private static void OnExecute(DTE dte)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            var item = dte.SelectedItems.Item(1).ProjectItem;

            var nswagStudioFile = item.FileNames[0];
            var codeGenerator = new NSwagStudioCodeGenerator(nswagStudioFile);
            codeGenerator.GenerateCode(null);
        }
    }
}