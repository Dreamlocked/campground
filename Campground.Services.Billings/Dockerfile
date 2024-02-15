#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Campground.Services.Billings/Campground.Services.Billings.csproj", "Campground.Services.Billings/"]
COPY ["Campground.Services.Billings.Domain/Campground.Services.Billings.Domain.csproj", "Campground.Services.Billings.Domain/"]
COPY ["Campground.Services.Billings.Infrastructure/Campground.Services.Billings.Infrastructure.csproj", "Campground.Services.Billings.Infrastructure/"]
RUN dotnet restore "./Campground.Services.Billings/Campground.Services.Billings.csproj"
COPY . .
WORKDIR "/src/Campground.Services.Billings"
RUN dotnet build "./Campground.Services.Billings.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Campground.Services.Billings.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Campground.Services.Billings.dll"]