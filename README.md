# ModShardPacker

A CLI tool to pack mod code source from MSL.

## Install

if not already installed:
```shell
dotnet tool install --global ModShardPacker --version LastVersionNumber
```

To update:
```shell
dotnet tool update --global ModShardPacker --version LastVersionNumber --no-cache
```

## Uses

Basic use:
```shell
msp -f path/to/your/mod -n NameOfYourMod -o path/to/the/output
```

> To enable logging output, you can add the `-v` flag.

> You can also specify the location of extra dlls with the `-d` argument.