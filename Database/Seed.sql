--Seed

use CatalogManager
go
--Begin transaction
declare @ParentCategoryId int;
declare @CategoryId int;
if not exists( select top 1 * from Category where Name = 'Computers')
begin
	insert into Category(Name) values('Computers');
	select @ParentCategoryId =SCOPE_IDENTITY()	
	insert into Category(Name, ParentCategoryId) values('Desktop-Computers', @ParentCategoryId)	
	select @CategoryId =SCOPE_IDENTITY()	
	insert into Product(Name, Description, CategoryId, Price) values('Lenovo Horizon II 27in Portable Desktop i5 1.7GHz 8GB 1TB WiFi','Lenovo Horizon II 27in Portable Desktop i5 1.7GHz 8GB 1TB WiFi',@CategoryId, 1091.00)

end

--select * from category
--rollback transaction