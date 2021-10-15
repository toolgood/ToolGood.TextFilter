#!/usr/bin/env python
# -*- coding:utf-8 -*-
# TextFilterResult.py
# 2021, Lin Zhijun, https://github.com/toolgood/ToolGood.TextFilter.Api
# MIT Licensed 
 
__all__ = ['ToolGood.TextFilter.TextFilterResult']

class TextFilterResult():
    '返回码：0) 成功，1) 失败'
    code=0
    '返回码详情描述'
    message=""
    '请求标识'
    requestId=0
    '风险级别： PASS：正常内容，建议直接放行 REVIEW：可疑内容，建议人工审核 REJECT：违规内容，建议直接拦截'
    riskLevel=""
    '风险类别：Char：非正常字符 Politics：涉政文本 Terrorism：涉恐文本 Porn：涉黄文本 Gamble：涉赌文本 Drug：涉毒文本 Contraband：非法交易 Abuse：辱骂文本 Other：推广引诱诈骗 Custom：自定义敏感词'
    riskCode=""
    '情感值，>0 正面，<0 负向 ，riskLevel为REVIEW（可疑内容），会出现此值'
    sentimentScore=0.0
    '风险详情, 详见 details'
    details=[]
    '联系方式详情, 详见 contacts'
    contacts=[]
 
 