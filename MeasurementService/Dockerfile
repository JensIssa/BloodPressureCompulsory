FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["MeasurementService/MeasurementService.csproj", "MeasurementService/"]
COPY ["MeasurementApplication/MeasurementApplication.csproj", "MeasurementApplication/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["MeasurementInfrastructure/MeasurementInfrastructure.csproj", "MeasurementInfrastructure/"]
COPY . .
RUN dotnet restore "./MeasurementService/MeasurementService.csproj"
WORKDIR "/src/MeasurementService"
RUN dotnet build "./MeasurementService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MeasurementService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MeasurementService.dll"]