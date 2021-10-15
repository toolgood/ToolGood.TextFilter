mkdir java

protoc.exe --plugin=protoc-gen-grpc-java=protoc-gen-grpc-java-1.39.0-windows-x86_64.exe --grpc-java_out=./java imageClassify.proto

protoc.exe --plugin=protoc-gen-grpc-java=protoc-gen-grpc-java-1.39.0-windows-x86_64.exe --grpc-java_out=./java imageFilter.proto

protoc.exe --plugin=protoc-gen-grpc-java=protoc-gen-grpc-java-1.39.0-windows-x86_64.exe --grpc-java_out=./java textFilter.proto

protoc.exe imageClassify.proto --java_out=./java

protoc.exe imageFilter.proto --java_out=./java

protoc.exe textFilter.proto --java_out=./java


