# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

COPY *.sln .
COPY Application/ Application/
COPY Domain/ Domain/
COPY Infrastructure/ Infrastructure/
COPY DesafioBancoDigital.Test/ DesafioBancoDigital.Test/
COPY Desafio_Banco_Digital/ Desafio_Banco_Digital/

RUN dotnet restore
RUN dotnet publish Desafio_Banco_Digital/DesafioBancoDigital.API.csproj -c Release -o out

# Etapa de runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

EXPOSE 80
ENTRYPOINT ["dotnet", "DesafioBancoDigital.API.dll"]
