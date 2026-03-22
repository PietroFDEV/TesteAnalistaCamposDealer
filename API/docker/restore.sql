RESTORE DATABASE TesteCamposDealer
FROM DISK = '/backup/TesteCamposDealer.bak'
WITH 
    MOVE 'TesteCamposDealer' TO '/var/opt/mssql/data/TesteCamposDealer.mdf',
    MOVE 'TesteCamposDealer_log' TO '/var/opt/mssql/data/TesteCamposDealer_log.ldf',
    REPLACE;
GO