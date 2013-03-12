param
(
	[string]$currentDirectory = $(throw "currentDirectory is a required parameter"),
	[string]$subscriptionName = $(throw "subscriptionName is a required parameter"),
	[string]$affinityGroupName = $(throw "affinityGroupName is a required parameter"),
	[string]$storageAccountName = $(throw "storageAccountName is a required parameter"),
	[string]$storageAccountLabel = $(throw "storageAccountLabel is a required parameter")
)

Import-Module $currentDirectory\StealFocus.Build.WindowsAzure.psm1 -DisableNameChecking

Create-AzureStorageAccount -subscriptionName $subscriptionName -storageAccountName $storageAccountName -storageAccountLabel $storageAccountLabel -affinityGroupName $affinityGroupName
