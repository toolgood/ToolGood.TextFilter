package toolgood.textfilter.api.GrpcBase;

import static io.grpc.MethodDescriptor.generateFullMethodName;

/**
 * <pre>
 * The greeting service definition.
 * </pre>
 */
@javax.annotation.Generated(
    value = "by gRPC proto compiler (version 1.39.0)",
    comments = "Source: textFilter.proto")
public final class TextFilterGrpcGrpc {

  private TextFilterGrpcGrpc() {}

  public static final String SERVICE_NAME = "toolgood.textfilter.api.GrpcBase.TextFilterGrpc";

  // Static method descriptors that strictly reflect the proto.
  private static volatile io.grpc.MethodDescriptor<toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllGrpcRequest,
      toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllGrpcReply> getTextFilterMethod;

  @io.grpc.stub.annotations.RpcMethod(
      fullMethodName = SERVICE_NAME + '/' + "TextFilter",
      requestType = toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllGrpcRequest.class,
      responseType = toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllGrpcReply.class,
      methodType = io.grpc.MethodDescriptor.MethodType.UNARY)
  public static io.grpc.MethodDescriptor<toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllGrpcRequest,
      toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllGrpcReply> getTextFilterMethod() {
    io.grpc.MethodDescriptor<toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllGrpcRequest, toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllGrpcReply> getTextFilterMethod;
    if ((getTextFilterMethod = TextFilterGrpcGrpc.getTextFilterMethod) == null) {
      synchronized (TextFilterGrpcGrpc.class) {
        if ((getTextFilterMethod = TextFilterGrpcGrpc.getTextFilterMethod) == null) {
          TextFilterGrpcGrpc.getTextFilterMethod = getTextFilterMethod =
              io.grpc.MethodDescriptor.<toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllGrpcRequest, toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllGrpcReply>newBuilder()
              .setType(io.grpc.MethodDescriptor.MethodType.UNARY)
              .setFullMethodName(generateFullMethodName(SERVICE_NAME, "TextFilter"))
              .setSampledToLocalTracing(true)
              .setRequestMarshaller(io.grpc.protobuf.ProtoUtils.marshaller(
                  toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllGrpcRequest.getDefaultInstance()))
              .setResponseMarshaller(io.grpc.protobuf.ProtoUtils.marshaller(
                  toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllGrpcReply.getDefaultInstance()))
              .setSchemaDescriptor(new TextFilterGrpcMethodDescriptorSupplier("TextFilter"))
              .build();
        }
      }
    }
    return getTextFilterMethod;
  }

  private static volatile io.grpc.MethodDescriptor<toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllGrpcRequest,
      toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllGrpcReply> getJsonFilterMethod;

  @io.grpc.stub.annotations.RpcMethod(
      fullMethodName = SERVICE_NAME + '/' + "JsonFilter",
      requestType = toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllGrpcRequest.class,
      responseType = toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllGrpcReply.class,
      methodType = io.grpc.MethodDescriptor.MethodType.UNARY)
  public static io.grpc.MethodDescriptor<toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllGrpcRequest,
      toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllGrpcReply> getJsonFilterMethod() {
    io.grpc.MethodDescriptor<toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllGrpcRequest, toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllGrpcReply> getJsonFilterMethod;
    if ((getJsonFilterMethod = TextFilterGrpcGrpc.getJsonFilterMethod) == null) {
      synchronized (TextFilterGrpcGrpc.class) {
        if ((getJsonFilterMethod = TextFilterGrpcGrpc.getJsonFilterMethod) == null) {
          TextFilterGrpcGrpc.getJsonFilterMethod = getJsonFilterMethod =
              io.grpc.MethodDescriptor.<toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllGrpcRequest, toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllGrpcReply>newBuilder()
              .setType(io.grpc.MethodDescriptor.MethodType.UNARY)
              .setFullMethodName(generateFullMethodName(SERVICE_NAME, "JsonFilter"))
              .setSampledToLocalTracing(true)
              .setRequestMarshaller(io.grpc.protobuf.ProtoUtils.marshaller(
                  toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllGrpcRequest.getDefaultInstance()))
              .setResponseMarshaller(io.grpc.protobuf.ProtoUtils.marshaller(
                  toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllGrpcReply.getDefaultInstance()))
              .setSchemaDescriptor(new TextFilterGrpcMethodDescriptorSupplier("JsonFilter"))
              .build();
        }
      }
    }
    return getJsonFilterMethod;
  }

  private static volatile io.grpc.MethodDescriptor<toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllGrpcRequest,
      toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllGrpcReply> getHtmlFilterMethod;

  @io.grpc.stub.annotations.RpcMethod(
      fullMethodName = SERVICE_NAME + '/' + "HtmlFilter",
      requestType = toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllGrpcRequest.class,
      responseType = toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllGrpcReply.class,
      methodType = io.grpc.MethodDescriptor.MethodType.UNARY)
  public static io.grpc.MethodDescriptor<toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllGrpcRequest,
      toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllGrpcReply> getHtmlFilterMethod() {
    io.grpc.MethodDescriptor<toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllGrpcRequest, toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllGrpcReply> getHtmlFilterMethod;
    if ((getHtmlFilterMethod = TextFilterGrpcGrpc.getHtmlFilterMethod) == null) {
      synchronized (TextFilterGrpcGrpc.class) {
        if ((getHtmlFilterMethod = TextFilterGrpcGrpc.getHtmlFilterMethod) == null) {
          TextFilterGrpcGrpc.getHtmlFilterMethod = getHtmlFilterMethod =
              io.grpc.MethodDescriptor.<toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllGrpcRequest, toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllGrpcReply>newBuilder()
              .setType(io.grpc.MethodDescriptor.MethodType.UNARY)
              .setFullMethodName(generateFullMethodName(SERVICE_NAME, "HtmlFilter"))
              .setSampledToLocalTracing(true)
              .setRequestMarshaller(io.grpc.protobuf.ProtoUtils.marshaller(
                  toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllGrpcRequest.getDefaultInstance()))
              .setResponseMarshaller(io.grpc.protobuf.ProtoUtils.marshaller(
                  toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllGrpcReply.getDefaultInstance()))
              .setSchemaDescriptor(new TextFilterGrpcMethodDescriptorSupplier("HtmlFilter"))
              .build();
        }
      }
    }
    return getHtmlFilterMethod;
  }

  private static volatile io.grpc.MethodDescriptor<toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllGrpcRequest,
      toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllGrpcReply> getMarkdownFilterMethod;

  @io.grpc.stub.annotations.RpcMethod(
      fullMethodName = SERVICE_NAME + '/' + "MarkdownFilter",
      requestType = toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllGrpcRequest.class,
      responseType = toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllGrpcReply.class,
      methodType = io.grpc.MethodDescriptor.MethodType.UNARY)
  public static io.grpc.MethodDescriptor<toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllGrpcRequest,
      toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllGrpcReply> getMarkdownFilterMethod() {
    io.grpc.MethodDescriptor<toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllGrpcRequest, toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllGrpcReply> getMarkdownFilterMethod;
    if ((getMarkdownFilterMethod = TextFilterGrpcGrpc.getMarkdownFilterMethod) == null) {
      synchronized (TextFilterGrpcGrpc.class) {
        if ((getMarkdownFilterMethod = TextFilterGrpcGrpc.getMarkdownFilterMethod) == null) {
          TextFilterGrpcGrpc.getMarkdownFilterMethod = getMarkdownFilterMethod =
              io.grpc.MethodDescriptor.<toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllGrpcRequest, toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllGrpcReply>newBuilder()
              .setType(io.grpc.MethodDescriptor.MethodType.UNARY)
              .setFullMethodName(generateFullMethodName(SERVICE_NAME, "MarkdownFilter"))
              .setSampledToLocalTracing(true)
              .setRequestMarshaller(io.grpc.protobuf.ProtoUtils.marshaller(
                  toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllGrpcRequest.getDefaultInstance()))
              .setResponseMarshaller(io.grpc.protobuf.ProtoUtils.marshaller(
                  toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllGrpcReply.getDefaultInstance()))
              .setSchemaDescriptor(new TextFilterGrpcMethodDescriptorSupplier("MarkdownFilter"))
              .build();
        }
      }
    }
    return getMarkdownFilterMethod;
  }

  private static volatile io.grpc.MethodDescriptor<toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceGrpcRequest,
      toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceGrpcReply> getTextReplaceMethod;

  @io.grpc.stub.annotations.RpcMethod(
      fullMethodName = SERVICE_NAME + '/' + "TextReplace",
      requestType = toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceGrpcRequest.class,
      responseType = toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceGrpcReply.class,
      methodType = io.grpc.MethodDescriptor.MethodType.UNARY)
  public static io.grpc.MethodDescriptor<toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceGrpcRequest,
      toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceGrpcReply> getTextReplaceMethod() {
    io.grpc.MethodDescriptor<toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceGrpcRequest, toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceGrpcReply> getTextReplaceMethod;
    if ((getTextReplaceMethod = TextFilterGrpcGrpc.getTextReplaceMethod) == null) {
      synchronized (TextFilterGrpcGrpc.class) {
        if ((getTextReplaceMethod = TextFilterGrpcGrpc.getTextReplaceMethod) == null) {
          TextFilterGrpcGrpc.getTextReplaceMethod = getTextReplaceMethod =
              io.grpc.MethodDescriptor.<toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceGrpcRequest, toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceGrpcReply>newBuilder()
              .setType(io.grpc.MethodDescriptor.MethodType.UNARY)
              .setFullMethodName(generateFullMethodName(SERVICE_NAME, "TextReplace"))
              .setSampledToLocalTracing(true)
              .setRequestMarshaller(io.grpc.protobuf.ProtoUtils.marshaller(
                  toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceGrpcRequest.getDefaultInstance()))
              .setResponseMarshaller(io.grpc.protobuf.ProtoUtils.marshaller(
                  toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceGrpcReply.getDefaultInstance()))
              .setSchemaDescriptor(new TextFilterGrpcMethodDescriptorSupplier("TextReplace"))
              .build();
        }
      }
    }
    return getTextReplaceMethod;
  }

