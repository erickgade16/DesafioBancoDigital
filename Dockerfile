# Acesse https://aka.ms/customizecontainer para saber como personalizar seu contêiner de depuração e como o Visual Studio usa este Dockerfile para criar suas imagens para uma depuração mais rápida.

# Esta fase é usada durante a execução no VS no modo rápido (Padrão para a configuração de Depuração)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copiar os arquivos de projeto e restaurar as dependências
COPY ["Desafio_Banco_Digital/DesafioBancoDigital.API.csproj", "Desafio_Banco_Digital/"]
COPY ["Infrastructure/DesafioBancoDigital.Infrastructure.csproj", "Infrastructure/"]
COPY ["Application/DesafioBancoDigital.Application.csproj", "Application/"]
COPY ["Domain/DesafioBancoDigital.Domain.csproj", "Domain/"]
RUN dotnet restore "Desafio_Banco_Digital/DesafioBancoDigital.API.csproj"

# Copiar o resto do código e fazer o build
COPY . .
RUN dotnet build "Desafio_Banco_Digital/DesafioBancoDigital.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Desafio_Banco_Digital/DesafioBancoDigital.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DesafioBancoDigital.API.dll"]