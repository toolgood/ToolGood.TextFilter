package Texts

type TextFilterResult struct {
	Code           int32
	Message        string
	RequestId      string
	RiskLevel      string
	RiskCode       string
	SentimentScore float32

	Details  []TextFilterDetailItem
	Contacts []TextFilterContactItem
}
