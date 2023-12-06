FROM mcr.microsoft.com/dotnet/sdk:7.0 as build
WORKDIR /src

COPY ./ /src/

RUN dotnet restore && dotnet publish -o /publish

FROM mcr.microsoft.com/dotnet/aspnet:7.0 as runtime

WORKDIR /app

COPY --from=build /publish /app

ENTRYPOINT ["dotnet", "SecretSeeder.dll"]