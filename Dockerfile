# 1. Imagen del SDK para compilar
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# 2. Copiamos el archivo .csproj que está en la raíz
# Según tu VS Code, el archivo se llama SimpsonsDle.Api.csproj
COPY ["SimpsonsDle.Api.csproj", "./"]
RUN dotnet restore "SimpsonsDle.Api.csproj"

# 3. Copiamos todo lo demás (incluida la carpeta Data)
COPY . .

# 4. Publicamos la app
RUN dotnet publish "SimpsonsDle.Api.csproj" -c Release -o out

# 5. Imagen final para correr la app
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

# Configuración de puerto para Render
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "SimpsonsDle.Api.dll"]