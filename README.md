# Group5 iCLOTHING website

**Wen-Hsin Chen** - Created all tables & relationships for iCLOTHING SQL database. Created MVC for UserPassword, AboutUs, UserQuery, Home, Cataloging, Department, Catagory, Product implementations

**Blake Simpson** - Created logic for email controller, created logic for delivery, created models and views for email and delivery

**Olo Masiza** - Created controller logic for ShoppingCart & OrderStatus, created Views for ShoppingCart & OrderStatus.

!!!Important note: All IDs & queryNo must be 10 chars regardless of which model it's creating whether UserPassword, ShoppingCart, ItemDelivery, etc.

## Administrator login: username = admin | password = admin

Administrator permissions:

    * Permission to edit AboutUs model
    * Permission to read & 'reply' to all UserQuery models created by Customers. 'Reply' is just editting the UserQuery description and typing admin response on top of the UserQuery description. 
    * Permission to perform CRUD operations on Department, Category, and Product
    * Permission to run almost all^ customer functionalities (^no permission to add to a user's ShoppingCart)
    * Permission to perfom CRUD operations on ItemDelivery
    * Permission to read all ShoppingCarts
    * Permission to perform CRUD operations on OrderStatus
    * Permission to perform CRUD operations on Email
    
## User login: username = blobby1 | password = blobby1

User permissions:

    * Permission to read AboutUs model
    * Permission to read, edit, and delete their own corresponding UserQuery models
    * Permission to read, edit, and delete their own corresponding UserComment models
    * Permission to read & sort by name for Department, Category, and Product
    * Permission to perform CRUD operations on thier own ShoppingCart & add new ShoppingCart
    * Permission to read all UserComments 
