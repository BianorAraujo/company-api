
ARG DOTNET_VERSION=9.0

FROM mcr.microsoft.com/dotnet/sdk:${DOTNET_VERSION} AS build

WORKDIR /src

COPY ./CompanyApi.sln ./
COPY ./CompanyApi/*.csproj ./CompanyApi/
COPY ./CompanyApi.Business/*.csproj ./CompanyApi.Business/
COPY ./CompanyApi.Test/*.csproj ./CompanyApi.Test/

RUN dotnet restore "CompanyApi/CompanyApi.csproj"

COPY . ./

WORKDIR /src/CompanyApi
RUN dotnet publish -c Release -o /publish

#
FROM mcr.microsoft.com/dotnet/aspnet:${DOTNET_VERSION} AS runtime

WORKDIR /app
EXPOSE 80

COPY --from=build /publish ./
ENTRYPOINT ["dotnet", "CompanyApi.dll"]