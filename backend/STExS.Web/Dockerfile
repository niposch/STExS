#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["STExS.Web/STExS.Web.csproj", "STExS.Web/"]
COPY ["../Application/Application.csproj", "Application/"]
COPY ["Common/Common.csproj", "Common/"]
COPY ["Repositories/Repositories.csproj", "Repositories/"]
COPY ["External.Judge0/External.Judge0.csproj", "External.Judge0/"]
COPY ["External.Storage/External.Storage.csproj", "External.Storage/"]
RUN dotnet restore "STExS.Web/STExS.Web.csproj"
COPY . .
WORKDIR "/src/STExS.Web"
RUN dotnet build "STExS.Web.csproj" -c Release -o /app/build -p:DontBuildSwagger=true

FROM build AS publish
RUN dotnet publish "STExS.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false -p:DontBuildSwagger=true

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "STExS.Web.dll"]