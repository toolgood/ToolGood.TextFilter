### 快速上手


``` cs
            MemoryCache.DataFile = "TextFilter-20210923.data";
            MemoryCache.Init();

            var r = TextFilterApplication.FindAll("测试文本");
            Debug.Assert(r.RiskLevel == IllegalWordsRiskLevel.Pass);

            var t = TextFilterApplication.Replace("测试文本", '*', false, false);
            Debug.Assert(t.RiskLevel == IllegalWordsRiskLevel.Pass);

```



