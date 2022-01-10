# Docker / Kubectl

# Contents

1.  docker
2.  docker-compose
3.  kubectl
4.  docker images
5.  docker examples
6.  docker-compose example
7.  kubernetes example

# docker

docker ps -a
docker images

docker pull mcr.microsoft.com/dotnet/runtime:6.0
docker pull mcr.microsoft.com/dotnet/sdk:6.0
docker pull mcr.microsoft.com/dotnet/aspnet:6.0

docker build -t testapp .
docker build --build-arg RUNTIME_SERVICE=MiniTools.HostApp.Services.ExampleBackgroundService -t testapp:latest .

docker build -t testapp . --file .\MiniTrade.ConsoleApp\Dockerfile

docker run -it --rm testapp
docker run -it --rm --env RUNTIME_SERVICE=SOME_RTS_VALUE testapp

docker run -it --rm -w /app testapp
docker run -it --rm -w /app -v "$(pwd)/data:/data" testapp

docker login

docker tag testapp [YOUR DOCKER USER NAME]/testapp

docker push [YOUR DOCKER USER NAME]/testapp


# docker-compose

docker-compose build
docker-compose build --build-arg RUNTIME_SERVICE=SOME_RTS_VALUE222

docker-compose up
docker-compose up -d

docker-compose down

# kubectl

kubectl get <resources:pods|services|deployments>
kubectl get services
kubectl get pods
kubectl get deployments

kubectl apply  -f kb-deployment-testapp1.yml
kubectl delete -f kb-deployment-testapp1.yml

kubectl scale --replicas=5 deployment/testapp1
kubectl scale --replicas=1 deployment/testapp1

kubectl delete pod <pod>
kubectl delete deployment testapp1

# docker images

dotnet/sdk          : .NET SDK
dotnet/aspnet       : ASP.NET Core Runtime
dotnet/runtime      : .NET Runtime
dotnet/runtime-deps : .NET Runtime Dependencies
dotnet/monitor      : .NET Monitor Tool
dotnet/samples      : .NET Samples

# docker examples

```Dockerfile
# Build
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY MiniTrade.ConsoleApp/MiniTrade.ConsoleApp.csproj .
RUN dotnet restore
COPY . .
RUN dotnet publish -c release -o /app MiniTrade.ConsoleApp/MiniTrade.ConsoleApp.csproj

# Runtime entrypoint
FROM mcr.microsoft.com/dotnet/runtime:6.0
WORKDIR /app
EXPOSE 80
EXPOSE 443
COPY --from=build /app .
ARG RUNTIME_SERVICE=default_value
ENV RUNTIME_SERVICE=$RUNTIME_SERVICE
ENTRYPOINT ["dotnet", "MiniTrade.ConsoleApp.dll"]

```

# docker-compose example

```yml:docker-compose.yml
version: '3.4'

services: 

  testApp:
    image: testapp
    build:
      context: MiniTrade.ConsoleApp
      dockerfile: Dockerfile
    environment:
      - backendUrl=http://backend
    ports:
      - "5900:80"
    volumes:
       - miniTrade-consoleApp-data:/data
       - type: bind
         source: ./_runtime-data
         target: /data

volumes:
  miniTrade-consoleApp-data:
```

# kubernetes example

```yml:kb-deployment-testapp1.yml
apiVersion: apps/v1
kind: Deployment
metadata:
  name: testapp1
spec:
  replicas: 1
  template:
    metadata:
      labels:
        app: testapp2
    spec:
      containers:
      - name: testapp3
        image: testapp:latest
        imagePullPolicy: Never
        ports:
        - containerPort: 80
  selector:
    matchLabels:
      app: testapp2
```


# Reference

https://docs.docker.com/compose/compose-file/compose-versioning/