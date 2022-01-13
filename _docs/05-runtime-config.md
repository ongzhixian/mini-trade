# Runtime config

Configure the behavior of .NET applications at run time. 

Configuration options are places in a file: `runtimeconfig.template.json`

When a project is built, an `<app>.runtimeconfig.json` file is generated in the output directory. 
If a `runtimeconfig.template.json` file exists in the same folder as the project file, 
any configuration options it contains are inserted into the `<app>.runtimeconfig.json` file.

# GenerateRuntimeConfigurationFiles

`<app>.runtimeconfig.json` are only generated for executable projects.

For non-executable projects, `<app>.runtimeconfig.json`.

To force copy configuration options from `runtimeconfig.template.json` to `<app>.runtimeconfig.json`
We need to add `<GenerateRuntimeConfigurationFiles>` element to project file and set it to `true`.

```xml
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net6.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
    <UserSecretsId>8f26ab1f-7c94-4ef0-8a8d-ac7731bfa2b8</UserSecretsId>
    <GenerateRuntimeConfigurationFiles>true</GenerateRuntimeConfigurationFiles>
  </PropertyGroup>
```

## Compilation settings

### Tiered compilation
Configures whether the just-in-time (JIT) compiler uses tiered compilation. 
Tiered compilation transitions methods through two tiers:
The first tier generates code more quickly (quick JIT) or loads pre-compiled code (ReadyToRun).
The second tier generates optimized code in the background ("optimizing JIT").

### TieredCompilation.QuickJit
Configures whether the JIT compiler uses quick JIT. 
For methods that don't contain loops and for which pre-compiled code is not available, 
quick JIT compiles them more quickly but without optimizations.

Enabling quick JIT decreases startup time but can produce code with degraded performance characteristics. 
For example, the code may use more stack space, allocate more memory, and run slower.
If quick JIT is disabled but tiered compilation is enabled, only pre-compiled code participates in tiered compilation. 
If a method is not pre-compiled with ReadyToRun, the JIT behavior is the same as if tiered compilation were disabled.

### TieredCompilation.QuickJitForLoops

Configures whether the JIT compiler uses quick JIT on methods that contain loops.
Enabling quick JIT for loops may improve startup performance. 
However, long-running loops can get stuck in less-optimized code for long periods.
If quick JIT is disabled, this setting has no effect.

If you omit this setting, quick JIT is not used for methods that contain loops. 
This is equivalent to setting the value to false.

TieredCompilation                   (default: true = enabled in .NET Core 3.0 and later)
TieredCompilation.QuickJit          (default: true = enabled in .NET Core 3.0 and later)
TieredCompilation.QuickJitForLoops  ()


```json
{
   "runtimeOptions": {
        "configProperties": {
            "System.Runtime.TieredCompilation": false,
            "System.Runtime.TieredCompilation.QuickJit": false,
            "System.Runtime.TieredCompilation.QuickJitForLoops": false
      }
   }
}
```

Environment var     Description                     Value if omitted
DOTNET_ReadyToRun   ReadyToRun                      1 = enabled
                    Uses pre-compiled code for images with available ReadyToRun data. 
                    Disabling this option forces the runtime to JIT-compile framework code.
DOTNET_TieredPGO    Profile-guided optimization     ?
                    enables dynamic or tiered profile-guided optimization (PGO)

## Runtime configuration options for debugging and profiling

Only configurable via environment variables.

Environment var             Description                     Value if omitted
DOTNET_EnableDiagnostics    Enable diagnostics              1 = enabled
                            Configures whether debugger, profiler, and EventPipe diagnostics are enabled.
CORECLR_ENABLE_PROFILING    Enable profiling                0 - disabled
                            Configures whether profiling is enabled for the currently running process.
CORECLR_PROFILER            Profiler GUID                   <guid-string>
                            Specifies GUID of the profiler to load into the currently running process.
CORECLR_PROFILER_PATH                                       <string-path>
CORECLR_PROFILER_PATH_32
CORECLR_PROFILER_PATH_64
                            Specifies the path to the profiler DLL to load into the currently running process (or 32-bit or 64-bit process).