  private static volatile io.grpc.MethodDescriptor<toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceGrpcRequest,
      toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceGrpcReply> getJsonReplaceMethod;

  @io.grpc.stub.annotations.RpcMethod(
      fullMethodName = SERVICE_NAME + '/' + "JsonReplace",
      requestType = toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceGrpcRequest.class,
      responseType = toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceGrpcReply.class,
      methodType = io.grpc.MethodDescriptor.MethodType.UNARY)
  public static io.grpc.MethodDescriptor<toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceGrpcRequest,
      toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceGrpcReply> getJsonReplaceMethod() {
    io.grpc.MethodDescriptor<toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceGrpcRequest, toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceGrpcReply> getJsonReplaceMethod;
    if ((getJsonReplaceMethod = TextFilterGrpcGrpc.getJsonReplaceMethod) == null) {
      synchronized (TextFilterGrpcGrpc.class) {
        if ((getJsonReplaceMethod = TextFilterGrpcGrpc.getJsonReplaceMethod) == null) {
          TextFilterGrpcGrpc.getJsonReplaceMethod = getJsonReplaceMethod =
              io.grpc.MethodDescriptor.<toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceGrpcRequest, toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceGrpcReply>newBuilder()
              .setType(io.grpc.MethodDescriptor.MethodType.UNARY)
              .setFullMethodName(generateFullMethodName(SERVICE_NAME, "JsonReplace"))
              .setSampledToLocalTracing(true)
              .setRequestMarshaller(io.grpc.protobuf.ProtoUtils.marshaller(
                  toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceGrpcRequest.getDefaultInstance()))
              .setResponseMarshaller(io.grpc.protobuf.ProtoUtils.marshaller(
                  toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceGrpcReply.getDefaultInstance()))
              .setSchemaDescriptor(new TextFilterGrpcMethodDescriptorSupplier("JsonReplace"))
              .build();
        }
      }
    }
    return getJsonReplaceMethod;
  }

  private static volatile io.grpc.MethodDescriptor<toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceGrpcRequest,
      toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceGrpcReply> getHtmlReplaceMethod;

  @io.grpc.stub.annotations.RpcMethod(
      fullMethodName = SERVICE_NAME + '/' + "HtmlReplace",
      requestType = toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceGrpcRequest.class,
      responseType = toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceGrpcReply.class,
      methodType = io.grpc.MethodDescriptor.MethodType.UNARY)
  public static io.grpc.MethodDescriptor<toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceGrpcRequest,
      toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceGrpcReply> getHtmlReplaceMethod() {
    io.grpc.MethodDescriptor<toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceGrpcRequest, toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceGrpcReply> getHtmlReplaceMethod;
    if ((getHtmlReplaceMethod = TextFilterGrpcGrpc.getHtmlReplaceMethod) == null) {
      synchronized (TextFilterGrpcGrpc.class) {
        if ((getHtmlReplaceMethod = TextFilterGrpcGrpc.getHtmlReplaceMethod) == null) {
          TextFilterGrpcGrpc.getHtmlReplaceMethod = getHtmlReplaceMethod =
              io.grpc.MethodDescriptor.<toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceGrpcRequest, toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceGrpcReply>newBuilder()
              .setType(io.grpc.MethodDescriptor.MethodType.UNARY)
              .setFullMethodName(generateFullMethodName(SERVICE_NAME, "HtmlReplace"))
              .setSampledToLocalTracing(true)
              .setRequestMarshaller(io.grpc.protobuf.ProtoUtils.marshaller(
                  toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceGrpcRequest.getDefaultInstance()))
              .setResponseMarshaller(io.grpc.protobuf.ProtoUtils.marshaller(
                  toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceGrpcReply.getDefaultInstance()))
              .setSchemaDescriptor(new TextFilterGrpcMethodDescriptorSupplier("HtmlReplace"))
              .build();
        }
      }
    }
    return getHtmlReplaceMethod;
  }

  private static volatile io.grpc.MethodDescriptor<toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceGrpcRequest,
      toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceGrpcReply> getMarkdownReplaceMethod;

  @io.grpc.stub.annotations.RpcMethod(
      fullMethodName = SERVICE_NAME + '/' + "MarkdownReplace",
      requestType = toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceGrpcRequest.class,
      responseType = toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceGrpcReply.class,
      methodType = io.grpc.MethodDescriptor.MethodType.UNARY)
  public static io.grpc.MethodDescriptor<toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceGrpcRequest,
      toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceGrpcReply> getMarkdownReplaceMethod() {
    io.grpc.MethodDescriptor<toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceGrpcRequest, toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceGrpcReply> getMarkdownReplaceMethod;
    if ((getMarkdownReplaceMethod = TextFilterGrpcGrpc.getMarkdownReplaceMethod) == null) {
      synchronized (TextFilterGrpcGrpc.class) {
        if ((getMarkdownReplaceMethod = TextFilterGrpcGrpc.getMarkdownReplaceMethod) == null) {
          TextFilterGrpcGrpc.getMarkdownReplaceMethod = getMarkdownReplaceMethod =
              io.grpc.MethodDescriptor.<toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceGrpcRequest, toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceGrpcReply>newBuilder()
              .setType(io.grpc.MethodDescriptor.MethodType.UNARY)
              .setFullMethodName(generateFullMethodName(SERVICE_NAME, "MarkdownReplace"))
              .setSampledToLocalTracing(true)
              .setRequestMarshaller(io.grpc.protobuf.ProtoUtils.marshaller(
                  toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceGrpcRequest.getDefaultInstance()))
              .setResponseMarshaller(io.grpc.protobuf.ProtoUtils.marshaller(
                  toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceGrpcReply.getDefaultInstance()))
              .setSchemaDescriptor(new TextFilterGrpcMethodDescriptorSupplier("MarkdownReplace"))
              .build();
        }
      }
    }
    return getMarkdownReplaceMethod;
  }

  private static volatile io.grpc.MethodDescriptor<toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllAsyncGrpcRequest,
      toolgood.textfilter.api.GrpcBase.TextFilter.TextFilterRequestIdGrpcReply> getTextFilterAsyncMethod;

  @io.grpc.stub.annotations.RpcMethod(
      fullMethodName = SERVICE_NAME + '/' + "TextFilterAsync",
      requestType = toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllAsyncGrpcRequest.class,
      responseType = toolgood.textfilter.api.GrpcBase.TextFilter.TextFilterRequestIdGrpcReply.class,
      methodType = io.grpc.MethodDescriptor.MethodType.UNARY)
  public static io.grpc.MethodDescriptor<toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllAsyncGrpcRequest,
      toolgood.textfilter.api.GrpcBase.TextFilter.TextFilterRequestIdGrpcReply> getTextFilterAsyncMethod() {
    io.grpc.MethodDescriptor<toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllAsyncGrpcRequest, toolgood.textfilter.api.GrpcBase.TextFilter.TextFilterRequestIdGrpcReply> getTextFilterAsyncMethod;
    if ((getTextFilterAsyncMethod = TextFilterGrpcGrpc.getTextFilterAsyncMethod) == null) {
      synchronized (TextFilterGrpcGrpc.class) {
        if ((getTextFilterAsyncMethod = TextFilterGrpcGrpc.getTextFilterAsyncMethod) == null) {
          TextFilterGrpcGrpc.getTextFilterAsyncMethod = getTextFilterAsyncMethod =
              io.grpc.MethodDescriptor.<toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllAsyncGrpcRequest, toolgood.textfilter.api.GrpcBase.TextFilter.TextFilterRequestIdGrpcReply>newBuilder()
              .setType(io.grpc.MethodDescriptor.MethodType.UNARY)
              .setFullMethodName(generateFullMethodName(SERVICE_NAME, "TextFilterAsync"))
              .setSampledToLocalTracing(true)
              .setRequestMarshaller(io.grpc.protobuf.ProtoUtils.marshaller(
                  toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllAsyncGrpcRequest.getDefaultInstance()))
              .setResponseMarshaller(io.grpc.protobuf.ProtoUtils.marshaller(
                  toolgood.textfilter.api.GrpcBase.TextFilter.TextFilterRequestIdGrpcReply.getDefaultInstance()))
              .setSchemaDescriptor(new TextFilterGrpcMethodDescriptorSupplier("TextFilterAsync"))
              .build();
        }
      }
    }
    return getTextFilterAsyncMethod;
  }

  private static volatile io.grpc.MethodDescriptor<toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllAsyncGrpcRequest,
      toolgood.textfilter.api.GrpcBase.TextFilter.TextFilterRequestIdGrpcReply> getJsonFilterAsyncMethod;

