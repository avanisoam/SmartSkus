# Blazor Webassembly App with Sqlite Database

### Version Information:

* .NET SDK: net7.0
* ASP.NET Core Blazor WebAssembly: 7.0.2
* Entity Framework Core: 7.0.2
* Blazorise: 1.1.4.1
* WebAssembly Authentication : 7.0.2
* IdentityModel : 6.30.0

The Smart SKU is a tool that generates an SKU for your inventory items based on the attributes and rules decided by you.
Smart SKU is free, open source and works on Web, Windows, Linux, Android, iOS and macOS!


![Index](https://github.com/Samvatsara2023/HitNtry_Ididit_Template/blob/SKU/DueVej/SmartSku.Shared/SmartSku-Screen%20Shots/IndexSmartSku.PNG)


# Using a 'real' database with a powerful API in the browser

In popular SPA frameworks such as Angular or React, the IndexedDB is often used to store client-side data. IndexedDB is more or less the de-facto database of the today’s browsers. Since the common SPA frameworks are JavaScript-based, they can communicate directly with the browser’s database. Blazor WebAssembly is different. In the case of Blazor WebAssembly, we have to add a JavaScript interop (JSInterop) wrapper to communicate with the database and thus persist data.

But is that really necessary? Since we are in the .NET world, using the Entity Framework (EF Core) as the database access approach & technology of our choice would be great. With this scenario, we have the power of EF Core to execute fast and complex SQL queries on a database without having to build the bridge to the IndexedDB.

In this article, we will see a sneak preview of using EF Core and SQLite in the browser. And almost without any JavaScript.

# Models

#### We have two model classes :
 
 1. SettingsModel
 2. SmartSkuModel

These are the classes in which we maintain the initial state of the objects and Lists required by our components. Whenever any change happen in the state of the object or list due to Add, Update or Delete opration performed on th UI these classes are also updated along with the database updation. These classes mintain the current state of the object so that we do not required to make database calls in case of any Select operation.

# Services

Service layer in the application is responsible for communicating with the APIs. We are using repository pattern in the application which calls this service layer to perform any database operations. Service layer then communicate with the API's to perform further operations.


 We are using three Services all in all:
 
  1. Inventory Service : It is responsible to get all the Items and their variations avialable in the inventory. Also it saves the enterd data from UI to the database.
  2. Master Service : All admin functionalities related to inventory is performed by Master Service.
  3. Settings Service : 

# Repository
 
 ### Repository Interface and Implementation class
 
 The componnets call the repository to perform Add, Update and Delete operations. Every time any change happen in the application, the database is updated and also the model classes are updated so that they can maintain the current state of the objects.
 

# How it Works:

There are two ways by which items can be added in Inventory.
 1. Manual Entry or One Item at a time entry.
 2. Bulk Items Entry.

### Manual Entry:

![Manual Entry](https://github.com/Samvatsara2023/HitNtry_Ididit_Template/blob/SKU/DueVej/SmartSku.Shared/SmartSku-Screen%20Shots/ManualEntry.PNG)

### After Adding Single Item (Manual Entry), Data On Dashboard:

![ManualDisplay](https://github.com/Samvatsara2023/HitNtry_Ididit_Template/blob/SKU/DueVej/SmartSku.Shared/SmartSku-Screen%20Shots/ManualDataDisplay.PNG)

### Bulk Entry :

![BulkEntry](https://github.com/Samvatsara2023/HitNtry_Ididit_Template/blob/SKU/DueVej/SmartSku.Shared/SmartSku-Screen%20Shots/AddBulk.PNG)

### After Adding Bulk Items, Data On Dashboard

![BulkDisplay](https://github.com/Samvatsara2023/HitNtry_Ididit_Template/blob/SKU/DueVej/SmartSku.Shared/SmartSku-Screen%20Shots/BulkAddDataDisplay.PNG)

#### You can also see the newly added item/ items seprately by clicking the following buttons :

![SideBarViewButtons](https://github.com/Samvatsara2023/HitNtry_Ididit_Template/blob/SKU/DueVej/SmartSku.Shared/SmartSku-Screen%20Shots/ViewAll.PNG)

#### Item1 (Default Item) Seperate View on Dashboard :

![Item1](https://github.com/Samvatsara2023/HitNtry_Ididit_Template/blob/SKU/DueVej/SmartSku.Shared/SmartSku-Screen%20Shots/ItemId1-View.PNG)

#### Item2 (Manual / Single Add Item) Seperate View on Dashboard :

![ManualDisplay](https://github.com/Samvatsara2023/HitNtry_Ididit_Template/blob/SKU/DueVej/SmartSku.Shared/SmartSku-Screen%20Shots/Item2-View.PNG)

#### Item3 (Bulk Add Items) Seperate View on Dashboard :

![BulkDisplay](https://github.com/Samvatsara2023/HitNtry_Ididit_Template/blob/SKU/DueVej/SmartSku.Shared/SmartSku-Screen%20Shots/Item3-View.PNG)


# Security:

* Login and Logout Flow.
* Equiped with JWT Authentication.
* Seperate Admin Panel.

 ### Click To Login
 
![BeforeLogin](https://github.com/Samvatsara2023/HitNtry_Ididit_Template/blob/SKU/DueVej/SmartSku.Shared/SmartSku-Screen%20Shots/BeforeLogin.PNG)

### Enter Email And Password (As of now, only click on login button on the popup to logged in)

![LoginPanel](https://github.com/Samvatsara2023/HitNtry_Ididit_Template/blob/SKU/DueVej/SmartSku.Shared/SmartSku-Screen%20Shots/LoginPopUp.PNG)

### After you enter email and password you are now logged in as Admin User and can see the admin panel at the bottom of the page

![Admin Footbar](https://github.com/Samvatsara2023/HitNtry_Ididit_Template/blob/SKU/DueVej/SmartSku.Shared/SmartSku-Screen%20Shots/CloserLook-AdminFootbar.PNG)

### Header Says Logged In User Name

![AfterLogin](https://github.com/Samvatsara2023/HitNtry_Ididit_Template/blob/SKU/DueVej/SmartSku.Shared/SmartSku-Screen%20Shots/AfterLogin.PNG)

# Admin Panel

### AdminInventory:

#### Admin User can Add, Update, Delete, View items in Inventory using this panel:

![AdminInventory](https://github.com/Samvatsara2023/HitNtry_Ididit_Template/blob/SKU/DueVej/SmartSku.Shared/SmartSku-Screen%20Shots/AdminInventory.PNG)

#### Admin Add New Item in Inventory :

![AddInventory](https://github.com/Samvatsara2023/HitNtry_Ididit_Template/blob/SKU/DueVej/SmartSku.Shared/SmartSku-Screen%20Shots/AddInventory.PNG)

#### After Adding :

![AddResult](https://github.com/Samvatsara2023/HitNtry_Ididit_Template/blob/SKU/DueVej/SmartSku.Shared/SmartSku-Screen%20Shots/AddInventoryResult.PNG)

#### Admin Edit Item in Inventory :

![EditInventory](https://github.com/Samvatsara2023/HitNtry_Ididit_Template/blob/SKU/DueVej/SmartSku.Shared/SmartSku-Screen%20Shots/EditInventory.PNG)

#### After Editing :

![EditResult](https://github.com/Samvatsara2023/HitNtry_Ididit_Template/blob/SKU/DueVej/SmartSku.Shared/SmartSku-Screen%20Shots/EditInventoryResult.PNG)

#### View a particular item in Inventory :

![ViewInventory](https://github.com/Samvatsara2023/HitNtry_Ididit_Template/blob/SKU/DueVej/SmartSku.Shared/SmartSku-Screen%20Shots/ViewInventory.PNG)

#### Delete Item In Inventory :

![Delete Inventory](https://github.com/Samvatsara2023/HitNtry_Ididit_Template/blob/SKU/DueVej/SmartSku.Shared/SmartSku-Screen%20Shots/DeleteInventory.PNG)

