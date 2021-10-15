package Requests


type TextReplaceAsyncRequest struct{
	Txt string
	ReplaceChar string
	ReviewReplace bool
	ContactReplace bool
	SkipBidi bool
	OnlyPosition bool
	RequestId string
	Url string

}

func NewTextReplaceAsyncRequest() *TextReplaceAsyncRequest{
	request := &TextReplaceAsyncRequest{}
	return request;
}