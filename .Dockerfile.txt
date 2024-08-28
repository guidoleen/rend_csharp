# Use the official .NET 8 SDK image as the base image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory inside the container
WORKDIR /app

# Copy the project files to the working directory
COPY . .

# Build the project
RUN dotnet build -c Release

# Publish the project
RUN dotnet publish -c Release -o /app/publish

# Use the official .NET 8 runtime image as the base image for the final image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

# Set the working directory inside the container
WORKDIR /app

# Copy the published files from the build stage to the final image
COPY --from=build /app/publish .

# Expose the port that the application listens on
EXPOSE 8020

# Set the entry point for the container
ENTRYPOINT ["dotnet", "RD_Enviornment.dll"]
