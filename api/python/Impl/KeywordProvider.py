#!/usr/bin/env python
# -*- coding:utf-8 -*-
# KeywordProvider.py
# 2021, Lin Zhijun, https://github.com/toolgood/ToolGood.TextFilter.Api
# MIT Licensed 
 
__all__ = ['KeywordProvider']
import json
import http.client, urllib.parse
import requests


class KeywordProvider():

    def __init__(self,textFilterConfig):
        self.__textFilterConfig=textFilterConfig
        self.__GetKeywordListUrl = "/api/get-keyword-list"
        self.__AddKeywordUrl = "/api/add-keyword"
        self.__EditKeywordUrl = "/api/edit-keyword"
        self.__DeleteKeywordUrl = "/api/delete-keyword"

    def GetKeywordList(self,text:str,type:int,page:int,pageSize:int):
        """      获取自定义敏感词列表

        @text     敏感词 可空

        @type     敏感等级 ,0）REJECT,屏蔽删除， 1）REVIEW,人工审核，2）PASS,直接通过

        @page     页数，默认为1
        
        @pageSize 每页个数，默认为20
        """
        dictionary={"text": text,"type":type,"page":page,"pageSize":pageSize}
        result=doPost(self.__textFilterConfig,self.__GetKeywordListUrl,dictionary)
        return json.loads(result)

    def AddKeyword(self,text:str,type:int,comment:str):
        """ * 添加自定义敏感词
      
        @text    敏感词

        @type    类型：0）REJECT,屏蔽删除， 1）REVIEW,人工审核，2）PASS,直接通过
        
        @comment 备注
        """
        dictionary={"text": text,"type":type,"comment":comment}
        result=doPost(self.__textFilterConfig,self.__AddKeywordUrl,dictionary)
        return json.loads(result)

    def EditKeyword(self,id:int,text:str,type:int,comment:str):
        """      编辑自定义敏感词
      
        @id      敏感词ID

        @text    敏感词

        @type    类型：0）REJECT,屏蔽删除， 1）REVIEW,人工审核，2）PASS,直接通过

        @comment 备注
        """
        dictionary={"id":id,"text": text,"type":type,"comment":comment}
        result=doPost(self.__textFilterConfig,self.__EditKeywordUrl,dictionary)
        return json.loads(result)

    def DeleteKeyword(self,id:int):
        """   删除自定义敏感词
      
        @id 敏感词ID
        """
        dictionary={"id":id}
        result=doPost(self.__textFilterConfig,self.__DeleteKeywordUrl,dictionary)
        return json.loads(result)

def doPost(textFilterConfig,url,postData):
    u=textFilterConfig.GetTextFilterHost()+url
    result = requests.post(u, postData)
    return  result.text