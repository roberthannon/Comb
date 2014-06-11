Release Process

* git tag -a v0.2.0-alpha -m 'Fleshing out the API.'
* Update AssemblyInfo.cs version.
* Update release notes in Comb.nuspec
* Rebuild All
* nuget pack .\Comb.csproj -symbols
* nuget push .\Comb.0.2.0.0.nupkg