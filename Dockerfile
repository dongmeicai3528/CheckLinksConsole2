FROM microsoft/dotnet:2.1-runtime

WORKDIR /app
COPY bin/Debug/netcoreapp2.1/publish .

ENTRYPOINT ["dotnet", "CheckLinksConsole.dll"]