  @io.grpc.stub.annotations.RpcMethod(
      fullMethodName = SERVICE_NAME + '/' + "JsonFilterAsync",
      requestType = toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllAsyncGrpcRequest.class,
      responseType = toolgood.textfilter.api.GrpcBase.TextFilter.TextFilterRequestIdGrpcReply.class,
      methodType = io.grpc.MethodDescriptor.MethodType.UNARY)
  public static io.grpc.MethodDescriptor<toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllAsyncGrpcRequest,
      toolgood.textfilter.api.GrpcBase.TextFilter.TextFilterRequestIdGrpcReply> getJsonFilterAsyncMethod() {
    io.grpc.MethodDescriptor<toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllAsyncGrpcRequest, toolgood.textfilter.api.GrpcBase.TextFilter.TextFilterRequestIdGrpcReply> getJsonFilterAsyncMethod;
    if ((getJsonFilterAsyncMethod = TextFilterGrpcGrpc.getJsonFilterAsyncMethod) == null) {
      synchronized (TextFilterGrpcGrpc.class) {
        if ((getJsonFilterAsyncMethod = TextFilterGrpcGrpc.getJsonFilterAsyncMethod) == null) {
          TextFilterGrpcGrpc.getJsonFilterAsyncMethod = getJsonFilterAsyncMethod =
              io.grpc.MethodDescriptor.<toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllAsyncGrpcRequest, toolgood.textfilter.api.GrpcBase.TextFilter.TextFilterRequestIdGrpcReply>newBuilder()
              .setType(io.grpc.MethodDescriptor.MethodType.UNARY)
              .setFullMethodName(generateFullMethodName(SERVICE_NAME, "JsonFilterAsync"))
              .setSampledToLocalTracing(true)
              .setRequestMarshaller(io.grpc.protobuf.ProtoUtils.marshaller(
                  toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllAsyncGrpcRequest.getDefaultInstance()))
              .setResponseMarshaller(io.grpc.protobuf.ProtoUtils.marshaller(
                  toolgood.textfilter.api.GrpcBase.TextFilter.TextFilterRequestIdGrpcReply.getDefaultInstance()))
              .setSchemaDescriptor(new TextFilterGrpcMethodDescriptorSupplier("JsonFilterAsync"))
              .build();
        }
      }
    }
    return getJsonFilterAsyncMethod;
  }

  private static volatile io.grpc.MethodDescriptor<toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllAsyncGrpcRequest,
      toolgood.textfilter.api.GrpcBase.TextFilter.TextFilterRequestIdGrpcReply> getHtmlFilterAsyncMethod;

  @io.grpc.stub.annotations.RpcMethod(
      fullMethodName = SERVICE_NAME + '/' + "HtmlFilterAsync",
      requestType = toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllAsyncGrpcRequest.class,
      responseType = toolgood.textfilter.api.GrpcBase.TextFilter.TextFilterRequestIdGrpcReply.class,
      methodType = io.grpc.MethodDescriptor.MethodType.UNARY)
  public static io.grpc.MethodDescriptor<toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllAsyncGrpcRequest,
      toolgood.textfilter.api.GrpcBase.TextFilter.TextFilterRequestIdGrpcReply> getHtmlFilterAsyncMethod() {
    io.grpc.MethodDescriptor<toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllAsyncGrpcRequest, toolgood.textfilter.api.GrpcBase.TextFilter.TextFilterRequestIdGrpcReply> getHtmlFilterAsyncMethod;
    if ((getHtmlFilterAsyncMethod = TextFilterGrpcGrpc.getHtmlFilterAsyncMethod) == null) {
      synchronized (TextFilterGrpcGrpc.class) {
        if ((getHtmlFilterAsyncMethod = TextFilterGrpcGrpc.getHtmlFilterAsyncMethod) == null) {
          TextFilterGrpcGrpc.getHtmlFilterAsyncMethod = getHtmlFilterAsyncMethod =
              io.grpc.MethodDescriptor.<toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllAsyncGrpcRequest, toolgood.textfilter.api.GrpcBase.TextFilter.TextFilterRequestIdGrpcReply>newBuilder()
              .setType(io.grpc.MethodDescriptor.MethodType.UNARY)
              .setFullMethodName(generateFullMethodName(SERVICE_NAME, "HtmlFilterAsync"))
              .setSampledToLocalTracing(true)
              .setRequestMarshaller(io.grpc.protobuf.ProtoUtils.marshaller(
                  toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllAsyncGrpcRequest.getDefaultInstance()))
              .setResponseMarshaller(io.grpc.protobuf.ProtoUtils.marshaller(
                  toolgood.textfilter.api.GrpcBase.TextFilter.TextFilterRequestIdGrpcReply.getDefaultInstance()))
              .setSchemaDescriptor(new TextFilterGrpcMethodDescriptorSupplier("HtmlFilterAsync"))
              .build();
        }
      }
    }
    return getHtmlFilterAsyncMethod;
  }

  private static volatile io.grpc.MethodDescriptor<toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllAsyncGrpcRequest,
      toolgood.textfilter.api.GrpcBase.TextFilter.TextFilterRequestIdGrpcReply> getMarkdownFilterAsyncMethod;

  @io.grpc.stub.annotations.RpcMethod(
      fullMethodName = SERVICE_NAME + '/' + "MarkdownFilterAsync",
      requestType = toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllAsyncGrpcRequest.class,
      responseType = toolgood.textfilter.api.GrpcBase.TextFilter.TextFilterRequestIdGrpcReply.class,
      methodType = io.grpc.MethodDescriptor.MethodType.UNARY)
  public static io.grpc.MethodDescriptor<toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllAsyncGrpcRequest,
      toolgood.textfilter.api.GrpcBase.TextFilter.TextFilterRequestIdGrpcReply> getMarkdownFilterAsyncMethod() {
    io.grpc.MethodDescriptor<toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllAsyncGrpcRequest, toolgood.textfilter.api.GrpcBase.TextFilter.TextFilterRequestIdGrpcReply> getMarkdownFilterAsyncMethod;
    if ((getMarkdownFilterAsyncMethod = TextFilterGrpcGrpc.getMarkdownFilterAsyncMethod) == null) {
      synchronized (TextFilterGrpcGrpc.class) {
        if ((getMarkdownFilterAsyncMethod = TextFilterGrpcGrpc.getMarkdownFilterAsyncMethod) == null) {
          TextFilterGrpcGrpc.getMarkdownFilterAsyncMethod = getMarkdownFilterAsyncMethod =
              io.grpc.MethodDescriptor.<toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllAsyncGrpcRequest, toolgood.textfilter.api.GrpcBase.TextFilter.TextFilterRequestIdGrpcReply>newBuilder()
              .setType(io.grpc.MethodDescriptor.MethodType.UNARY)
              .setFullMethodName(generateFullMethodName(SERVICE_NAME, "MarkdownFilterAsync"))
              .setSampledToLocalTracing(true)
              .setRequestMarshaller(io.grpc.protobuf.ProtoUtils.marshaller(
                  toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllAsyncGrpcRequest.getDefaultInstance()))
              .setResponseMarshaller(io.grpc.protobuf.ProtoUtils.marshaller(
                  toolgood.textfilter.api.GrpcBase.TextFilter.TextFilterRequestIdGrpcReply.getDefaultInstance()))
              .setSchemaDescriptor(new TextFilterGrpcMethodDescriptorSupplier("MarkdownFilterAsync"))
              .build();
        }
      }
    }
    return getMarkdownFilterAsyncMethod;
  }

  private static volatile io.grpc.MethodDescriptor<toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceAsyncGrpcRequest,
      toolgood.textfilter.api.GrpcBase.TextFilter.TextFilterRequestIdGrpcReply> getTextReplaceAsyncMethod;

  @io.grpc.stub.annotations.RpcMethod(
      fullMethodName = SERVICE_NAME + '/' + "TextReplaceAsync",
      requestType = toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceAsyncGrpcRequest.class,
      responseType = toolgood.textfilter.api.GrpcBase.TextFilter.TextFilterRequestIdGrpcReply.class,
      methodType = io.grpc.MethodDescriptor.MethodType.UNARY)
  public static io.grpc.MethodDescriptor<toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceAsyncGrpcRequest,
      toolgood.textfilter.api.GrpcBase.TextFilter.TextFilterRequestIdGrpcReply> getTextReplaceAsyncMethod() {
    io.grpc.MethodDescriptor<toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceAsyncGrpcRequest, toolgood.textfilter.api.GrpcBase.TextFilter.TextFilterRequestIdGrpcReply> getTextReplaceAsyncMethod;
    if ((getTextReplaceAsyncMethod = TextFilterGrpcGrpc.getTextReplaceAsyncMethod) == null) {
      synchronized (TextFilterGrpcGrpc.class) {
        if ((getTextReplaceAsyncMethod = TextFilterGrpcGrpc.getTextReplaceAsyncMethod) == null) {
          TextFilterGrpcGrpc.getTextReplaceAsyncMethod = getTextReplaceAsyncMethod =
              io.grpc.MethodDescriptor.<toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceAsyncGrpcRequest, toolgood.textfilter.api.GrpcBase.TextFilter.TextFilterRequestIdGrpcReply>newBuilder()
              .setType(io.grpc.MethodDescriptor.MethodType.UNARY)
              .setFullMethodName(generateFullMethodName(SERVICE_NAME, "TextReplaceAsync"))
              .setSampledToLocalTracing(true)
              .setRequestMarshaller(io.grpc.protobuf.ProtoUtils.marshaller(
                  toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceAsyncGrpcRequest.getDefaultInstance()))
              .setResponseMarshaller(io.grpc.protobuf.ProtoUtils.marshaller(
                  toolgood.textfilter.api.GrpcBase.TextFilter.TextFilterRequestIdGrpcReply.getDefaultInstance()))
              .setSchemaDescriptor(new TextFilterGrpcMethodDescriptorSupplier("TextReplaceAsync"))
              .build();
        }
      }
    }
    return getTextReplaceAsyncMethod;
  }

  private static volatile io.grpc.MethodDescriptor<toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceAsyncGrpcRequest,
      toolgood.textfilter.api.GrpcBase.TextFilter.TextFilterRequestIdGrpcReply> getJsonReplaceAsyncMethod;

