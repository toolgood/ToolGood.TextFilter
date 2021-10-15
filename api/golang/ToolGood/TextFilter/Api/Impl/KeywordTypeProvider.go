package Impl

import "encoding/json"
import "net/http"
import "net/url"
import	"io/ioutil"
import	. "../Datas/KeywordTypes"
import	. "../Datas"
import	"strconv"


type KeywordTypeProvider struct{
	textFilterHost string
}

func NewKeywordTypeProvider(config string) *KeywordTypeProvider{
	provider := &KeywordTypeProvider{}
	provider.textFilterHost = config
	return  provider;
}

const getListUrl string = "/api/get-keywordtype-list";
const setKeywordTypeUrl string= "/api/set-keywordtype";

func (this *KeywordTypeProvider)GetList() *KeywordtypeListResult  {
	u:=this.textFilterHost+getListUrl;

	resp, err := http.Get(u) 
    if err != nil {
		return nil
    }
	defer resp.Body.Close() 
    body,err := ioutil.ReadAll(resp.Body)
    if err != nil {
		return nil
    }
	var result *KeywordtypeListResult
	err = json.Unmarshal([]byte(body), &result)
	if err != nil {
		return nil
	}
	return result
}

func (this *KeywordTypeProvider)SetKeywordType(typeId int,level_1_UseType int,level_2_UseType int,level_3_UseType int,useTime bool,startTime string,endTime string) *CommonResult  {
	u:=this.textFilterHost+getListUrl;

	resp, err := http.PostForm(u, url.Values{ "typeId":{strconv.Itoa(typeId)}, "level_1_UseType":{strconv.Itoa(level_1_UseType)}, "level_2_UseType":{strconv.Itoa(level_2_UseType)},"level_3_UseType":{strconv.Itoa(level_3_UseType)},"useTime":{strconv.FormatBool(useTime)},"startTime":{startTime},"endTime":{endTime}	}) 
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
