FROM mcr.microsoft.com/dotnet/sdk:7.0 AS dotnet-build
COPY ./back .
RUN dotnet restore Journalist.Crm.Api
RUN dotnet publish Journalist.Crm.Api -c Releases -o /dist/

FROM node:lts as angular-build
COPY ./front ./front
WORKDIR /front
RUN npm i --legacy-peer-deps
RUN npm run build && \
    mkdir /dist && \
    cp -r /back/Journalist.Crm.Api/wwwroot/* /dist

FROM mcr.microsoft.com/dotnet/aspnet:7.0-alpine
WORKDIR /app
COPY --from=dotnet-build /dist .
COPY --from=angular-build /dist ./wwwroot
RUN chmod -R 777 ./wwwroot
USER nobody
CMD ["dotnet", "exec",  "Journalist.Crm.Api.dll"]