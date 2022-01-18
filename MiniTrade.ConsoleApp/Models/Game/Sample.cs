using StackExchange.Profiling;
using System.Diagnostics;

namespace MiniTrade.ConsoleApp.Models.Game;

[DebuggerDisplay("{StringProperty,nq} - {IntProperty}")]
public class ExampleDebuggerDisplayObject
{
    public int ObjectId { get; set; }
    
    public string StringProperty { get; set; }
    
    public int IntProperty { get; set; }
}

[DebuggerDisplay("{DebuggerDisplay(),nq}")]
public class ExampleDebuggerDisplayObject2
{
    public int ObjectId { get; set; }
    public string StringProperty { get; set; }
    public int IntProperty { get; set; }

#if DEBUG
    private string DebuggerDisplay()
    {
        return
            $"ObjectId:{this.ObjectId}, StringProperty:{this.StringProperty}, Type:{this.GetType()}";
    }
#endif
}

public class ExampleMiniProfiler
{
    // See: https://miniprofiler.com/dotnet/ConsoleDotNetCore
    // See: https://www.code4it.dev/blog/miniprofiler

    public void DoWork()
    {
        var profiler = MiniProfiler.StartNew("My Profiler Name");
        using (profiler.Step("Main Work"))
        {
            // Do some work...
        }

        // View results
        Console.WriteLine(profiler.RenderPlainText());
        // or for the active profiler:
        Console.WriteLine(MiniProfiler.Current.RenderPlainText());

    }
}