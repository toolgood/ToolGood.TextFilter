package Keywords

import "time"


type KeywordItem struct{
	Id int
	Text string
	Type int
	Comment string
	AddingTime time.Time
	ModifyTime time.Time
}