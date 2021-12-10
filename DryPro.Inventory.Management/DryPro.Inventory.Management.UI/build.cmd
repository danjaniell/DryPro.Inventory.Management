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
dotnet restore "DryPro.Inventory.Management.UI.csproj"
dotnet build "DryPro.Inventory.Management.UI.csproj" -c Release /p:env=Test --no-restore
dotnet publish "DryPro.Inventory.Management.UI.csproj" -c Release /p:env=Test -o "C:\Users\danjaniell7\source\repos\DryPro.Inventory.Management\builds\UI\Test"
GOTO BYE

:PROD
setx ASPNETCORE_ENVIRONMENT "Production" /M
dotnet restore "DryPro.Inventory.Management.UI.csproj"
dotnet build "DryPro.Inventory.Management.UI.csproj" -c Release /p:env=Production --no-restore
dotnet publish "DryPro.Inventory.Management.UI.csproj" -c Release /p:env=Production -o "C:\Users\danjaniell7\source\repos\DryPro.Inventory.Management\builds\UI\Production"
GOTO BYE

:BYE
Pause