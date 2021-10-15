#!/usr/bin/env python
# -*- coding:utf-8 -*-
# CommonResult.py
# 2021, Lin Zhijun, https://github.com/toolgood/ToolGood.TextFilter.Api
# MIT Licensed 
 
__all__ = ['ToolGood.TextFilter.CommonResult']

class CommonResult():
    '返回码：0) 成功，1) 失败'
    code=0
    '返回码详情描述'
    message=""
    '请求标识'
    requestId=0