# Get base image from SDK Image from Microsoft
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app
#COPY the Cs project file CSPROJ and restore any dependencies (via NUGET)
COPY *.csproj ./
RUN dotnet restore 

#COPY the project files and build our release
COPY . ./ 
RUN dotnet publish -c Release -o out 

#Generate runtime image 
FROM mcr.microsoft.com/dotnet/sdk:6.0
WORKDIR /app
EXPOSE 5000
COPY --from=build-env /app/out .
ENTRYPOINT [ "dotnet","demo.dll" ]