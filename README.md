# Group5 iCLOTHING website

**Wen-Hsin Chen** - Created all tables & relationships for iCLOTHING SQL database. Created MVC for UserPassword, AboutUs, UserQuery, Home, Cataloging, Department, Catagory, Product implementations
**Blake Simpson** - Created logic for email controller, created logic for delivery, created models and views for email and delivery

Important notes: All IDs & queryNo must be 10 chars regardless of whether model it's creating whether UserPassword, ShoppingCart, ItemDelivery, etc.

Administrator login: username = admin | password = admin

Administrator permissions:

    * Permission to edit AboutUs model
    * Permission to read & 'reply' to all UserQuery models created by Customers 
    * Permission to perform CRUD operations on Department, Category, and Product
    * Permission to run almost all^ customer functionalities (^no permission to add to ShoppingCart)
    * Permission to perfom CRUD operations on ItemDelivery
    
User login: username = blobby1 | password = blobby1

User permissions:

    * Permission to read AboutUs model
    * Permission to read, edit, and delete their own corresponding UserQuery models
    * Permission to perform read & sort by name functions for Department, Category, and Product
    * Permission to perform CRUD operations on thier own ShoppingCart
