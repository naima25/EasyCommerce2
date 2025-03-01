# Use the official image from Microsoft as a base for runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80

# Set environment variables securely
ENV ASPNETCORE_ENVIRONMENT=Production \
    ConnectionStrings__DefaultConnection="Server=db;Database=EasyCommerce;User Id=sa;Password=yourStrong(!)Password"

# Use the official SDK image for building the app
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy the project files and restore any dependencies
COPY ["EasyCommerce.csproj", "./"]
RUN dotnet restore "EasyCommerce.csproj"

# Copy the rest of the source code and build the project
COPY . .
WORKDIR "/src" 
RUN dotnet build "EasyCommerce.csproj" -c Release -o /app/build

# Publish the app to a folder
FROM build AS publish
RUN dotnet publish "EasyCommerce.csproj" -c Release -o /app/publish

# Copy the app into the base image and define entrypoint
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Expose additional environment variables dynamically 
CMD ["dotnet", "EasyCommerce.dll"]
