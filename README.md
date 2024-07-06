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

```shell
msp -f path/to/your/mod -n NameOfYourMod -o path/to/the/output -d path/to/the/needed/dll
```