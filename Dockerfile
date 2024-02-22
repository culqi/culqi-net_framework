# Usa la imagen oficial de .NET Framework SDK 4.8 como base
FROM mcr.microsoft.com/dotnet/framework/sdk:4.8 AS build

# Establece el directorio de trabajo en la raíz del proyecto
WORKDIR /app

# Copia el código fuente de tu proyecto a la imagen
COPY . /app

# Ejecuta la instalación de NuGet y restaura las dependencias de las pruebas
RUN dotnet restore Pruebas2/Pruebas.csproj

# Cambia al directorio de las pruebas
WORKDIR /app/Pruebas2

# Ejecuta las pruebas
CMD ["dotnet", "test", "TestAll.cs"]