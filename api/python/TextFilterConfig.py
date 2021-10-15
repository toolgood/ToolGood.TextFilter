#!/usr/bin/env python
# -*- coding:utf-8 -*-
# TextFilterConfig.py
# 2021, Lin Zhijun, https://github.com/toolgood/ToolGood.TextFilter.Api
# MIT Licensed    
 

__all__ = ['TextFilterConfig']
__author__ = 'Lin Zhijun'
__date__ = '2020.05.16'

from Impl import KeywordProvider
from Impl import KeywordTypeProvider
from Impl import SysProvider
from Impl import TextFilterAsyncProvider
from Impl import TextFilterProvider
from Grpcs import TextFilterGrpcProvider
from Grpcs import TextFilterGrpcAsyncProvider
from ConsulDiscovery import ConsulServiceDiscovery

class TextFilterConfig():

    def __init__(self):
        self.__registryAddressHost="127.0.0.1"
        self.__registryAddressPost=8500
        self.HttpServiceName= "ToolGood.TextFilter"
        self.GrpcServiceName= "ToolGood.TextFilter.Grpc"
        self.__textFilterHost="http://localhost:9191"
        self.__GrpcHost="localhost:9192"

    def SetTextFilterHost(self,value:str):
        self.__textFilterHost = value

    def GetTextFilterHost(self):
        return self.__textFilterHost
    
    def GetGrpcHost(self):
        return self.__GrpcHost

    def SetGrpcHost(self,value:str):
        self.__GrpcHost=value

    def SetConsulServiceHost(self,value:str):
        self.__registryAddressHost=value

    def SetConsulServicePost(self,value:str):
        self.__registryAddressPost=value


    def GetServiceUrls_Http(self):
        Consul=ConsulServiceDiscovery.ConsulServiceDiscovery(self.__registryAddressHost,self.__registryAddressPost)
        return Consul.Discovery(self.HttpServiceName)

    def GetServiceUrls_Grpc(self):
        Consul=ConsulServiceDiscovery.ConsulServiceDiscovery(self.__registryAddressHost,self.__registryAddressPost)
        return Consul.Discovery(self.GrpcServiceName)

    def CreateKeywordProvider(self):
        result=KeywordProvider.KeywordProvider(self)
        return result

    def CreateKeywordTypeProvider(self):
        result=KeywordTypeProvider.KeywordTypeProvider(self)
        return result

    def CreateSysProvider(self):
        result=SysProvider.SysProvider(self)
        return result

    def CreateTextFilterProvider(self):
        result=TextFilterProvider.TextFilterProvider(self)
        return result

    def CreateTextFilterAsyncProvider(self):
        result=TextFilterAsyncProvider.TextFilterAsyncProvider(self)
        return result


    def CreateTextFilterGrpcClient(self):
        result=TextFilterGrpcProvider.TextFilterGrpcProvider(self)
        return result
    def CreateTextFilterGrpcAsyncClient(self):
        result=TextFilterGrpcAsyncProvider.TextFilterGrpcAsyncProvider(self)
        return result

