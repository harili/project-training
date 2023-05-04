#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["BankAccount.API/BankAccount.API.csproj", "BankAccount.API/"]
COPY ["BankAccount.Core/BankAccount.Core.csproj", "BankAccount.Core/"]
RUN dotnet restore "BankAccount.API/BankAccount.API.csproj"
COPY . .
WORKDIR "/src/BankAccount.API"
RUN dotnet build "BankAccount.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BankAccount.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BankAccount.API.dll"]