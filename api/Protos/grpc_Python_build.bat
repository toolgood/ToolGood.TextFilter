mkdir python

python -m grpc_tools.protoc --python_out=./python --grpc_python_out=./python -I. imageClassify.proto

python -m grpc_tools.protoc --python_out=./python --grpc_python_out=./python -I. imageFilter.proto

python -m grpc_tools.protoc --python_out=./python --grpc_python_out=./python -I. textFilter.proto