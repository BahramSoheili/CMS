#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Command.API/Command.API.csproj", "Command.API/"]
RUN dotnet restore "Command.API/Command.API.csproj"
COPY . .
WORKDIR "/src/Command.API"
RUN dotnet build "Command.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Command.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Command.API.dll"]