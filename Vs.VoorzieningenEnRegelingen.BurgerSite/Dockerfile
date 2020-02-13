FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Vs.VoorzieningenEnRegelingen.BurgerSite/Vs.VoorzieningenEnRegelingen.BurgerSite.csproj", "Vs.VoorzieningenEnRegelingen.BurgerSite/"]
RUN dotnet restore "Vs.VoorzieningenEnRegelingen.BurgerSite/Vs.VoorzieningenEnRegelingen.BurgerSite.csproj"
COPY . .
WORKDIR "/src/Vs.VoorzieningenEnRegelingen.BurgerSite"
RUN dotnet build "Vs.VoorzieningenEnRegelingen.BurgerSite.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Vs.VoorzieningenEnRegelingen.BurgerSite.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Vs.VoorzieningenEnRegelingen.BurgerSite.dll"]