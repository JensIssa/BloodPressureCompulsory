FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8090
EXPOSE 8091

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["PatientService/PatientService.csproj", "PatientService/"]
COPY ["PatientApplication/PatientApplication.csproj", "PatientApplication/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["PatientInfrastructure/PatientInfrastructure.csproj", "PatientInfrastructure/"]
COPY . .
RUN dotnet restore "./PatientService/PatientService.csproj"
WORKDIR "/src/PatientService"
RUN dotnet build "./PatientService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./PatientService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PatientService.dll"]