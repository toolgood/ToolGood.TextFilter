package Requests


type TextFilterAsyncRequest struct{
	Txt string
	SkipBidi bool
	OnlyPosition bool
	RequestId string
	Url string

}


func NewTextFilterAsyncRequest() *TextFilterAsyncRequest{
	request := &TextFilterAsyncRequest{}
	return request;
}