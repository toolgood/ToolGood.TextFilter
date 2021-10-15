echo off 
echo Protos转目标编程语言中，go语言是最坑的，没有官方protoc-gen-go.exe
echo 按一下键，马上生成
pause

protoc --plugin=protoc-gen-go=protoc-gen-go.exe  --go_out=plugins=grpc:. imageClassify.proto

protoc --plugin=protoc-gen-go=protoc-gen-go.exe  --go_out=plugins=grpc:. imageFilter.proto

protoc --plugin=protoc-gen-go=protoc-gen-go.exe  --go_out=plugins=grpc:. textFilter.proto

