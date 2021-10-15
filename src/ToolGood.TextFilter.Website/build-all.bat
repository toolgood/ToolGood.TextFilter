f:
cd F:\git\ToolGood.TextFilter\src\ToolGood.TextFilter.Website

dotnet publish -c Win -p:PublishProfile=Win -o bin/Release-Win
dotnet publish -c Win-GRPC-Consul -p:PublishProfile=Win -o bin/Release-Win-GRPC-Consul

dotnet publish -c Linux -p:PublishProfile=Linux  -o bin/Release-Linux
dotnet publish -c Linux-GRPC-Consul -p:PublishProfile=Linux -o bin/Release-Linux-GRPC-Consul
