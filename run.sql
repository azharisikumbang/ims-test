-- Soal 2
-- KontrakNO = "ARG00004" kontrak ARG00001 telah terpakai untuk data lain
-- Buatlah sebuah query untuk menampilkan total angsuran dengan client sugus yang sudah jatuh tempo per tanggal 14 agustus 2024
SELECT  
    k.Id as "ID",
    k.KontrakNo as "KONTRAK NO",
    k.ClientName as "CLIENT NAME",
    SUM(s.AngsuranPerBulan) as "TOTAL ANGSURAN JATUH TEMPO"
FROM Kontrak k 
JOIN Angsuran s ON k.Id = s.KontrakId
WHERE k.KontrakNo = "ARG00004" AND s.TanggalJatuhTempo < "2024-08-14"
GROUP BY k.Id;

/* 
Soal 3

Pak sugus sudah melakukan pembayaran tepat waktu sampai dengan angsuran bulan mei 2024.
namun belum melakukan pembayaran untuk angsuran juni 2024 sampai dengan 14 agustus
2024. Pada IMS Finance diberlakukan denda per hari 0,1% dari berdasarkan angsuran yang
belum dibayarkan. Buat query untuk menampilkan berapa denda yang dikenakan, total hari
keterlambatan untuk pak sugus per periode sampai dengan tanggal 14 Agustus 2024. 
*/

-- On SQLITE

SELECT 
    k.Id as "ID",
    k.KontrakNo as "KONTRAK NO",
    k.ClientName as "CLIENT NAME",
    s.AngsuranKe as "INSTALLMENT NO",
    CAST(
        JULIANDAY("2024-08-14") - JULIANDAY(s.TanggalJatuhTempo) AS INTEGER
    ) AS "HARI KETERLAMBATAN",
    (CAST(
        JULIANDAY("2024-08-14") - JULIANDAY(s.TanggalJatuhTempo) AS INTEGER
    )) * 0.001 * s.AngsuranPerBulan AS "TOTAL DENDA"
FROM Kontrak k
JOIN Angsuran s ON k.Id = s.KontrakId
WHERE 
    k.KontrakNo = "ARG00004" AND
    s.TanggalJatuhTempo >= "2024-06-01" AND 
    s.TanggalJatuhTempo <= "2024-08-14"
;

-- On SQL
SELECT 
    k.Id as "ID",
    k.KontrakNo as "KONTRAK NO",
    k.ClientName as "CLIENT NAME",
    s.AngsuranKe as "INSTALLMENT NO",
    DATEDIFF(DAY, s.s.TanggalJatuhTempo, "2024-08-14") AS "HARI KETERLAMBATAN",
    DATEDIFF(DAY, s.s.TanggalJatuhTempo, "2024-08-14") * 0.001 * s.AngsuranPerBulan AS "TOTAL DENDA"
FROM Kontrak k
JOIN Angsuran s ON k.Id = s.KontrakId
WHERE 
    k.KontrakNo = "ARG00004" AND
    s.TanggalJatuhTempo >= "2024-06-01" AND 
    s.TanggalJatuhTempo <= "2024-08-14"
;