using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class AliassesCommand : ICommand<string>
{
    private readonly IDebugConsole<string> console;

    public AliassesCommand(IDebugConsole<string> console) => this.console = console;
    public void Execute(Action<string> log, params string[] args)
    {
        var cmdNameOrAlias = args[0];

        if (console.IsValidCommand(cmdNameOrAlias))
        {
            var command = console.Commands.FirstOrDefault(c => c.Name == cmdNameOrAlias || c.Aliases.Contains(cmdNameOrAlias));
            if (command != null)
                log($"{command.Name} => [{command.Aliases.Aggregate("", (current, alias) => $"{current}, {alias}")}]");
            return;
        }

        log($"Command not found: {cmdNameOrAlias}");
    }

    public string Name => "alias";
    public IEnumerable<string> Aliases => new[] { "aliases", "ALIAS", "ALIASES" };
    public string Description => $"Logs the aliases for current command";
}
