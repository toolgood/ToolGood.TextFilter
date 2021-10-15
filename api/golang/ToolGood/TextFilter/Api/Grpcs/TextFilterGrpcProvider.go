package Grpcs

import (
	"log"

	pb "./GrpcBase"
	"golang.org/x/net/context"
	"google.golang.org/grpc"

	. "../Datas/Requests"
	. "../Datas/Texts"
)

type TextFilterGrpcProvider struct {
	grpcHost string
}

func NewTextFilterGrpcProvider(grpcHost string) *TextFilterGrpcProvider {
	result := &TextFilterGrpcProvider{}
	result.grpcHost = grpcHost
	return result
}

func (this *TextFilterGrpcProvider) TextFilter(request *TextFilterRequest) *TextFilterResult {
	conn, err := grpc.Dial(this.grpcHost, grpc.WithInsecure())
	if err != nil {
		log.Fatalf("did not connect: %v", err)
	}
	t := pb.NewTextFilterGrpcClient(conn)

	reqBody := new(pb.TextFindAllGrpcRequest)
	reqBody.Txt = request.Txt
	reqBody.SkipBidi = request.SkipBidi
	reqBody.OnlyPosition = request.OnlyPosition

	tr, err := t.TextFilter(context.Background(), reqBody)
	if err != nil {
		log.Fatalf("did not connect: %v", err)
		return nil
	}
	result := this.createTextFilterResult(tr)
	defer conn.Close()
	return result
}
func (this *TextFilterGrpcProvider) HtmlFilter(request *TextFilterRequest) *TextFilterResult {
	conn, err := grpc.Dial(this.grpcHost, grpc.WithInsecure())
	if err != nil {
		log.Fatalf("did not connect: %v", err)
	}
	t := pb.NewTextFilterGrpcClient(conn)

	reqBody := new(pb.TextFindAllGrpcRequest)
	reqBody.Txt = request.Txt
	reqBody.SkipBidi = request.SkipBidi
	reqBody.OnlyPosition = request.OnlyPosition

	tr, err := t.HtmlFilter(context.Background(), reqBody)
	if err != nil {
		log.Fatalf("did not connect: %v", err)
		return nil
	}
	result := this.createTextFilterResult(tr)
	defer conn.Close()
	return result
}
func (this *TextFilterGrpcProvider) JsonFilter(request *TextFilterRequest) *TextFilterResult {
	conn, err := grpc.Dial(this.grpcHost, grpc.WithInsecure())
	if err != nil {
		log.Fatalf("did not connect: %v", err)
	}
	t := pb.NewTextFilterGrpcClient(conn)

	reqBody := new(pb.TextFindAllGrpcRequest)
	reqBody.Txt = request.Txt
	reqBody.SkipBidi = request.SkipBidi
	reqBody.OnlyPosition = request.OnlyPosition

	tr, err := t.JsonFilter(context.Background(), reqBody)
	if err != nil {
		log.Fatalf("did not connect: %v", err)
		return nil
	}
	result := this.createTextFilterResult(tr)
	defer conn.Close()
	return result
}
func (this *TextFilterGrpcProvider) MarkdownFilter(request *TextFilterRequest) *TextFilterResult {
	conn, err := grpc.Dial(this.grpcHost, grpc.WithInsecure())
	if err != nil {
		log.Fatalf("did not connect: %v", err)
	}
	t := pb.NewTextFilterGrpcClient(conn)

	reqBody := new(pb.TextFindAllGrpcRequest)
	reqBody.Txt = request.Txt
	reqBody.SkipBidi = request.SkipBidi
	reqBody.OnlyPosition = request.OnlyPosition

	tr, err := t.MarkdownFilter(context.Background(), reqBody)
	if err != nil {
		log.Fatalf("did not connect: %v", err)
		return nil
	}
	result := this.createTextFilterResult(tr)
	defer conn.Close()
	return result
}

func (this *TextFilterGrpcProvider) TextReplace(request *TextReplaceRequest) *TextReplaceResult {
	conn, err := grpc.Dial(this.grpcHost, grpc.WithInsecure())
	if err != nil {
		log.Fatalf("did not connect: %v", err)
	}
	t := pb.NewTextFilterGrpcClient(conn)

	reqBody := new(pb.TextReplaceGrpcRequest)
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

	tr, err := t.TextReplace(context.Background(), reqBody)
	if err != nil {
		log.Fatalf("did not connect: %v", err)
		return nil
	}
	result := this.createTextReplaceResult(tr)
	defer conn.Close()
	return result
}

