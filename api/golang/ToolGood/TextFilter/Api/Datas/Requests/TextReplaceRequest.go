package Requests


type TextReplaceRequest struct{
	Txt string
	ReplaceChar string
	ReviewReplace bool
	ContactReplace bool
	SkipBidi bool
	OnlyPosition bool

}

func NewTextReplaceRequest() *TextReplaceRequest{
	request := &TextReplaceRequest{}
	return request;
}