DOTNET_PerfMapEnabled       Write perf map                  0 - disabled
                            Enables or disables writing /tmp/perf-$pid.map on Linux systems.
DOTNET_PerfMapIgnoreSignal  Perf log markers                0 - disabled
                            Enables the specified signal to be accepted and ignored as a marker in the perf logs.


## Runtime configuration options for garbage collection

Incomplete! See: https://docs.microsoft.com/en-us/dotnet/core/run-time-config/garbage-collector

```json
{
    "runtimeOptions": {
        "configProperties": {
            "System.GC.Server": true,
            "System.GC.Concurrent": false,
            "System.GC.HeapCount": 16,
            "System.GC.HeapAffinitizeMask": 1023,
            "System.GC.GCHeapAffinitizeRanges": "0:1-10,0:12,1:50-52,1:70",
            "System.GC.CpuGroup": 0,
            "System.GC.NoAffinitize": true,
            "System.GC.HeapHardLimit": 209715200,
            "System.GC.HeapHardLimitPercent": 30,
            ...
            "System.GC.HeapHardLimitSOH": 0,
            "System.GC.HeapHardLimitLOH": 0,
            "System.GC.HeapHardLimitPOH": 0,
            "System.GC.HeapHardLimitSOHPercent": 0,
            "System.GC.HeapHardLimitLOHPercent": 0,
            "System.GC.HeapHardLimitPOHPercent": 0,
            "System.GC.HighMemoryPercent": 0,
            "System.GC.RetainVM": false,
            "System.GC.LOHThreshold": 85000 

        }
    }
}
```

Environment var                 Description                     Value if omitted
DOTNET_GCName                   Standalone GC                   <string_path>
DOTNET_gcAllowVeryLargeObjects  Allow large objects             1 - enabled
DOTNET_GCLargePages             Large pages                     0 - disabled



## Runtime configuration options for globalization

```json : Default
{
   "runtimeOptions": {
        "configProperties": {
            "System.Globalization.Invariant": true,
            "Switch.System.Globalization.EnforceJapaneseEraYearRanges": false, 
            "Switch.System.Globalization.EnforceLegacyJapaneseDateParsing": false,
            "Switch.System.Globalization.FormatJapaneseFirstYearAsANumber": false,
            "System.Globalization.UseNls": false,
            "System.Globalization.PredefinedCulturesOnly": true
      }
   }
}
```

## Runtime configuration options for networking

```json : Default
{
   "runtimeOptions": {
        "configProperties": {
            "System.Net.Http.SocketsHttpHandler.Http2Support": true,
            "System.Net.Http.UsePortInSpn": false,
            "System.Net.Http.UseSocketsHttpHandler": true,
            "System.Net.Http.SocketsHttpHandler.AllowLatin1Headers": false
      }
   }
}
```

## Runtime configuration options for threading

Environment var                 Description                     Value if omitted
DOTNET_Thread_UseAllCpuGroups   CPU groups                      0 - disabled
                                Configures whether threads are automatically distributed across CPU groups.


```json : Default
{
   "runtimeOptions": {
        "configProperties": {
            "System.Threading.ThreadPool.MinThreads": 4,
            "System.Threading.ThreadPool.MaxThreads": 20,

            "System.Threading.ThreadPool.Blocking.ThreadsToAddWithoutDelay_ProcCountFactor": 5
            "System.Threading.ThreadPool.Blocking.ThreadsToAddWithoutDelay_ProcCountFactor": 
            "System.Threading.ThreadPool.Blocking.ThreadsPerDelayStep_ProcCountFactor": 
            "System.Threading.ThreadPool.Blocking.DelayStepMs":
            "System.Threading.ThreadPool.Blocking.MaxDelayMs":
            "System.Threading.Thread.EnableAutoreleasePool": true

            
      }
   }
}
```


# Reference

.NET runtime configuration settings
https://docs.microsoft.com/en-us/dotnet/core/run-time-config/