  @io.grpc.stub.annotations.RpcMethod(
      fullMethodName = SERVICE_NAME + '/' + "JsonReplaceAsync",
      requestType = toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceAsyncGrpcRequest.class,
      responseType = toolgood.textfilter.api.GrpcBase.TextFilter.TextFilterRequestIdGrpcReply.class,
      methodType = io.grpc.MethodDescriptor.MethodType.UNARY)
  public static io.grpc.MethodDescriptor<toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceAsyncGrpcRequest,
      toolgood.textfilter.api.GrpcBase.TextFilter.TextFilterRequestIdGrpcReply> getJsonReplaceAsyncMethod() {
    io.grpc.MethodDescriptor<toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceAsyncGrpcRequest, toolgood.textfilter.api.GrpcBase.TextFilter.TextFilterRequestIdGrpcReply> getJsonReplaceAsyncMethod;
    if ((getJsonReplaceAsyncMethod = TextFilterGrpcGrpc.getJsonReplaceAsyncMethod) == null) {
      synchronized (TextFilterGrpcGrpc.class) {
        if ((getJsonReplaceAsyncMethod = TextFilterGrpcGrpc.getJsonReplaceAsyncMethod) == null) {
          TextFilterGrpcGrpc.getJsonReplaceAsyncMethod = getJsonReplaceAsyncMethod =
              io.grpc.MethodDescriptor.<toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceAsyncGrpcRequest, toolgood.textfilter.api.GrpcBase.TextFilter.TextFilterRequestIdGrpcReply>newBuilder()
              .setType(io.grpc.MethodDescriptor.MethodType.UNARY)
              .setFullMethodName(generateFullMethodName(SERVICE_NAME, "JsonReplaceAsync"))
              .setSampledToLocalTracing(true)
              .setRequestMarshaller(io.grpc.protobuf.ProtoUtils.marshaller(
                  toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceAsyncGrpcRequest.getDefaultInstance()))
              .setResponseMarshaller(io.grpc.protobuf.ProtoUtils.marshaller(
                  toolgood.textfilter.api.GrpcBase.TextFilter.TextFilterRequestIdGrpcReply.getDefaultInstance()))
              .setSchemaDescriptor(new TextFilterGrpcMethodDescriptorSupplier("JsonReplaceAsync"))
              .build();
        }
      }
    }
    return getJsonReplaceAsyncMethod;
  }

  private static volatile io.grpc.MethodDescriptor<toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceAsyncGrpcRequest,
      toolgood.textfilter.api.GrpcBase.TextFilter.TextFilterRequestIdGrpcReply> getHtmlReplaceAsyncMethod;

  @io.grpc.stub.annotations.RpcMethod(
      fullMethodName = SERVICE_NAME + '/' + "HtmlReplaceAsync",
      requestType = toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceAsyncGrpcRequest.class,
      responseType = toolgood.textfilter.api.GrpcBase.TextFilter.TextFilterRequestIdGrpcReply.class,
      methodType = io.grpc.MethodDescriptor.MethodType.UNARY)
  public static io.grpc.MethodDescriptor<toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceAsyncGrpcRequest,
      toolgood.textfilter.api.GrpcBase.TextFilter.TextFilterRequestIdGrpcReply> getHtmlReplaceAsyncMethod() {
    io.grpc.MethodDescriptor<toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceAsyncGrpcRequest, toolgood.textfilter.api.GrpcBase.TextFilter.TextFilterRequestIdGrpcReply> getHtmlReplaceAsyncMethod;
    if ((getHtmlReplaceAsyncMethod = TextFilterGrpcGrpc.getHtmlReplaceAsyncMethod) == null) {
      synchronized (TextFilterGrpcGrpc.class) {
        if ((getHtmlReplaceAsyncMethod = TextFilterGrpcGrpc.getHtmlReplaceAsyncMethod) == null) {
          TextFilterGrpcGrpc.getHtmlReplaceAsyncMethod = getHtmlReplaceAsyncMethod =
              io.grpc.MethodDescriptor.<toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceAsyncGrpcRequest, toolgood.textfilter.api.GrpcBase.TextFilter.TextFilterRequestIdGrpcReply>newBuilder()
              .setType(io.grpc.MethodDescriptor.MethodType.UNARY)
              .setFullMethodName(generateFullMethodName(SERVICE_NAME, "HtmlReplaceAsync"))
              .setSampledToLocalTracing(true)
              .setRequestMarshaller(io.grpc.protobuf.ProtoUtils.marshaller(
                  toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceAsyncGrpcRequest.getDefaultInstance()))
              .setResponseMarshaller(io.grpc.protobuf.ProtoUtils.marshaller(
                  toolgood.textfilter.api.GrpcBase.TextFilter.TextFilterRequestIdGrpcReply.getDefaultInstance()))
              .setSchemaDescriptor(new TextFilterGrpcMethodDescriptorSupplier("HtmlReplaceAsync"))
              .build();
        }
      }
    }
    return getHtmlReplaceAsyncMethod;
  }

  private static volatile io.grpc.MethodDescriptor<toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceAsyncGrpcRequest,
      toolgood.textfilter.api.GrpcBase.TextFilter.TextFilterRequestIdGrpcReply> getMarkdownReplaceAsyncMethod;

  @io.grpc.stub.annotations.RpcMethod(
      fullMethodName = SERVICE_NAME + '/' + "MarkdownReplaceAsync",
      requestType = toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceAsyncGrpcRequest.class,
      responseType = toolgood.textfilter.api.GrpcBase.TextFilter.TextFilterRequestIdGrpcReply.class,
      methodType = io.grpc.MethodDescriptor.MethodType.UNARY)
  public static io.grpc.MethodDescriptor<toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceAsyncGrpcRequest,
      toolgood.textfilter.api.GrpcBase.TextFilter.TextFilterRequestIdGrpcReply> getMarkdownReplaceAsyncMethod() {
    io.grpc.MethodDescriptor<toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceAsyncGrpcRequest, toolgood.textfilter.api.GrpcBase.TextFilter.TextFilterRequestIdGrpcReply> getMarkdownReplaceAsyncMethod;
    if ((getMarkdownReplaceAsyncMethod = TextFilterGrpcGrpc.getMarkdownReplaceAsyncMethod) == null) {
      synchronized (TextFilterGrpcGrpc.class) {
        if ((getMarkdownReplaceAsyncMethod = TextFilterGrpcGrpc.getMarkdownReplaceAsyncMethod) == null) {
          TextFilterGrpcGrpc.getMarkdownReplaceAsyncMethod = getMarkdownReplaceAsyncMethod =
              io.grpc.MethodDescriptor.<toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceAsyncGrpcRequest, toolgood.textfilter.api.GrpcBase.TextFilter.TextFilterRequestIdGrpcReply>newBuilder()
              .setType(io.grpc.MethodDescriptor.MethodType.UNARY)
              .setFullMethodName(generateFullMethodName(SERVICE_NAME, "MarkdownReplaceAsync"))
              .setSampledToLocalTracing(true)
              .setRequestMarshaller(io.grpc.protobuf.ProtoUtils.marshaller(
                  toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceAsyncGrpcRequest.getDefaultInstance()))
              .setResponseMarshaller(io.grpc.protobuf.ProtoUtils.marshaller(
                  toolgood.textfilter.api.GrpcBase.TextFilter.TextFilterRequestIdGrpcReply.getDefaultInstance()))
              .setSchemaDescriptor(new TextFilterGrpcMethodDescriptorSupplier("MarkdownReplaceAsync"))
              .build();
        }
      }
    }
    return getMarkdownReplaceAsyncMethod;
  }

  /**
   * Creates a new async stub that supports all call types for the service
   */
  public static TextFilterGrpcStub newStub(io.grpc.Channel channel) {
    io.grpc.stub.AbstractStub.StubFactory<TextFilterGrpcStub> factory =
      new io.grpc.stub.AbstractStub.StubFactory<TextFilterGrpcStub>() {
        @java.lang.Override
        public TextFilterGrpcStub newStub(io.grpc.Channel channel, io.grpc.CallOptions callOptions) {
          return new TextFilterGrpcStub(channel, callOptions);
        }
      };
    return TextFilterGrpcStub.newStub(factory, channel);
  }

  /**
   * Creates a new blocking-style stub that supports unary and streaming output calls on the service
   */
  public static TextFilterGrpcBlockingStub newBlockingStub(
      io.grpc.Channel channel) {
    io.grpc.stub.AbstractStub.StubFactory<TextFilterGrpcBlockingStub> factory =
      new io.grpc.stub.AbstractStub.StubFactory<TextFilterGrpcBlockingStub>() {
        @java.lang.Override
        public TextFilterGrpcBlockingStub newStub(io.grpc.Channel channel, io.grpc.CallOptions callOptions) {
          return new TextFilterGrpcBlockingStub(channel, callOptions);
        }
      };
    return TextFilterGrpcBlockingStub.newStub(factory, channel);
  }

  /**
   * Creates a new ListenableFuture-style stub that supports unary calls on the service
   */
  public static TextFilterGrpcFutureStub newFutureStub(
      io.grpc.Channel channel) {
    io.grpc.stub.AbstractStub.StubFactory<TextFilterGrpcFutureStub> factory =
      new io.grpc.stub.AbstractStub.StubFactory<TextFilterGrpcFutureStub>() {
        @java.lang.Override
        public TextFilterGrpcFutureStub newStub(io.grpc.Channel channel, io.grpc.CallOptions callOptions) {
          return new TextFilterGrpcFutureStub(channel, callOptions);
        }
      };
    return TextFilterGrpcFutureStub.newStub(factory, channel);
  }

  /**
   * <pre>
   * The greeting service definition.
   * </pre>
   */
  public static abstract class TextFilterGrpcImplBase implements io.grpc.BindableService {

    /**
     */
    public void textFilter(toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllGrpcRequest request,
        io.grpc.stub.StreamObserver<toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllGrpcReply> responseObserver) {
      io.grpc.stub.ServerCalls.asyncUnimplementedUnaryCall(getTextFilterMethod(), responseObserver);
    }

