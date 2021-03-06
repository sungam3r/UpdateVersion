﻿using Monbsoft.UpdateVersion.Commands;
using Monbsoft.UpdateVersion.Core;
using Monbsoft.UpdateVersion.Models;
using Monbsoft.UpdateVersion.Tests.Utilities;
using Xunit;

namespace Monbsoft.UpdateVersion.Tests
{
    public class PatchTests
    {
        private TestConsole _console;

        public PatchTests()
        {
            _console = new TestConsole();
        }

        [Fact]
        public void ChangePatchVersionTest()
        {
            using (var fs = new DisposableFileSystem())
            {
                fs.CreateFile("MySolution.sln");
                fs.CreateFolder("src/Services");
                fs.CreateFile("src/Services/project1.csproj", ProjectHelper.BuildVersion("1.5.1"));
                var store = new ProjectStore();
                var command = new PatchCommand();
                var context = new CommandContext(_console, Verbosity.Info);
                context.Directory = fs.RootPath;

                command.Execute(context);
                var project = store.Read(PathHelper.GetFile(fs, "src/Services/project1.csproj"));

                Assert.Equal("1.5.2", project.Version);

            }
        }
    }
}
