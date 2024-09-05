# syntax=docker/dockerfile:1

################################################################################
# Build Stage: Build the .NET Core Application
################################################################################
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build

# Set working directory inside the container
WORKDIR /source

# Copy only the .csproj file to utilize caching for dependencies
COPY BE-membership-connect/*.csproj ./

# Restore dependencies
RUN --mount=type=cache,id=nuget,target=/root/.nuget/packages \
    dotnet restore

# Copy all source files to the container
COPY BE-membership-connect/. ./

# Build and publish the application
RUN --mount=type=cache,id=nuget,target=/root/.nuget/packages \
    dotnet publish -r linux-musl-x64 --use-current-runtime --self-contained false -o /app

################################################################################
# Final Stage: Set up the runtime environment to run the application
################################################################################
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS final

# Set the working directory to the app folder
WORKDIR /app

# Copy the build output from the previous stage
COPY --from=build /app .

# Switch to a non-privileged user (defined in the base image)
USER $APP_UID

# Entry point to run the application
ENTRYPOINT ["dotnet", "BE-membership-connect.dll", "--environment=Staging"]