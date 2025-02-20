# Use the official image from Microsoft as a base
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

# Use the official SDK image for building the app
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copy the project files and restore any dependencies
COPY ["EasyCommerce/EasyCommerce.csproj", "EasyCommerce/"]
RUN dotnet restore "EasyCommerce/EasyCommerce.csproj"

# Copy the rest of the source code and build the project
COPY . .
WORKDIR "/src/EasyCommerce"
RUN dotnet build "EasyCommerce.csproj" -c Release -o /app/build

# Publish the app to a folder
FROM build AS publish
RUN dotnet publish "EasyCommerce.csproj" -c Release -o /app/publish

# Copy the app into the base image and define entrypoint
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EasyCommerce.dll"]
