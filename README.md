# Cashier Api Endpoints
---
## 1. Introduction:
This API is designed to manage Cashier System.
It provides functionalities such as creating, retrieving, 
updating, and deleting all data related to the business model for Cashier System.
This documentation is intended for any front-end developer in general that would like to build a Cashier system.
Before using this API, ensure that you have .NET Core 6 or later installed in your system.

---

## 2. Setup and Installation:

### Prerequisites
- .NET Core 3.1 SDK or later
- An IDE such as Visual Studio or Visual Studio Code

---

## 3. API Endpoints:

### 1./api/auth endpoints:
**POST /api/auth/login**

**Authorization:**

- Authenticated Users  

Login users

**Parameters:**
- none

**Example request:**


   
      {
      "id": 1,
      "phoneNumber": "00966565432345",
      "password": "testPassword2022#"
       }
    
   


**POST /api/auth/register**

register new user

**Authorization:**
- none

**Parameters:**

- `id` : (required - int field)
- `firstName` : (required - string field)
- `lastName` : (required - string field)
- `email` : (required - string field)
- `password` : (required - string field)
- `phoneNumber` : (required - string field)

**Returns:**
- registered user data with token details

**Example request:**

    
      {
      "id": 1,
      "firstName": "mohammed",
      "lastName": "ali",
      "phoneNumber": "0966565434234",
      "email": "test@gmail.com",
      "password": "testPAssword20#"
       }

**Example response:**

    
      {
      "id": 1,
      "isAuthenticated": true,
      "token": "token",
      "firstName": "mohammed",
      "lastName": "ali",
      "phoneNumber": "0966565434234",
      "email": "test@gmail.com",
      "refreshToken": "refresh_token",
      "refreshTokenExpiration": "2023-9-20",
      "createdAt": "2022-3-24",
      "createdAt": null
       }
    

**PUT /api/auth/revoke-token**

Revokes submitted refresh token

**Authorization:**
- Authenticated users


**Parameters:**

- `refreshToken` : (required - string field)


**Returns:**
- 200 Success if submitted data was correct
- 400 Bad Request if submitted data was not correct

**Example Request Body Data:**

   
    {
      "refreshToken": "refresh_token"
    
    }

**Example response:**

    
      {
      "id": 1,
      "isAuthenticated": true,
      "token": "token",
      "firstName": "mohammed",
      "lastName": "ali",
      "phoneNumber": "0966565434234",
      "email": "test@gmail.com",
      "refreshToken": "refresh_token",
      "refreshTokenExpiration": "2023-9-20",
      "createdAt": "2022-3-24",
      "createdAt": null
       }
    

**POST /api/auth/add-user-to-role**

Adds a user to submitted role
 
**Authorization:**
- Authenticated users

**Parameters:**

- `userId` : (required - string field)
- `role` : (required - string field)


**Returns:**
- 200 Success 
- 400 Bad Request if submitted data was not correct


**Example Request Body Data:**

   
      {
      "userId": "user_id"
      "role": "role_name"
      }
    



### 2./api/users endpoints:
**GET /api/users**

**Authorization:**

- Authenticated Users  

Get all users

**Parameters:**
- none

**Example response:**


   
      {
      "id": 1,
      "firstName": "ali",
      "lastName": "sami",
      "email": "test@gmail.com",
      "imagePath": "image_path",
      "phoneNumber": "00966565432345",
      "createdAt": "2022-3-24",
      "createdAt": null
       }
    
   


**GET /api/users/(id)**

Gets requested user 

**Authorization:**
- Authenticated users

**Parameters:**

- `id` : (required - string field)


**Returns:**
- registered user record

**Example response:**

    
      {
      "id": "user-id",
      "firstName": "mohammed",
      "lastName": "ali",
      "phoneNumber": "0966565434234",
      "email": "test@gmail.com",
      "password": "testPAssword20#",
      "createdAt": "2022-3-24",
      "createdAt": null
       }



**PUT /api/users/id**

Update a user

**Authorization:**
- Authenticated users


**Parameters:**

- `id` : (required - string field)
- `firstName` : (required - string field)
- `lastName` : (required - string field)
- `imagePath` : (required - string field)
- `phoneNumber` : (required - string field)
- `email` : (required - string field)
- `password` : (required - string field)


**Returns:**
- 200 Success if submitted data was correct
- 400 Bad Request if submitted data was not correct

**Example Request Body Data:**

   
    {
      "id": "user-id"
    
    }