func (this *TextFilterGrpcProvider) HtmlReplace(request *TextReplaceRequest) *TextReplaceResult {
	conn, err := grpc.Dial(this.grpcHost, grpc.WithInsecure())
	if err != nil {
		log.Fatalf("did not connect: %v", err)
	}
	t := pb.NewTextFilterGrpcClient(conn)

	reqBody := new(pb.TextReplaceGrpcRequest)
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

	tr, err := t.HtmlReplace(context.Background(), reqBody)
	if err != nil {
		log.Fatalf("did not connect: %v", err)
		return nil
	}
	result := this.createTextReplaceResult(tr)
	defer conn.Close()
	return result
}

func (this *TextFilterGrpcProvider) JsonReplace(request *TextReplaceRequest) *TextReplaceResult {
	conn, err := grpc.Dial(this.grpcHost, grpc.WithInsecure())
	if err != nil {
		log.Fatalf("did not connect: %v", err)
	}
	t := pb.NewTextFilterGrpcClient(conn)

	reqBody := new(pb.TextReplaceGrpcRequest)
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

	tr, err := t.JsonReplace(context.Background(), reqBody)
	if err != nil {
		log.Fatalf("did not connect: %v", err)
		return nil
	}
	result := this.createTextReplaceResult(tr)
	defer conn.Close()
	return result
}

func (this *TextFilterGrpcProvider) MarkdownReplace(request *TextReplaceRequest) *TextReplaceResult {
	conn, err := grpc.Dial(this.grpcHost, grpc.WithInsecure())
	if err != nil {
		log.Fatalf("did not connect: %v", err)
	}
	t := pb.NewTextFilterGrpcClient(conn)

	reqBody := new(pb.TextReplaceGrpcRequest)
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

	tr, err := t.MarkdownReplace(context.Background(), reqBody)
	if err != nil {
		log.Fatalf("did not connect: %v", err)
		return nil
	}
	result := this.createTextReplaceResult(tr)
	defer conn.Close()
	return result
}

func (this *TextFilterGrpcProvider) createTextFilterResult(response *pb.TextFindAllGrpcReply) *TextFilterResult {
	result := &TextFilterResult{}
	result.Code = response.GetCode()
	result.RiskCode = response.GetRiskCode()
	result.RequestId = response.GetRequestId()
	result.Message = response.GetMessage()
	result.RiskLevel = response.GetRiskLevel()
	result.SentimentScore = response.GetSentimentScore()
	details := make([]TextFilterDetailItem, len(response.Details))
	for index, r := range response.Details {
		item := &TextFilterDetailItem{}
		item.Position = r.GetPosition()
		item.RiskCode = r.GetRiskCode()
		item.RiskLevel = r.GetRiskLevel()
		item.Text = r.GetText()
		details[index] = *item
	}
	result.Details = details

	contactItems := make([]TextFilterContactItem, len(response.Contacts))
	for index, r := range response.Contacts {
		item := &TextFilterContactItem{}
		item.ContactType = r.GetContactType()
		item.ContactString = r.GetContactString()
		item.Position = r.GetPosition()
		contactItems[index] = *item
	}
	result.Contacts = contactItems
	return result
}

func (this *TextFilterGrpcProvider) createTextReplaceResult(response *pb.TextReplaceGrpcReply) *TextReplaceResult {
	result := &TextReplaceResult{}
	result.Code = response.GetCode()
	result.RequestId = response.GetRequestId()
	result.Message = response.GetMessage()
	result.RiskLevel = response.GetRiskLevel()
	details := make([]TextFilterDetailItem, len(response.Details))
	for index, r := range response.Details {
		item := &TextFilterDetailItem{}
		item.Position = r.GetPosition()
		item.RiskCode = r.GetRiskCode()
		item.RiskLevel = r.GetRiskLevel()
		item.Text = r.GetText()
		details[index] = *item
	}
	result.Details = details

	return result
}
