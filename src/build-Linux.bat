
cd  ToolGood.TextFilter.App

dotnet build -c Release

cd ..

xcopy /y ".\ToolGood.TextFilter.App\bin\Release\net5.0\*.*" ".\libs\Release\"

xcopy /y ".\ToolGood.TextFilter.App\bin\Release\net6.0\*.*" ".\libs\Release\"
 

cd ToolGood.TextFilter.Website


dotnet publish -c Linux -p:PublishProfile=Linux  -o bin/Release-Linux

pause