using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface ICommand<T>
{
    T Name { get; }
    IEnumerable<T> Aliases { get; }
    T Description { get; }
    void Execute(Action<T> log, params T[] args);
}
