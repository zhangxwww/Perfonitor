# Perfonitor

## What is it?

Perfonitor is a performance monitor on the CPU usage, memory usage, disk IO speed and network upload/download speed.

[MaterialDesignInXamlToolkit](https://github.com/MaterialDesignInXAML/MaterialDesignInXamlToolkit) is used to beautify the UI, and DynamicDataDisplay is used to plot the chart.

## Build from source

### Requirement: 

- Visual Studio 2019
- .net framework 4.7.2

To build the project from source code, you should first install the references:

```powershell
Install-Package MaterialDesignThemes
Install-Package ShowMeTheXAML.MSBuild
Install-Package DynamicDataDisplay
```

And then just open the solution and build the project in vs2019.