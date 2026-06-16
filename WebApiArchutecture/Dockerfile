
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src


COPY ["WebApiArchitecture.csproj", "./"] 


RUN dotnet restore "WebApiArchitecture.csproj"

COPY . .
RUN dotnet publish "WebApiArchitecture.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "WebApiArchitecture.dll"]