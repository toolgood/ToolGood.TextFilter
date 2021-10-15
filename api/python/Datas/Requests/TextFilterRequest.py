#!/usr/bin/env python
# -*- coding:utf-8 -*-
# TextFilterRequest.py
# 2021, Lin Zhijun, https://github.com/toolgood/ToolGood.TextFilter.Api
# MIT Licensed 
 
__all__ = ['ToolGood.TextFilter.TextFilterRequest']

class TextFilterRequest():
    '需要检测的文本'
    Txt=0
    '是否跳过 Bidi 字符，默认 false'
    SkipBidi=False
    '只显示位置，不显示匹配文字'
    OnlyPosition=False

   
 
 