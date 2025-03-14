
ARG DOTNET_VERSION=9.0

FROM mcr.microsoft.com/dotnet/sdk:${DOTNET_VERSION} AS build

WORKDIR /src

COPY ./CompanyApp.sln ./
COPY ./CompanyApp.Api/*.csproj ./CompanyApp.Api/
COPY ./CompanyApp.Domain/*.csproj ./CompanyApp.Domain/
COPY ./CompanyApp.Infrastructure/*.csproj ./CompanyApp.Infrastructure/

RUN dotnet restore "CompanyApp.Api/CompanyApp.Api.csproj"

COPY . ./

WORKDIR /src/CompanyApp.Api
RUN dotnet publish -c Release -o /publish

#
FROM mcr.microsoft.com/dotnet/aspnet:${DOTNET_VERSION} AS runtime

WORKDIR /app
EXPOSE 80

COPY --from=build /publish ./
ENTRYPOINT ["dotnet", "CompanyApp.Api.dll"]