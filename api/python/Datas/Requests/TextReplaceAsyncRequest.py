#!/usr/bin/env python
# -*- coding:utf-8 -*-
# TextReplaceAsyncRequest.py
# 2021, Lin Zhijun, https://github.com/toolgood/ToolGood.TextFilter.Api
# MIT Licensed 
 
__all__ = ['ToolGood.TextFilter.TextReplaceAsyncRequest']

class TextReplaceAsyncRequest():
    '需要检测的文本'
    Txt=0
    '是否跳过 Bidi 字符，默认 false'
    SkipBidi=False
    '只显示位置，不显示匹配文字'
    OnlyPosition=False
    '替换符号, 默认 星号'
    ReplaceChar='*'
    '人工审核替换，默认true'
    ReviewReplace=True
    '联系字符串替换，默认true'
    ContactReplace=True
    '请求标识，为空时会自动生成'
    RequestId=""
    '异步回调地址 可空'
    Url=""
 

 
 