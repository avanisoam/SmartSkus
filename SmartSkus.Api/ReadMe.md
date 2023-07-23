# Smart Sku

## Introduction

Inventory management
   1. Create APIs to
      1. Create inventory items
         1. With SKU, description and quantity
         2. Adding inventory with unknown SKU, it should create new.
         3. Adding inventory with known SKU, it should add the quantity to existing
      2. Remove a defined quantity for a specific SKU
      3. List of all inventory


------------------------------------------------------------------

Inventory Apis:

1. Create inventory items
         1. With SKU, description and quantity
         2. Adding inventory with unknown SKU, it should create new.
         3. Adding inventory with known SKU, it should add the quantity to existing

Request Url:
http://localhost:5000/api/inventory/add

Verb: POST

Input:
{
  "sku": "HNM-SM-RED",
  "description": "HnM-T-Shirt Size-Small Colour-Red",
  "quantity": 100
}

Output:
{
  "itemVariationID": 4,
  "skuNumber": "HNM-SM-RED",
  "description": "Variation: HnM-T-Shirt Size-Small Colour-Red",
  "price": 0,
  "quantity": 100,
  "itemID": 2,
  "item": {
    "itemID": 2,
    "itemCode": "HNM-SM-RED",
    "name": "Nmae: HNM-SM-RED",
    "description": "Item: HnM-T-Shirt Size-Small Colour-Red",
    "region": 4,
    "categoryID": 1,
    "category": null,
    "itemVariations": []
  },
  "optionVariations": null
}

------------------------------------

Find Item By SKU :

Request Url:
http://localhost:5000/api/inventory/items/HNM-SM-RED

Verb:
GET

Input:
skuNumber : HNM-SM-RED

Output:
{
  "itemVariationID": 4,
  "skuNumber": "HNM-SM-RED",
  "description": "Variation: HnM-T-Shirt Size-Small Colour-Red",
  "price": 0,
  "quantity": 100,
  "itemID": 2,
  "item": null,
  "optionVariations": null
}

-----------------------------------------

 Remove a defined quantity for a specific SKU



Verb:
PATCH

Input:
skuNumber : HNM-SM-RED
quantity : 20

Output:
Success

----------------------------------------

List of all inventory:

Request Url:
http://localhost:5000/api/inventory/items

Output:
[
  {
    "itemID": 1,
    "itemCode": "T-SH",
    "name": "T-Shirt",
    "description": "None of the above",
    "region": 4,
    "categoryID": 2,
    "category": {
      "categoryID": 2,
      "title": "Apperal",
      "items": []
    },
    "itemVariations": [
      {
        "itemVariationID": 1,
        "skuNumber": "T-SH-SM",
        "description": "",
        "price": 0,
        "quantity": 0,
        "itemID": 1,
        "optionVariations": null
      },
      {
        "itemVariationID": 2,
        "skuNumber": "T-SH-ME",
        "description": "",
        "price": 0,
        "quantity": 0,
        "itemID": 1,
        "optionVariations": null
      },
      {
        "itemVariationID": 3,
        "skuNumber": "T-SH-LA",
        "description": "",
        "price": 0,
        "quantity": 0,
        "itemID": 1,
        "optionVariations": null
      }
    ]
  },
  {
    "itemID": 2,
    "itemCode": "HNM-SM-RED",
    "name": "Nmae: HNM-SM-RED",
    "description": "Item: HnM-T-Shirt Size-Small Colour-Red",
    "region": 4,
    "categoryID": 1,
    "category": {
      "categoryID": 1,
      "title": "None",
      "items": []
    },
    "itemVariations": [
      {
        "itemVariationID": 4,
        "skuNumber": "HNM-SM-RED",
        "description": "Variation: HnM-T-Shirt Size-Small Colour-Red",
        "price": 0,
        "quantity": 180,
        "itemID": 2,
        "optionVariations": null
      }
    ]
  }
]


