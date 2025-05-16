FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["GameStore.Api/GameStore.Api.csproj", "GameStore.Api/"]
RUN dotnet restore "GameStore.Api/GameStore.Api.csproj"
RUN echo "Restore completed."
COPY . .
RUN echo "Copy completed."
WORKDIR "/src/GameStore.Api"
RUN dotnet build "GameStore.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build
RUN echo "Building completed."

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "GameStore.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false
RUN echo "Publishing completed."

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GameStore.Api.dll"]
