FROM microsoft/aspnetcore-build:2.0 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY ./Api ./Api/
COPY ./Infrastructure ./Infrastructure/
COPY ./Domain ./Domain/
RUN dotnet restore ./Api/SmileDirectClub.CodingTest.Api.csproj

# Copy everything else and build
COPY ./Api ./Api/
RUN dotnet publish ./Api/SmileDirectClub.CodingTest.Api.csproj -c Release -o out

# Build runtime image
FROM microsoft/aspnetcore:2.0.5-jessie
WORKDIR /app
COPY --from=build-env /app/Api/out .
ENTRYPOINT ["dotnet", "SmileDirectClub.CodingTest.Api.dll"]
