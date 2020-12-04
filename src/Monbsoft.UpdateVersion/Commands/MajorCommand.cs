﻿using Monbsoft.UpdateVersion.Core;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.IO;
using System.Threading.Tasks;

namespace Monbsoft.UpdateVersion.Commands
{
    public class MajorCommand : VersionCommandBase
    {
        public static Command Create()
        {
            var command = CreateCommand("major", "Increment major version number");

            command.Handler = CommandHandler.Create<VersionCommandArguments>(async args =>
            {
                var context = new CommandContext(args.Console, args.Verbosity)
                {
                    Directory = Directory.GetCurrentDirectory(),
                    Message = args.Message,
                    Verbosity = args.Verbosity
                };
                var command = new MajorCommand();
                await command.ExecuteAsync(context);
            });

            return command;
        }

        public async Task ExecuteAsync(CommandContext context)
        {
            int count = await UpdateAsync(context, (oldVersion) =>
            {
                return oldVersion.Change(major: oldVersion.Major + 1, 0, 0);
            });
            context.WriteInfo($"{count} major versions are updated.");
        }
    }
}
