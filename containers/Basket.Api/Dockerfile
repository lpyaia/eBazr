#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["containers/Basket.Api/Basket.Api.csproj", "containers/Basket.Api/"]
COPY ["dotnet-packages/basket/src/Basket.Infra/Basket.Infra.csproj", "dotnet-packages/basket/src/Basket.Infra/"]
COPY ["dotnet-packages/basket/src/Basket/Basket.csproj", "dotnet-packages/basket/src/Basket/"]
COPY ["dotnet-packages/common/src/Common/Common.csproj", "dotnet-packages/common/src/Common/"]
RUN dotnet restore "containers/Basket.Api/Basket.Api.csproj"
COPY . .
WORKDIR "/src/containers/Basket.Api"
RUN dotnet build "Basket.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Basket.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Basket.Api.dll"]
