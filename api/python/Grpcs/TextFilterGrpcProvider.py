#!/usr/bin/env python
# -*- coding:utf-8 -*-
# ImageFilterAsyncProvider.py
# 2021, Lin Zhijun, https://github.com/toolgood/ToolGood.TextFilter.Api
# MIT Licensed 

import grpc
from Grpcs.GrpcBase import textFilter_pb2
from Grpcs.GrpcBase import textFilter_pb2_grpc


class TextFilterGrpcProvider():
    def __init__(self,textFilterConfig):
        self.__textFilterConfig=textFilterConfig

    def TextFilter(self,txt:str,skipBidi:bool,onlyPosition:bool):
        """
        @txt 需要检测的文本

        @skipBidi 是否跳过 Bidi 字符，默认 false

        @onlyPosition 只显示位置，不显示匹配文字
        """
        channel = grpc.insecure_channel(self.__textFilterConfig.GetGrpcHost())
        stub = textFilter_pb2_grpc.TextFilterGrpcStub(channel)
        response = stub.TextFilter(textFilter_pb2.TextFindAllGrpcRequest(txt=txt, skipBidi=skipBidi,onlyPosition=onlyPosition))
        return response


    def TextReplace(self,txt:str,replaceChar:str,reviewReplace:bool,contactReplace:bool,skipBidi:bool,onlyPosition:bool):
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
        response = stub.TextReplace(textFilter_pb2.TextReplaceGrpcRequest(txt=txt,replaceChar=replaceCh,reviewReplace=reviewReplace,contactReplace=contactReplace, skipBidi=skipBidi,onlyPosition=onlyPosition))
        return response

    def HtmlFilter(self,txt:str,skipBidi:bool,onlyPosition:bool):
        """
        @txt 需要检测的文本
        @skipBidi 是否跳过 Bidi 字符，默认 false
        @onlyPosition 只显示位置，不显示匹配文字
        """
        channel = grpc.insecure_channel(self.__textFilterConfig.GetGrpcHost())
        stub = textFilter_pb2_grpc.TextFilterGrpcStub(channel)
        response = stub.HtmlFilter(textFilter_pb2.TextFindAllGrpcRequest(txt=txt, skipBidi=skipBidi,onlyPosition=onlyPosition))
        return response
 

    def HtmlReplace(self,txt:str,replaceChar:str,reviewReplace:bool,contactReplace:bool,skipBidi:bool,onlyPosition:bool):
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
        response = stub.HtmlReplace(textFilter_pb2.TextReplaceGrpcRequest(txt=txt,replaceChar=replaceCh,reviewReplace=reviewReplace,contactReplace=contactReplace, skipBidi=skipBidi,onlyPosition=onlyPosition))
        return response

    def JsonFilter(self,txt:str,skipBidi:bool,onlyPosition:bool):
        """
        @txt 需要检测的文本

        @skipBidi 是否跳过 Bidi 字符，默认 false

        @onlyPosition 只显示位置，不显示匹配文字
        """
        channel = grpc.insecure_channel(self.__textFilterConfig.GetGrpcHost())
        stub = textFilter_pb2_grpc.TextFilterGrpcStub(channel)
        response = stub.JsonFilter(textFilter_pb2.TextFindAllGrpcRequest(txt=txt, skipBidi=skipBidi,onlyPosition=onlyPosition))
        return response

    def JsonReplace(self,txt:str,replaceChar:str,reviewReplace:bool,contactReplace:bool,skipBidi:bool,onlyPosition:bool):
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
        response = stub.JsonReplace(textFilter_pb2.TextReplaceGrpcRequest(txt=txt,replaceChar=replaceCh,reviewReplace=reviewReplace,contactReplace=contactReplace, skipBidi=skipBidi,onlyPosition=onlyPosition))
        return response
 
    def MarkdownFilter(self,txt:str,skipBidi:bool,onlyPosition:bool):
        """
        @txt 需要检测的文本

        @skipBidi 是否跳过 Bidi 字符，默认 false

        @onlyPosition 只显示位置，不显示匹配文字
        """
        channel = grpc.insecure_channel(self.__textFilterConfig.GetGrpcHost())
        stub = textFilter_pb2_grpc.TextFilterGrpcStub(channel)
        response = stub.MarkdownFilter(textFilter_pb2.TextFindAllGrpcRequest(txt=txt, skipBidi=skipBidi,onlyPosition=onlyPosition))
        return response

    def MarkdownReplace(self,txt:str,replaceChar:str,reviewReplace:bool,contactReplace:bool,skipBidi:bool,onlyPosition:bool):
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
        response = stub.MarkdownReplace(textFilter_pb2.TextReplaceGrpcRequest(txt=txt,replaceChar=replaceCh,reviewReplace=reviewReplace,contactReplace=contactReplace, skipBidi=skipBidi,onlyPosition=onlyPosition))
        return response