#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["DryPro.Inventory.Management.UI/DryPro.Inventory.Management.UI.csproj", "DryPro.Inventory.Management.UI/"]
COPY ["DryPro.Inventory.Management.Common/DryPro.Inventory.Management.Common.csproj", "DryPro.Inventory.Management.Common/"]
COPY ["DryPro.Inventory.Management.Application/DryPro.Inventory.Management.Application.csproj", "DryPro.Inventory.Management.Application/"]
COPY ["DryPro.Inventory.Management.Core/DryPro.Inventory.Management.Core.csproj", "DryPro.Inventory.Management.Core/"]
COPY ["DryPro.Inventory.Management.Infrastructure/DryPro.Inventory.Management.Infrastructure.csproj", "DryPro.Inventory.Management.Infrastructure/"]
RUN dotnet restore "DryPro.Inventory.Management.UI/DryPro.Inventory.Management.UI.csproj"
COPY . .
WORKDIR "/src/DryPro.Inventory.Management.UI"
RUN dotnet build "DryPro.Inventory.Management.UI.csproj" -c Release /p:env=Test -o /app/build

FROM build AS publish
RUN dotnet publish "DryPro.Inventory.Management.UI.csproj" -c Release /p:env=Test -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DryPro.Inventory.Management.UI.dll", "--environment=Test"]