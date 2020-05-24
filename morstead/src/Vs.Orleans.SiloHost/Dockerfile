FROM mcr.microsoft.com/dotnet/core/runtime:3.1 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["Vs.Orleans.SiloHost.csproj", "./"]
RUN dotnet restore "./Vs.Orleans.SiloHost.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "Vs.Orleans.SiloHost.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Vs.Orleans.SiloHost.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Vs.Orleans.SiloHost.dll"]
