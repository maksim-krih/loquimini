FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR .
COPY . .
RUN dotnet restore
RUN dotnet publish -c Debug -o /out

FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR .
COPY --from=build /out .
ENTRYPOINT ["dotnet", "Loquimini.API.dll"]