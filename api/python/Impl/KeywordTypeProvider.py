#!/usr/bin/env python
# -*- coding:utf-8 -*-
# KeywordTypeProvider.py
# 2021, Lin Zhijun, https://github.com/toolgood/ToolGood.TextFilter.Api
# MIT Licensed 
 
__all__ = ['KeywordTypeProvider']
import json
import http.client, urllib.parse
import requests


class KeywordTypeProvider():

    def __init__(self,textFilterConfig):
        self.__textFilterConfig=textFilterConfig
        self.__GetListUrl = "/api/get-keywordtype-list"
        self.__SetKeywordTypeUrl = "/api/set-keywordtype"
    
    def GetList(self):
        '获取敏感词类型列表'
        result=doGet(self.__textFilterConfig,self.__GetListUrl)
        return json.loads(result)

    def SetKeywordType(self,typeId:int, level_1_UseType:int, level_2_UseType:int, level_3_UseType:int,useTime:bool, startTime:str, endTime:str):
        """             设置敏感词类型
     
        @typeId          敏感词类型ID

        @level_1_UseType 内置触线类型1：0）REJECT,屏蔽删除，1）REVIEW,人工审核，2）PASS,直接通过, 默认 1

        @level_2_UseType 内置触线类型2：0）REJECT,屏蔽删除，1）REVIEW,人工审核，2）PASS,直接通过, 默认 0

        @level_3_UseType 内置触线类型2：0）REJECT,屏蔽删除，1）REVIEW,人工审核，2）PASS,直接通过, 默认 0

        @useTime         是否启用指定日期

        @startTime       开始日期：格式 MM-dd

        @endTime         结束日期：格式 MM-dd
        """
        dictionary={"typeId": typeId,"level_1_UseType":level_1_UseType,"level_2_UseType":level_2_UseType,"level_3_UseType":level_3_UseType,"useTime":useTime,"startTime":startTime,"endTime":endTime}
        result=doPost(self.__textFilterConfig,self.__SetKeywordTypeUrl,dictionary)
        return json.loads(result)
 

def doGet(textFilterConfig,url):
    u=textFilterConfig.GetTextFilterHost()+url
    result = requests.get(u)
    return  result.text

def doPost(textFilterConfig,url,postData):
    u=textFilterConfig.GetTextFilterHost()+url
    result = requests.post(u, postData)
    return  result.text