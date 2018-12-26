FROM microsoft/dotnet:sdk AS build-env
# FROM microsoft/dotnet:2.2-sdk-stretch-arm32v7 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM microsoft/dotnet:aspnetcore-runtime
# FROM microsoft/dotnet:2.2-runtime-deps-stretch-slim-arm32v7 AS runtime
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "homeautomation.ui.dll"]

