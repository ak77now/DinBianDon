


UPDATE X
SET X.Student_ID=12,X.Student_name='汪建民',X.Student_class=2
from

(select *
from
(select ROW_NUMBER() OVER(ORDER BY Student_ID) AS ROWID,*
from Student) as A
where 
ROWID=12)   X