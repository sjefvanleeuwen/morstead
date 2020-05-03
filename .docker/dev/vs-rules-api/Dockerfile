#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["../../Vs.Rules.OpenApi/Vs.Rules.OpenApi.csproj", "Vs.Rules.OpenApi/"]
RUN dotnet restore "Vs.Rules.OpenApi/Vs.Rules.OpenApi.csproj"
COPY . .
WORKDIR "/src/Vs.Rules.OpenApi"
RUN dotnet build "Vs.Rules.OpenApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Vs.Rules.OpenApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Vs.Rules.OpenApi.dll"]