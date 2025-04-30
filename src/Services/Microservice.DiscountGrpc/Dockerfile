FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["src/Services/Microservice.DiscountGrpc/Microservice.DiscountGrpc.csproj", "src/Services/Microservice.DiscountGrpc/"]
RUN dotnet restore "src/Services/Microservice.DiscountGrpc/Microservice.DiscountGrpc.csproj"
COPY . .
WORKDIR "/src/src/Services/Microservice.DiscountGrpc"
RUN dotnet build "Microservice.DiscountGrpc.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "Microservice.DiscountGrpc.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY ["src/Services/Microservice.DiscountGrpc/Migrations", "/app/Migrations"]
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Microservice.DiscountGrpc.dll"]
