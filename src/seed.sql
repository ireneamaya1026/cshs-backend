USE CSHS_Master_Dev;

-- Clear existing data
DELETE FROM SystemUsers;
DELETE FROM Campuses;
DELETE FROM SchoolConfigs;

-- Insert School Config (only one row ever)
INSERT INTO SchoolConfigs (
    Name, ShortName, Email, Phone,
    PrimaryColor, SecondaryColor,
    PortalLabel, SupportLabel, PortalWelcome,
    PortalBgStyle, [Plan],
    CreatedAt, UpdatedAt
)
VALUES (
    'Cebu Sacred Heart School',
    'CSHS',
    'admin@cshs.edu.ph',
    '09123456789',
    '#750014',
    '#080c42',
    'School Management Portal',
    'Contact IT Support',
    'Welcome back.',
    0,
    1,
    GETDATE(),
    GETDATE()
);

-- Insert Campus
INSERT INTO Campuses (CampusKey, Name, Address, IsActive, HasBasicEd, HasCollege, SortOrder, CreatedAt, UpdatedAt)
VALUES ('CEBU', 'Cebu Main Campus', 'Cebu City, Philippines', 1, 1, 1, 0, GETDATE(), GETDATE());

-- Get the actual campus ID
DECLARE @CampusId BIGINT = SCOPE_IDENTITY();

-- Insert Admin User
INSERT INTO SystemUsers (CampusId, Name, Email, PasswordHash, Role, IsActive, CreatedAt, UpdatedAt)
VALUES (
    @CampusId,
    'Admin User',
    'admin@cshs.edu.ph',
    '$2a$11$HecaRknSZqVTSEjYqBKG.eRaaFn5OEo.XomuOP2yzhnVCmyq21DG2',
    'technical_admin',
    1,
    GETDATE(),
    GETDATE()
);

-- Verify
SELECT Id, Name FROM SchoolConfigs;
SELECT Id, Name, CampusKey FROM Campuses;
SELECT Id, Name, Email, Role, CampusId FROM SystemUsers;

SELECT Id, CampusKey, Name FROM Campuses;