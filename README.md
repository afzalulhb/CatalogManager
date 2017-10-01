# CatalogManager
Oct 1, 2017
Configured Unity to resolve Interface dependencies using assemblies.

Oct 3, 2015

Solution setup:

In application setup, both CatalogManager.DistributedService and CatalogManager.Presentation should be set to run at startup.
Connection strings should be pointing to SQL database at 
a)CatalogManager.DistributedService --> web.config 
b)CatalogManager.Test --> App.config.

CatalogManager.Presentation should point to webapi service at CatalogManager.Presentation\Scripts\Services\catalogManagerServices.js 
like below: var apiroot = "http://localhost:28742";
Run the application to create Database and seed data. 

To manually set up db, 
a) Run the the schema.sql script in sql server . The script is located at Database folder 
b) Run the seed script.