**Example response:**

    
      {
      "id": "user-id",
      "firstName": "mohammed",
      "lastName": "ali",
      "phoneNumber": "0966565434234",
      "email": "test@gmail.com",
      "password": "testPAssword20#",
      "createdAt": "2022-3-24",
      "createdAt": null
       }
    

**PUT /api/users/id/image**

Update a user image
 
**Authorization:**
- Authenticated users

**Parameters:**

- `id` : (required - string field)
- `imageFile` : (required - string field)


**Returns:**
- 200 Success 
- 400 Bad Request if submitted data was not correct
- 404 If no user with submitted id


**Example Request Body Data:**

   
      {
      "id": "user_id",
      "imageFile": "image_path"
      }
    

**PATCH /api/users/id**

Updates the record with submitted id field using PATCH method

**Authorization:**
- Authenticated users

**Parameters:**

- `id` : (required - string field)
- `operationType` : (not required - int field)
- `path` : (required - string field- property name)
- `op` : (required - string field - `replace`)
- `from` : (not required - string field)
- `value` : (required - property’s type)

**Returns:**
- 204 Success no content
- 400 Bad Request if submitted data was not correct
- 404 Not found if there is no user record with submitted id

**Example Request Body Data:**

   
    {
    
          "operationType": 0,
          "path": "/propertyName",
            "op": "replace",
           "from": "string",
            "value": "string"
    }



**DELETE /api/users/id**

deletes the record with the submitted id field.


**Authorization:**
- Authenticated users

**Parameters:**

- `id` : (required - string field)


**Returns:**
- 200 Ok 
- 400 Bad Request if submitted data was not correct
- 404 Not found if there is no  record with submitted id

**Example Request Body Data:**

**DELETE /api/users/id**

### 3./api/brands endpoints:
**GET /api/brands**

**Authorization:**

- Authenticated Users  

Get all brands

**Parameters:**
- none

**Example response:**


   
      {
      "id": 1,
      "name": "name",
      "description": "description",
      "logoPath": "logo_path",
      "createdAt": "2022-3-24",
      "createdAt": null
       }
    
   


**GET /api/brands/(id:int)**

Gets requested brand

**Authorization:**
- Authenticated users

**Parameters:**

- `id` : (required - string field)


**Returns:**
- registered user record

**Example response:**

    
      {
      "id": 1,
      "name": "name",
      "description": "description",
      "logoPath": "logo_path",
      "createdAt": "2022-3-24",
      "createdAt": null
       }

