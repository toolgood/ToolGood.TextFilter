package Grpcs

import (
	"log"

	pb "./GrpcBase"
	"golang.org/x/net/context"
	"google.golang.org/grpc"

	. "../Datas"
	. "../Datas/Requests"
)

type TextFilterGrpcAsyncProvider struct {
	grpcHost string
}

func NewTextFilterGrpcAsyncProvider(grpcHost string) *TextFilterGrpcAsyncProvider {
	result := &TextFilterGrpcAsyncProvider{}
	result.grpcHost = grpcHost
	return result
}

func (this *TextFilterGrpcAsyncProvider) TextFilter(request *TextFilterAsyncRequest) *CommonResult {
	conn, err := grpc.Dial(this.grpcHost, grpc.WithInsecure())
	if err != nil {
		log.Fatalf("did not connect: %v", err)
	}
	t := pb.NewTextFilterGrpcClient(conn)

	reqBody := new(pb.TextFindAllAsyncGrpcRequest)
	reqBody.Txt = request.Txt
	reqBody.SkipBidi = request.SkipBidi
	reqBody.OnlyPosition = request.OnlyPosition

	tr, err := t.TextFilterAsync(context.Background(), reqBody)
	if err != nil {
		log.Fatalf("did not connect: %v", err)
		return nil
	}
	result := this.createCommonResult(tr)
	defer conn.Close()
	return result
}
func (this *TextFilterGrpcAsyncProvider) HtmlFilter(request *TextFilterAsyncRequest) *CommonResult {
	conn, err := grpc.Dial(this.grpcHost, grpc.WithInsecure())
	if err != nil {
		log.Fatalf("did not connect: %v", err)
	}
	t := pb.NewTextFilterGrpcClient(conn)

	reqBody := new(pb.TextFindAllAsyncGrpcRequest)
	reqBody.Txt = request.Txt
	reqBody.SkipBidi = request.SkipBidi
	reqBody.OnlyPosition = request.OnlyPosition

	tr, err := t.HtmlFilterAsync(context.Background(), reqBody)
	if err != nil {
		log.Fatalf("did not connect: %v", err)
		return nil
	}
	result := this.createCommonResult(tr)
	defer conn.Close()
	return result
}
func (this *TextFilterGrpcAsyncProvider) JsonFilter(request *TextFilterAsyncRequest) *CommonResult {
	conn, err := grpc.Dial(this.grpcHost, grpc.WithInsecure())
	if err != nil {
		log.Fatalf("did not connect: %v", err)
	}
	t := pb.NewTextFilterGrpcClient(conn)

	reqBody := new(pb.TextFindAllAsyncGrpcRequest)
	reqBody.Txt = request.Txt
	reqBody.SkipBidi = request.SkipBidi
	reqBody.OnlyPosition = request.OnlyPosition

	tr, err := t.JsonFilterAsync(context.Background(), reqBody)
	if err != nil {
		log.Fatalf("did not connect: %v", err)
		return nil
	}
	result := this.createCommonResult(tr)
	defer conn.Close()
	return result
}
func (this *TextFilterGrpcAsyncProvider) MarkdownFilter(request *TextFilterAsyncRequest) *CommonResult {
	conn, err := grpc.Dial(this.grpcHost, grpc.WithInsecure())
	if err != nil {
		log.Fatalf("did not connect: %v", err)
	}
	t := pb.NewTextFilterGrpcClient(conn)

	reqBody := new(pb.TextFindAllAsyncGrpcRequest)
	reqBody.Txt = request.Txt
	reqBody.SkipBidi = request.SkipBidi
	reqBody.OnlyPosition = request.OnlyPosition

	tr, err := t.MarkdownFilterAsync(context.Background(), reqBody)
	if err != nil {
		log.Fatalf("did not connect: %v", err)
		return nil
	}
	result := this.createCommonResult(tr)
	defer conn.Close()
	return result
}

