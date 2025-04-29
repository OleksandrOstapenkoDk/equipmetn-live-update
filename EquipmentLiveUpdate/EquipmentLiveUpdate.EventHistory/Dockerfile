#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["EquipmentLiveUpdate.EventHistory/EquipmentLiveUpdate.EventHistory.csproj", "EquipmentLiveUpdate.EventHistory/"]
RUN dotnet restore "./EquipmentLiveUpdate.EventHistory/EquipmentLiveUpdate.EventHistory.csproj"
COPY . .
WORKDIR "/src/EquipmentLiveUpdate.EventHistory"
RUN dotnet build "./EquipmentLiveUpdate.EventHistory.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./EquipmentLiveUpdate.EventHistory.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EquipmentLiveUpdate.EventHistory.dll"]