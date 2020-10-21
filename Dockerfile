FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app
#RUN dotnet tool install --global dotnet-ef --version 3.1.0
#RUN export PATH="$PATH:/root/.dotnet/tools"
# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
#RUN dotnet ef database update
RUN dotnet publish -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY HRP.db* ./
COPY --from=build-env /app/out .
ENV ASPNETCORE_URLS=http://+:5000
CMD dotnet Headway-Rhythm-Project-API.dll

