# ---- Build ----
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copia primero el csproj para aprovechar cache (ajusta el nombre si difiere)
COPY MiniWebAppORM.csproj ./
RUN dotnet restore

# Copia el resto y publica
COPY . .
RUN dotnet publish -c Release -o /out

# ---- Runtime ----
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /out .
COPY certs/root.crt /certs/root.crt
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "MiniWebAppORM.dll"]
