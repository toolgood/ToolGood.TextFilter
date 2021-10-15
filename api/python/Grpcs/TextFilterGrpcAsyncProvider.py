#!/usr/bin/env python
# -*- coding:utf-8 -*-
# ImageFilterAsyncProvider.py
# 2021, Lin Zhijun, https://github.com/toolgood/ToolGood.TextFilter.Api
# MIT Licensed 

import grpc
from Grpcs.GrpcBase import textFilter_pb2
from Grpcs.GrpcBase import textFilter_pb2_grpc


class TextFilterGrpcAsyncProvider():
    def __init__(self,textFilterConfig):
        self.__textFilterConfig=textFilterConfig

    def TextFilter(self,txt:str,skipBidi:bool,onlyPosition:bool,requestId:str,url:str):
        """
        @txt 需要检测的文本

        @skipBidi 是否跳过 Bidi 字符，默认 false

        @onlyPosition 只显示位置，不显示匹配文字
        """
        channel = grpc.insecure_channel(self.__textFilterConfig.GetGrpcHost())
        stub = textFilter_pb2_grpc.TextFilterGrpcStub(channel)
        response = stub.TextFilterAsync(textFilter_pb2.TextFindAllAsyncGrpcRequest(txt=txt, skipBidi=skipBidi,onlyPosition=onlyPosition,requestId=requestId,url=url))
        return response


    def TextReplace(self,txt:str,replaceChar:str,reviewReplace:bool,contactReplace:bool,skipBidi:bool,onlyPosition:bool,requestId:str,url:str):
        """
        @txt 需要检测的文本

        @replaceChar 替换符号, 默认 星号

        @reviewReplace 人工审核替换，默认true

        @contactReplace 联系字符串替换，默认true

        @skipBidi 是否跳过 Bidi 字符，默认 false

        @onlyPosition 只显示位置，不显示匹配文字

        """
        channel = grpc.insecure_channel(self.__textFilterConfig.GetGrpcHost())
        stub = textFilter_pb2_grpc.TextFilterGrpcStub(channel)
        replaceCh=ord('*')
        if len(replaceChar)>0:
            replaceCh=ord(replaceChar[0])
        response = stub.TextReplaceAsync(textFilter_pb2.TextReplaceAsyncGrpcRequest(txt=txt,replaceChar=replaceCh,reviewReplace=reviewReplace,contactReplace=contactReplace, skipBidi=skipBidi,onlyPosition=onlyPosition,requestId=requestId,url=url))
        return response

    def HtmlFilter(self,txt:str,skipBidi:bool,onlyPosition:bool,requestId:str,url:str):
        """
        @txt 需要检测的文本

        @skipBidi 是否跳过 Bidi 字符，默认 false
        
        @onlyPosition 只显示位置，不显示匹配文字
        """
        channel = grpc.insecure_channel(self.__textFilterConfig.GetGrpcHost())
        stub = textFilter_pb2_grpc.TextFilterGrpcStub(channel)
        response = stub.HtmlFilterAsync(textFilter_pb2.TextFindAllAsyncGrpcRequest(txt=txt, skipBidi=skipBidi,onlyPosition=onlyPosition,requestId=requestId,url=url))
        return response
 

    def HtmlReplace(self,txt:str,replaceChar:str,reviewReplace:bool,contactReplace:bool,skipBidi:bool,onlyPosition:bool,requestId:str,url:str):
        """
        @txt 需要检测的文本

        @replaceChar 替换符号, 默认 星号

        @reviewReplace 人工审核替换，默认true

        @contactReplace 联系字符串替换，默认true

        @skipBidi 是否跳过 Bidi 字符，默认 false

        @onlyPosition 只显示位置，不显示匹配文字

        """
        channel = grpc.insecure_channel(self.__textFilterConfig.GetGrpcHost())
        stub = textFilter_pb2_grpc.TextFilterGrpcStub(channel)
        replaceCh=ord('*')
        if len(replaceChar)>0:
            replaceCh=ord(replaceChar[0])
        response = stub.HtmlReplaceAsync(textFilter_pb2.TextReplaceAsyncGrpcRequest(txt=txt,replaceChar=replaceCh,reviewReplace=reviewReplace,contactReplace=contactReplace, skipBidi=skipBidi,onlyPosition=onlyPosition,requestId=requestId,url=url))
        return response

    def JsonFilter(self,txt:str,skipBidi:bool,onlyPosition:bool,requestId:str,url:str):
        """
        @txt 需要检测的文本

        @skipBidi 是否跳过 Bidi 字符，默认 false

        @onlyPosition 只显示位置，不显示匹配文字
        """
        channel = grpc.insecure_channel(self.__textFilterConfig.GetGrpcHost())
        stub = textFilter_pb2_grpc.TextFilterGrpcStub(channel)
        response = stub.JsonFilterAsync(textFilter_pb2.TextFindAllAsyncGrpcRequest(txt=txt, skipBidi=skipBidi,onlyPosition=onlyPosition,requestId=requestId,url=url))
        return response

    def JsonReplace(self,txt:str,replaceChar:str,reviewReplace:bool,contactReplace:bool,skipBidi:bool,onlyPosition:bool,requestId:str,url:str):
        """
        @txt 需要检测的文本

        @replaceChar 替换符号, 默认 星号

        @reviewReplace 人工审核替换，默认true

        @contactReplace 联系字符串替换，默认true

        @skipBidi 是否跳过 Bidi 字符，默认 false

        @onlyPosition 只显示位置，不显示匹配文字

        """
        channel = grpc.insecure_channel(self.__textFilterConfig.GetGrpcHost())
        stub = textFilter_pb2_grpc.TextFilterGrpcStub(channel)
        replaceCh=ord('*')
        if len(replaceChar)>0:
            replaceCh=ord(replaceChar[0])
        response = stub.JsonReplaceAsync(textFilter_pb2.TextReplaceAsyncGrpcRequest(txt=txt,replaceChar=replaceCh,reviewReplace=reviewReplace,contactReplace=contactReplace, skipBidi=skipBidi,onlyPosition=onlyPosition,requestId=requestId,url=url))
        return response
 
    def MarkdownFilter(self,txt:str,skipBidi:bool,onlyPosition:bool,requestId:str,url:str):
        """
        @txt 需要检测的文本

        @skipBidi 是否跳过 Bidi 字符，默认 false

        @onlyPosition 只显示位置，不显示匹配文字
        """
        channel = grpc.insecure_channel(self.__textFilterConfig.GetGrpcHost())
        stub = textFilter_pb2_grpc.TextFilterGrpcStub(channel)
        response = stub.MarkdownFilterAsync(textFilter_pb2.TextFindAllAsyncGrpcRequest(txt=txt, skipBidi=skipBidi,onlyPosition=onlyPosition,requestId=requestId,url=url))
        return response

    def MarkdownReplace(self,txt:str,replaceChar:str,reviewReplace:bool,contactReplace:bool,skipBidi:bool,onlyPosition:bool,requestId:str,url:str):
        """
        @txt 需要检测的文本

        @replaceChar 替换符号, 默认 星号

        @reviewReplace 人工审核替换，默认true

        @contactReplace 联系字符串替换，默认true

        @skipBidi 是否跳过 Bidi 字符，默认 false

        @onlyPosition 只显示位置，不显示匹配文字

        """
        channel = grpc.insecure_channel(self.__textFilterConfig.GetGrpcHost())
        stub = textFilter_pb2_grpc.TextFilterGrpcStub(channel)
        replaceCh=ord('*')
        if len(replaceChar)>0:
            replaceCh=ord(replaceChar[0])
        response = stub.MarkdownReplaceAsync(textFilter_pb2.TextReplaceAsyncGrpcRequest(txt=txt,replaceChar=replaceCh,reviewReplace=reviewReplace,contactReplace=contactReplace, skipBidi=skipBidi,onlyPosition=onlyPosition,requestId=requestId,url=url))
        return response