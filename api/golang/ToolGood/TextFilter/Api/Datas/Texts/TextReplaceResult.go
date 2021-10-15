package Texts

type TextReplaceResult struct {
	Code       int32
	Message    string
	RequestId  string
	RiskLevel  string
	ResultText string
	Details    []TextFilterDetailItem
}
