#!/usr/bin/env python
# -*- coding:utf-8 -*-
# SysProvider.py
# 2021, Lin Zhijun, https://github.com/toolgood/ToolGood.TextFilter.Api
# MIT Licensed 
 
__all__ = ['SysProvider']
import json
import http.client, urllib.parse
import requests


class SysProvider():

    def __init__(self,textFilterConfig):
        self.__textFilterConfig=textFilterConfig
        self.__UpdateSystemUrl = "/api/sys-update"
        self.__RefreshUrl = "/api/sys-refresh"
        self.__InfoUrl = "/api/sys-info"
        self.__UpdateLicenceUrl = "/api/sys-Update-Licence"
        self.__InitDataUrl = "/api/sys-init-Data"
        self.__GCCollectUrl = "/api/sys-GC-Collect"

    
    def UpdateSystem(self,textFilterNoticeUrl:str, textReplaceNoticeUrl:str,skipword:str):
        """     * 更新系统
      
        @textFilterNoticeUrl    默认 文本检测异步地址

        @textReplaceNoticeUrl   默认 文本替换异步地址

        @imageFilterNoticeUrl   默认 图片检测异步地址

        @imageClassifyNoticeUrl 默认 图片分类异步地址

        @imageTempPath          图片临时保存地址

        @skipword               自定义跳词

        """
        dictionary={
            'textFilterNoticeUrl':textFilterNoticeUrl,
            'textReplaceNoticeUrl':textReplaceNoticeUrl,
            'skipword':skipword
        }
        result=doPost(self.__textFilterConfig,self.__UpdateSystemUrl,dictionary)
        return json.loads(result)

    def Refresh(self):
        '刷新缓存'
        result=doGet(self.__textFilterConfig,self.__RefreshUrl)
        return json.loads(result)

    def Info(self):
        '产品信息'
        result=doGet(self.__textFilterConfig,self.__InfoUrl)
        return json.loads(result)

    def InitData(self):
        '重载数据'
        result=doGet(self.__textFilterConfig,self.__InitDataUrl)
        return json.loads(result)

    def GCCollect(self):
        'GC垃圾回收'
        result=doGet(self.__textFilterConfig,self.__GCCollectUrl)
        return json.loads(result)

 
 

def doGet(textFilterConfig,url):
    u=textFilterConfig.GetTextFilterHost()+url
    result = requests.get(u)
    return  result.text

def doPost(textFilterConfig,url,postData):
    u=textFilterConfig.GetTextFilterHost()+url
    result = requests.post(u, postData)
    return  result.text