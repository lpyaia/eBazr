#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["containers/Order.Api/Order.Api.csproj", "containers/Order.Api/"]
COPY ["dotnet-packages/order/src/Order/Order.csproj", "dotnet-packages/order/src/Order/"]
COPY ["dotnet-packages/common/src/Common/Common.csproj", "dotnet-packages/common/src/Common/"]
COPY ["dotnet-packages/order/src/Order.Infra/Order.Infra.csproj", "dotnet-packages/order/src/Order.Infra/"]
RUN dotnet restore "containers/Order.Api/Order.Api.csproj"
COPY . .
WORKDIR "/src/containers/Order.Api"
RUN dotnet build "Order.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Order.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Order.Api.dll"]
