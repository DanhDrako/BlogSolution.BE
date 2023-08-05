nuget:
design
sqlserver
------------
view -> other windown -> package manager console

dotnet tool install --global dotnet-ef
cd todolist.data =>
dotnet ef migrations add CreateInitial
dotnet ef database update

