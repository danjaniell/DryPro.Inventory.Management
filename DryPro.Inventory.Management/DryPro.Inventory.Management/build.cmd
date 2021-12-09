ECHO OFF
CLS
:MENU
ECHO.
ECHO .............................
ECHO  SELECT ENVIRONMENT TO BUILD
ECHO .............................
ECHO.
ECHO 1 - Test
ECHO 2 - Production
ECHO.

SET /P M=Enter choice: 
IF %M%==1 GOTO TEST
IF %M%==2 GOTO PROD

:TEST
setx ASPNETCORE_ENVIRONMENT "Development" /M
dotnet restore "DryPro.Inventory.Management.csproj"
dotnet build "DryPro.Inventory.Management.csproj" -c Release /p:env=Test --no-restore
dotnet publish "DryPro.Inventory.Management.csproj" -c Release /p:env=Test -o "C:\Users\danjaniell7\source\repos\DryPro.Inventory.Management\builds\Test"
cd "C:\Users\danjaniell7\source\repos\DryPro.Inventory.Management\"
docker build --pull -t dryproapi:dev .
GOTO BYE

:PROD
setx ASPNETCORE_ENVIRONMENT "Production" /M
dotnet restore "DryPro.Inventory.Management.csproj"
dotnet build "DryPro.Inventory.Management.csproj" -c Release /p:env=Production --no-restore
dotnet publish "DryPro.Inventory.Management.csproj" -c Release /p:env=Production -o "C:\Users\danjaniell7\source\repos\DryPro.Inventory.Management\builds\Production"
cd "C:\Users\danjaniell7\source\repos\DryPro.Inventory.Management\"
docker build --pull -t dryproapi .
GOTO BYE

:BYE
Pause