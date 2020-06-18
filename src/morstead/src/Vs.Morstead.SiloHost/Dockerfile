#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/runtime:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/Vs.Morstead.SiloHost/Vs.Morstead.SiloHost.csproj", "src/Vs.Morstead.SiloHost/"]
COPY ["src/Vs.Morstead.Grains/Vs.Morstead.Grains.csproj", "src/Vs.Morstead.Grains/"]
COPY ["src/Vs.Morstead.Grains.Interfaces/Vs.Morstead.Grains.Interfaces.csproj", "src/Vs.Morstead.Grains.Interfaces/"]
COPY ["Vs.Morstead.Orleans.Configuration/Vs.Morstead.Orleans.Configuration.csproj", "Vs.Morstead.Orleans.Configuration/"]
RUN dotnet restore "src/Vs.Morstead.SiloHost/Vs.Morstead.SiloHost.csproj"
COPY . .
WORKDIR "/src/src/Vs.Morstead.SiloHost"
RUN dotnet build "Vs.Morstead.SiloHost.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Vs.Morstead.SiloHost.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 11111/tcp
EXPOSE 30000/tcp
ENTRYPOINT ["dotnet", "Vs.Morstead.SiloHost.dll"]