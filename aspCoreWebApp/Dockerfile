#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Stage 2: Build Image
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copy the project file and restore dependencies
COPY ["./aspCoreWebApp/aspCoreWebApp.csproj", "aspCoreWebApp/"]
RUN dotnet restore "aspCoreWebApp/aspCoreWebApp.csproj"

# Copy the rest of the source code and build the application
COPY . .
WORKDIR "/src/aspCoreWebApp"
RUN dotnet build "./aspCoreWebApp/aspCoreWebApp.csproj" -c Release -o /app/build

# Stage 3: Publish Image
FROM build AS publish
RUN dotnet publish "./aspCoreWebApp/aspCoreWebApp.csproj" -c Release -o /app/publish

# Stage 4: Final Image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "aspCoreWebApp.dll"]