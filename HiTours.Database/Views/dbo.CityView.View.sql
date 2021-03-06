
CREATE VIEW [dbo].[CityView] 
WITH SCHEMABINDING
AS
select 
pcity.Id as CityId,
pcity.Code as CityCode,
pcity.CountryId,
pcity.Name as CityName,
pcity.ShortDetail,
pcity.[Description],
pcountry.SortName AS CountryCode,
pcountry.Name AS CountryName
 from package.city pcity inner join package.country pcountry on pcity.CountryId= pcountry.Id
 where pcity.IsActive=1

GO
