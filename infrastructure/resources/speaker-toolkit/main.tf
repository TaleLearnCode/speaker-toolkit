data "azurerm_client_config" "current" {}

module "AzureResourceTypes" {
  source = "../../modules/azure-resource-types"
}

module "ResourceTags" {
  source = "../../modules/resource-tags"
}

# Resource Group (e.g. rg-SiteSpeaker-dev-eus2)
resource "azurerm_resource_group" "rgSpeakerToolkit" {
  name     = "${module.AzureResourceTypes.resource_type_resource_group}-SpeakerToolkit-${var.resource_name_environment}-${var.resource_name_location}"
  location = var.azure_region
  tags = {
    Product     = module.ResourceTags.resource_tag_product_name
    Criticality = module.ResourceTags.resource_tag_criticality
    CostCenter  = "${module.ResourceTags.resource_tag_cost_center}-${var.resource_name_environment}"
    DR          = module.ResourceTags.resource_tag_dr
    Env         = var.resource_name_environment
  }
}

# API Management (e.g. apim-SiteSpeaker-dev-eus2)
resource "azurerm_api_management" "apimSpeakerToolkit" {
  name     = "${module.AzureResourceTypes.resource_type_api_management}-SpeakerToolkit-${var.resource_name_environment}-${var.resource_name_location}"
  location            = azurerm_resource_group.rgSpeakerToolkit.location
  resource_group_name = azurerm_resource_group.rgSpeakerToolkit.name
  publisher_name      = "Speaker Toolkit"
  publisher_email     = "chadgreen@chadgreen.com"
  sku_name = "Developer_1"
}