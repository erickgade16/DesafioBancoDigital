# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copia os arquivos .csproj de todas as camadas
COPY Desafio_Banco_Digital/DesafioBancoDigital.Api/DesafioBancoDigital.Api.csproj ./DesafioBancoDigital.Api/
COPY Desafio_Banco_Digital/DesafioBancoDigital.Application/*.csproj ./DesafioBancoDigital.Application/
COPY Desafio_Banco_Digital/DesafioBancoDigital.Domain/*.csproj ./DesafioBancoDigital.Domain/
COPY Desafio_Banco_Digital/DesafioBancoDigital.Infrastructure/*.csproj ./DesafioBancoDigital.Infrastructure/

# Restaura os pacotes
RUN dotnet restore ./DesafioBancoDigital.Api/DesafioBancoDigital.Api.csproj

# Copia todo o conteúdo do projeto
COPY Desafio_Banco_Digital/. .

# Publica o projeto em modo Release
RUN dotnet publish ./DesafioBancoDigital.Api/DesafioBancoDigital.Api.csproj -c Release -o /out

# Etapa de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /out ./

# Exponha a porta que a aplicação usa (ajuste se necessário)
EXPOSE 8080

# Executa o projeto
ENTRYPOINT ["dotnet", "DesafioBancoDigital.Api.dll"]
