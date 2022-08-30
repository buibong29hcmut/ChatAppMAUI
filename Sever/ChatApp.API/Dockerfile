#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Sever/ChatApp.API/ChatApp.API.csproj", "Sever/ChatApp.API/"]
COPY ["Sever/ChatApp.Infrastructure/ChatApp.Infrastructure.csproj", "Sever/ChatApp.Infrastructure/"]
COPY ["Sever/ChatApp.Application/ChatApp.Application.csproj", "Sever/ChatApp.Application/"]
COPY ["Sever/ChatApp.Domain/ChatApp.Domain.csproj", "Sever/ChatApp.Domain/"]
RUN dotnet restore "Sever/ChatApp.API/ChatApp.API.csproj"
COPY . .
WORKDIR "/src/Sever/ChatApp.API"
RUN dotnet build "ChatApp.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ChatApp.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ChatApp.API.dll"]