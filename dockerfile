# ---------------------------
# BUILD
# ---------------------------
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["WebApp/WebApp.csproj", "WebApp/"]
COPY ["SharedUI/SharedUI.csproj", "SharedUI/"]

RUN dotnet restore "WebApp/WebApp.csproj"

COPY . .

WORKDIR "/src/WebApp"
RUN dotnet publish "WebApp.csproj" -c Release -o /app/publish /p:UseAppHost=false

# ---------------------------
# RUNTIME
# ---------------------------
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

EXPOSE 80
EXPOSE 443

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "WebApp.dll"]