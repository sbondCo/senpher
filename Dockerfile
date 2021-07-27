FROM mcr.microsoft.com/dotnet/sdk:5.0 AS builder
WORKDIR /app
COPY . .
RUN dotnet restore
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=builder /app/out .
ENTRYPOINT ["dotnet", "senpher.dll"]