    /**
     */
    public void jsonFilter(toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllGrpcRequest request,
        io.grpc.stub.StreamObserver<toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllGrpcReply> responseObserver) {
      io.grpc.stub.ServerCalls.asyncUnimplementedUnaryCall(getJsonFilterMethod(), responseObserver);
    }

    /**
     */
    public void htmlFilter(toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllGrpcRequest request,
        io.grpc.stub.StreamObserver<toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllGrpcReply> responseObserver) {
      io.grpc.stub.ServerCalls.asyncUnimplementedUnaryCall(getHtmlFilterMethod(), responseObserver);
    }

    /**
     */
    public void markdownFilter(toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllGrpcRequest request,
        io.grpc.stub.StreamObserver<toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllGrpcReply> responseObserver) {
      io.grpc.stub.ServerCalls.asyncUnimplementedUnaryCall(getMarkdownFilterMethod(), responseObserver);
    }

    /**
     */
    public void textReplace(toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceGrpcRequest request,
        io.grpc.stub.StreamObserver<toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceGrpcReply> responseObserver) {
      io.grpc.stub.ServerCalls.asyncUnimplementedUnaryCall(getTextReplaceMethod(), responseObserver);
    }

    /**
     */
    public void jsonReplace(toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceGrpcRequest request,
        io.grpc.stub.StreamObserver<toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceGrpcReply> responseObserver) {
      io.grpc.stub.ServerCalls.asyncUnimplementedUnaryCall(getJsonReplaceMethod(), responseObserver);
    }

    /**
     */
    public void htmlReplace(toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceGrpcRequest request,
        io.grpc.stub.StreamObserver<toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceGrpcReply> responseObserver) {
      io.grpc.stub.ServerCalls.asyncUnimplementedUnaryCall(getHtmlReplaceMethod(), responseObserver);
    }

    /**
     */
    public void markdownReplace(toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceGrpcRequest request,
        io.grpc.stub.StreamObserver<toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceGrpcReply> responseObserver) {
      io.grpc.stub.ServerCalls.asyncUnimplementedUnaryCall(getMarkdownReplaceMethod(), responseObserver);
    }

    /**
     */
    public void textFilterAsync(toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllAsyncGrpcRequest request,
        io.grpc.stub.StreamObserver<toolgood.textfilter.api.GrpcBase.TextFilter.TextFilterRequestIdGrpcReply> responseObserver) {
      io.grpc.stub.ServerCalls.asyncUnimplementedUnaryCall(getTextFilterAsyncMethod(), responseObserver);
    }

    /**
     */
    public void jsonFilterAsync(toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllAsyncGrpcRequest request,
        io.grpc.stub.StreamObserver<toolgood.textfilter.api.GrpcBase.TextFilter.TextFilterRequestIdGrpcReply> responseObserver) {
      io.grpc.stub.ServerCalls.asyncUnimplementedUnaryCall(getJsonFilterAsyncMethod(), responseObserver);
    }

    /**
     */
    public void htmlFilterAsync(toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllAsyncGrpcRequest request,
        io.grpc.stub.StreamObserver<toolgood.textfilter.api.GrpcBase.TextFilter.TextFilterRequestIdGrpcReply> responseObserver) {
      io.grpc.stub.ServerCalls.asyncUnimplementedUnaryCall(getHtmlFilterAsyncMethod(), responseObserver);
    }

    /**
     */
    public void markdownFilterAsync(toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllAsyncGrpcRequest request,
        io.grpc.stub.StreamObserver<toolgood.textfilter.api.GrpcBase.TextFilter.TextFilterRequestIdGrpcReply> responseObserver) {
      io.grpc.stub.ServerCalls.asyncUnimplementedUnaryCall(getMarkdownFilterAsyncMethod(), responseObserver);
    }

    /**
     */
    public void textReplaceAsync(toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceAsyncGrpcRequest request,
        io.grpc.stub.StreamObserver<toolgood.textfilter.api.GrpcBase.TextFilter.TextFilterRequestIdGrpcReply> responseObserver) {
      io.grpc.stub.ServerCalls.asyncUnimplementedUnaryCall(getTextReplaceAsyncMethod(), responseObserver);
    }

    /**
     */
    public void jsonReplaceAsync(toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceAsyncGrpcRequest request,
        io.grpc.stub.StreamObserver<toolgood.textfilter.api.GrpcBase.TextFilter.TextFilterRequestIdGrpcReply> responseObserver) {
      io.grpc.stub.ServerCalls.asyncUnimplementedUnaryCall(getJsonReplaceAsyncMethod(), responseObserver);
    }

    /**
     */
    public void htmlReplaceAsync(toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceAsyncGrpcRequest request,
        io.grpc.stub.StreamObserver<toolgood.textfilter.api.GrpcBase.TextFilter.TextFilterRequestIdGrpcReply> responseObserver) {
      io.grpc.stub.ServerCalls.asyncUnimplementedUnaryCall(getHtmlReplaceAsyncMethod(), responseObserver);
    }

    /**
     */
    public void markdownReplaceAsync(toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceAsyncGrpcRequest request,
        io.grpc.stub.StreamObserver<toolgood.textfilter.api.GrpcBase.TextFilter.TextFilterRequestIdGrpcReply> responseObserver) {
      io.grpc.stub.ServerCalls.asyncUnimplementedUnaryCall(getMarkdownReplaceAsyncMethod(), responseObserver);
    }

    @java.lang.Override public final io.grpc.ServerServiceDefinition bindService() {
      return io.grpc.ServerServiceDefinition.builder(getServiceDescriptor())
          .addMethod(
            getTextFilterMethod(),
            io.grpc.stub.ServerCalls.asyncUnaryCall(
              new MethodHandlers<
                toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllGrpcRequest,
                toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllGrpcReply>(
                  this, METHODID_TEXT_FILTER)))
          .addMethod(
            getJsonFilterMethod(),
            io.grpc.stub.ServerCalls.asyncUnaryCall(
              new MethodHandlers<
                toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllGrpcRequest,
                toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllGrpcReply>(
                  this, METHODID_JSON_FILTER)))
          .addMethod(
            getHtmlFilterMethod(),
            io.grpc.stub.ServerCalls.asyncUnaryCall(
              new MethodHandlers<
                toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllGrpcRequest,
                toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllGrpcReply>(
                  this, METHODID_HTML_FILTER)))
          .addMethod(
            getMarkdownFilterMethod(),
            io.grpc.stub.ServerCalls.asyncUnaryCall(
              new MethodHandlers<
                toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllGrpcRequest,
                toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllGrpcReply>(
                  this, METHODID_MARKDOWN_FILTER)))
          .addMethod(
            getTextReplaceMethod(),
            io.grpc.stub.ServerCalls.asyncUnaryCall(
              new MethodHandlers<
                toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceGrpcRequest,
                toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceGrpcReply>(
                  this, METHODID_TEXT_REPLACE)))
          .addMethod(
            getJsonReplaceMethod(),
            io.grpc.stub.ServerCalls.asyncUnaryCall(
              new MethodHandlers<
                toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceGrpcRequest,
                toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceGrpcReply>(
                  this, METHODID_JSON_REPLACE)))
          .addMethod(
            getHtmlReplaceMethod(),
            io.grpc.stub.ServerCalls.asyncUnaryCall(
              new MethodHandlers<
                toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceGrpcRequest,
                toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceGrpcReply>(
                  this, METHODID_HTML_REPLACE)))
          .addMethod(
            getMarkdownReplaceMethod(),
            io.grpc.stub.ServerCalls.asyncUnaryCall(
              new MethodHandlers<
                toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceGrpcRequest,
                toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceGrpcReply>(
                  this, METHODID_MARKDOWN_REPLACE)))
          .addMethod(
            getTextFilterAsyncMethod(),
            io.grpc.stub.ServerCalls.asyncUnaryCall(
              new MethodHandlers<
                toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllAsyncGrpcRequest,
                toolgood.textfilter.api.GrpcBase.TextFilter.TextFilterRequestIdGrpcReply>(
                  this, METHODID_TEXT_FILTER_ASYNC)))
          .addMethod(
            getJsonFilterAsyncMethod(),
            io.grpc.stub.ServerCalls.asyncUnaryCall(
              new MethodHandlers<
                toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllAsyncGrpcRequest,
                toolgood.textfilter.api.GrpcBase.TextFilter.TextFilterRequestIdGrpcReply>(
                  this, METHODID_JSON_FILTER_ASYNC)))
          .addMethod(
            getHtmlFilterAsyncMethod(),
            io.grpc.stub.ServerCalls.asyncUnaryCall(
              new MethodHandlers<
                toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllAsyncGrpcRequest,
                toolgood.textfilter.api.GrpcBase.TextFilter.TextFilterRequestIdGrpcReply>(
                  this, METHODID_HTML_FILTER_ASYNC)))
          .addMethod(
            getMarkdownFilterAsyncMethod(),
            io.grpc.stub.ServerCalls.asyncUnaryCall(
              new MethodHandlers<
                toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllAsyncGrpcRequest,
                toolgood.textfilter.api.GrpcBase.TextFilter.TextFilterRequestIdGrpcReply>(
                  this, METHODID_MARKDOWN_FILTER_ASYNC)))
          .addMethod(
            getTextReplaceAsyncMethod(),
            io.grpc.stub.ServerCalls.asyncUnaryCall(
              new MethodHandlers<
                toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceAsyncGrpcRequest,
                toolgood.textfilter.api.GrpcBase.TextFilter.TextFilterRequestIdGrpcReply>(
                  this, METHODID_TEXT_REPLACE_ASYNC)))
          .addMethod(
            getJsonReplaceAsyncMethod(),
            io.grpc.stub.ServerCalls.asyncUnaryCall(
              new MethodHandlers<
                toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceAsyncGrpcRequest,
                toolgood.textfilter.api.GrpcBase.TextFilter.TextFilterRequestIdGrpcReply>(
                  this, METHODID_JSON_REPLACE_ASYNC)))
          .addMethod(
            getHtmlReplaceAsyncMethod(),
            io.grpc.stub.ServerCalls.asyncUnaryCall(
              new MethodHandlers<
                toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceAsyncGrpcRequest,
                toolgood.textfilter.api.GrpcBase.TextFilter.TextFilterRequestIdGrpcReply>(
                  this, METHODID_HTML_REPLACE_ASYNC)))
          .addMethod(
            getMarkdownReplaceAsyncMethod(),
            io.grpc.stub.ServerCalls.asyncUnaryCall(
              new MethodHandlers<
                toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceAsyncGrpcRequest,
                toolgood.textfilter.api.GrpcBase.TextFilter.TextFilterRequestIdGrpcReply>(
                  this, METHODID_MARKDOWN_REPLACE_ASYNC)))
          .build();
    }
  }

