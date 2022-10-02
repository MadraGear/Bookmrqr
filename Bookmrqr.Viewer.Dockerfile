FROM microsoft/dotnet:2.1-sdk AS build-env
WORKDIR /app

# copy solution, build(which also restores all projects) and publish only Bookmrqr.Viewer
COPY . ./
RUN dotnet build
RUN dotnet publish Bookmrqr.Viewer/*.csproj -c Release -o out

# Build runtime image
FROM microsoft/dotnet:2.1-aspnetcore-runtime
WORKDIR /app
COPY --from=build-env /app/Bookmrqr.Viewer/out .
ENTRYPOINT ["dotnet", "Bookmrqr.Viewer.dll"]
