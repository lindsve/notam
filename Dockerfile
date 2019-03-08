FROM microsoft/dotnet:2.2-sdk AS base
WORKDIR /app

COPY notam.sln .
COPY src/notam.csproj src/notam.csproj
RUN dotnet restore

FROM base as build
COPY src src 
RUN dotnet publish src/notam.csproj -c Release -o out

FROM microsoft/dotnet:2.2-aspnetcore-runtime
WORKDIR /app
COPY --from=build /app/src/out .
ENTRYPOINT ["dotnet", "notam.dll"]