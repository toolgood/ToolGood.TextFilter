package Impl

import "encoding/json"
import "net/http"
import "net/url"
import	"io/ioutil"
import	. "../Datas/Keywords"
import	. "../Datas"
import	"strconv"


type KeywordProvider struct{
	textFilterHost string
}

func NewKeywordProvider(config string) *KeywordProvider{
	provider := &KeywordProvider{}
	provider.textFilterHost = config
	return  provider;
}

const getKeywordListUrl string = "/api/get-keyword-list";
const addKeywordUrl string  = "/api/add-keyword";
const editKeywordUrl string  = "/api/edit-keyword";
const deleteKeywordUrl string  = "/api/delete-keyword";

func (this *KeywordProvider)SetKeywordType(text string,type_ int,page int,pageSize int) *KeywordListResult  {
	u:=this.textFilterHost+getListUrl;

	resp, err := http.PostForm(u, url.Values{ "text":{text}, "type":{strconv.Itoa(type_)}, "page":{strconv.Itoa(page)},"pageSize":{strconv.Itoa(pageSize)}}) 
    if err != nil {
		return nil
    }
	defer resp.Body.Close() 
    body,err := ioutil.ReadAll(resp.Body)
    if err != nil {
		return nil
    }
	var result *KeywordListResult
	err = json.Unmarshal([]byte(body), &result)
	if err != nil {
		return nil
	}
	return result
}
func (this *KeywordProvider)AddKeyword(text string,type_ int,comment string) *CommonResult  {
	u:=this.textFilterHost+addKeywordUrl;

	resp, err := http.PostForm(u, url.Values{ "text":{text}, "type":{strconv.Itoa(type_)}, "comment":{comment}}) 
    if err != nil {
		return nil
    }
	defer resp.Body.Close() 
    body,err := ioutil.ReadAll(resp.Body)
    if err != nil {
		return nil
    }
	var result *CommonResult
	err = json.Unmarshal([]byte(body), &result)
	if err != nil {
		return nil
	}
	return result
}
func (this *KeywordProvider)EditKeyword(id int,text string,type_ int,comment string) *CommonResult  {
	u:=this.textFilterHost+editKeywordUrl;

	resp, err := http.PostForm(u, url.Values{"id":{strconv.Itoa(id)}, "text":{text}, "type":{strconv.Itoa(type_)}, "comment":{comment}}) 
    if err != nil {
		return nil
    }
	defer resp.Body.Close() 
    body,err := ioutil.ReadAll(resp.Body)
    if err != nil {
		return nil
    }
	var result *CommonResult
	err = json.Unmarshal([]byte(body), &result)
	if err != nil {
		return nil
	}
	return result
}

func (this *KeywordProvider)DeleteKeyword(id int) *CommonResult  {
	u:=this.textFilterHost+deleteKeywordUrl;

	resp, err := http.PostForm(u, url.Values{"id":{strconv.Itoa(id)}}) 
    if err != nil {
		return nil
    }
	defer resp.Body.Close() 
    body,err := ioutil.ReadAll(resp.Body)
    if err != nil {
		return nil
    }
	var result *CommonResult
	err = json.Unmarshal([]byte(body), &result)
	if err != nil {
		return nil
	}
	return result
}