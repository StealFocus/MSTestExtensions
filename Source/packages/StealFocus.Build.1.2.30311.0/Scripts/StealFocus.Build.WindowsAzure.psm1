if ((Get-Module | ?{$_.Name -eq "Azure"}) -eq $null)
{
	$programFilesX86EnvironmentVariable = Get-Childitem env:"ProgramFiles(x86)"
	$azureModulePath = $programFilesX86EnvironmentVariable.Value + "\Microsoft SDKs\Windows Azure\PowerShell\Azure\Azure.psd1"
	Import-Module $azureModulePath -erroraction SilentlyContinue
	$azureModule = Get-Module Azure -erroraction SilentlyContinue
	if ($azureModule -eq $null)
	{
		Write-Error "To run this script the 'Windows Azure PowerShell' component is required. Please download and install via the Microsoft Web Platform installer ('http://www.microsoft.com/web/downloads/platform.aspx')."
		Exit
	}
}

function Get-ManagementCertificate
{
	param(
			[string]$managementCertificateThumbprint = $(throw '$managementCertificateThumbprint is a required parameter')
		)

	if ((Test-Path cert:\CurrentUser\MY\$managementCertificateThumbprint) -eq $true)
	{
		$certificate = (Get-Item cert:\CurrentUser\MY\$managementCertificateThumbprint)
	}
	elseif ((Test-Path cert:\LocalMachine\MY\$managementCertificateThumbprint) -eq $true)
	{
		$certificate = (Get-Item cert:\LocalMachine\MY\$managementCertificateThumbprint)
	}
	else
	{
		Write-Error "Could not find Management Certificate matching thumbprint $managementCertificateThumbprint"
	}

	return $certificate
}

function Create-AzureSubscription
{
	param( 
			[string]$subscriptionName = $(throw '$subscriptionName is a required parameter'),
			[string]$subscriptionId = $(throw '$subscriptionId is a required parameter'),
			[string]$managementCertificateThumbprint = $(throw '$managementCertificateThumbprint is a required parameter')
		)
	
	$azureSubscription = Get-AzureSubscription -subscriptionName $subscriptionName
	if ($azureSubscription -eq $null)
	{
		$certificate = Get-ManagementCertificate -managementCertificateThumbprint $managementCertificateThumbprint
		Set-AzureSubscription -SubscriptionName $subscriptionName -Certificate $certificate -SubscriptionId $subscriptionId
	}
	elseif ($azureSubscription.SubscriptionId.ToUpper() -ne $subscriptionId.ToLower())
	{
		Write-Error "An Azure Subscription existed but the Subscription ID did not match that of the new subscription."
	}
	else
	{
		Write-Host "An Azure Subscription already existed with that name and Subscription ID."
	}

	Select-AzureSubscription -SubscriptionName $subscriptionName
}

function Create-AzureAffinityGroup
{
	param( 
			[string]$subscriptionName = $(throw '$subscriptionName is a required parameter'),
			[string]$affinityGroupName = $(throw '$affinityGroupName is a required parameter'),
			[string]$affinityGroupLabel = $(throw '$affinityGroupLabel is a required parameter'),
			[string]$affinityGroupLocation = $(throw '$affinityGroupLocation is a required parameter')
		)
	
	Select-AzureSubscription $subscriptionName
	$affinityGroup = Get-AzureAffinityGroup | where {$_.Name -eq $affinityGroupName}
	if ($affinityGroup -eq $null)
	{
		Write-Host "Creating new Windows Azure Affinity Group with name ""$affinityGroupName"""
		$operation = New-AzureAffinityGroup -Name $affinityGroupName -Description $affinityGroupLabel -Label $affinityGroupLabel -Location $affinityGroupLocation
		Write-Host "`n"
	}
	elseif ($affinityGroup.Location -ne $affinityGroupLocation)
	{
		$existingAffinityGroupLocation = $affinityGroup.Location
		Write-Error "An Affinity Group named ""$affinityGroupName"" already exists but is in the location ""$existingAffinityGroupLocation"" when the requested location was ""$affinityGroupLocation""."
	}
	else
	{
		Write-Host "Windows Azure Affinity Group already exists with name ""$affinityGroupName"""
		Write-Host "`n"
	}

	return $affinityGroup
}

