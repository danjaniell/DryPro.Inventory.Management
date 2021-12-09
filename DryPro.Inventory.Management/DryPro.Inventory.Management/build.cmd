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
dotnet build "DryPro.Inventory.Management.csproj" -c Release /p:env=Test
dotnet publish "DryPro.Inventory.Management.csproj" -c Release /p:env=Test -o "C:\Users\danjaniell7\source\repos\DryPro.Inventory.Management\builds\Test"
GOTO BYE

:PROD
setx ASPNETCORE_ENVIRONMENT "Production" /M
dotnet build "DryPro.Inventory.Management.csproj" -c Release /p:env=Production
dotnet publish "DryPro.Inventory.Management.csproj" -c Release /p:env=Production -o "C:\Users\danjaniell7\source\repos\DryPro.Inventory.Management\builds\Production"
GOTO BYE

:BYE
Pause