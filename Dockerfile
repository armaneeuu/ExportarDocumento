# Etapa de construcción
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app
EXPOSE 80

# Usar la imagen SDK de .NET para construir la aplicación
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["ExportarDocumento/ExportarDocumento.csproj", "ExportarDocumento/"]
RUN dotnet restore "ExportarDocumento/ExportarDocumento.csproj"
COPY . .
WORKDIR "/src/ExportarDocumento"
RUN dotnet build "ExportarDocumento.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ExportarDocumento.csproj" -c Release -o /app/publish

# Configurar el contenedor para ejecutar la app
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ExportarDocumento.dll"]