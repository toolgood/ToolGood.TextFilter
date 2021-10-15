#!/usr/bin/env python
# -*- coding:utf-8 -*-
# TextFilterProvider.py
# 2021, Lin Zhijun, https://github.com/toolgood/ToolGood.TextFilter.Api
# MIT Licensed 
 
__all__ = ['TextFilterProvider']
import json
import http.client, urllib.parse
import requests


class TextFilterProvider():


    def __init__(self,textFilterConfig):
        self.__textFilterConfig=textFilterConfig
        self.__TextFilterUrl = "/api/text-filter"
        self.__TextReplaceUrl = "/api/text-replace"
        self.__HtmlFilterUrl = "/api/html-filter"
        self.__HtmlReplaceUrl = "/api/html-replace"

        self.__JsonFilterUrl = "/api/json-filter"
        self.__JsonReplaceUrl = "/api/json-replace"
        self.__MarkdownFilterUrl = "/api/markdown-filter"
        self.__MarkdownReplaceUrl = "/api/markdown-replace"
    
    def TextFilter(self,txt:str,skipBidi:bool,onlyPosition:bool):
        """
        @txt 需要检测的文本

        @skipBidi 是否跳过 Bidi 字符，默认 false

        @onlyPosition 只显示位置，不显示匹配文字
        """
        request={'txt':txt,'skipBidi':skipBidi,'onlyPosition':onlyPosition}
        result=doPost(self.__textFilterConfig, self.__TextFilterUrl,request)
        return json.loads(result)

    def TextReplace(self,txt:str,replaceChar:str,reviewReplace:bool,contactReplace:bool,skipBidi:bool,onlyPosition:bool):
        """
        @txt 需要检测的文本

        @replaceChar 替换符号, 默认 星号

        @reviewReplace 人工审核替换，默认true

        @contactReplace 联系字符串替换，默认true

        @skipBidi 是否跳过 Bidi 字符，默认 false

        @onlyPosition 只显示位置，不显示匹配文字

        """
        request={'txt':txt,'replaceChar':replaceChar,'reviewReplace':reviewReplace,'contactReplace':contactReplace,'skipBidi':skipBidi,'onlyPosition':onlyPosition}
        result=doPost(self.__textFilterConfig,self.__TextReplaceUrl,request)
        return json.loads(result)

    def HtmlFilter(self,txt:str,skipBidi:bool,onlyPosition:bool):
        """
        @txt 需要检测的文本
        @skipBidi 是否跳过 Bidi 字符，默认 false
        @onlyPosition 只显示位置，不显示匹配文字
        """
        request={'txt':txt,'skipBidi':skipBidi,'onlyPosition':onlyPosition}
        result=doPost(self.__textFilterConfig,self.__HtmlFilterUrl,request)
        return json.loads(result)

    def HtmlReplace(self,txt:str,replaceChar:str,reviewReplace:bool,contactReplace:bool,skipBidi:bool,onlyPosition:bool):
        """
        @txt 需要检测的文本

        @replaceChar 替换符号, 默认 星号

        @reviewReplace 人工审核替换，默认true

        @contactReplace 联系字符串替换，默认true

        @skipBidi 是否跳过 Bidi 字符，默认 false

        @onlyPosition 只显示位置，不显示匹配文字

        """
        request={'txt':txt,'replaceChar':replaceChar,'reviewReplace':reviewReplace,'contactReplace':contactReplace,'skipBidi':skipBidi,'onlyPosition':onlyPosition}
        result=doPost(self.__textFilterConfig,self.__HtmlReplaceUrl,request)
        return json.loads(result)

    def JsonFilter(self,txt:str,skipBidi:bool,onlyPosition:bool):
        """
        @txt 需要检测的文本

        @skipBidi 是否跳过 Bidi 字符，默认 false

        @onlyPosition 只显示位置，不显示匹配文字
        """
        request={'txt':txt,'skipBidi':skipBidi,'onlyPosition':onlyPosition}
        result=doPost(self.__textFilterConfig,self.__JsonFilterUrl,request)
        return json.loads(result)

    def JsonReplace(self,txt:str,replaceChar:str,reviewReplace:bool,contactReplace:bool,skipBidi:bool,onlyPosition:bool):
        """
        @txt 需要检测的文本

        @replaceChar 替换符号, 默认 星号

        @reviewReplace 人工审核替换，默认true

        @contactReplace 联系字符串替换，默认true

        @skipBidi 是否跳过 Bidi 字符，默认 false

        @onlyPosition 只显示位置，不显示匹配文字

        """
        request={'txt':txt,'replaceChar':replaceChar,'reviewReplace':reviewReplace,'contactReplace':contactReplace,'skipBidi':skipBidi,'onlyPosition':onlyPosition}
        
        result=doPost(self.__textFilterConfig,self.__JsonReplaceUrl,request)
        return json.loads(result)
 
    def MarkdownFilter(self,txt:str,skipBidi:bool,onlyPosition:bool):
        """
        @txt 需要检测的文本

        @skipBidi 是否跳过 Bidi 字符，默认 false

        @onlyPosition 只显示位置，不显示匹配文字
        """
        request={'txt':txt,'skipBidi':skipBidi,'onlyPosition':onlyPosition}
        result=doPost(self.__textFilterConfig,self.__MarkdownFilterUrl,request)
        return json.loads(result)

    def MarkdownReplace(self,txt:str,replaceChar:str,reviewReplace:bool,contactReplace:bool,skipBidi:bool,onlyPosition:bool):
        """
        @txt 需要检测的文本

        @replaceChar 替换符号, 默认 星号

        @reviewReplace 人工审核替换，默认true

        @contactReplace 联系字符串替换，默认true

        @skipBidi 是否跳过 Bidi 字符，默认 false

        @onlyPosition 只显示位置，不显示匹配文字

        """
        request={'txt':txt,'replaceChar':replaceChar,'reviewReplace':reviewReplace,'contactReplace':contactReplace,'skipBidi':skipBidi,'onlyPosition':onlyPosition}
        result=doPost(self.__textFilterConfig,self.__MarkdownReplaceUrl,request)
        return json.loads(result)

 
 
def doPost(textFilterConfig,url,postData):
    u=textFilterConfig.GetTextFilterHost()+url
    result = requests.post(u, postData)
    return  result.text