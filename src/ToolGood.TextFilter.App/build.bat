f:
cd F:\git\ToolGood.TextFilter\src\ToolGood.TextFilter.App

dotnet build -c Win
dotnet build -c Linux

copy /y "F:\git\ToolGood.TextFilter\src\ToolGood.TextFilter.App\bin\Linux\net5.0" "F:\git\ToolGood.TextFilter\src\libs\Linux"
copy /y "F:\git\ToolGood.TextFilter\src\ToolGood.TextFilter.App\bin\Win\net5.0" "F:\git\ToolGood.TextFilter\src\libs\Win"
