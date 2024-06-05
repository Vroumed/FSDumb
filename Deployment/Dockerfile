FROM mcr.microsoft.com/dotnet/sdk:8.0 as build

# Copy project to container
COPY . /src
WORKDIR /src

# Restore and build project
ARG PLATFORM
ARG BUILD_TYPE=Debug
ENV NUGET_PACKAGES /cache/nuget
RUN --mount=type=cache,target=/cache \
dotnet build --sc -c $BUILD_TYPE -r $PLATFORM --verbosity detailed -o /output FSDumb

# Export build artifacts
FROM scratch
COPY --from=build /output .
