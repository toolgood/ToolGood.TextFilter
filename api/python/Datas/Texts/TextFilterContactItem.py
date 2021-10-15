#!/usr/bin/env python
# -*- coding:utf-8 -*-
# TextFilterContactItem.py
# 2021, Lin Zhijun, https://github.com/toolgood/ToolGood.TextFilter.Api
# MIT Licensed 
 
__all__ = ['ToolGood.TextFilter.TextFilterContactItem']

class TextFilterContactItem():
    '联系方式类型 1) 账号，2）邮箱，3）网址，4）手机号， 5) QQ号, 6) 微信号, 7) Q群号'
    contactType=""
    '联系方式串'
    contactString=""
    '联系方式串位置，例：1,3,5,7-12,15'
    position=False
  