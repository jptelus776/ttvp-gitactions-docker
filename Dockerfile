#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 5000
#add curl for liveness probe
RUN apt-get update \
    && apt-get -y install curl

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Src/Entity-Info/EntityInfoService.csproj", "Entity-Info/"]
RUN dotnet restore "Entity-Info/EntityInfoService.csproj"
COPY Src .
WORKDIR "/src/Entity-Info"
RUN dotnet build "EntityInfoService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EntityInfoService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
ENV ASPNETCORE_URLS=http://+:5000 DOTNET_RUNNING_IN_CONTAINER=true 
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EntityInfoService.dll"]
