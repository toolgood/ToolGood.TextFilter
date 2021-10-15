package Impl

 
import "encoding/json"
import "net/http"
import	"net/url"
import	. "../Datas/Requests"
import	"io/ioutil"
import	"strconv"
import	. "../Datas/Texts"


type TextFilterProvider struct{
	textFilterHost string
}

const textFilterUrl string = "/api/text-filter";
const textReplaceUrl string = "/api/text-replace";
const htmlFilterUrl string = "/api/html-filter";
const htmlReplaceUrl string = "/api/html-replace";
const jsonFilterUrl string = "/api/json-filter";
const jsonReplaceUrl string = "/api/json-replace";
const markdownFilterUrl string = "/api/markdown-filter";
const markdownReplaceUrl string = "/api/markdown-replace";

func NewTextFilterProvider(config string) *TextFilterProvider{
	provider := &TextFilterProvider{}
	provider.textFilterHost = config
	return provider;
}

func (this *TextFilterProvider)TextFilter(request *TextFilterRequest) *TextFilterResult  {
	u:=this.textFilterHost+textFilterUrl;
	return this.textFilter(request,u);
}
func (this *TextFilterProvider)HtmlFilter(request *TextFilterRequest) *TextFilterResult  {
	u:=this.textFilterHost+htmlFilterUrl;
	return this.textFilter(request,u);
}
func (this *TextFilterProvider)JsonFilter(request *TextFilterRequest) *TextFilterResult  {
	u:=this.textFilterHost+jsonFilterUrl;
	return this.textFilter(request,u);
}
func (this *TextFilterProvider)MarkdownFilter(request *TextFilterRequest) *TextFilterResult  {
	u:=this.textFilterHost+markdownFilterUrl;
	return this.textFilter(request,u);
}


func (this *TextFilterProvider)TextReplace(request *TextReplaceRequest) *TextReplaceResult  {
	u:=this.textFilterHost+textReplaceUrl;
	return this.textReplace(request,u);
}
func (this *TextFilterProvider)HtmlReplace(request *TextReplaceRequest) *TextReplaceResult  {
	u:=this.textFilterHost+htmlReplaceUrl;
	return this.textReplace(request,u);
}
func (this *TextFilterProvider)JsonReplace(request *TextReplaceRequest) *TextReplaceResult  {
	u:=this.textFilterHost+jsonReplaceUrl;
	return this.textReplace(request,u);
}
func (this *TextFilterProvider)MarkdownReplace(request *TextReplaceRequest) *TextReplaceResult  {
	u:=this.textFilterHost+markdownReplaceUrl;
	return this.textReplace(request,u);
}



func (this *TextFilterProvider)textReplace(request *TextReplaceRequest,u string)*TextReplaceResult   {
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


func (this *TextFilterProvider)textFilter(request *TextFilterRequest,u string)*TextFilterResult   {
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
