FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /api/Test
EXPOSE 80
EXPOSE 5024

FROM base AS final
COPY ["builds/Test/", "/api/Test/"]
WORKDIR /api/Test
ENTRYPOINT ["dotnet", "/api/Test/DryPro.Inventory.Management.dll", "--environment=Test"]