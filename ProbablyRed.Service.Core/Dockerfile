#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ProbablyRed.Service.Core/ProbablyRed.Service.Core.csproj", "ProbablyRed.Service.Core/"]
RUN dotnet restore "ProbablyRed.Service.Core/ProbablyRed.Service.Core.csproj"
COPY . .
WORKDIR "/src/ProbablyRed.Service.Core"
RUN dotnet build "ProbablyRed.Service.Core.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProbablyRed.Service.Core.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProbablyRed.Service.Core.dll"]