FROM mcr.microsoft.com/dotnet/sdk:6.0

ENV DOTNET_CLI_HOME="/tmp/DOTNET_CLI_HOME"
ENV DEB_HOST=localhost
ENV BQ_DB=fcd3
ENV BATCH_SIZE=500

#USER 1001 # Kaniko does not like this

WORKDIR /app
COPY *.csproj ./
RUN dotnet restore

COPY . ./
ENTRYPOINT dotnet run --configuration Release insert "$DEB_HOST" "$BQ_DB" --batch-size "$BATCH_SIZE"