function Create-AzureHostedService
{
	param( 
			[string]$subscriptionName = $(throw '$subscriptionName is a required parameter'),
			[string]$hostedServiceName = $(throw '$hostedServiceName is a required parameter'),
			[string]$hostedServiceLabel = $(throw '$hostedServiceLabel is a required parameter'),
			[string]$affinityGroupName = $(throw '$affinityGroupName is a required parameter')
		)
	
	Select-AzureSubscription $subscriptionName
	$service = Get-AzureService | where {$_.ServiceName -eq $hostedServiceName}
	if ($service -eq $null)
	{
		Write-Host "Creating new Windows Azure Hosted Service with name ""$hostedServiceName"""
		$operation = New-AzureService -ServiceName $hostedServiceName -Label $hostedServiceLabel -AffinityGroup $affinityGroupName
		$service = Get-AzureService -ServiceName $hostedServiceName
		Write-Host "`n"
	}
	else
	{
		Write-Host "Windows Azure Hosted Service already exists with name ""$hostedServiceName"""
		Write-Host "`n"
	}

	return $service
}

function Create-AzureStorageAccount
{
	param( 
			[string]$subscriptionName = $(throw '$subscriptionName is a required parameter'),
			[string]$storageAccountName = $(throw '$storageAccountName is a required parameter'),
			[string]$storageAccountLabel = $(throw '$storageAccountLabel is a required parameter'),
			[string]$affinityGroupName = $(throw '$affinityGroupName is a required parameter')
		)
	
	Select-AzureSubscription $subscriptionName
	$storageAccount = Get-AzureStorageAccount | where {$_.StorageAccountName -eq $storageAccountName}
	if ($storageAccount -eq $null)
	{
		Write-Host "Creating new Windows Azure Storage Account with name ""$storageAccountName"""
		$storageAccount = New-AzureStorageAccount -StorageAccountName $storageAccountName -Label $storageAccountLabel -AffinityGroup $affinityGroupName
		Write-Host "`n"
	}
	else
	{
		Write-Host "Windows Azure Storage Account already exists with name ""$storageAccountName"""
		Write-Host "`n"
	}

	return $storageAccount
}

function Get-AzureHostedServiceDeploymentIsReady
{
	param(
			[string]$hostedServiceName = $(throw '$hostedServiceName is a required parameter'),
			[string]$hostedServiceDeploymentSlot = $(throw '$hostedServiceDeploymentSlot is a required parameter')
		)

	$hostedServiceDeploymentSlotStatus = Get-AzureDeployment -ServiceName $hostedServiceName -Slot $hostedServiceDeploymentSlot
    if (-not $($hostedServiceDeploymentSlotStatus.Status -eq "Running"))
	{
        Write-Host $("Deployment slot status is not Running. Value is " + $hostedServiceDeploymentSlotStatus.Running)
        return $False
    }

    if (-not $hostedServiceDeploymentSlotStatus.RoleInstanceList)
	{
        Write-Host "Deployment slot has no instances configured yet."
        return $False
    }

    $notReady = $False

    Foreach ($roleInstance in $hostedServiceDeploymentSlotStatus.RoleInstanceList)
	{
        if (-not $($roleInstance.InstanceStatus -eq "ReadyRole"))
		{
            Write-Host $("Deployment slot instance " + $roleInstance.InstanceName + " has status " + $roleInstance.InstanceStatus)
            $notReady = $True
        }
    }

    if ($notReady)
	{
        Write-Host "One or more deployment instances are not yet running."
        return $False
    }

    Write-Host "Deployment slot ready for use."
    return $True
}

