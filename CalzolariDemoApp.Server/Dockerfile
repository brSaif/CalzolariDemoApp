FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["CalzolariDemoApp.Server/CalzolariDemoApp.Server.csproj", "CalzolariDemoApp.Server/"]
RUN dotnet restore "CalzolariDemoApp.Server/CalzolariDemoApp.Server.csproj"
COPY . .
WORKDIR "/src/CalzolariDemoApp.Server"
RUN dotnet build "CalzolariDemoApp.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CalzolariDemoApp.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CalzolariDemoApp.Server.dll"]
