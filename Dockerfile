#FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
#WORKDIR /api/Test
#EXPOSE 80
#EXPOSE 5024
#
#FROM base AS final
#COPY ["builds/Test/", "/api/Test/"]
#WORKDIR /api/Test
#ENTRYPOINT ["dotnet", "/api/Test/DryPro.Inventory.Management.dll", "--environment=Test"]

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 5024

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["DryPro.Inventory.Management/DryPro.Inventory.Management", "DryPro.Inventory.Management/DryPro.Inventory.Management/"]
COPY ["DryPro.Inventory.Management/DryPro.Inventory.Management.Application/", "DryPro.Inventory.Management/DryPro.Inventory.Management.Application/"]
COPY ["DryPro.Inventory.Management/DryPro.Inventory.Management.Common/", "DryPro.Inventory.Management/DryPro.Inventory.Management.Common/"]
COPY ["DryPro.Inventory.Management/DryPro.Inventory.Management.Core/", "DryPro.Inventory.Management/DryPro.Inventory.Management.Core/"]
COPY ["DryPro.Inventory.Management/DryPro.Inventory.Management.Infrastructure/", "DryPro.Inventory.Management/DryPro.Inventory.Management.Infrastructure/"]
RUN dotnet restore "DryPro.Inventory.Management/DryPro.Inventory.Management/DryPro.Inventory.Management.csproj"
WORKDIR "/src/DryPro.Inventory.Management/DryPro.Inventory.Management"
RUN dotnet build "DryPro.Inventory.Management.csproj" -c Release /p:env=Test -o /app/build --no-restore

FROM build AS publish
RUN dotnet publish "DryPro.Inventory.Management.csproj" -c Release /p:env=Test -o /app/publish

FROM base AS final
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:5024
EXPOSE 5024
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DryPro.Inventory.Management.dll", "--environment=Test"]
#CMD ASPNETCORE_URLS=http://*:$PORT dotnet DryPro.Inventory.Management.dll --environment=Test