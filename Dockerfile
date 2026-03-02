# 1. Imagen del SDK para compilar
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# 2. Copiamos ABSOLUTAMENTE TODO primero para evitar errores de rutas
COPY . .

# 3. Restauramos y publicamos usando el nombre exacto de tu proyecto
# Render buscará este archivo en cualquier subcarpeta
RUN dotnet restore "SimpsonsDle.Api.csproj"
RUN dotnet publish "SimpsonsDle.Api.csproj" -c Release -o /app/publish

# 4. Imagen final para correr la app
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

# Exponemos el puerto para Render
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "SimpsonsDle.Api.dll"]