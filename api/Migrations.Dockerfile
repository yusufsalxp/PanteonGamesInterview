FROM mcr.microsoft.com/dotnet/sdk:7.0
ENV PATH $PATH:/root/.dotnet/tools

WORKDIR /App

# Copy everything
COPY . ./

# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish -c Release -o out

RUN dotnet tool install --global dotnet-ef

RUN chmod +x ./setup.sh
CMD /bin/bash ./setup.sh