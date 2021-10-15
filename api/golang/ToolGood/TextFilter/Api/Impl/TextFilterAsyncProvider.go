package Impl

import "encoding/json"
import "net/http"
import "net/url"
import	. "../Datas/Requests"
import	"io/ioutil"
import	"strconv"
import	. "../Datas/Texts"


type TextFilterAsyncProvider struct{
	textFilterHost string
}

func NewTextFilterAsyncProvider(config string) *TextFilterAsyncProvider{
	provider := &TextFilterAsyncProvider{}
	provider.textFilterHost = config
	return  provider;
}


const textFilterUrl_async string = "/api/async/text-filter";
const textReplaceUrl_async string = "/api/async/text-replace";
const htmlFilterUrl_async string = "/api/async/html-filter";
const htmlReplaceUrl_async string = "/api/async/html-replace";
const jsonFilterUrl_async string = "/api/async/json-filter";
const jsonReplaceUrl_async string = "/api/async/json-replace";
const markdownFilterUrl_async string = "/api/async/markdown-filter";
const markdownReplaceUrl_async string = "/api/async/markdown-replace";


 

func (this *TextFilterAsyncProvider)TextFilter(request *TextFilterRequest) *TextFilterResult  {
	u:=this.textFilterHost+textFilterUrl_async;
	return this.textFilter(request,u);
}
func (this *TextFilterAsyncProvider)HtmlFilter(request *TextFilterRequest) *TextFilterResult  {
	u:=this.textFilterHost+htmlFilterUrl_async;
	return this.textFilter(request,u);
}
func (this *TextFilterAsyncProvider)JsonFilter(request *TextFilterRequest) *TextFilterResult  {
	u:=this.textFilterHost+jsonFilterUrl_async;
	return this.textFilter(request,u);
}
func (this *TextFilterAsyncProvider)MarkdownFilter(request *TextFilterRequest) *TextFilterResult  {
	u:=this.textFilterHost+markdownFilterUrl_async;
	return this.textFilter(request,u);
}


func (this *TextFilterAsyncProvider)TextReplace(request *TextReplaceRequest) *TextReplaceResult  {
	u:=this.textFilterHost+textReplaceUrl_async;
	return this.textReplace(request,u);
}
func (this *TextFilterAsyncProvider)HtmlReplace(request *TextReplaceRequest) *TextReplaceResult  {
	u:=this.textFilterHost+htmlReplaceUrl_async;
	return this.textReplace(request,u);
}
func (this *TextFilterAsyncProvider)JsonReplace(request *TextReplaceRequest) *TextReplaceResult  {
	u:=this.textFilterHost+jsonReplaceUrl_async;
	return this.textReplace(request,u);
}
func (this *TextFilterAsyncProvider)MarkdownReplace(request *TextReplaceRequest) *TextReplaceResult  {
	u:=this.textFilterHost+markdownReplaceUrl_async;
	return this.textReplace(request,u);
}



func (this *TextFilterAsyncProvider)textReplace(request *TextReplaceRequest,u string)*TextReplaceResult   {
	resp, err := http.PostForm(u, url.Values{ "txt":{request.Txt}, "replaceChar":{request.ReplaceChar}, "reviewReplace":{strconv.FormatBool(request.ReviewReplace)}, "contactReplace":{strconv.FormatBool(request.ContactReplace)}, "skipBidi":{strconv.FormatBool(request.SkipBidi)}, "onlyPosition":{strconv.FormatBool(request.OnlyPosition)} })
    if err != nil {
		return nil
    }
	defer resp.Body.Close() 
    body,err := ioutil.ReadAll(resp.Body)
    if err != nil {
		return nil
    }
	var result *TextReplaceResult
	err = json.Unmarshal([]byte(body), &result)
	if err != nil {
		return nil
	}
	return result
}


func (this *TextFilterAsyncProvider)textFilter(request *TextFilterRequest,u string)*TextFilterResult   {
	resp, err := http.PostForm(u, url.Values{ "txt":{request.Txt}, "skipBidi":{strconv.FormatBool(request.SkipBidi)}, "onlyPosition":{strconv.FormatBool(request.OnlyPosition)}	}) 
    if err != nil {
		return nil
    }
	defer resp.Body.Close() 
    body,err := ioutil.ReadAll(resp.Body)
    if err != nil {
		return nil
    }
	var result *TextFilterResult
	err = json.Unmarshal([]byte(body), &result)
	if err != nil {
		return nil
	}
	return result
}




