# Use the official .NET image as the base image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Use the SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copy the project file(s) and restore the dependencies
COPY ["EasyCommerce.csproj", "./"]
RUN dotnet restore "EasyCommerce.csproj"

# Copy the rest of the application and publish
COPY . .
WORKDIR "/src/."
RUN dotnet build "EasyCommerce.csproj" -c Release -o /app/build
RUN dotnet publish "EasyCommerce.csproj" -c Release -o /app/publish

# Set the entry point for the application
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "EasyCommerce.dll"]
