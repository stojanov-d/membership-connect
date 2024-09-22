# syntax=docker/dockerfile:1

FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build

WORKDIR /source

# Copy the .csproj file(s)
COPY ./*.csproj ./

# Restore dependencies
RUN --mount=type=cache,id=nuget,target=/root/.nuget/packages \
    dotnet restore

# Copy all source files to the container
COPY . ./

# Build and publish the application
RUN --mount=type=cache,id=nuget,target=/root/.nuget/packages \
    dotnet publish -r linux-musl-x64 --use-current-runtime --self-contained false -o /app

# Final Stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS final

WORKDIR /app

COPY --from=build /app .

# Switch to a non-privileged user
USER $APP_UID

ENTRYPOINT ["dotnet", "BE-membership-connect.dll", "--environment=Staging"]