  /**
   * <pre>
   * The greeting service definition.
   * </pre>
   */
  public static final class TextFilterGrpcStub extends io.grpc.stub.AbstractAsyncStub<TextFilterGrpcStub> {
    private TextFilterGrpcStub(
        io.grpc.Channel channel, io.grpc.CallOptions callOptions) {
      super(channel, callOptions);
    }

    @java.lang.Override
    protected TextFilterGrpcStub build(
        io.grpc.Channel channel, io.grpc.CallOptions callOptions) {
      return new TextFilterGrpcStub(channel, callOptions);
    }

    /**
     */
    public void textFilter(toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllGrpcRequest request,
        io.grpc.stub.StreamObserver<toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllGrpcReply> responseObserver) {
      io.grpc.stub.ClientCalls.asyncUnaryCall(
          getChannel().newCall(getTextFilterMethod(), getCallOptions()), request, responseObserver);
    }

    /**
     */
    public void jsonFilter(toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllGrpcRequest request,
        io.grpc.stub.StreamObserver<toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllGrpcReply> responseObserver) {
      io.grpc.stub.ClientCalls.asyncUnaryCall(
          getChannel().newCall(getJsonFilterMethod(), getCallOptions()), request, responseObserver);
    }

    /**
     */
    public void htmlFilter(toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllGrpcRequest request,
        io.grpc.stub.StreamObserver<toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllGrpcReply> responseObserver) {
      io.grpc.stub.ClientCalls.asyncUnaryCall(
          getChannel().newCall(getHtmlFilterMethod(), getCallOptions()), request, responseObserver);
    }

    /**
     */
    public void markdownFilter(toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllGrpcRequest request,
        io.grpc.stub.StreamObserver<toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllGrpcReply> responseObserver) {
      io.grpc.stub.ClientCalls.asyncUnaryCall(
          getChannel().newCall(getMarkdownFilterMethod(), getCallOptions()), request, responseObserver);
    }

    /**
     */
    public void textReplace(toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceGrpcRequest request,
        io.grpc.stub.StreamObserver<toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceGrpcReply> responseObserver) {
      io.grpc.stub.ClientCalls.asyncUnaryCall(
          getChannel().newCall(getTextReplaceMethod(), getCallOptions()), request, responseObserver);
    }

    /**
     */
    public void jsonReplace(toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceGrpcRequest request,
        io.grpc.stub.StreamObserver<toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceGrpcReply> responseObserver) {
      io.grpc.stub.ClientCalls.asyncUnaryCall(
          getChannel().newCall(getJsonReplaceMethod(), getCallOptions()), request, responseObserver);
    }

    /**
     */
    public void htmlReplace(toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceGrpcRequest request,
        io.grpc.stub.StreamObserver<toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceGrpcReply> responseObserver) {
      io.grpc.stub.ClientCalls.asyncUnaryCall(
          getChannel().newCall(getHtmlReplaceMethod(), getCallOptions()), request, responseObserver);
    }

    /**
     */
    public void markdownReplace(toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceGrpcRequest request,
        io.grpc.stub.StreamObserver<toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceGrpcReply> responseObserver) {
      io.grpc.stub.ClientCalls.asyncUnaryCall(
          getChannel().newCall(getMarkdownReplaceMethod(), getCallOptions()), request, responseObserver);
    }

    /**
     */
    public void textFilterAsync(toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllAsyncGrpcRequest request,
        io.grpc.stub.StreamObserver<toolgood.textfilter.api.GrpcBase.TextFilter.TextFilterRequestIdGrpcReply> responseObserver) {
      io.grpc.stub.ClientCalls.asyncUnaryCall(
          getChannel().newCall(getTextFilterAsyncMethod(), getCallOptions()), request, responseObserver);
    }

    /**
     */
    public void jsonFilterAsync(toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllAsyncGrpcRequest request,
        io.grpc.stub.StreamObserver<toolgood.textfilter.api.GrpcBase.TextFilter.TextFilterRequestIdGrpcReply> responseObserver) {
      io.grpc.stub.ClientCalls.asyncUnaryCall(
          getChannel().newCall(getJsonFilterAsyncMethod(), getCallOptions()), request, responseObserver);
    }

    /**
     */
    public void htmlFilterAsync(toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllAsyncGrpcRequest request,
        io.grpc.stub.StreamObserver<toolgood.textfilter.api.GrpcBase.TextFilter.TextFilterRequestIdGrpcReply> responseObserver) {
      io.grpc.stub.ClientCalls.asyncUnaryCall(
          getChannel().newCall(getHtmlFilterAsyncMethod(), getCallOptions()), request, responseObserver);
    }

    /**
     */
    public void markdownFilterAsync(toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllAsyncGrpcRequest request,
        io.grpc.stub.StreamObserver<toolgood.textfilter.api.GrpcBase.TextFilter.TextFilterRequestIdGrpcReply> responseObserver) {
      io.grpc.stub.ClientCalls.asyncUnaryCall(
          getChannel().newCall(getMarkdownFilterAsyncMethod(), getCallOptions()), request, responseObserver);
    }

    /**
     */
    public void textReplaceAsync(toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceAsyncGrpcRequest request,
        io.grpc.stub.StreamObserver<toolgood.textfilter.api.GrpcBase.TextFilter.TextFilterRequestIdGrpcReply> responseObserver) {
      io.grpc.stub.ClientCalls.asyncUnaryCall(
          getChannel().newCall(getTextReplaceAsyncMethod(), getCallOptions()), request, responseObserver);
    }

    /**
     */
    public void jsonReplaceAsync(toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceAsyncGrpcRequest request,
        io.grpc.stub.StreamObserver<toolgood.textfilter.api.GrpcBase.TextFilter.TextFilterRequestIdGrpcReply> responseObserver) {
      io.grpc.stub.ClientCalls.asyncUnaryCall(
          getChannel().newCall(getJsonReplaceAsyncMethod(), getCallOptions()), request, responseObserver);
    }

    /**
     */
    public void htmlReplaceAsync(toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceAsyncGrpcRequest request,
        io.grpc.stub.StreamObserver<toolgood.textfilter.api.GrpcBase.TextFilter.TextFilterRequestIdGrpcReply> responseObserver) {
      io.grpc.stub.ClientCalls.asyncUnaryCall(
          getChannel().newCall(getHtmlReplaceAsyncMethod(), getCallOptions()), request, responseObserver);
    }

    /**
     */
    public void markdownReplaceAsync(toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceAsyncGrpcRequest request,
        io.grpc.stub.StreamObserver<toolgood.textfilter.api.GrpcBase.TextFilter.TextFilterRequestIdGrpcReply> responseObserver) {
      io.grpc.stub.ClientCalls.asyncUnaryCall(
          getChannel().newCall(getMarkdownReplaceAsyncMethod(), getCallOptions()), request, responseObserver);
    }
  }

  /**
   * <pre>
   * The greeting service definition.
   * </pre>
   */
  public static final class TextFilterGrpcBlockingStub extends io.grpc.stub.AbstractBlockingStub<TextFilterGrpcBlockingStub> {
    private TextFilterGrpcBlockingStub(
        io.grpc.Channel channel, io.grpc.CallOptions callOptions) {
      super(channel, callOptions);
    }

    @java.lang.Override
    protected TextFilterGrpcBlockingStub build(
        io.grpc.Channel channel, io.grpc.CallOptions callOptions) {
      return new TextFilterGrpcBlockingStub(channel, callOptions);
    }

    /**
     */
    public toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllGrpcReply textFilter(toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllGrpcRequest request) {
      return io.grpc.stub.ClientCalls.blockingUnaryCall(
          getChannel(), getTextFilterMethod(), getCallOptions(), request);
    }

    /**
     */
    public toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllGrpcReply jsonFilter(toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllGrpcRequest request) {
      return io.grpc.stub.ClientCalls.blockingUnaryCall(
          getChannel(), getJsonFilterMethod(), getCallOptions(), request);
    }

    /**
     */
    public toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllGrpcReply htmlFilter(toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllGrpcRequest request) {
      return io.grpc.stub.ClientCalls.blockingUnaryCall(
          getChannel(), getHtmlFilterMethod(), getCallOptions(), request);
    }

    /**
     */
    public toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllGrpcReply markdownFilter(toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllGrpcRequest request) {
      return io.grpc.stub.ClientCalls.blockingUnaryCall(
          getChannel(), getMarkdownFilterMethod(), getCallOptions(), request);
    }

    /**
     */
    public toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceGrpcReply textReplace(toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceGrpcRequest request) {
      return io.grpc.stub.ClientCalls.blockingUnaryCall(
          getChannel(), getTextReplaceMethod(), getCallOptions(), request);
    }

    /**
     */
    public toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceGrpcReply jsonReplace(toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceGrpcRequest request) {
      return io.grpc.stub.ClientCalls.blockingUnaryCall(
          getChannel(), getJsonReplaceMethod(), getCallOptions(), request);
    }

