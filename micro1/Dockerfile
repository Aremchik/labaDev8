# Используем многостадийную сборку
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# Устанавливаем правильные права и конвертируем файлы
WORKDIR /src
COPY . .
RUN apt-get update && apt-get install -y dos2unix && \
    find . -type f -name "*.cs" -exec dos2unix {} + && \
    find . -type f -name "*.csproj" -exec dos2unix {} + && \
    dotnet restore && \
    dotnet publish -c release -o /app

# Финальный образ
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app .
ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80
ENTRYPOINT ["dotnet", "CSharpService.dll"]