func (this *TextFilterGrpcAsyncProvider) TextReplace(request *TextReplaceRequest) *CommonResult {
	conn, err := grpc.Dial(this.grpcHost, grpc.WithInsecure())
	if err != nil {
		log.Fatalf("did not connect: %v", err)
	}
	t := pb.NewTextFilterGrpcClient(conn)

	reqBody := new(pb.TextReplaceAsyncGrpcRequest)
	reqBody.Txt = request.Txt
	reqBody.ReplaceChar = 42 //*
	if len(request.ReplaceChar) > 0 {
		byteArray := []rune(request.ReplaceChar)
		reqBody.ReplaceChar = uint32(byteArray[0])
	}
	reqBody.ReviewReplace = request.ReviewReplace
	reqBody.ContactReplace = request.ContactReplace
	reqBody.SkipBidi = request.SkipBidi
	reqBody.OnlyPosition = request.OnlyPosition

	tr, err := t.TextReplaceAsync(context.Background(), reqBody)
	if err != nil {
		log.Fatalf("did not connect: %v", err)
		return nil
	}
	result := this.createCommonResult(tr)
	defer conn.Close()
	return result
}

func (this *TextFilterGrpcAsyncProvider) HtmlReplace(request *TextReplaceRequest) *CommonResult {
	conn, err := grpc.Dial(this.grpcHost, grpc.WithInsecure())
	if err != nil {
		log.Fatalf("did not connect: %v", err)
	}
	t := pb.NewTextFilterGrpcClient(conn)

	reqBody := new(pb.TextReplaceAsyncGrpcRequest)
	reqBody.Txt = request.Txt
	reqBody.ReplaceChar = 42 //*
	if len(request.ReplaceChar) > 0 {
		byteArray := []rune(request.ReplaceChar)
		reqBody.ReplaceChar = uint32(byteArray[0])
	}
	reqBody.ReviewReplace = request.ReviewReplace
	reqBody.ContactReplace = request.ContactReplace
	reqBody.SkipBidi = request.SkipBidi
	reqBody.OnlyPosition = request.OnlyPosition

	tr, err := t.HtmlReplaceAsync(context.Background(), reqBody)
	if err != nil {
		log.Fatalf("did not connect: %v", err)
		return nil
	}
	result := this.createCommonResult(tr)
	defer conn.Close()
	return result
}

func (this *TextFilterGrpcAsyncProvider) JsonReplace(request *TextReplaceRequest) *CommonResult {
	conn, err := grpc.Dial(this.grpcHost, grpc.WithInsecure())
	if err != nil {
		log.Fatalf("did not connect: %v", err)
	}
	t := pb.NewTextFilterGrpcClient(conn)

	reqBody := new(pb.TextReplaceAsyncGrpcRequest)
	reqBody.Txt = request.Txt
	reqBody.ReplaceChar = 42 //*
	if len(request.ReplaceChar) > 0 {
		byteArray := []rune(request.ReplaceChar)
		reqBody.ReplaceChar = uint32(byteArray[0])
	}
	reqBody.ReviewReplace = request.ReviewReplace
	reqBody.ContactReplace = request.ContactReplace
	reqBody.SkipBidi = request.SkipBidi
	reqBody.OnlyPosition = request.OnlyPosition

	tr, err := t.JsonReplaceAsync(context.Background(), reqBody)
	if err != nil {
		log.Fatalf("did not connect: %v", err)
		return nil
	}
	result := this.createCommonResult(tr)
	defer conn.Close()
	return result
}

func (this *TextFilterGrpcAsyncProvider) MarkdownReplace(request *TextReplaceRequest) *CommonResult {
	conn, err := grpc.Dial(this.grpcHost, grpc.WithInsecure())
	if err != nil {
		log.Fatalf("did not connect: %v", err)
	}
	t := pb.NewTextFilterGrpcClient(conn)

	reqBody := new(pb.TextReplaceAsyncGrpcRequest)
	reqBody.Txt = request.Txt
	reqBody.ReplaceChar = 42 //*
	if len(request.ReplaceChar) > 0 {
		byteArray := []rune(request.ReplaceChar)
		reqBody.ReplaceChar = uint32(byteArray[0])
	}
	reqBody.ReviewReplace = request.ReviewReplace
	reqBody.ContactReplace = request.ContactReplace
	reqBody.SkipBidi = request.SkipBidi
	reqBody.OnlyPosition = request.OnlyPosition

	tr, err := t.MarkdownReplaceAsync(context.Background(), reqBody)
	if err != nil {
		log.Fatalf("did not connect: %v", err)
		return nil
	}
	result := this.createCommonResult(tr)
	defer conn.Close()
	return result
}

func (this *TextFilterGrpcAsyncProvider) createCommonResult(response *pb.TextFilterRequestIdGrpcReply) *CommonResult {
	result := &CommonResult{}
	result.Code = response.GetCode()
	result.RequestId = response.GetRequestId()
	result.Message = response.GetMessage()

	return result
}
