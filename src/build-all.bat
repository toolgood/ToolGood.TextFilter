
cd  ToolGood.TextFilter.App

dotnet build -c Release
dotnet build -c Release-Async

cd ..

xcopy /y ".\ToolGood.TextFilter.App\bin\Release\net5.0\*.*" ".\libs\Release\"
xcopy /y ".\ToolGood.TextFilter.App\bin\Release-Async\net5.0\*.*" ".\libs\Release-Async\"

xcopy /y ".\ToolGood.TextFilter.App\bin\Release\net6.0\*.*" ".\libs\Release\"
xcopy /y ".\ToolGood.TextFilter.App\bin\Release-Async\net6.0\*.*" ".\libs\Release-Async\"
 

cd ToolGood.TextFilter.Website

dotnet publish -c Win -p:PublishProfile=Win -o bin/Release-Win
dotnet publish -c Win-GRPC-Consul -p:PublishProfile=Win -o bin/Release-Win-GRPC-Consul

dotnet publish -c Linux -p:PublishProfile=Linux  -o bin/Release-Linux
dotnet publish -c Linux-GRPC-Consul -p:PublishProfile=Linux -o bin/Release-Linux-GRPC-Consul

