# FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
FROM harbor-01.docker-registry.grupofarsiman.io/external/aspnet:8.0

WORKDIR /app

COPY ./compilado .

EXPOSE 8080

ENTRYPOINT ["dotnet", "AcademiaFS.HomeJourney.WebAPI.dll"]



# 

# RUN dotnet restore

# RUN dotnet publish -c Release -o out

# FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

# WORKDIR /app

# COPY --from=build /app/out .


ENTRYPOINT ["dotnet", "AcademiaFS.HomeJourney.WebAPI.dll"]