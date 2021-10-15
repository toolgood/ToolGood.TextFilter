package Requests


type TextFilterRequest struct{
	Txt string
	SkipBidi bool
	OnlyPosition bool

}

func NewTextFilterRequest() *TextFilterRequest{
	request := &TextFilterRequest{}
	return request;
}