    /**
     */
    public toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceGrpcReply htmlReplace(toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceGrpcRequest request) {
      return io.grpc.stub.ClientCalls.blockingUnaryCall(
          getChannel(), getHtmlReplaceMethod(), getCallOptions(), request);
    }

    /**
     */
    public toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceGrpcReply markdownReplace(toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceGrpcRequest request) {
      return io.grpc.stub.ClientCalls.blockingUnaryCall(
          getChannel(), getMarkdownReplaceMethod(), getCallOptions(), request);
    }

    /**
     */
    public toolgood.textfilter.api.GrpcBase.TextFilter.TextFilterRequestIdGrpcReply textFilterAsync(toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllAsyncGrpcRequest request) {
      return io.grpc.stub.ClientCalls.blockingUnaryCall(
          getChannel(), getTextFilterAsyncMethod(), getCallOptions(), request);
    }

    /**
     */
    public toolgood.textfilter.api.GrpcBase.TextFilter.TextFilterRequestIdGrpcReply jsonFilterAsync(toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllAsyncGrpcRequest request) {
      return io.grpc.stub.ClientCalls.blockingUnaryCall(
          getChannel(), getJsonFilterAsyncMethod(), getCallOptions(), request);
    }

    /**
     */
    public toolgood.textfilter.api.GrpcBase.TextFilter.TextFilterRequestIdGrpcReply htmlFilterAsync(toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllAsyncGrpcRequest request) {
      return io.grpc.stub.ClientCalls.blockingUnaryCall(
          getChannel(), getHtmlFilterAsyncMethod(), getCallOptions(), request);
    }

    /**
     */
    public toolgood.textfilter.api.GrpcBase.TextFilter.TextFilterRequestIdGrpcReply markdownFilterAsync(toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllAsyncGrpcRequest request) {
      return io.grpc.stub.ClientCalls.blockingUnaryCall(
          getChannel(), getMarkdownFilterAsyncMethod(), getCallOptions(), request);
    }

    /**
     */
    public toolgood.textfilter.api.GrpcBase.TextFilter.TextFilterRequestIdGrpcReply textReplaceAsync(toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceAsyncGrpcRequest request) {
      return io.grpc.stub.ClientCalls.blockingUnaryCall(
          getChannel(), getTextReplaceAsyncMethod(), getCallOptions(), request);
    }

    /**
     */
    public toolgood.textfilter.api.GrpcBase.TextFilter.TextFilterRequestIdGrpcReply jsonReplaceAsync(toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceAsyncGrpcRequest request) {
      return io.grpc.stub.ClientCalls.blockingUnaryCall(
          getChannel(), getJsonReplaceAsyncMethod(), getCallOptions(), request);
    }

    /**
     */
    public toolgood.textfilter.api.GrpcBase.TextFilter.TextFilterRequestIdGrpcReply htmlReplaceAsync(toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceAsyncGrpcRequest request) {
      return io.grpc.stub.ClientCalls.blockingUnaryCall(
          getChannel(), getHtmlReplaceAsyncMethod(), getCallOptions(), request);
    }

    /**
     */
    public toolgood.textfilter.api.GrpcBase.TextFilter.TextFilterRequestIdGrpcReply markdownReplaceAsync(toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceAsyncGrpcRequest request) {
      return io.grpc.stub.ClientCalls.blockingUnaryCall(
          getChannel(), getMarkdownReplaceAsyncMethod(), getCallOptions(), request);
    }
  }

  /**
   * <pre>
   * The greeting service definition.
   * </pre>
   */
  public static final class TextFilterGrpcFutureStub extends io.grpc.stub.AbstractFutureStub<TextFilterGrpcFutureStub> {
    private TextFilterGrpcFutureStub(
        io.grpc.Channel channel, io.grpc.CallOptions callOptions) {
      super(channel, callOptions);
    }

    @java.lang.Override
    protected TextFilterGrpcFutureStub build(
        io.grpc.Channel channel, io.grpc.CallOptions callOptions) {
      return new TextFilterGrpcFutureStub(channel, callOptions);
    }

    /**
     */
    public com.google.common.util.concurrent.ListenableFuture<toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllGrpcReply> textFilter(
        toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllGrpcRequest request) {
      return io.grpc.stub.ClientCalls.futureUnaryCall(
          getChannel().newCall(getTextFilterMethod(), getCallOptions()), request);
    }

    /**
     */
    public com.google.common.util.concurrent.ListenableFuture<toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllGrpcReply> jsonFilter(
        toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllGrpcRequest request) {
      return io.grpc.stub.ClientCalls.futureUnaryCall(
          getChannel().newCall(getJsonFilterMethod(), getCallOptions()), request);
    }

    /**
     */
    public com.google.common.util.concurrent.ListenableFuture<toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllGrpcReply> htmlFilter(
        toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllGrpcRequest request) {
      return io.grpc.stub.ClientCalls.futureUnaryCall(
          getChannel().newCall(getHtmlFilterMethod(), getCallOptions()), request);
    }

    /**
     */
    public com.google.common.util.concurrent.ListenableFuture<toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllGrpcReply> markdownFilter(
        toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllGrpcRequest request) {
      return io.grpc.stub.ClientCalls.futureUnaryCall(
          getChannel().newCall(getMarkdownFilterMethod(), getCallOptions()), request);
    }

    /**
     */
    public com.google.common.util.concurrent.ListenableFuture<toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceGrpcReply> textReplace(
        toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceGrpcRequest request) {
      return io.grpc.stub.ClientCalls.futureUnaryCall(
          getChannel().newCall(getTextReplaceMethod(), getCallOptions()), request);
    }

    /**
     */
    public com.google.common.util.concurrent.ListenableFuture<toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceGrpcReply> jsonReplace(
        toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceGrpcRequest request) {
      return io.grpc.stub.ClientCalls.futureUnaryCall(
          getChannel().newCall(getJsonReplaceMethod(), getCallOptions()), request);
    }

    /**
     */
    public com.google.common.util.concurrent.ListenableFuture<toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceGrpcReply> htmlReplace(
        toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceGrpcRequest request) {
      return io.grpc.stub.ClientCalls.futureUnaryCall(
          getChannel().newCall(getHtmlReplaceMethod(), getCallOptions()), request);
    }

    /**
     */
    public com.google.common.util.concurrent.ListenableFuture<toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceGrpcReply> markdownReplace(
        toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceGrpcRequest request) {
      return io.grpc.stub.ClientCalls.futureUnaryCall(
          getChannel().newCall(getMarkdownReplaceMethod(), getCallOptions()), request);
    }

    /**
     */
    public com.google.common.util.concurrent.ListenableFuture<toolgood.textfilter.api.GrpcBase.TextFilter.TextFilterRequestIdGrpcReply> textFilterAsync(
        toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllAsyncGrpcRequest request) {
      return io.grpc.stub.ClientCalls.futureUnaryCall(
          getChannel().newCall(getTextFilterAsyncMethod(), getCallOptions()), request);
    }

    /**
     */
    public com.google.common.util.concurrent.ListenableFuture<toolgood.textfilter.api.GrpcBase.TextFilter.TextFilterRequestIdGrpcReply> jsonFilterAsync(
        toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllAsyncGrpcRequest request) {
      return io.grpc.stub.ClientCalls.futureUnaryCall(
          getChannel().newCall(getJsonFilterAsyncMethod(), getCallOptions()), request);
    }

    /**
     */
    public com.google.common.util.concurrent.ListenableFuture<toolgood.textfilter.api.GrpcBase.TextFilter.TextFilterRequestIdGrpcReply> htmlFilterAsync(
        toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllAsyncGrpcRequest request) {
      return io.grpc.stub.ClientCalls.futureUnaryCall(
          getChannel().newCall(getHtmlFilterAsyncMethod(), getCallOptions()), request);
    }

    /**
     */
    public com.google.common.util.concurrent.ListenableFuture<toolgood.textfilter.api.GrpcBase.TextFilter.TextFilterRequestIdGrpcReply> markdownFilterAsync(
        toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllAsyncGrpcRequest request) {
      return io.grpc.stub.ClientCalls.futureUnaryCall(
          getChannel().newCall(getMarkdownFilterAsyncMethod(), getCallOptions()), request);
    }

    /**
     */
    public com.google.common.util.concurrent.ListenableFuture<toolgood.textfilter.api.GrpcBase.TextFilter.TextFilterRequestIdGrpcReply> textReplaceAsync(
        toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceAsyncGrpcRequest request) {
      return io.grpc.stub.ClientCalls.futureUnaryCall(
          getChannel().newCall(getTextReplaceAsyncMethod(), getCallOptions()), request);
    }

    /**
     */
    public com.google.common.util.concurrent.ListenableFuture<toolgood.textfilter.api.GrpcBase.TextFilter.TextFilterRequestIdGrpcReply> jsonReplaceAsync(
        toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceAsyncGrpcRequest request) {
      return io.grpc.stub.ClientCalls.futureUnaryCall(
          getChannel().newCall(getJsonReplaceAsyncMethod(), getCallOptions()), request);
    }

    /**
     */
    public com.google.common.util.concurrent.ListenableFuture<toolgood.textfilter.api.GrpcBase.TextFilter.TextFilterRequestIdGrpcReply> htmlReplaceAsync(
        toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceAsyncGrpcRequest request) {
      return io.grpc.stub.ClientCalls.futureUnaryCall(
          getChannel().newCall(getHtmlReplaceAsyncMethod(), getCallOptions()), request);
    }

    /**
     */
    public com.google.common.util.concurrent.ListenableFuture<toolgood.textfilter.api.GrpcBase.TextFilter.TextFilterRequestIdGrpcReply> markdownReplaceAsync(
        toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceAsyncGrpcRequest request) {
      return io.grpc.stub.ClientCalls.futureUnaryCall(
          getChannel().newCall(getMarkdownReplaceAsyncMethod(), getCallOptions()), request);
    }
  }

