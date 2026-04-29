-- USE CSHS_Master_Dev;

-- -- Insert a School
-- INSERT INTO Schools (Name, Slug, Address, ContactEmail, ContactNumber, IsActive, CreatedAt, IsDeleted)
-- VALUES ('Cebu Sacred Heart School', 'cshs', 'Cebu City', 'admin@cshs.edu.ph', '032-1234567', 1, GETDATE(), 0);

-- -- Insert a Campus
-- INSERT INTO Campuses (Name, Address, ContactNumber, IsActive, SchoolId, CreatedAt, IsDeleted)
-- VALUES ('Main Campus', 'Cebu City', '032-1234567', 1, 1, GETDATE(), 0);

-- -- Insert SuperAdmin (Password: Admin@123)
-- INSERT INTO Users (FirstName, LastName, Email, PasswordHash, Role, IsActive, SchoolSlug, CampusId, CreatedAt, IsDeleted)
-- VALUES (
--   'Super', 
--   'Admin', 
--   'superadmin@cshs.edu.ph',
--   '$2a$11$N9qo8uLOickgx2ZMRZoMyeIjZAgcfl7p92ldGxad68LPVbIfkEDga',
--   'SuperAdmin',
--   1,
--   'cshs',
--   NULL,
--   GETDATE(),
--   0
-- );

-- -- Verify data
-- SELECT * FROM Schools;
-- SELECT * FROM Campuses;
-- SELECT * FROM Users;

USE CSHS_Master_Dev;

SELECT Id, Email, PasswordHash FROM Users;

-- UPDATE Users 
-- SET PasswordHash = '$2a$11$QinteHb.7wbDAc5LEBISR.ao5w3tghN1CTGIsiHbAzArhQCUmJBz2'
-- WHERE Email = 'superadmin@cshs.edu.ph';

-- -- Verify
-- SELECT Email, PasswordHash FROM Users WHERE Email = 'superadmin@cshs.edu.ph';