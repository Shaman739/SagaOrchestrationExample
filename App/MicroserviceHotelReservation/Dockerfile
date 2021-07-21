#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["App/MicroserviceHotelReservation/MicroserviceHotelReservation.csproj", "App/MicroserviceHotelReservation/"]
RUN dotnet restore "App/MicroserviceHotelReservation/MicroserviceHotelReservation.csproj"
COPY . .
WORKDIR "/src/App/MicroserviceHotelReservation"
RUN dotnet build "MicroserviceHotelReservation.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MicroserviceHotelReservation.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MicroserviceHotelReservation.dll"]