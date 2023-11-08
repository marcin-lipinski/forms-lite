FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Features/Features.csproj", "src/Features/"]
RUN dotnet restore "src/Features/Features.csproj"
COPY . .
WORKDIR "/src/src/Features"
RUN dotnet build "Features.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Features.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Features.dll"]
