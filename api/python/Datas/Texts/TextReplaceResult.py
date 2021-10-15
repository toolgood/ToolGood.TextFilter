#!/usr/bin/env python
# -*- coding:utf-8 -*-
# TextReplaceResult.py
# 2021, Lin Zhijun, https://github.com/toolgood/ToolGood.TextFilter.Api
# MIT Licensed 
 
__all__ = ['ToolGood.TextFilter.TextReplaceResult']

class TextReplaceResult():
    '返回码：0) 成功，1) 失败'
    code=0
    '返回码详情描述'
    message=""
    '请求标识'
    requestId=0
    '风险级别： PASS：正常内容，建议直接放行 REVIEW：可疑内容，建议人工审核 REJECT：违规内容，建议直接拦截'
    riskLevel=""
    '替换后的文本'
    resultText=""
    '风险详情, 详见 details'
    details=[]
 