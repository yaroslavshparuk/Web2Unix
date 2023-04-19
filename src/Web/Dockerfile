#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Web/Web.csproj", "src/Web/"]
COPY ["src/Web2Unix.Infrastructure/Web2Unix.Infrastructure.csproj", "src/Web2Unix.Infrastructure/"]
COPY ["src/Web2Unix.Application/Web2Unix.Application.csproj", "src/Web2Unix.Application/"]
COPY ["src/Web2Unix.Domain/Web2Unix.Domain.csproj", "src/Web2Unix.Domain/"]
COPY ["src/Web2Unix.Presentation/Web2Unix.Presentation.csproj", "src/Web2Unix.Presentation/"]
RUN dotnet restore "src/Web/Web.csproj"
COPY . .
WORKDIR "/src/src/Web"
RUN dotnet build "Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Web.dll"]