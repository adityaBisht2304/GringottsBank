# GringottsBank
Gringotts Bank is a bank that has an online branch for wizards to do some account transactions

# Access Instructions
1. Make clone of this repository by using command "git clone https://github.com/adityaBisht2304/GringottsBank.git". It will copy the code in a local folder.
2. Go to folder "RepositoryLocalFolderPath/GringottsBank/" and open "GringottsBank.sln" in Visual Studio 2019
3. Open package manager console and type 2 commands "database-migration NewMigration" and then do "update-database"
4. Build the solution and run GringottsBank.exe from path "ReositoryLocalFolderPath/GringottsBank/bin/Debug/net5.0"
5. It will open up the Swagger UI(https://localhost:5001/swagger/index.html) with all the REST APIs available. Run the following APIs in sequence for authentication
   1. POST /v1/api/Customer/register 
      1. Provide Request Body correctly
   2. POST /v1/api/Customer/login 
      1. Provide Request Body correctly
      2. Copy the token generated - Let's say "abc"
   3. Go to Authorize Section at the top right of the Swagger UI
      1. Type "Bearer abc" or in other words "Bearer TokenNumberGenerated"
      2. No double quotes are to be put
   4. The authentication done in above steps will ensure only the authorized user can access the APIs
6. We can also run the APis from Postman for registration and authentication
   1. POST https://localhost:5001/v1/api/Customer/register 
      1. Provide Request Body correctly
   2. POST https://localhost:5001/v1/api/Customer/login
      1. Provide Request Body correctly
      2. Copy the token generated - Let's say "abc"
   3. Go to Authorization Section and select the Bearer token type and paste the token
   4. Now we can use all those APIs which do not require admin access
7. For admin access from Swagger UI(https://localhost:5001/swagger/index.html)
   1. POST /v1/api/Customer/register-admin
      1. Provide Request Body correctly
   2. POST /v1/api/Customer/login 
      1. Provide Request Body correctly
      2. Copy the token generated - Let's say "abc"
   3. Go to Authorize Section at the top right of the Swagger UI
      1. Type "Bearer abc" or in other words "Bearer TokenNumberGenerated"
      2. No double quotes are to be put
   4. The authentication done in above steps will ensure admin access for the customer
      1. Access to all APIs is possible through admin login

# API Instructions
## Customer Endpoint
### Common User Access
1. POST /v1/api/Customer/register
   1. Request Body should have {"emailID": "user@example.com","password": "string","name": "string"}
   2. Password should be 8 to 128 characters long
   3. Password should atleast have one UPPERCASE / one LOWERCASE / one SPECIAL_CHARACTER / one DIGIT
   4. POSTMAN LINK : https://localhost:5001/v1/api/Customer/register
2. POST /v1/api/Customer/login
   1. Request body should have {"emailID": "user@example.com","password": "string"}
   2. The parameters should exactly match for correct token generation
   3. Token will be generated in the response body
   4. POSTMAN LINK : https://localhost:5001/v1/api/Customer/login
3. GET /v1/api/Customer/get-by-id/{id}
   1. id can not be null and should be provided
   2. POSTMAN LINK : https://localhost:5001/v1/api/Customer/get-by-id/{id}
   3. Example for (CustomerID-5) : https://localhost:5001/v1/api/Customer/get-by-id/5
4. GET /v1/api/Customer/get-by-name/{name}
   1. name can not be null and should be provided
   2. POSTMAN LINK : https://localhost:5001/v1/api/Customer/get-by-name/{name}
   3. Example for (CustomerName - Harry Potter) : https://localhost:5001/v1/api/Customer/get-by-name/Harry%20Potter
6. POST /v1/api/Customer/update
   1. Request body should have {"emailID": "user@example.com","password": "string", "name":"string"}
   2. Email ID should be existing
   3. Only password and name can be changed
   5. POSTMAN LINK : https://localhost:5001/v1/api/Customer/update

### Admin Access
1. POST /v1/api/Customer/register-admin
   1. Request Body should have {"emailID": "user@example.com","password": "string","name": "string"}
   2. Password should be 8 to 128 characters long
   3. Password should atleast have one UPPERCASE / one LOWERCASE / one SPECIAL_CHARACTER / one DIGIT
   4. This api will register user as admin
   5. POSTMAN LINK : https://localhost:5001/v1/api/Customer/register-admin
2. GET /v1/api/Customer/get-all
   1. It will get the details of all the customers present in the banking system
   2. POSTMAN LINK : https://localhost:5001/v1/api/Customer/get-all
4. GET /v1/api/Customer/get-customers-by-name/{name}
   1. name can not be null and should be provided
   2. It will get the details of all the customers present in the banking system containing the name keyword
   3. POSTMAN LINK : https://localhost:5001/v1/api/Customer/get-customers-by-name/{name}
   4. Example for (Customer Names - [Harry Potter] and [James Potter]) : https://localhost:5001/v1/api/Customer/get-by-name/Potter
5. DELETE /v1/api/Customer/delete/{id}
   1. id can not be null and should be provided
   2. POSTMAN LINK : https://localhost:5001/v1/api/Customer/delete/{id}
   3. Example to delete (CustomerID-5) : https://localhost:5001/v1/api/Customer/delete/5

## Account Endpoint
1. POST /v1/api/Account/create
   1. Request body should have {"accountType": 0,"customerID": 0}
   2. Customer ID is the customer to which we want to associate this account with
   3. Account Type = 0 for Savings and 1 for Current
   4. POSTMAN LINK : https://localhost:5001/v1/api/Account/create
2. GET /v1/api/Account/get-all
   1. customerID should be passed as a query
   2. It will get the details of all the accounts present in the banking system for a particular customer
   3. POSTMAN LINK : https://localhost:5001/v1/api/Account/get-all?customerId={customerId}
   4. Example for (CustomerID-5) :  https://localhost:5001/v1/api/Account/get-all?customerId=5
3. GET /v1/api/Account/get-by-id/{id}
   1. id can not be null and should be provided
   2. here the id provided is of account for which we want information
   3. POSTMAN LINK : https://localhost:5001/v1/api/Account/get-by-id/{id}
   4. Example for (AccountID-5) : https://localhost:5001/v1/api/Account/get-by-id/5
4. GET /v1/api/Account/get-by-account-number/{accountNumber}
   1. accountNumber can not be null and should be provided
   2. here the accountNumber provided is of account for which we want information
   3. POSTMAN LINK : https://localhost:5001/v1/api/Account/get-by-account-number/{accountNumber}
   4. Example for (AccountID-17827354129701496) : https://localhost:5001/v1/api/Account/get-by-account-number/17827354129701496
5. POST /v1/api/Account/delete/{id}
   1. id can not be null and should be provided
   2. POSTMAN LINK : https://localhost:5001/v1/api/Account/delete/{id}
   3. Example to delete (AccountID-5) : https://localhost:5001/v1/api/Account/delete/5

## Transaction Endpoint
1. POST /v1/api/Transaction/deposit-money
   1. Request body should have {"transactionAmount": 0,"accountID": 0}
   2. Account ID is the account in which we are depositing the money
   3. POSTMAN LINK : https://localhost:5001/v1/api/Transaction/deposit-money
2. GET /v1/api/Transaction/withdraw-money
   1. Request body should have {"transactionAmount": 0,"accountID": 0}
   2. Account ID is the account from which we are withdrawing the money
   3. POSTMAN LINK : https://localhost:5001/v1/api/Transaction/withdraw-money
3. GET /v1/api/Transaction/get-all-transactions
   1. accountID should be passed as a query
   2. It will get the details of all the transactions done for a particular account
   3. POSTMAN LINK : https://localhost:5001/v1/api/Transaction/get-all?accountId={accountId}
   4. Example for (AccountID-5) :  https://localhost:5001/v1/api/Transaction/get-all?accountId=5
4. GET /v1/api/Transaction/get-transactions-in-time-period
   1. accountID, fromTime and toTime should be passed as a query
   2. This api will get the details of all the transactions done for a particular account in a certain time period
   3. POSTMAN LINK : https://localhost:5001/v1/api/Transaction/get-transactions-in-time-period?accountId={accountId}&fromTime={fromTime}&toTime={toTime}
   4. Example for (AccountID-5/ fromTime-2022-02-21T04:34:45 / toTime-2022-02-21T17:18:47) 
      https://localhost:5001/v1/api/Transaction/get-transactions-in-time-period?accountId=5&fromTime=2022-02-21T04%3A34%3A45&toTime=2022-02-21T17%3A18%3A47
   5. fromTime should be less than toTime
   6. Format for editing fromTime or toTime : yyyy-MM-ddTHH:mm:ss which in url transalate to yyyy-MM-ddTHH%3Amm%3Ass