if __name__ == "__main__":
    config =TextFilterConfig()
    config.SetTextFilterHost("http://localhost:9191")

    print("-------------------- SysProvider -----------------------")
    SysProvider= config.CreateSysProvider()
    json=SysProvider.GCCollect()
    print(json)
    json=SysProvider.Info()
    print(json)
    json=SysProvider.InitData()
    print(json)
    json=SysProvider.Refresh()
    print(json)

    print("-------------------- KeywordTypeProvider -----------------------")
    KeywordTypeProvider=config.CreateKeywordTypeProvider()
    json= KeywordTypeProvider.GetList()
    print(json)
    json=KeywordTypeProvider.SetKeywordType(1,0,0,0,False,'','')
    print(json)

    print("-------------------- KeywordProvider -----------------------")
    KeywordProvider= config.CreateKeywordProvider()
    json= KeywordProvider.AddKeyword("txt",1,'测试')
    print(json)
    json= KeywordProvider.GetKeywordList('','',1,20)
    print(json)
    json= KeywordProvider.EditKeyword(1,"txt5222",1,'测试')
    print(json)
    json= KeywordProvider.GetKeywordList('','',1,20)
    print(json)
    json= KeywordProvider.DeleteKeyword(1)
    print(json)
    json= KeywordProvider.GetKeywordList('','',1,20)
    print(json)

    print("-------------------- TextFilterProvider -----------------------")
    TextFilterProvider= config.CreateTextFilterProvider()
    json= TextFilterProvider.TextFilter('你妈的',False,False)
    print(json)
    json= TextFilterProvider.TextReplace('你妈的','*',False,False,False,False)
    print(json)
    json= TextFilterProvider.JsonFilter('你妈的',False,False)
    print(json)
    json= TextFilterProvider.JsonReplace('你妈的','*',False,False,False,False)
    print(json)
    json= TextFilterProvider.HtmlFilter('你妈的',False,False)
    print(json)
    json= TextFilterProvider.HtmlReplace('你妈的','*',False,False,False,False)
    print(json)
    json= TextFilterProvider.MarkdownFilter('你妈的',False,False)
    print(json)
    json= TextFilterProvider.MarkdownReplace('你妈的','*',False,False,False,False)
    print(json)

    print("-------------------- TextFilterAsyncProvider -----------------------")
    TextFilterAsyncProvider= config.CreateTextFilterAsyncProvider()
    json= TextFilterAsyncProvider.TextFilter('你妈的',False,False,'','http://127.0.0.1:8888/testOut')
    print(json)
    json= TextFilterAsyncProvider.TextReplace('你妈的','*',False,False,False,False,'','http://127.0.0.1:8888/testOut')
    print(json)
    json= TextFilterAsyncProvider.JsonFilter('你妈的',False,False,'','http://127.0.0.1:8888/testOut')
    print(json)
    json= TextFilterAsyncProvider.JsonReplace('你妈的','*',False,False,False,False,'','http://127.0.0.1:8888/testOut')
    print(json)
    json= TextFilterAsyncProvider.HtmlFilter('你妈的',False,False,'','http://127.0.0.1:8888/testOut')
    print(json)
    json= TextFilterAsyncProvider.HtmlReplace('你妈的','*',False,False,False,False,'','http://127.0.0.1:8888/testOut')
    print(json)
    json= TextFilterAsyncProvider.MarkdownFilter('你妈的',False,False,'','http://127.0.0.1:8888/testOut')
    print(json)
    json= TextFilterAsyncProvider.MarkdownReplace('你妈的','*',False,False,False,False,'','http://127.0.0.1:8888/testOut')
    print(json)

    print("-------------------- CreateTextFilterGrpcClient -----------------------")
    config.SetGrpcHost("localhost:9192")
    TextFilterGrpcProvider=config.CreateTextFilterGrpcClient()
    json= TextFilterGrpcProvider.TextFilter('你妈的',False,False)
    print(json)
    json= TextFilterGrpcProvider.TextReplace('你妈的','*',False,False,False,False)
    print(json)
    json= TextFilterGrpcProvider.JsonFilter('你妈的',False,False)
    print(json)
    json= TextFilterGrpcProvider.JsonReplace('你妈的','*',False,False,False,False)
    print(json)
    json= TextFilterGrpcProvider.HtmlFilter('你妈的',False,False)
    print(json)
    json= TextFilterGrpcProvider.HtmlReplace('你妈的','*',False,False,False,False)
    print(json)
    json= TextFilterGrpcProvider.MarkdownFilter('你妈的',False,False)
    print(json)
    json= TextFilterGrpcProvider.MarkdownReplace('你妈的','*',False,False,False,False)
    print(json)

    print("-------------------- CreateTextFilterGrpcAsyncClient -----------------------")
    TextFilterGrpcAsyncProvider=config.CreateTextFilterGrpcAsyncClient()
    json= TextFilterGrpcAsyncProvider.TextFilter('你妈的',False,False,'','http://127.0.0.1:8888/testOut')
    print(json)
    json= TextFilterGrpcAsyncProvider.TextReplace('你妈的','*',False,False,False,False,'','http://127.0.0.1:8888/testOut')
    print(json)
    json= TextFilterGrpcAsyncProvider.JsonFilter('你妈的',False,False,'','http://127.0.0.1:8888/testOut')
    print(json)
    json= TextFilterGrpcAsyncProvider.JsonReplace('你妈的','*',False,False,False,False,'','http://127.0.0.1:8888/testOut')
    print(json)
    json= TextFilterGrpcAsyncProvider.HtmlFilter('你妈的',False,False,'','http://127.0.0.1:8888/testOut')
    print(json)
    json= TextFilterGrpcAsyncProvider.HtmlReplace('你妈的','*',False,False,False,False,'','http://127.0.0.1:8888/testOut')
    print(json)
    json= TextFilterGrpcAsyncProvider.MarkdownFilter('你妈的',False,False,'','http://127.0.0.1:8888/testOut')
    print(json)
    json= TextFilterGrpcAsyncProvider.MarkdownReplace('你妈的','*',False,False,False,False,'','http://127.0.0.1:8888/testOut')
    print(json)

    print("------------------ GetServiceUrls_Http ----------------")
    json=config.GetServiceUrls_Http()
    print(json)
    print("------------------ GetServiceUrls_Grpc ----------------")
    json=config.GetServiceUrls_Grpc()
    print(json)
    pass