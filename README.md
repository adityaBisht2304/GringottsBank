# GringottsBank
Gringotts Bank is a bank that has an online branch for wizards to do some account transactions

# Access Instructions
1. Make clone of this repository by using command "git clone https://github.com/adityaBisht2304/GringottsBank.git". It will copy the code in a local folder.
2. Go to folder "RepositoryLocalFolderPath/GringottsBank/" and open "GringottsBank.sln" in Visual Studio 2019
3. Open package manager console and type 2 commands "add-migration NewMigration" and then do "update-database"
4. Build the solution and run GringottsBank.exe from path "ReositoryLocalFolderPath/GringottsBank/bin/Debug/net5.0"
5. It will open up the Swagger UI(https://localhost:5001/swagger/index.html) with all the REST APIs available. Run the following APIs in sequence for authentication
   1. POST /v1/api/customer/register 
      1. Provide Request Body correctly
   2. POST /v1/api/customer/login 
      1. Provide Request Body correctly
      2. Copy the token generated - Let's say "abc"
   3. Go to Authorize Section at the top right of the Swagger UI
      1. Type "Bearer abc" or in other words "Bearer TokenNumberGenerated"
      2. No double quotes are to be put
   4. The authentication done in above steps will ensure only the authorized user can access the APIs
6. We can also run the APis from Postman for registration and authentication
   1. POST https://localhost:5001/v1/api/customer/register 
      1. Provide Request Body correctly
   2. POST https://localhost:5001/v1/api/customer/login
      1. Provide Request Body correctly
      2. Copy the token generated - Let's say "abc"
   3. Go to Authorization Section and select the Bearer token type and paste the token
   4. Now we can use all those APIs which do not require admin access
7. For admin access from Swagger UI(https://localhost:5001/swagger/index.html)
   1. POST /v1/api/customer/register-admin
      1. Provide Request Body correctly
   2. POST /v1/api/customer/login 
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
1. POST /v1/api/customer/register
   1. Request Body should have {"emailID": "user@example.com","password": "string","name": "string"}
   2. Password should be 8 to 128 characters long
   3. Password should atleast have one UPPERCASE / one LOWERCASE / one SPECIAL_CHARACTER / one DIGIT
   4. POSTMAN LINK : https://localhost:5001/v1/api/customer/register
2. POST /v1/api/customer/login
   1. Request body should have {"emailID": "user@example.com","password": "string"}
   2. The parameters should exactly match for correct token generation
   3. Token will be generated in the response body
   4. POSTMAN LINK : https://localhost:5001/v1/api/customer/login
3. GET /v1/api/customer/get-by-id/{customerId}
   1. customerId can not be null and should be provided
   2. POSTMAN LINK : https://localhost:5001/v1/api/customer/get-by-id/{customerId}
   3. Example for (customerID-5) : https://localhost:5001/v1/api/customer/get-by-id/5
4. GET /v1/api/customer/get-by-name/{customerName}
   1. customerName can not be null and should be provided
   2. POSTMAN LINK : https://localhost:5001/v1/api/customer/get-by-name/{customerName}
   3. Example for (customerName - Harry Potter) : https://localhost:5001/v1/api/customer/get-by-name/Harry%20Potter
6. POST /v1/api/customer/update
   1. Request body should have {"emailID": "user@example.com","password": "string", "name":"string"}
   2. Email ID should be existing
   3. Only password and name can be changed
   5. POSTMAN LINK : https://localhost:5001/v1/api/customer/update

### Admin Access
1. POST /v1/api/customer/register-admin
   1. Request Body should have {"emailID": "user@example.com","password": "string","name": "string"}
   2. Password should be 8 to 128 characters long
   3. Password should atleast have one UPPERCASE / one LOWERCASE / one SPECIAL_CHARACTER / one DIGIT
   4. This api will register user as admin
   5. POSTMAN LINK : https://localhost:5001/v1/api/customer/register-admin
2. GET /v1/api/customer/get-all
   1. It will get the details of all the customers present in the banking system
   2. POSTMAN LINK : https://localhost:5001/v1/api/customer/get-all
4. GET /v1/api/customer/get-customers-by-name/{namePattern}
   1. namePattern can not be null and should be provided
   2. It will get the details of all the customers present in the banking system containing the namePattern keyword
   3. POSTMAN LINK : https://localhost:5001/v1/api/customer/get-customers-by-name/{namePattern}
   4. Example for (Customer Names - [Harry Potter] and [James Potter]) : https://localhost:5001/v1/api/customer/get-customers-by-name/Potter
5. DELETE /v1/api/customer/delete/{customerId}
   1. customerId can not be null and should be provided
   2. POSTMAN LINK : https://localhost:5001/v1/api/customer/delete/{customerId}
   3. Example to delete (customerID-5) : https://localhost:5001/v1/api/customer/delete/5

## Account Endpoint
1. POST /v1/api/account/create
   1. Request body should have {"accountType": 0,"customerID": 0}
   2. Customer ID is the customer to which we want to associate this account with
   3. account Type = 0 for Savings and 1 for Current
   4. POSTMAN LINK : https://localhost:5001/v1/api/account/create
2. GET /v1/api/account/get-all
   1. customerID should be passed as a query
   2. It will get the details of all the accounts present in the banking system for a particular customer
   3. POSTMAN LINK : https://localhost:5001/v1/api/account/get-all?customerId={customerId}
   4. Example for (customerID-5) :  https://localhost:5001/v1/api/account/get-all?customerId=5
3. GET /v1/api/account/get-by-id/{accountId}
   1. accountId can not be null and should be provided
   2. here the id provided is of account for which we want information
   3. POSTMAN LINK : https://localhost:5001/v1/api/account/get-by-id/{accountId}
   4. Example for (accountID-5) : https://localhost:5001/v1/api/account/get-by-id/5
4. GET /v1/api/account/get-by-account-number/{accountNumber}
   1. accountNumber can not be null and should be provided
   2. here the accountNumber provided is of account for which we want information
   3. POSTMAN LINK : https://localhost:5001/v1/api/account/get-by-account-number/{accountNumber}
   4. Example for (accountID-1782) : https://localhost:5001/v1/api/account/get-by-account-number/1782
5. POST /v1/api/account/delete/{accountId}
   1. accountId can not be null and should be provided
   2. POSTMAN LINK : https://localhost:5001/v1/api/account/delete/{accountId}
   3. Example to delete (accountID-5) : https://localhost:5001/v1/api/account/delete/5

## Transaction Endpoint
1. POST /v1/api/transaction/deposit-money
   1. Request body should have {"transactionAmount": 0,"accountID": 0}
   2. account ID is the account in which we are depositing the money
   3. POSTMAN LINK : https://localhost:5001/v1/api/transaction/deposit-money
2. GET /v1/api/transaction/withdraw-money
   1. Request body should have {"transactionAmount": 0,"accountID": 0}
   2. account ID is the account from which we are withdrawing the money
   3. POSTMAN LINK : https://localhost:5001/v1/api/transaction/withdraw-money
3. GET /v1/api/transaction/get-all-transactions
   1. accountID should be passed as a query
   2. It will get the details of all the transactions done for a particular account
   3. POSTMAN LINK : https://localhost:5001/v1/api/transaction/get-all?accountId={accountId}
   4. Example for (accountID-5) :  https://localhost:5001/v1/api/transaction/get-all?accountId=5
4. GET /v1/api/transaction/get-account-transactions-in-time-period
   1. accountID, fromTime and toTime should be passed as a query
   2. This api will get the details of all the transactions done for a particular account in a certain time period
   3. POSTMAN LINK : https://localhost:5001/v1/api/transaction/get-account-transactions-in-time-period?accountId={accountId}&fromTime={fromTime}&toTime={toTime}
   4. Example for (accountID-5/ fromTime-2022-02-21T04:34:45 / toTime-2022-02-21T17:18:47) 
      https://localhost:5001/v1/api/transaction/get-account-transactions-in-time-period?accountId=5&fromTime=2022-02-21T04%3A34%3A45&toTime=2022-02-21T17%3A18%3A47
   5. fromTime should be less than toTime
   6. Format for editing fromTime or toTime : yyyy-MM-ddTHH:mm:ss which in url transalate to yyyy-MM-ddTHH%3Amm%3Ass
5. GET /v1/api/transaction/get-customer-transactions-in-time-period
   1. customerID, fromTime and toTime should be passed as a query
   2. This api will get the details of all the transactions done for a particular customer in all the accounts in a certain time period
   3. POSTMAN LINK : https://localhost:5001/v1/api/transaction/get-customer-transactions-in-time-period?customerId={customerId}&fromTime={fromTime}&toTime={toTime}
   4. Example for (customerID-5/ fromTime-2022-02-21T04:34:45 / toTime-2022-02-21T17:18:47) 
      https://localhost:5001/v1/api/transaction/get-customer-transactions-in-time-period?customerId=5&fromTime=2022-02-21T04%3A34%3A45&toTime=2022-02-21T17%3A18%3A47
   5. fromTime should be less than toTime
   6. Format for editing fromTime or toTime : yyyy-MM-ddTHH:mm:ss which in url transalate to yyyy-MM-ddTHH%3Amm%3Ass
