# Build
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY MiniTrade.ConsoleApp/MiniTrade.ConsoleApp.csproj .
RUN dotnet restore
COPY . .
RUN dotnet publish -c release -o /app MiniTrade.ConsoleApp/MiniTrade.ConsoleApp.csproj

# Runtime entrypoint
FROM mcr.microsoft.com/dotnet/runtime:6.0
WORKDIR /app
EXPOSE 80
EXPOSE 443
COPY --from=build /app .
ARG RUNTIME_SERVICE=default_value
ENV RUNTIME_SERVICE=$RUNTIME_SERVICE
ENTRYPOINT ["dotnet", "MiniTrade.ConsoleApp.dll"]
