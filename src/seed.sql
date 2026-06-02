USE CSHS_Master_Dev;

UPDATE SystemUsers 
SET PasswordHash = '$2a$11$V5PI7e3E33/23HQrFtFlA.Gmc6zHlrYEWr1JfpLwYmbOOQLcL3wUq'
WHERE Email = 'admin@cshs.edu.ph';

-- Verify password updated
SELECT Email, PasswordHash FROM SystemUsers WHERE Email = 'admin@cshs.edu.ph';

-- Check all tables
SELECT 'Schools' as TableName, COUNT(*) as Count FROM Schools
UNION ALL
SELECT 'Campuses', COUNT(*) FROM Campuses
UNION ALL
SELECT 'SystemUsers', COUNT(*) FROM SystemUsers;