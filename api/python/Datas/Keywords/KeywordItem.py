#!/usr/bin/env python
# -*- coding:utf-8 -*-
# KeywordItem.py
# 2021, Lin Zhijun, https://github.com/toolgood/ToolGood.TextFilter.Api
# MIT Licensed 
 
__all__ = ['ToolGood.TextFilter.KeywordItem']

class KeywordItem():
    '自定义敏感词ID'
    id=0
    '自定义敏感词'
    text=""
    '类型：0）REJECT,屏蔽删除， 1）REVIEW,人工审核，2）PASS,直接通过'
    type=0
    '备注'
    comment=0.0
    '添加日期'
    addingTime=""
    '修改日期'
    modifyTime=""

 