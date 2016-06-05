--From2
select  Order_ID,Item_ID ,Company_name,Product_name,Quantity,Price,Student_name
from Order_detail
inner join Student 
on Order_detail.Student_ID=Student.Student_ID
inner join Product
on Order_detail.Product_ID=Product.Product_ID
inner join Company
on Product.Company_ID=Company.Company_ID
order by Order_ID,Item_ID


select*
from Order_detail

select*
from Student

select*
from Product

-----------------
ROW_NUMBER() OVER+where用法:

1.拆解:
--先在原本查詢句的「select項目」「額外」加上一筆 ROW_NUMBER() OVER(ORDER BY 想要排序的順序) AS ROWID
--若原本查詢句結尾有order by責刪掉並將他放到ROW_NUMBER() OVER後面的ORDER BY內
select  ROW_NUMBER() OVER(ORDER BY Order_ID,Item_ID) AS ROWID,Order_ID,Item_ID ,Company_name,Product_name,Quantity,Price,Student_name
from Order_detail
inner join Student 
on Order_detail.Student_ID=Student.Student_ID
inner join Product
on Order_detail.Product_ID=Product.Product_ID
inner join Company
on Product.Company_ID=Company.Company_ID
--結果會像上面這樣

2.
將上面的結果用小括號框起來並後面接上as 隨便一個名稱
(第一步的內容) as A

3.
select *
from
(第一步的內容) as A
where 
ROWID=想要查的項次

--ROWID查詢的項次可以搭配C#一些工具的項目選取用
完整的內容:
select *
from
(select  ROW_NUMBER() OVER(ORDER BY Order_ID,Item_ID) AS ROWID,Order_ID,Item_ID ,Company_name,Product_name,Quantity,Price,Student_name
from Order_detail
inner join Student 
on Order_detail.Student_ID=Student.Student_ID
inner join Product
on Order_detail.Product_ID=Product.Product_ID
inner join Company
on Product.Company_ID=Company.Company_ID) as A
where ROWID=3


