package Impl

import "encoding/json"
import "net/http"
import "net/url"
import	"io/ioutil"
import	. "../Datas/Sys"
import	. "../Datas"

type SysProvider struct{
	textFilterHost string
}

func NewSysProvider(config string) *SysProvider{
	provider := &SysProvider{}
	provider.textFilterHost = config
	return  provider;
}

const updateSystemUrl string = "/api/sys-update";
const refreshUrl string = "/api/sys-refresh";
const infoUrl string = "/api/sys-info";
const updateLicenceUrl string= "/api/sys-Update-Licence";
const initDataUrl string = "/api/sys-init-Data";
const gcCollectUrl string = "/api/sys-GC-Collect";

func (this *SysProvider)UpdateSystem(textFilterNoticeUrl string,textReplaceNoticeUrl string,skipword string) *CommonResult  {
	u:=this.textFilterHost+updateSystemUrl;

	resp, err := http.PostForm(u, url.Values{ "textFilterNoticeUrl":{textFilterNoticeUrl}, "textReplaceNoticeUrl":{textReplaceNoticeUrl},"skipword":{skipword}	}) 
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
func (this *SysProvider)Refresh() *CommonResult  {
	u:=this.textFilterHost+refreshUrl;
	return this.httpGet(u)
}
 
func (this *SysProvider)Info() *SysInfo  {
	u:=this.textFilterHost+infoUrl;
	resp, err := http.Get(u) 
    if err != nil {
		return nil
    }
	defer resp.Body.Close() 
    body,err := ioutil.ReadAll(resp.Body)
    if err != nil {
		return nil
    }
	var result *SysInfo
	err = json.Unmarshal([]byte(body), &result)
	if err != nil {
		return nil
	}
	return result
}
func (this *SysProvider)UpdateLicence(lic string) *CommonResult  {
	u:=this.textFilterHost+updateLicenceUrl;

	resp, err := http.PostForm(u, url.Values{ "Licence":{lic}}) 
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

func (this *SysProvider)InitData() *CommonResult  {
	u:=this.textFilterHost+initDataUrl;
	return this.httpGet(u)
}
func (this *SysProvider)GCCollect() *CommonResult  {
	u:=this.textFilterHost+gcCollectUrl;
	return this.httpGet(u)
}






func (this *SysProvider)httpGet(u string) *CommonResult  {
	resp, err := http.Get(u) 
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
