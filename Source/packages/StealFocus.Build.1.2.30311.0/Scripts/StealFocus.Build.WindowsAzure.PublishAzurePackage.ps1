param
(
	[string]$currentDirectory = $(throw '$currentDirectory is a required parameter'),
	[string]$subscriptionName = $(throw "subscriptionName is required"),
	[string]$affinityGroupName = $(throw "affinityGroupName is required"),
	[string]$hostedServiceName = $(throw "hostedServiceName is required"),
	[string]$hostedServiceLabel = $(throw "hostedServiceLabel is required"),
	[string]$storageAccountName = $(throw "storageAccountName is required"),
	[string]$packageFilePath = $(throw "packageFilePath is required"),
	[string]$configurationFilePath = $(throw "configurationFilePath is required"),
	[string]$deploymentLabel = $(throw "deploymentLabel is required"),
	[string]$removeStagingEnvironmentAfterwards = $(throw "removeStagingEnvironmentAfterwards is required"),
	[string]$promoteToProductionEnvironment = $(throw "promoteToProductionEnvironment is required")
)

[bool]$removeStagingEnvironmentAfterwardsValue = [System.Convert]::ToBoolean($removeStagingEnvironmentAfterwards)
[bool]$promoteToProductionEnvironmentValue = [System.Convert]::ToBoolean($promoteToProductionEnvironment)

Import-Module $currentDirectory\StealFocus.Build.WindowsAzure.psm1 -DisableNameChecking

$hostedService = Create-AzureHostedService -subscriptionName $subscriptionName -hostedServiceName $hostedServiceName -hostedServiceLabel $hostedServiceLabel -affinityGroupName $affinityGroupName
Delete-AzureHostedServiceDeployment -subscriptionName $subscriptionName -hostedServiceName $hostedServiceName -hostedServiceDeploymentSlot "Staging"
Create-AzureHostedServiceDeployment -subscriptionName $subscriptionName -packageFilePath $packageFilePath -configurationFilePath $configurationFilePath -deploymentLabel $deploymentLabel -hostedServiceName $hostedServiceName -storageAccountName $storageAccountName -promoteToProductionEnvironment $promoteToProductionEnvironmentValue -removeStagingEnvironmentAfterwards $removeStagingEnvironmentAfterwardsValue
