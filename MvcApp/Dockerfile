FROM sgtux/dotnet-sdk-6 AS build
COPY files /source
WORKDIR /source/
RUN dotnet publish -c release -o /app

FROM sgtux/aspnetcore-runtime-6
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "NetCoreWebGoat.dll", "--urls=http://*"]
