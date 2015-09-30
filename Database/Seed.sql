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
	insert into Product(Name, Description, CategoryId, Price) values('Acer Aspire E 15.6" Laptop - Black/Iron ','Acer Aspire E 15.6" Laptop - Black/Iron ',@CategoryId, 599.99)	
	insert into Product(Name, Description, CategoryId, Price) values('HP Pavilion 15.6" Laptop - Silver (Intel Core i5-5200U / 1TB HDD / 16GB RAM / Windows 8.1)','HP Pavilion 15.6" Laptop - Silver (Intel Core i5-5200U / 1TB HDD / 16GB RAM / Windows 8.1)',@CategoryId, 729.99)	
	insert into Product(Name, Description, CategoryId, Price) values('ASUS 15.6" Laptop - Black (Intel Pentium N3540 / 1TB HDD / 8GB RAM / Windows 8.1)','ASUS 15.6" Laptop - Black (Intel Pentium N3540 / 1TB HDD / 8GB RAM / Windows 8.1)',@CategoryId, 479.99)	
	insert into Product(Name, Description, CategoryId, Price) values('Acer One 10.1" Touch Convertible Laptop-Iron (Intel Atom Z3735F/32GB eMMc/2GB RAM)' ,'Acer One 10.1" Touch Convertible Laptop-Iron (Intel Atom Z3735F/32GB eMMc/2GB RAM)',@CategoryId, 299.99)	
	insert into Category(Name) values('Gaming');
	insert into Category(Name) values('Audio & Video');
	insert into Category(Name) values('Electronics');
end

--select * from category
--select * from product
--rollback transaction