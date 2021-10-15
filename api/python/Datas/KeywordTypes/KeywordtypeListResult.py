#!/usr/bin/env python
# -*- coding:utf-8 -*-
# KeywordItem.py
# 2021, Lin Zhijun, https://github.com/toolgood/ToolGood.TextFilter.Api
# MIT Licensed 
 
__all__ = ['ToolGood.TextFilter.KeywordtypeListResult']

class KeywordtypeListResult():
    '返回码：0) 成功，1) 失败'
    code=0
    '返回码详情描述'
    message=""
    '总个数'
    total=0
    '敏感词类型列表'
    data=[]
 