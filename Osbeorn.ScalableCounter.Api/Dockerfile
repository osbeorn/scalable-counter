FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Osbeorn.ScalableCounter.Api/Osbeorn.ScalableCounter.Api.csproj", "Osbeorn.ScalableCounter.Api/"]
RUN dotnet restore "Osbeorn.ScalableCounter.Api/Osbeorn.ScalableCounter.Api.csproj"
COPY . .
WORKDIR "/src/Osbeorn.ScalableCounter.Api"
RUN dotnet build "Osbeorn.ScalableCounter.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Osbeorn.ScalableCounter.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Osbeorn.ScalableCounter.Api.dll"]
