FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 49901
EXPOSE 44371

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY Services/Identity/Identity.API/Identity.API.csproj Services/Identity/Identity.API/
RUN dotnet restore Services/Identity/Identity.API/Identity.API.csproj
COPY . .
WORKDIR /src/Services/Identity/Identity.API
RUN dotnet build Identity.API.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Identity.API.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Identity.API.dll"]