  private static final int METHODID_TEXT_FILTER = 0;
  private static final int METHODID_JSON_FILTER = 1;
  private static final int METHODID_HTML_FILTER = 2;
  private static final int METHODID_MARKDOWN_FILTER = 3;
  private static final int METHODID_TEXT_REPLACE = 4;
  private static final int METHODID_JSON_REPLACE = 5;
  private static final int METHODID_HTML_REPLACE = 6;
  private static final int METHODID_MARKDOWN_REPLACE = 7;
  private static final int METHODID_TEXT_FILTER_ASYNC = 8;
  private static final int METHODID_JSON_FILTER_ASYNC = 9;
  private static final int METHODID_HTML_FILTER_ASYNC = 10;
  private static final int METHODID_MARKDOWN_FILTER_ASYNC = 11;
  private static final int METHODID_TEXT_REPLACE_ASYNC = 12;
  private static final int METHODID_JSON_REPLACE_ASYNC = 13;
  private static final int METHODID_HTML_REPLACE_ASYNC = 14;
  private static final int METHODID_MARKDOWN_REPLACE_ASYNC = 15;

  private static final class MethodHandlers<Req, Resp> implements
      io.grpc.stub.ServerCalls.UnaryMethod<Req, Resp>,
      io.grpc.stub.ServerCalls.ServerStreamingMethod<Req, Resp>,
      io.grpc.stub.ServerCalls.ClientStreamingMethod<Req, Resp>,
      io.grpc.stub.ServerCalls.BidiStreamingMethod<Req, Resp> {
    private final TextFilterGrpcImplBase serviceImpl;
    private final int methodId;

    MethodHandlers(TextFilterGrpcImplBase serviceImpl, int methodId) {
      this.serviceImpl = serviceImpl;
      this.methodId = methodId;
    }

    @java.lang.Override
    @java.lang.SuppressWarnings("unchecked")
    public void invoke(Req request, io.grpc.stub.StreamObserver<Resp> responseObserver) {
      switch (methodId) {
        case METHODID_TEXT_FILTER:
          serviceImpl.textFilter((toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllGrpcRequest) request,
              (io.grpc.stub.StreamObserver<toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllGrpcReply>) responseObserver);
          break;
        case METHODID_JSON_FILTER:
          serviceImpl.jsonFilter((toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllGrpcRequest) request,
              (io.grpc.stub.StreamObserver<toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllGrpcReply>) responseObserver);
          break;
        case METHODID_HTML_FILTER:
          serviceImpl.htmlFilter((toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllGrpcRequest) request,
              (io.grpc.stub.StreamObserver<toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllGrpcReply>) responseObserver);
          break;
        case METHODID_MARKDOWN_FILTER:
          serviceImpl.markdownFilter((toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllGrpcRequest) request,
              (io.grpc.stub.StreamObserver<toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllGrpcReply>) responseObserver);
          break;
        case METHODID_TEXT_REPLACE:
          serviceImpl.textReplace((toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceGrpcRequest) request,
              (io.grpc.stub.StreamObserver<toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceGrpcReply>) responseObserver);
          break;
        case METHODID_JSON_REPLACE:
          serviceImpl.jsonReplace((toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceGrpcRequest) request,
              (io.grpc.stub.StreamObserver<toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceGrpcReply>) responseObserver);
          break;
        case METHODID_HTML_REPLACE:
          serviceImpl.htmlReplace((toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceGrpcRequest) request,
              (io.grpc.stub.StreamObserver<toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceGrpcReply>) responseObserver);
          break;
        case METHODID_MARKDOWN_REPLACE:
          serviceImpl.markdownReplace((toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceGrpcRequest) request,
              (io.grpc.stub.StreamObserver<toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceGrpcReply>) responseObserver);
          break;
        case METHODID_TEXT_FILTER_ASYNC:
          serviceImpl.textFilterAsync((toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllAsyncGrpcRequest) request,
              (io.grpc.stub.StreamObserver<toolgood.textfilter.api.GrpcBase.TextFilter.TextFilterRequestIdGrpcReply>) responseObserver);
          break;
        case METHODID_JSON_FILTER_ASYNC:
          serviceImpl.jsonFilterAsync((toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllAsyncGrpcRequest) request,
              (io.grpc.stub.StreamObserver<toolgood.textfilter.api.GrpcBase.TextFilter.TextFilterRequestIdGrpcReply>) responseObserver);
          break;
        case METHODID_HTML_FILTER_ASYNC:
          serviceImpl.htmlFilterAsync((toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllAsyncGrpcRequest) request,
              (io.grpc.stub.StreamObserver<toolgood.textfilter.api.GrpcBase.TextFilter.TextFilterRequestIdGrpcReply>) responseObserver);
          break;
        case METHODID_MARKDOWN_FILTER_ASYNC:
          serviceImpl.markdownFilterAsync((toolgood.textfilter.api.GrpcBase.TextFilter.TextFindAllAsyncGrpcRequest) request,
              (io.grpc.stub.StreamObserver<toolgood.textfilter.api.GrpcBase.TextFilter.TextFilterRequestIdGrpcReply>) responseObserver);
          break;
        case METHODID_TEXT_REPLACE_ASYNC:
          serviceImpl.textReplaceAsync((toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceAsyncGrpcRequest) request,
              (io.grpc.stub.StreamObserver<toolgood.textfilter.api.GrpcBase.TextFilter.TextFilterRequestIdGrpcReply>) responseObserver);
          break;
        case METHODID_JSON_REPLACE_ASYNC:
          serviceImpl.jsonReplaceAsync((toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceAsyncGrpcRequest) request,
              (io.grpc.stub.StreamObserver<toolgood.textfilter.api.GrpcBase.TextFilter.TextFilterRequestIdGrpcReply>) responseObserver);
          break;
        case METHODID_HTML_REPLACE_ASYNC:
          serviceImpl.htmlReplaceAsync((toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceAsyncGrpcRequest) request,
              (io.grpc.stub.StreamObserver<toolgood.textfilter.api.GrpcBase.TextFilter.TextFilterRequestIdGrpcReply>) responseObserver);
          break;
        case METHODID_MARKDOWN_REPLACE_ASYNC:
          serviceImpl.markdownReplaceAsync((toolgood.textfilter.api.GrpcBase.TextFilter.TextReplaceAsyncGrpcRequest) request,
              (io.grpc.stub.StreamObserver<toolgood.textfilter.api.GrpcBase.TextFilter.TextFilterRequestIdGrpcReply>) responseObserver);
          break;
        default:
          throw new AssertionError();
      }
    }

    @java.lang.Override
    @java.lang.SuppressWarnings("unchecked")
    public io.grpc.stub.StreamObserver<Req> invoke(
        io.grpc.stub.StreamObserver<Resp> responseObserver) {
      switch (methodId) {
        default:
          throw new AssertionError();
      }
    }
  }

  private static abstract class TextFilterGrpcBaseDescriptorSupplier
      implements io.grpc.protobuf.ProtoFileDescriptorSupplier, io.grpc.protobuf.ProtoServiceDescriptorSupplier {
    TextFilterGrpcBaseDescriptorSupplier() {}

    @java.lang.Override
    public com.google.protobuf.Descriptors.FileDescriptor getFileDescriptor() {
      return toolgood.textfilter.api.GrpcBase.TextFilter.getDescriptor();
    }

    @java.lang.Override
    public com.google.protobuf.Descriptors.ServiceDescriptor getServiceDescriptor() {
      return getFileDescriptor().findServiceByName("TextFilterGrpc");
    }
  }

  private static final class TextFilterGrpcFileDescriptorSupplier
      extends TextFilterGrpcBaseDescriptorSupplier {
    TextFilterGrpcFileDescriptorSupplier() {}
  }

  private static final class TextFilterGrpcMethodDescriptorSupplier
      extends TextFilterGrpcBaseDescriptorSupplier
      implements io.grpc.protobuf.ProtoMethodDescriptorSupplier {
    private final String methodName;

    TextFilterGrpcMethodDescriptorSupplier(String methodName) {
      this.methodName = methodName;
    }

    @java.lang.Override
    public com.google.protobuf.Descriptors.MethodDescriptor getMethodDescriptor() {
      return getServiceDescriptor().findMethodByName(methodName);
    }
  }

  private static volatile io.grpc.ServiceDescriptor serviceDescriptor;

  public static io.grpc.ServiceDescriptor getServiceDescriptor() {
    io.grpc.ServiceDescriptor result = serviceDescriptor;
    if (result == null) {
      synchronized (TextFilterGrpcGrpc.class) {
        result = serviceDescriptor;
        if (result == null) {
          serviceDescriptor = result = io.grpc.ServiceDescriptor.newBuilder(SERVICE_NAME)
              .setSchemaDescriptor(new TextFilterGrpcFileDescriptorSupplier())
              .addMethod(getTextFilterMethod())
              .addMethod(getJsonFilterMethod())
              .addMethod(getHtmlFilterMethod())
              .addMethod(getMarkdownFilterMethod())
              .addMethod(getTextReplaceMethod())
              .addMethod(getJsonReplaceMethod())
              .addMethod(getHtmlReplaceMethod())
              .addMethod(getMarkdownReplaceMethod())
              .addMethod(getTextFilterAsyncMethod())
              .addMethod(getJsonFilterAsyncMethod())
              .addMethod(getHtmlFilterAsyncMethod())
              .addMethod(getMarkdownFilterAsyncMethod())
              .addMethod(getTextReplaceAsyncMethod())
              .addMethod(getJsonReplaceAsyncMethod())
              .addMethod(getHtmlReplaceAsyncMethod())
              .addMethod(getMarkdownReplaceAsyncMethod())
              .build();
        }
      }
    }
    return result;
  }
}
