
DELETE FROM X
from
        (select *
	    from

		(select ROW_NUMBER() OVER(ORDER BY Student_ID) AS ROWID,*
		from Student) as A

			where ROWID=9)    X