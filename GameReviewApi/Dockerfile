#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["GameReviewApi/GameReviewApi.csproj", "GameReviewApi/"]
COPY ["GameReviewApi.Service/GameReviewApi.Service.csproj", "GameReviewApi.Service/"]
COPY ["GameReviewApi.DAL/GameReviewApi.DAL.csproj", "GameReviewApi.DAL/"]
COPY ["GameReviewApi.Domain/GameReviewApi.Domain.csproj", "GameReviewApi.Domain/"]
RUN dotnet restore "GameReviewApi/GameReviewApi.csproj"
COPY . .
WORKDIR "/src/GameReviewApi"
RUN dotnet build "GameReviewApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GameReviewApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GameReviewApi.dll"]