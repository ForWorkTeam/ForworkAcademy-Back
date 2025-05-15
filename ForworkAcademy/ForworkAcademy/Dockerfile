## See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.
#
## This stage is used when running from VS in fast mode (Default for Debug configuration)
#FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
#USER $APP_UID
#WORKDIR /app
#EXPOSE 8080
#EXPOSE 8081
#
#
## This stage is used to build the service project
#FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
#ARG BUILD_CONFIGURATION=Release
#WORKDIR /src
#COPY ["ForworkAcademy/ForworkAcademy.csproj", "ForworkAcademy/"]
#RUN dotnet restore "./ForworkAcademy/ForworkAcademy.csproj"
#COPY . .
#WORKDIR "/src/ForworkAcademy"
#RUN dotnet build "./ForworkAcademy.csproj" -c $BUILD_CONFIGURATION -o /app/build
#
## This stage is used to publish the service project to be copied to the final stage
#FROM build AS publish
#ARG BUILD_CONFIGURATION=Release
#RUN dotnet publish "./ForworkAcademy.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false
#
## This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "ForworkAcademy.dll"]



## Base image for runtime
#FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
#WORKDIR /app
#EXPOSE 8080
#EXPOSE 8081
#
## Build image
#FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
#ARG BUILD_CONFIGURATION=Release
#WORKDIR /src
#
## Copy only the csproj and restore as separate step for caching
#COPY ForworkAcademy/ForworkAcademy.csproj ./ForworkAcademy/
#RUN dotnet restore ./ForworkAcademy/ForworkAcademy.csproj
#
## Copy the rest of the source code
#COPY ForworkAcademy/ForworkAcademy/. ./ForworkAcademy/
#WORKDIR /src/ForworkAcademy
#
## Build the project
#RUN dotnet build ./ForworkAcademy.csproj -c $BUILD_CONFIGURATION -o /app/build
#
## Publish the project
#FROM build AS publish
#ARG BUILD_CONFIGURATION=Release
#RUN dotnet publish ./ForworkAcademy.csproj -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false
#
## Final image
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "ForworkAcademy.dll"]


FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ForworkAcademy/ForworkAcademy/ForworkAcademy.csproj ./ForworkAcademy/
RUN dotnet restore ./ForworkAcademy/ForworkAcademy.csproj

COPY ForworkAcademy/ForworkAcademy/. ./ForworkAcademy/
WORKDIR /src/ForworkAcademy
RUN dotnet build ./ForworkAcademy.csproj -c Release -o /app/build
RUN dotnet publish ./ForworkAcademy.csproj -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ForworkAcademy.dll"]