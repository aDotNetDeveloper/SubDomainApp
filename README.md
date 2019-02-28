# Introduction 
SubDomain API is a REST endpoint supporting two queries, enumerate all subdomains for a domain and lookup IP addresses for those subdomains.

SubDomain App is a react app calling the SubDomain API endpoints

## Running the API locally

To run the app locally on port 50948

```
 dotnet run -project .\subdomainapi.csproj
```

## Using the API

All endpoints accept and return a content type of 'application/json'.

/subdomain/enumerate/

/subdomain/findipaddresses

A sample VSCode 'rest client' file is included in the source code.

## Sample VSCode 'rest client' Query

To return a list of possible subdomains for a domain
```
### Get sub domains
GET http://localhost:50948/subdomain/enumerate/yahoo.com  HTTP/1.1

### Get IP Addresses
POST http://localhost:50948/subdomain/findipaddresses HTTP/1.1
content-type: application/json

[ "au.yahoo.com", "us.yahoo.com" ]
```
## Running the APP locally

To run the app locally on port 3000

```
\subdomain-app> npm start
```