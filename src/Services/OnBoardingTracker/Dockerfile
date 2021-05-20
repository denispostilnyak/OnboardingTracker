FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS pre-build
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["./OnBoardingTracker.Application/OnBoardingTracker.Application.csproj", "/OnBoardingTracker.Application/"]
COPY ["./OnBoardingTracker.Domain/OnBoardingTracker.Domain.csproj", "/OnBoardingTracker.Domain/"]
COPY ["./OnBoardingTracker.Infrastructure/OnBoardingTracker.Infrastructure.csproj", "/OnBoardingTracker.Infrastructure/"]
COPY ["./OnBoardingTracker.Persistence/OnBoardingTracker.Persistence.csproj", "/OnBoardingTracker.Persistence/"]
COPY ["./OnBoardingTracker.WebAPI/OnBoardingTracker.WebApi.csproj", "/OnBoardingTracker.WebAPI/"]
RUN dotnet restore "/OnBoardingTracker.WebAPI/OnBoardingTracker.WebApi.csproj"
COPY . .
WORKDIR "OnBoardingTracker.WebAPI"
RUN dotnet build "OnBoardingTracker.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OnBoardingTracker.WebApi.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 as runner
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OnBoardingTracker.WebApi.dll"]
