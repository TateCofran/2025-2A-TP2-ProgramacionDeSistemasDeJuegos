using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IDebugConsole<T>
{
    HashSet<ICommand<T>> Commands { get; set; }
    void AddCommand(ICommand<T> command);
    bool IsValidCommand(T name);
    void ExecuteCommand(T name, params T[] args);

    bool TryAddCommand(ICommand<T> command);
}

