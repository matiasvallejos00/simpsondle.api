FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiamos el proyecto y restauramos
COPY ["SimpsonsDle.Api/SimpsonsDle.Api.csproj", "SimpsonsDle.Api/"]
RUN dotnet restore "SimpsonsDle.Api/SimpsonsDle.Api.csproj"

# Copiamos todo lo demás y publicamos
COPY . .
WORKDIR "/src/SimpsonsDle.Api"
RUN dotnet publish "SimpsonsDle.Api.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

# Exponemos el puerto para Render
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "SimpsonsDle.Api.dll"]