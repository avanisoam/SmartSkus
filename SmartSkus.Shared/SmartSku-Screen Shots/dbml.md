// Use DBML to define your database structure
// Docs: https://dbml.dbdiagram.io/docs


Table "dbo"."Category" {
  "CategoryID" INT [not null, increment]
  "Title" NVARCHAR(100)
}

Table "dbo"."OptionKey" {
  "OptionKeyID" BIGINT [not null, increment]
  "KeyName" NVARCHAR(100)
  "CategoryID" INT

Indexes {
  CategoryID [name: "IX_OptionKey_CategoryID"]
  OptionKeyID [pk]
}
}

Table "dbo"."OptionValue" {
  "OptionValueID" BIGINT [not null, increment]
  "Value" NVARCHAR(100)
  "OptionKeyID" BIGINT [not null]

Indexes {
  OptionKeyID [name: "IX_OptionValue_OptionKeyID"]
  OptionValueID [pk]
}
}

Table "dbo"."CategoryOptionKey" {
  "CategoryOptionKeyId" INT [not null, increment]
  "CategoryID" INT [not null]
  "OptionKeyID" BIGINT [not null]

Indexes {
  CategoryID [name: "IX_CategoryOptionKey_CategoryID"]
  OptionKeyID [name: "IX_CategoryOptionKey_OptionKeyID"]
  CategoryOptionKeyId [pk]
}
}

Table "dbo"."Item" {
  "ItemID" BIGINT [not null, increment]
  "ItemCode" NVARCHAR(12) [not null]
  "Name" NVARCHAR(100)
  "Description" NVARCHAR(100)
  "Region" INT
  "CategoryID" INT [not null]

Indexes {
  CategoryID [name: "IX_Item_CategoryID"]
  ItemID [pk]
}
}

Table "dbo"."ItemVariation" {
  "ItemVariationID" BIGINT [not null, increment]
  "SKUNumber" NVARCHAR(12)
  "Description" NVARCHAR(100)
  "Price" FLOAT(53) [not null]
  "Quantity" BIGINT [not null]
  "ItemID" BIGINT [not null]

Indexes {
  ItemID [name: "IX_ItemVariation_ItemID"]
  ItemVariationID [pk]
}
}

Table "dbo"."OptionVariation" {
  "OptionVariationID" BIGINT [not null, increment]
  "ItemVariationID" BIGINT [not null]
  "OptionValueID" BIGINT [not null]

Indexes {
  ItemVariationID [name: "IX_OptionVariation_ItemVariationID"]
  OptionValueID [name: "IX_OptionVariation_OptionValueID"]
  OptionVariationID [pk]
}
}

Table "dbo"."Setting" {
  "Id" BIGINT [not null]
  "Name" NVARCHAR(100)
  "SelectedBackupFormat" INT [not null]
  "Size" INT [not null]
  "Culture" NVARCHAR(50)
  "Theme" NVARCHAR(100)
  "Background" NVARCHAR(100)
  "Screen" INT [not null]
}

Table "dbo"."SkuModel" {
  "Id" INT [not null, increment]
  "ItemName" NVARCHAR(12)
  "Attribute1" NVARCHAR(12)
  "Attribute2" NVARCHAR(12)
  "Attribute3" NVARCHAR(12)
  "GenerateSku" NVARCHAR(12)
  "ItemID" BIGINT [not null]
  "Description" NVARCHAR(100)
  "Quantity" BIGINT [not null]

Indexes {
  ItemID [name: "IX_SkuModel_ItemID"]
  Id [pk]
}
}

Ref "FK_OptionKey_Category_CategoryID":"dbo"."Category"."CategoryID" < "dbo"."OptionKey"."CategoryID"

Ref "FK_OptionValue_OptionKey_OptionKeyID":"dbo"."OptionKey"."OptionKeyID" < "dbo"."OptionValue"."OptionKeyID" [delete: cascade]

Ref "FK_CategoryOptionKey_Category_CategoryID":"dbo"."Category"."CategoryID" < "dbo"."CategoryOptionKey"."CategoryID" [delete: cascade]

Ref "FK_CategoryOptionKey_OptionKey_OptionKeyID":"dbo"."OptionKey"."OptionKeyID" < "dbo"."CategoryOptionKey"."OptionKeyID" [delete: cascade]

Ref "FK_Item_Category_CategoryID":"dbo"."Category"."CategoryID" < "dbo"."Item"."CategoryID" [delete: cascade]

Ref "FK_ItemVariation_Item_ItemID":"dbo"."Item"."ItemID" < "dbo"."ItemVariation"."ItemID" [delete: cascade]

Ref "FK_OptionVariation_ItemVariation_ItemVariationID":"dbo"."ItemVariation"."ItemVariationID" < "dbo"."OptionVariation"."ItemVariationID" [delete: cascade]

Ref "FK_OptionVariation_OptionValue_OptionValueID":"dbo"."OptionValue"."OptionValueID" < "dbo"."OptionVariation"."OptionValueID" [delete: cascade]

Ref "FK_SkuModel_Item_ItemID":"dbo"."Item"."ItemID" < "dbo"."SkuModel"."ItemID" [delete: cascade]
