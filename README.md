# What is a Smart SKU?

The Smart SKU is a tool that generates an SKU for your inventory items
based on the attributes and rules decided by you.
Smart SKU is free, open source and works on Web, Windows, Linux, Android, iOS and macOS!

# What is SKU (Stock Keeping Unit)

A SKU, or Stock Keeping Unit, is a unique number used to internally track a business' inventory.
Retailers use SKUs to track their inventory and sales, which can provide analytical data that are beneficial to have in order to develop relationships with your vendors and customers.

# Why is SKU important?

SKU numbers are important because they ensure your inventory is accurately tracked, and help you pinpoint when exactly to order new products so your merchandise never goes out of stock.

# Why Smart SKU is used?

 * To uniquely identify and track individual products within a company's inventory system.
 * To manage their inventory more efficiently.
 * To track the movement of each product from the time it enters the inventory until it is sold.
 * To pinpoint when to order new products, so your merchandise never goes out of stock.

# How to Run the Project :

* Open the project using Visual Studio
* Right click on the Solution 'SmartSkus' -> Properties -> Multiple startup projects -> Select(Start) - SmarSkus.Api and SmartSkus.Wasm -> OK
* Note: The database will automatically created upon run of the project, as InMemory Db is used in this project.

# Features

### Inventory Management - Dashboard will display all items in Inventory with Sku, Description and quantity:
 
 * User can add new item in inventory by providing SKU, Description and Quantity.
 * If user adds unknown SKU, it will create new item in inventory.
 * If user adds known SKU, it will add the quantity to existing item in inventory.
 * If user adds negetive quantity with known SKU, it will substract the quantity to existing item in inventory.

#### Dashboard :