function Wait-AzureHostedServiceDeploymentIsReady
{
	param(
			[string]$hostedServiceName = $(throw '$hostedServiceName is a required parameter'),
			[string]$hostedServiceDeploymentSlot = $(throw '$hostedServiceDeploymentSlot is a required parameter')
		)
	
	while ( -not $(Get-AzureHostedServiceDeploymentIsReady -hostedServiceName $hostedServiceName -hostedServiceDeploymentSlot $hostedServiceDeploymentSlot) ) 
	{
        Write-Host "Deployment slot not ready, waiting 10 seconds for instances."
        Start-Sleep -s 10
    }
}

function Delete-AzureHostedServiceDeployment
{
	param(
			[string]$subscriptionName = $(throw '$subscriptionName is a required parameter'),
			[string]$hostedServiceName = $(throw '$hostedServiceName is a required parameter'),
			[string]$hostedServiceDeploymentSlot = $(throw '$hostedServiceDeploymentSlot is a required parameter')
		)
		
	Select-AzureSubscription $subscriptionName
	$deployment = Get-AzureDeployment -ServiceName $hostedServiceName -Slot $hostedServiceDeploymentSlot -errorAction "SilentlyContinue"
	if ($deployment -eq $null)
	{
		Write-Host "No existing $hostedServiceDeploymentSlot Environment Deployment for $hostedServiceName"
		Write-Host "`n"
	}
	else
	{
		$operation = Remove-AzureDeployment -ServiceName $hostedServiceName -Slot $hostedServiceDeploymentSlot -Force
	}
}

function Create-AzureHostedServiceDeployment
{
	param(
			[string]$subscriptionName = $(throw '$subscriptionName is a required parameter'),
			[string]$packageFilePath = $(throw '$packageFilePath is a required parameter'),
			[string]$configurationFilePath = $(throw '$configurationFilePath is a required parameter'),
			[string]$deploymentLabel = $(throw '$deploymentLabel is a required parameter'),
			[string]$hostedServiceName = $(throw '$hostedServiceName is a required parameter'),
			[string]$storageAccountName = $(throw '$storageAccountName is a required parameter'),
			[bool]$promoteToProductionEnvironment = $(throw '$promoteToProductionEnvironment is a required parameter'),
			[bool]$removeStagingEnvironmentAfterwards = $(throw '$removeStagingEnvironmentAfterwards is a required parameter')
		)

	Set-AzureSubscription $subscriptionName -CurrentStorageAccount $storageAccountName
	Write-Host "Creating a Deployment to Staging Environment using Package from ""$packageFilePath"" and Configuration from ""$configurationFilePath"" with label ""$deploymentLabel"""
	New-AzureDeployment -ServiceName $hostedServiceName -Slot "Staging" -Package $packageFilePath -Configuration $configurationFilePath -Label $deploymentLabel
	Write-Host "`n"
	if ($promoteToProductionEnvironment)
	{
		Write-Host "VIP swapping the Deployment from Staging Environment to Production Environment (""PromoteToProductionEnvironment"" specified as true)"
		#Wait-AzureHostedServiceDeploymentIsReady -hostedServiceName $hostedServiceName -hostedServiceDeploymentSlot "Staging"
		#Move-AzureDeployment $hostedServiceName
		#Write-Host "`n"
		# Workaround until "Move-AzureDeployment" works as advertised.
		$deployment = Get-AzureDeployment -ServiceName $hostedServiceName -Slot "Production" -errorAction "SilentlyContinue"
		if ($deployment -eq $null)
		{
			New-AzureDeployment -ServiceName $hostedServiceName -Slot "Production" -Package $packageFilePath -Configuration $configurationFilePath -Label $deploymentLabel
		}
		else
		{
			Move-AzureDeployment $hostedServiceName
		}
		
		if ($removeStagingEnvironmentAfterwards)
		{
			Delete-AzureHostedServiceDeployment -subscriptionName $subscriptionName -hostedServiceName $hostedServiceName -hostedServiceDeploymentSlot "Staging"
		}
	}
}