**POST /api/brands/**

Adds a brand

**Authorization:**
- Authenticated users


**Parameters:**

- `id` : (required - int field)
- `name` : (required - string field)
- `description` : (required - string field)
- `logoPath` : (required - string field)
- `logoFile` : (required - string field)


**Returns:**
- 200 Success if submitted data was correct
- 400 Bad Request if submitted data was not correct

**Example Request Body Data:**

   
    {
      "id": 1,
      "name": "name",
      "description": "description",
      "logoPath": "logo_path"
    
    }




**PUT /api/brands/(id:int)**

Update a brand with submitted id

**Authorization:**
- Authenticated users


**Parameters:**

- `id` : (required - int field)
- `name` : (required - string field)
- `description` : (required - string field)
- `logoPath` : (required - string field)


**Returns:**
- 200 Success if submitted data was correct
- 400 Bad Request if submitted data was not correct

**Example Request Body Data:**

    
      {
      "id": 1,
      "name": "brand_name",
      "description": "description",
      "logoPath": "logo_path"
       }
    

**PUT /api/brands/(id:int)/image**

Update a brand logo image
 
**Authorization:**
- Authenticated users

**Parameters:**

- `id` : (required - int field)
- `logoFile` : (required - string field)


**Returns:**
- 200 Success 
- 400 Bad Request if submitted data was not correct
- 404 If no user with submitted id


**Example Request Body Data:**

   
      {
      "id": 1,
      "imageFile": "logo_file"
      }
    

**PATCH /api/brands/(id:int)**

Updates the record with submitted id field using PATCH method

**Authorization:**
- Authenticated users

**Parameters:**

- `id` : (required - int field)
- `operationType` : (not required - int field)
- `path` : (required - string field- property name)
- `op` : (required - string field - `replace`)
- `from` : (not required - string field)
- `value` : (required - property’s type)

**Returns:**
- 204 Success no content
- 400 Bad Request if submitted data was not correct
- 404 Not found if there is no user record with submitted id

**Example Request Body Data:**

   
    {
    
          "operationType": 0,
          "path": "/propertyName",
          "op": "replace",
          "from": "string",
          "value": "string"
    }



**DELETE /api/brands/(id:int)**

deletes the record with the submitted id field.


**Authorization:**
- Authenticated users

**Parameters:**

- `id` : (required - int field)


**Returns:**
- 200 Ok 
- 400 Bad Request if submitted data was not correct
- 404 Not found if there is no record with submitted id

**Example Request Body Data:**

**DELETE /api/brands/(id:int)**

### 4./api/companies endpoints:
**GET /api/companies**

**Authorization:**

- Authenticated Users  

Get all companies

**Parameters:**
- none

**Example response:**


   
      {
      "id": 1,
      "name": "name",
      "description": "description",
      "email": "company@test.com",
      "phoneNumber": "00966545423123",
      "createdAt": "2022-3-24",
      "createdAt": null
       }
    
   


**GET /api/companies/(id:int)**

Gets requested company

**Authorization:**
- Authenticated users

**Parameters:**

- `id` : (required - int field)


**Returns:**
- registered company record

**Example response:**

    
      {
      "id": 1,
      "name": "name",
      "description": "description",
      "email": "company@test.com",
      "phoneNumber": "00966545423123",
      "createdAt": "2022-3-24",
      "createdAt": null
       }

**POST /api/companies**

Adds a company

**Authorization:**
- Authenticated users


**Parameters:**

- `id` : (required - int field)
- `name` : (required - string field)
- `description` : (required - string field)
- `email` : (required - string field)
- `phoneNumber` : (required - string field)


**Returns:**
- 200 Success if submitted data was correct
- 400 Bad Request if submitted data was not correct

**Example Request Body Data:**

   
    {
      "id": 1,
      "name": "name",
      "description": "description",
      "email": "company@test.com",
      "phoneNumber": "00966545423123"
    
    }




**PUT /api/companies/(id:int)**

Update a company with submitted id

**Authorization:**
- Authenticated users


**Parameters:**


- `id` : (required - int field)
- `name` : (required - string field)
- `description` : (required - string field)
- `email` : (required - string field)
- `phoneNumber` : (required - string field)


**Returns:**
- 200 Success if submitted data was correct
- 400 Bad Request if submitted data was not correct

**Example Request Body Data:**

    
      {
      "id": 1,
      "name": "name",
      "description": "description",
      "email": "company@test.com",
      "phoneNumber": "00966545423123"
       }
    

**PATCH /api/companies/(id:int)**

Updates the record with submitted id field using PATCH method

**Authorization:**
- Authenticated users

**Parameters:**

- `id` : (required - int field)
- `operationType` : (not required - int field)
- `path` : (required - string field- property name)
- `op` : (required - string field - `replace`)
- `from` : (not required - string field)
- `value` : (required - property’s type)

**Returns:**
- 204 Success no content
- 400 Bad Request if submitted data was not correct
- 404 Not found if there is no user record with submitted id

**Example Request Body Data:**

   
    {
    
          "operationType": 0,
          "path": "/propertyName",
          "op": "replace",
          "from": "string",
          "value": "string"
    }



**DELETE /api/companies/(id:int)**

deletes the record with the submitted id field.


**Authorization:**
- Authenticated users

**Parameters:**

- `id` : (required - int field)


**Returns:**
- 200 Ok 
- 400 Bad Request if submitted data was not correct
- 404 Not found if there is no record with submitted id

**Example Request Body Data:**

**DELETE /api/companies/(id:int)**

### 5./api/invoices endpoints:
**GET /api/invoices**

**Authorization:**

- Authenticated Users  

Get all invoices

**Parameters:**
- none

**Example response:**


   
      {
      "id": 1,
      "number": "number",
      "VAT": 0.15,
      "deliveryPrice": 30,
      "userId": "user-id",
      "createdAt": "2022-3-24",
      "createdAt": null
       }
    
   


**GET /api/invoices/(id:int)**

Gets requested invoice 

**Authorization:**
- Authenticated users

**Parameters:**

- `id` : (required - int field)


**Returns:**
- an invoice record

**Example response:**

    
      {
      "id": 1,
      "number": "number",
      "VAT": 0.15,
      "deliveryPrice": 30,
      "userId": "user-id",
      "createdAt": "2022-3-24",
      "createdAt": null
       }

**POST /api/invoices**

Adds an invoice

**Authorization:**
- Authenticated users


**Parameters:**

- `id` : (required - int field)
- `number` : (required - string field)
- `VAT` : (required - string field)
- `deliveryPrice` : (required - string field)



**Returns:**
- 200 Success if submitted data was correct
- 400 Bad Request if submitted data was not correct

**Example Request Body Data:**

   
    {
      "id": 1,
      "number": "number",
      "VAT": 0.15,
      "deliveryPrice": 30,
      "userId": "user-id"
    
    }




**PUT /api/invoices/(id:int)**

Update an invoice with submitted id

**Authorization:**
- Authenticated users


**Parameters:**


- `id` : (required - int field)
- `number` : (required - string field)
- `VAT` : (required - string field)
- `deliveryPrice` : (required - string field)




**Returns:**
- 200 Success if submitted data was correct
- 400 Bad Request if submitted data was not correct

**Example Request Body Data:**

    
      {
      "id": 1,
      "number": "number",
      "VAT": 0.15,
      "deliveryPrice": 30,
      "userId": "user-id"
       }
    

**PATCH /api/invoices/(id:int)**

Updates the record with submitted id field using PATCH method

**Authorization:**
- Authenticated users

**Parameters:**

- `id` : (required - int field)
- `operationType` : (not required - int field)
- `path` : (required - string field- property name)
- `op` : (required - string field - `replace`)
- `from` : (not required - string field)
- `value` : (required - property’s type)

**Returns:**
- 204 Success no content
- 400 Bad Request if submitted data was not correct
- 404 Not found if there is no record with submitted id

**Example Request Body Data:**

   
    {
    
          "operationType": 0,
          "path": "/propertyName",
          "op": "replace",
          "from": "string",
          "value": "string"
    }



**DELETE /api/invoices/(id:int)**

deletes the record with the submitted id field.


**Authorization:**
- Authenticated users

**Parameters:**

- `id` : (required - int field)


**Returns:**
- 200 Ok 
- 400 Bad Request if submitted data was not correct
- 404 Not found if there is no record with submitted id

**Example Request Body Data:**

**DELETE /api/invoices/(id:int)**

### 6./api/invoice-items endpoints:
**GET /api/invoice-items**

**Authorization:**

- Authenticated Users  

Get all invoice items

**Parameters:**
- none

**Example response:**


   
      {
      "id": 1,
      "name": "name",
      "count": 10,
      "discount": 0,
      "totalPrice": 30,
      "invoiceId": 1,
      "productId": 1,
      "createdAt": "2022-3-24",
      "createdAt": null
       }
    
   


**GET /api/invoice-items/(id:int)**

Gets requested invoice item

**Authorization:**
- Authenticated users

**Parameters:**

- `id` : (required - int field)


**Returns:**
- an invoice record

**Example response:**

    
      {
      "id": 1,
      "name": "name",
      "count": 10,
      "discount": 0,
      "totalPrice": 30,
      "invoiceId": 1,
      "productId": 1,
      "createdAt": "2022-3-24",
      "createdAt": null
       }

**POST /api/invoice-items**

Adds an invoice item

**Authorization:**
- Authenticated users


**Parameters:**

- `id` : (required - int field)
- `name` : (required - string field)
- `count` : (required - int field)
- `discount` : (not required - double field)
- `totalPrice` : (not required - double field)
- `invoiceId` : (required - int field)
- `productId` : (required - int field)



**Returns:**
- 200 Success if submitted data was correct
- 400 Bad Request if submitted data was not correct

**Example Request Body Data:**

   
    {
      "id": 1,
      "name": "name",
      "count": 10,
      "discount": 0,
      "totalPrice": 30,
      "invoiceId": 1,
      "productId": 1
    
    }




**PUT /api/invoice-items/(id:int)**

Update an invoice item with submitted id

**Authorization:**
- Authenticated users


**Parameters:**


- `id` : (required - int field)
- `name` : (required - string field)
- `count` : (required - int field)
- `discount` : (not required - double field)
- `totalPrice` : (not required - double field)
- `invoiceId` : (required - int field)
- `productId` : (required - int field)




**Returns:**
- 200 Success if submitted data was correct
- 400 Bad Request if submitted data was not correct

**Example Request Body Data:**

    
      {
      "id": 1,
      "name": "name",
      "count": 10,
      "discount": 0,
      "totalPrice": 30,
      "invoiceId": 1,
      "productId": 1
       }
    

**PATCH /api/invoice-items/(id:int)**

Updates the record with submitted id field using PATCH method

**Authorization:**
- Authenticated users

**Parameters:**

- `id` : (required - int field)
- `operationType` : (not required - int field)
- `path` : (required - string field- property name)
- `op` : (required - string field - `replace`)
- `from` : (not required - string field)
- `value` : (required - property’s type)

**Returns:**
- 204 Success no content
- 400 Bad Request if submitted data was not correct
- 404 Not found if there is no record with submitted id

**Example Request Body Data:**

   
    {
    
          "operationType": 0,
          "path": "/propertyName",
          "op": "replace",
          "from": "string",
          "value": "string"
    }



**DELETE /api/invoice-items/(id:int)**

deletes the record with the submitted id field.


**Authorization:**
- Authenticated users

**Parameters:**

- `id` : (required - int field)


**Returns:**
- 200 Ok 
- 400 Bad Request if submitted data was not correct
- 404 Not found if there is no record with submitted id

**Example Request Body Data:**

**DELETE /api/invoice-items/(id:int)**

### 7./api/products endpoints:
**GET /api/products**

**Authorization:**

- Authenticated Users  

Get all products

**Parameters:**
- none

**Example response:**


   
      {
      "id": 1,
      "name": "name",
      "barcode": "barcode",
      "description": "description",
      "price": 10,
      "imagePath": "image_path"
      "createdAt": "2022-3-24",
      "createdAt": null
       }
    
   


**GET /api/products/(id:int)**

Gets requested product item

**Authorization:**
- Authenticated users

**Parameters:**

- `id` : (required - int field)


**Returns:**
- a product record

**Example response:**

    
      {
      "id": 1,
      "name": "name",
      "barcode": "barcode",
      "description": "description",
      "price": 10,
      "imagePath": "image_path",
      "createdAt": "2022-3-24",
      "createdAt": null
       }

**POST /api/products**

Adds a product item

**Authorization:**
- Authenticated users


**Parameters:**

- `id` : (required - int field)
- `name` : (required - string field)
- `barcode` : (required - string field)
- `description` : (not required - string field)
- `price` : (not required - double field)
- `imagePath` : (required - string field)




**Returns:**
- 200 Success if submitted data was correct
- 400 Bad Request if submitted data was not correct

**Example Request Body Data:**

   
    {
      "id": 1,
      "name": "name",
      "barcode": "barcode",
      "description": "description",
      "price": 10,
      "imagePath": "image_path"
    }




**PUT /api/products/(id:int)**

Update a product with submitted id

**Authorization:**
- Authenticated users


**Parameters:**


- `id` : (required - int field)
- `name` : (required - string field)
- `barcode` : (required - string field)
- `description` : (not required - string field)
- `price` : (not required - double field)
- `imagePath` : (required - string field)




**Returns:**
- 200 Success if submitted data was correct
- 400 Bad Request if submitted data was not correct

**Example Request Body Data:**

    
      {
      "id": 1,
      "name": "name",
      "barcode": "barcode",
      "description": "description",
      "price": 10,
      "imagePath": "image_path"
       }
    

**PATCH /api/products/(id:int)**

Updates the record with submitted id field using PATCH method

**Authorization:**
- Authenticated users

**Parameters:**

- `id` : (required - int field)
- `operationType` : (not required - int field)
- `path` : (required - string field- property name)
- `op` : (required - string field - `replace`)
- `from` : (not required - string field)
- `value` : (required - property’s type)

**Returns:**
- 204 Success no content
- 400 Bad Request if submitted data was not correct
- 404 Not found if there is no record with submitted id

**Example Request Body Data:**

   
    {
    
          "operationType": 0,
          "path": "/propertyName",
          "op": "replace",
          "from": "string",
          "value": "string"
    }



**DELETE /api/products/(id:int)**

deletes the record with the submitted id field.


**Authorization:**
- Authenticated users

**Parameters:**

- `id` : (required - int field)


**Returns:**
- 200 Ok 
- 400 Bad Request if submitted data was not correct
- 404 Not found if there is no record with submitted id

**Example Request Body Data:**

**DELETE /api/products/(id:int)**

---

## 4. Error Handling

Explain how your API handles errors, the types of error responses clients can expect, and what they mean.

Example:

markdown
## Error Handling

Our API uses standard HTTP status codes to indicate success or failure of a request. In case of an error, a JSON response will be returned with the following structure:

json
{
  "error": {
    "code": "string",
    "message": "string"
  }
}


For example, if you try to access a resource that does not exist, you will get a 404 status code with a response like:

json
{
  "error": {
    "code": "NotFound",
    "message": "The requested resource was not found."
  }
}

---

## 5. Contact Information

### Feel free to reach me at anytime and send a [message](mailto:moayed.abdulhafiez@gmail.com)

