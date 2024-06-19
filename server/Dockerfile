# Use the official Microsoft .NET SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory inside the container
WORKDIR /app

# Copy the solution file and restore the dependencies
COPY Koleo.sln ./
COPY API/API.csproj ./API/
COPY Application/Application.csproj ./Application/
COPY Domain/Domain.csproj ./Domain/
COPY Persistence/Persistence.csproj ./Persistence/

# Restore the dependencies
RUN dotnet restore

# Copy the entire project files to the container
COPY . .

# Build the project
RUN dotnet publish API/API.csproj -c Release -o out

# Use the official Microsoft .NET runtime image to run the app
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

# Set the working directory inside the container
WORKDIR /app

# Copy the build output from the build stage to the runtime stage
COPY --from=build /app/out .

# Expose the port your application will run on
EXPOSE 80

# Define the entry point for the container to run the app
ENTRYPOINT ["dotnet", "API.dll"]