![Dashboard](https://github.com/avanisoam/SmartSkus/blob/master/SmartSkus.Shared/SmartSku-Screen%20Shots/DefaultMainPage.PNG)

#### Inventory Management Form (User can add new item in inventory by providing SKU, Description and Quantity):

![InventoryManagementForm](https://github.com/avanisoam/SmartSkus/blob/master/SmartSkus.Shared/SmartSku-Screen%20Shots/InventoryManagementForm.PNG)

#### Result :

![InventoryItemAddUsingSKU](https://github.com/avanisoam/SmartSkus/blob/master/SmartSkus.Shared/SmartSku-Screen%20Shots/InventoryItemAdd.PNG)


### SKU Management - Dashboard displays all unique Sku's in inventory and their variations:
 
 * User can manually add new Item by specifying one or more attributes, whose Sku will be generated automatically.
 * User can add bulk items by specifying item name and selecting preconfigured options, whose Skus will be generated automatically.

#### Add Single Item in Inventory and SKU will be generated automatically along with QR-Code.

![ManualAddForm](https://github.com/avanisoam/SmartSkus/blob/master/SmartSkus.Shared/SmartSku-Screen%20Shots/ManualAddForm.PNG)

#### Result :

![ManualAddResult](https://github.com/avanisoam/SmartSkus/blob/master/SmartSkus.Shared/SmartSku-Screen%20Shots/ManualItemAdd.PNG)

#### Add Bulk Items in Inventory and SKUs will be generated automatically along with QR-Codes.

![BulkAddForm](https://github.com/avanisoam/SmartSkus/blob/master/SmartSkus.Shared/SmartSku-Screen%20Shots/BulkAddForm.PNG)

#### Result :

![BulkAddResult](https://github.com/avanisoam/SmartSkus/blob/master/SmartSkus.Shared/SmartSku-Screen%20Shots/BulkAddItem.PNG)


### Available Items in the Inventory Can be Viewed All or Seperately using following buttons in left panel :

![AvailableItemsPanel](https://github.com/avanisoam/SmartSkus/blob/master/SmartSkus.Shared/SmartSku-Screen%20Shots/SeeAllOrParticular.PNG)

### Admin Panel - After Login user can manage Master Data:

 * Admin User can manage(perform CRUD operations) items in Inventory.
 * Admin User can divide the items in inventory by adding different Categories.(e.g - Apperals, Electronics etc.)
 * For Each defined category different sub-options, can be added.(e.g - T-Shirt(Item Name), Apperal(Category), Size(Option Key), Small,Medium,Large(Option Values).
 * Admin User can manage(perform CRUD operations) all categories and sub options.


# Admin Panel

### AdminInventory:

#### Admin User can Add, Update, Delete, View items in Inventory using this panel:

![AdminInventory](https://github.com/avanisoam/SmartSkus/blob/master/SmartSkus.Shared/SmartSku-Screen%20Shots/Admin%20Inventory.PNG)

### Admin Category:

#### Admin User can add Categories to divide Inventory items into.

![AdminCategory](https://github.com/avanisoam/SmartSkus/blob/master/SmartSkus.Shared/SmartSku-Screen%20Shots/AdminCategory.PNG)

#### The Categories can futher select sub categories options from here:

![CategoryView](https://github.com/avanisoam/SmartSkus/blob/master/SmartSkus.Shared/SmartSku-Screen%20Shots/AdminCategoryView.PNG)

### Admin Options

#### The options selected under Category View button can be managed from AdminOptions:

![AdminOptions](https://github.com/avanisoam/SmartSkus/blob/master/SmartSkus.Shared/SmartSku-Screen%20Shots/AdminOptions.PNG)

#### These Options can have multiple sub options:

![AdminSubOptions](https://github.com/avanisoam/SmartSkus/blob/master/SmartSkus.Shared/SmartSku-Screen%20Shots/AdminOptionsSubOptions.PNG)

### Admin Settings

![AdminSettings](https://github.com/avanisoam/SmartSkus/blob/master/SmartSkus.Shared/SmartSku-Screen%20Shots/AdminSettings.PNG)

### QR Code
 * Each item in the Inventory has its own QR-Code so that item can be identified by scanning the QR-Code.

# Formats

## File export:

 * JSON export
 * YAML export

#### Download

![Export](https://github.com/avanisoam/SmartSkus/blob/master/SmartSkus.Shared/SmartSku-Screen%20Shots/JsonYamlExport.PNG)

# Themes

### There are 26 color themes:
 * Default
 * Cerulean
 * Cosmo
 * Cyborg
 * Darkly
 * Flatly
 * Journal
 * Litera
 * Lumen
 * Lux
 * Materia
 * Minty
 * Morph
 * Pulse
 * Quartz
 * Sandstone
 * Simplex
 * Sketchy
 * Slate
 * Solar
 * Spacelab
 * Superhero
 * United
 * Vapor
 * Yeti
 * Zephyr

# Security:

* Login and Logout Flow.
* Equiped with JWT Authentication.
* Seperate Admin Panel.

 ### Click To Login
 
![BeforeLogin](https://github.com/avanisoam/SmartSkus/blob/master/SmartSkus.Shared/SmartSku-Screen%20Shots/Login%20Header.PNG)

### Enter Email And Password (As of now, only click on login button on the popup to logged in)

![LoginPanel](https://github.com/avanisoam/SmartSkus/blob/master/SmartSkus.Shared/SmartSku-Screen%20Shots/Login%20Popup.PNG)

### After you enter email and password you are now logged in as Admin User and can see the admin panel at the bottom of the page

![Admin Footbar](https://github.com/avanisoam/SmartSkus/blob/master/SmartSkus.Shared/SmartSku-Screen%20Shots/Admin%20Panel.PNG)

### Header Says Logged In User Name

![AfterLogin](https://github.com/avanisoam/SmartSkus/blob/master/SmartSkus.Shared/SmartSku-Screen%20Shots/AfterLoginHeader.PNG)

# About

### Smart Sku is:

* Free
* Open source: GitHub
* Cross platform: Planned for Web, Windows, Android, iOS, macOS and Linux

### Made with latest technologies:

* .NET
* C#
* Blazor
* Blazorise
* Bootstrap
* EntityFramework Core
* InMemory/MS Sql Db
* REST API
* GITHub Version Control
* Bootswatch
* Font Awesome
* WebAssembly
* WebView2
* YamlDotNet

### Languages

  * English
  * Hindi

### Help 

#### Lower Left most button (?) is to describe the icons used in the application

![Help](https://github.com/avanisoam/SmartSkus/blob/master/SmartSkus.Shared/SmartSku-Screen%20Shots/Help.PNG)

### About

#### Lower Right most button (i) Tells some breif description about the application

![About](https://github.com/avanisoam/SmartSkus/blob/master/SmartSkus.Shared/SmartSku-Screen%20Shots/About.PNG)


### Setting

#### Can set the look and feel and language etc. :

![LeftUpperSetting](https://github.com/avanisoam/SmartSkus/blob/master/SmartSkus.Shared/SmartSku-Screen%20Shots/LeftOptions.PNG)

### DB Diagram :

![DbDiagram](https://github.com/avanisoam/SmartSkus/blob/master/SmartSkus.Shared/SmartSku-Screen%20Shots/Dbml.PNG)
