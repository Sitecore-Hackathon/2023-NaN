<#
    .SYNOPSIS
    Build and Publish ApetitoDe project

    .PARAMETER Configuration
    Configuration Debug, Dev, Qa or Prod
#>

param(
    [String] $configuration = "Debug",
    [String] $deployTo
)

. $PSScriptRoot\publish-base.ps1

$publishSource = "website"

PublishContent -PublishSource $publishSource -Configuration $configuration -WebsiteFolderPath $deployTo