# ------------------------------------------------------------
# Dockerfile for Government-services Project
# Author: Ahmed Mohamed Fathi
# Description: Build and run ASP.NET Core 8 Web API inside Docker
# ------------------------------------------------------------

# المرحلة الأولى: البناء
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# انسخ ملفات السوليوشن والمشروع
COPY Government.sln .
COPY Government/Government.csproj Government/

# نعمل Restore للـ NuGet packages
RUN dotnet restore Government/Government.csproj

# انسخ باقي ملفات المشروع
COPY Government/ Government/

# ابني المشروع
RUN dotnet publish Government/Government.csproj -c Release -o /app/publish

# المرحلة الثانية: التشغيل
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

# شغل التطبيق
ENTRYPOINT ["dotnet", "Government.dll"]
