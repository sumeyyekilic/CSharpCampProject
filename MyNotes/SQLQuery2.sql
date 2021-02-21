--YORUM SATIRI..
--çalıştırmak istediğimiz kodu seçip execute ederiz.
--case insensitive 

SELECT * FROM Customers

select ContactName, City, CompanyName from Customers  --bunu çalıştırınca arka planda c# daki array gibi bir fake tablo  oluşur.

--alias
--ANSII
select ContactName Adi, City Sehir, CompanyName SirketAdi from Customers 

-- tüm müşterileri tüm kolonları ile getir ama şehri London olanları:
SELECT *FROM Customers where City='London' 

--ürünler tablosunu tüm kolonları ile getir
select * from Products order by CategoryID, ProductName
select * from Products order by UnitPrice asc  --acsending
select * from Products order by UnitPrice desc   --descending  *azalan


select count(*) from Products  --bazı web sitelerinde 3000 ürün var gibi bir bilgi görürüz. kullanıcıya hava atmak
--count  tek bir koon tek bir satır gelir, çünkü sayi isteriz
select count(*) from Products where CategoryID=2

select count(*) Adet from Products where CategoryID=2


--group by : kullanılıuyorsa ; select edilen kolon sadece gruop by da yazılan alan olabilir...

select CategoryID from Products group by CategoryID  --her bir grup için arka planda grup oluşturuyormuş gibi düşün
select CategoryID, count(*) from Products group by CategoryID --count her bir kategori için ayrı bir count hesaplar

--2kolona birden group by yapılabilir.
--karar destek sistemler : hangi kategorilerde az ürünümz varsa onları besleyelim. yönetim ürün sayısı 10dan az olan kategorielerin listelenmesini isterse , where koşulu kümülatif dataya yazılır:
--having : kümülatif dataya yazılır
select CategoryID, count(*) from Products group by CategoryID having count(*)<10 



--join birleştirme

select * from Products inner join Categories  on Products.CategoryID=Categories.CategoryID  --hem ürün hem kategirlerilen bir araya getirilmesi

select * from Products inner join Categories  on Products.CategoryID=Categories.CategoryID  where Products.UnitPrice>10 --fiyatı 10dan büyük olanlaro

--DTO : Data transformation object   (bankadaki krediler için ayrı class yapıyorsak, joinler yapıp bizim için dto olarak karşımıza çıkar.)

--inner join  : sadce 2 tabloda da eşleşenleri getirir,
--eşleşmeyen data varsa onu getirmez.
select * from Products p inner join [Order Details] od on p.ProductID=od.ProductID   --tablodaki boşluk old için köşeli parantezle yazılı. tablo old anlasın

--(inner join sadece eşleşen kayıtları getirir...)

--yönetim : hiç satış yapamadığımız ürünleri söyle derse ?  
--inner left yapılırsa bu sağlanır. yani left join(solda olup **ürünler tablosunda olup satışı olamayan*)
select * from Products p left join [Order Details] od on p.ProductID=od.ProductID

select * from Customers c left join Orders o on c.CustomerID=o.CustomerID 

--yönetim : sistemimize kayıtlı ama bizden ürün almayan kişileri getir.
--bazı web sitelerinde *sana özel* demesi:
select * from Customers c left join Orders o on c.CustomerID=o.CustomerID where o.CustomerID is null

--
select * from Products p inner join [Order Details] od on p.ProductID=od.ProductID inner join Orders o on o.OrderID=od.OrderID

