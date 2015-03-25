/*
Navicat SQLite Data Transfer

Source Server         : ETD
Source Server Version : 30808
Source Host           : :0

Target Server Type    : SQLite
Target Server Version : 30808
File Encoding         : 65001

Date: 2015-03-25 12:16:42
*/

PRAGMA foreign_keys = OFF;

-- ----------------------------
-- Table structure for ABCs
-- ----------------------------
DROP TABLE IF EXISTS "main"."ABCs";
CREATE TABLE "ABCs" (
"ABC_ID"  INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
"Intervention_ID"  INTEGER NOT NULL,
"Consciousess"  TEXT,
"Disoriented"  BOOLEAN,
"Airways"  TEXT,
"Breathing"  TEXT,
"Breathing_Frequency"  INTEGER,
"Circulation"  TEXT,
"Circulation_Frequency"  INTEGER,
CONSTRAINT "fkey0" FOREIGN KEY ("Intervention_ID") REFERENCES "Interventions" ("Intervention_ID") ON DELETE CASCADE ON UPDATE CASCADE
);

-- ----------------------------
-- Records of ABCs
-- ----------------------------
INSERT INTO "main"."ABCs" VALUES (1, 0, null, null, null, null, null, null, null);
INSERT INTO "main"."ABCs" VALUES (2, 0, null, null, null, null, null, null, null);
INSERT INTO "main"."ABCs" VALUES (3, 0, null, null, null, null, null, null, null);

-- ----------------------------
-- Table structure for Additional_Informations
-- ----------------------------
DROP TABLE IF EXISTS "main"."Additional_Informations";
CREATE TABLE "Additional_Informations" (
"Additional_Info_ID"  INTEGER NOT NULL,
"Intervention_ID"  INTEGER NOT NULL,
"Information"  TEXT NOT NULL,
"Timestamp"  DATETIME NOT NULL,
PRIMARY KEY ("Additional_Info_ID" ASC),
CONSTRAINT "fkey0" FOREIGN KEY ("Intervention_ID") REFERENCES "Interventions" ("Intervention_ID") ON DELETE CASCADE ON UPDATE CASCADE
);

-- ----------------------------
-- Records of Additional_Informations
-- ----------------------------

-- ----------------------------
-- Table structure for Assigned_Equipment
-- ----------------------------
DROP TABLE IF EXISTS "main"."Assigned_Equipment";
CREATE TABLE "Assigned_Equipment" (
"Equipment_ID"  INTEGER NOT NULL,
"Team_ID"  INTEGER NOT NULL,
"Assigned_Time"  DATETIME,
"Removed_Time"  DATETIME,
PRIMARY KEY ("Equipment_ID", "Team_ID" ASC),
CONSTRAINT "fkey0" FOREIGN KEY ("Equipment_ID") REFERENCES "Equipments" ("Equipments_ID") ON DELETE CASCADE ON UPDATE CASCADE,
CONSTRAINT "fkey1" FOREIGN KEY ("Team_ID") REFERENCES "Teams" ("Team_ID") ON DELETE CASCADE ON UPDATE CASCADE
);

-- ----------------------------
-- Records of Assigned_Equipment
-- ----------------------------

-- ----------------------------
-- Table structure for Ending_Codes
-- ----------------------------
DROP TABLE IF EXISTS "main"."Ending_Codes";
CREATE TABLE [Ending_Codes] (
[Ending_Code] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
[Description] TEXT  NOT NULL
);

-- ----------------------------
-- Records of Ending_Codes
-- ----------------------------
INSERT INTO "main"."Ending_Codes" VALUES (1, 'Return to site');
INSERT INTO "main"."Ending_Codes" VALUES (2, 'Return to home');
INSERT INTO "main"."Ending_Codes" VALUES (3, 'Referred to doctor');
INSERT INTO "main"."Ending_Codes" VALUES (4, 'Equipment distribution');
INSERT INTO "main"."Ending_Codes" VALUES (5, 911);
INSERT INTO "main"."Ending_Codes" VALUES (6, 'Patient not found');
INSERT INTO "main"."Ending_Codes" VALUES (7, 'Treatment Refusal');
INSERT INTO "main"."Ending_Codes" VALUES (8, 'No interventions');
INSERT INTO "main"."Ending_Codes" VALUES (9, 'Other');

-- ----------------------------
-- Table structure for Equipments
-- ----------------------------
DROP TABLE IF EXISTS "main"."Equipments";
CREATE TABLE "Equipments" (
"Equipment_ID"  INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
"Operation_ID"  INTEGER NOT NULL,
"Type_ID"  INTEGER NOT NULL,
CONSTRAINT "fkey0" FOREIGN KEY ("Operation_ID") REFERENCES "Operations" ("Operation_ID") ON DELETE CASCADE ON UPDATE CASCADE,
CONSTRAINT "fkey1" FOREIGN KEY ("Type_ID") REFERENCES "Equipment_Types" ("Equipment_ID") ON DELETE CASCADE ON UPDATE CASCADE
);

-- ----------------------------
-- Records of Equipments
-- ----------------------------

-- ----------------------------
-- Table structure for Equipment_Types
-- ----------------------------
DROP TABLE IF EXISTS "main"."Equipment_Types";
CREATE TABLE "Equipment_Types" (
"Equipment_ID"  INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
"Description"  TEXT NOT NULL
);

-- ----------------------------
-- Records of Equipment_Types
-- ----------------------------
INSERT INTO "main"."Equipment_Types" VALUES (1, 'Ambulance Cart');
INSERT INTO "main"."Equipment_Types" VALUES (2, 'Sitting Cart');
INSERT INTO "main"."Equipment_Types" VALUES (3, 'Epipen');
INSERT INTO "main"."Equipment_Types" VALUES (4, 'Transport Stretcher');
INSERT INTO "main"."Equipment_Types" VALUES (5, 'Mounted Stretcher');
INSERT INTO "main"."Equipment_Types" VALUES (6, 'Wheelchair');

-- ----------------------------
-- Table structure for Interventions
-- ----------------------------
DROP TABLE IF EXISTS "main"."Interventions";
CREATE TABLE "Interventions" (
"Intervention_ID"  INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
"Operation_ID"  INTEGER NOT NULL,
"Intervention_Number"  INTEGER NOT NULL,
"Time_Of_Call"  DATETIME NOT NULL,
"Caller"  TEXT,
"Location"  TEXT,
"Nature_Of_Call"  TEXT,
"Code"  INTEGER,
"Gender"  TEXT,
"Age"  INTEGER,
"Chief_Complaint"  TEXT,
"Other_Chief_Complaint"  TEXT,
"Conclusion"  INTEGER,
CONSTRAINT "fkey0" FOREIGN KEY ("Operation_ID") REFERENCES "Operations" ("Operation_ID") ON DELETE CASCADE ON UPDATE CASCADE,
CONSTRAINT "fkey2" FOREIGN KEY ("Conclusion") REFERENCES "Ending_Codes" ("Ending_Code")
);

-- ----------------------------
-- Records of Interventions
-- ----------------------------
INSERT INTO "main"."Interventions" VALUES (1, 6, 1, '2015-3-24 16:48:14.842', 'A', 'RandomStreet', null, 2, 'M', 12, 'Seizures', null, null);
INSERT INTO "main"."Interventions" VALUES (2, 7, 1, '2015-3-24 19:25:25.873', 'Tester', 'Location', null, 1, 'M', 19, null, null, null);
INSERT INTO "main"."Interventions" VALUES (3, 0, 1, '2015-3-25 0:20:32.200', null, null, null, null, null, null, null, null, null);

-- ----------------------------
-- Table structure for Operations
-- ----------------------------
DROP TABLE IF EXISTS "main"."Operations";
CREATE TABLE "Operations" (
"Operation_ID"  INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
"Name"  TEXT NOT NULL,
"Acronym"  VARCHAR(3) NOT NULL,
"Shift_Start"  DATETIME NOT NULL,
"Shift_End"  DATETIME NOT NULL,
"Dispatcher"  TEXT NOT NULL,
"VolunteerFollowUp"  TEXT,
"Finance"  TEXT,
"Vehicle"  TEXT,
"ParticularSituation"  TEXT,
"OrganizationFollowUp"  TEXT,
"SupervisorFollowUp"  TEXT
);

-- ----------------------------
-- Records of Operations
-- ----------------------------
INSERT INTO "main"."Operations" VALUES (0, 'Test Operation', 'TO', '2015-3-19 0:0:0', '2015-3-20 12:12:12.0', 'Tester', null, null, null, null, null, null);
INSERT INTO "main"."Operations" VALUES (1, 'A', 'A', '2015-3-14 12:12:0.0', '2015-3-21 12:12:0.0', 'A', 'blahhhhh', 'blah', null, null, null, null);
INSERT INTO "main"."Operations" VALUES (2, 'C', 'C', '2015-3-28 12:0:0.0', '2015-4-2 10:0:0.0', 'C', null, null, null, null, null, null);
INSERT INTO "main"."Operations" VALUES (3, 'D', 'D', '2015-3-14 12:0:0.0', '2015-3-15 13:0:0.0', 'D', null, null, null, null, null, null);
INSERT INTO "main"."Operations" VALUES (4, 'E', 'E', '2015-3-21 12:11:0.0', '2015-3-22 12:11:0.0', 'A', 'Volunteer A injuered', '10$ Meals', 'None', 'None', '', 'Blehhhhhh');
INSERT INTO "main"."Operations" VALUES (5, 'E', 'E', '2015-3-19 12:12:0.0', '2015-3-22 12:12:0.0', 'A', null, null, null, null, null, null);
INSERT INTO "main"."Operations" VALUES (6, 'Lo', 'L', '2015-3-13 12:0:0.0', '2015-3-14 13:0:0.0', 'L', 'Random text', '', '', '', '', 'Mr. A');
INSERT INTO "main"."Operations" VALUES (7, 'TestingTeams', 'TT', '2015-1-14 12:0:0.0', '2015-1-15 12:0:0.0', 'T', 'AAAAA', 'RANDOMTEST', 'LKJLSF', 'SLDKJFL', 'SDLKFJKLS', 'SDLKFJ');
INSERT INTO "main"."Operations" VALUES (8, 'Bob', 'te', '2015-3-4 21:30:0.0', '2015-4-4 21:21:0.0', 'bob', '', '', '', '', '', '');
INSERT INTO "main"."Operations" VALUES (9, 'Bob', 'Te', '2015-2-22 21:21:0.0', '2015-4-3 21:21:0.0', 'bob', null, null, null, null, null, null);
INSERT INTO "main"."Operations" VALUES (10, 'Bob', 'te', '2001-1-1 21:21:0.0', '2002-2-2 21:21:0.0', 'bob', '', '', '', '', '', '');

-- ----------------------------
-- Table structure for Resources
-- ----------------------------
DROP TABLE IF EXISTS "main"."Resources";
CREATE TABLE "Resources" (
"Resource_ID"  INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
"Intervention_ID"  INTEGER NOT NULL,
"Name"  TEXT,
"Team_ID"  INTEGER NOT NULL,
"Intervening"  BOOLEAN,
"Moving"  DATETIME,
"IsMoving"  BOOLEAN DEFAULT FALSE,
"Arrival"  DATETIME,
"HasArrived"  BOOLEAN DEFAULT FALSE,
CONSTRAINT "fkey0" FOREIGN KEY ("Team_ID") REFERENCES "Teams" ("Team_ID") ON DELETE CASCADE ON UPDATE CASCADE,
CONSTRAINT "fkey1" FOREIGN KEY ("Intervention_ID") REFERENCES "Interventions" ("Intervention_ID") ON DELETE CASCADE ON UPDATE CASCADE
);

-- ----------------------------
-- Records of Resources
-- ----------------------------
INSERT INTO "main"."Resources" VALUES (1, 1, null, 6, null, null, 'FALSE', null, 'FALSE');
INSERT INTO "main"."Resources" VALUES (2, 2, null, 9, null, null, 'FALSE', null, 'FALSE');

-- ----------------------------
-- Table structure for sqlite_sequence
-- ----------------------------
DROP TABLE IF EXISTS "main"."sqlite_sequence";
CREATE TABLE sqlite_sequence(name,seq);

-- ----------------------------
-- Records of sqlite_sequence
-- ----------------------------
INSERT INTO "main"."sqlite_sequence" VALUES ('Equipment_Types', 6);
INSERT INTO "main"."sqlite_sequence" VALUES ('Statuses', 4);
INSERT INTO "main"."sqlite_sequence" VALUES ('Trainings', 3);
INSERT INTO "main"."sqlite_sequence" VALUES ('Ending_Codes', 9);
INSERT INTO "main"."sqlite_sequence" VALUES ('Volunteers', 13);
INSERT INTO "main"."sqlite_sequence" VALUES ('ABCs', 3);
INSERT INTO "main"."sqlite_sequence" VALUES ('Resources', 2);
INSERT INTO "main"."sqlite_sequence" VALUES ('Equipments', 0);
INSERT INTO "main"."sqlite_sequence" VALUES ('Operations', 10);
INSERT INTO "main"."sqlite_sequence" VALUES ('Teams', 9);
INSERT INTO "main"."sqlite_sequence" VALUES ('Interventions', 3);

-- ----------------------------
-- Table structure for Statuses
-- ----------------------------
DROP TABLE IF EXISTS "main"."Statuses";
CREATE TABLE "Statuses" (
"Status_ID"  INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
"Description"  TEXT NOT NULL
);

-- ----------------------------
-- Records of Statuses
-- ----------------------------
INSERT INTO "main"."Statuses" VALUES (1, 'Available');
INSERT INTO "main"."Statuses" VALUES (2, 'Moving');
INSERT INTO "main"."Statuses" VALUES (3, 'Intervening');
INSERT INTO "main"."Statuses" VALUES (4, 'Unavailable');

-- ----------------------------
-- Table structure for Teams
-- ----------------------------
DROP TABLE IF EXISTS "main"."Teams";
CREATE TABLE "Teams" (
"Team_ID"  INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
"Operation_ID"  INTEGER NOT NULL,
"Name"  TEXT NOT NULL,
"Status"  INTEGER NOT NULL DEFAULT 1,
FOREIGN KEY ("Operation_ID") REFERENCES "Operations" ("Operation_ID") ON DELETE CASCADE ON UPDATE CASCADE,
FOREIGN KEY ("Status") REFERENCES "Statuses" ("Status_ID") ON DELETE CASCADE ON UPDATE CASCADE
);

-- ----------------------------
-- Records of Teams
-- ----------------------------
INSERT INTO "main"."Teams" VALUES (1, 1, 'A', 10);
INSERT INTO "main"."Teams" VALUES (2, 2, 'C', 10);
INSERT INTO "main"."Teams" VALUES (3, 3, 'D', 10);
INSERT INTO "main"."Teams" VALUES (4, 4, 'E', 10);
INSERT INTO "main"."Teams" VALUES (5, 5, 'E', 10);
INSERT INTO "main"."Teams" VALUES (6, 6, 'B', 10);
INSERT INTO "main"."Teams" VALUES (7, 7, 'A', 10);
INSERT INTO "main"."Teams" VALUES (8, 7, 'B', 10);
INSERT INTO "main"."Teams" VALUES (9, 7, 'C', 10);

-- ----------------------------
-- Table structure for Team_Members
-- ----------------------------
DROP TABLE IF EXISTS "main"."Team_Members";
CREATE TABLE "Team_Members" (
"Team_ID"  INTEGER NOT NULL,
"Volunteer_ID"  INTEGER NOT NULL,
"Departure"  DATETIME,
"Joined"  DATETIME,
"Disbanded"  DATETIME,
PRIMARY KEY ("Team_ID" ASC, "Volunteer_ID" ASC),
CONSTRAINT "fkey0" FOREIGN KEY ("Team_ID") REFERENCES "Teams" ("Team_ID") ON DELETE CASCADE ON UPDATE CASCADE,
CONSTRAINT "fkey1" FOREIGN KEY ("Volunteer_ID") REFERENCES "Volunteers" ("Volunteer_ID") ON DELETE CASCADE ON UPDATE CASCADE
);

-- ----------------------------
-- Records of Team_Members
-- ----------------------------
INSERT INTO "main"."Team_Members" VALUES (1, 3, '2015-3-24 12:0:57.0', '2015-3-23 22:39:57.518', null);
INSERT INTO "main"."Team_Members" VALUES (2, 4, '2015-3-24 12:0:37.0', '2015-3-23 22:57:37.415', null);
INSERT INTO "main"."Team_Members" VALUES (3, 5, '2015-3-24 12:0:16.0', '2015-3-23 22:59:17.26', null);
INSERT INTO "main"."Team_Members" VALUES (4, 6, '2015-3-24 12:0:29.0', '2015-3-23 23:0:29.603', null);
INSERT INTO "main"."Team_Members" VALUES (5, 7, '2015-3-24 12:0:35.0', '2015-3-24 1:49:35.780', null);
INSERT INTO "main"."Team_Members" VALUES (6, 8, '2015-3-25 12:0:13.0', '2015-3-24 16:48:13.587', null);
INSERT INTO "main"."Team_Members" VALUES (7, 9, '2015-3-25 12:0:5.0', '2015-3-24 19:25:5.390', null);
INSERT INTO "main"."Team_Members" VALUES (7, 10, '2015-3-25 12:0:5.0', '2015-3-24 19:25:5.411', null);
INSERT INTO "main"."Team_Members" VALUES (8, 11, '2015-3-25 12:0:13.0', '2015-3-24 19:25:13.211', null);
INSERT INTO "main"."Team_Members" VALUES (9, 12, '2015-3-25 11:0:23.0', '2015-3-24 19:25:23.524', null);
INSERT INTO "main"."Team_Members" VALUES (9, 13, '2015-3-25 12:0:23.0', '2015-3-24 19:25:23.542', null);

-- ----------------------------
-- Table structure for Trainings
-- ----------------------------
DROP TABLE IF EXISTS "main"."Trainings";
CREATE TABLE "Trainings" (
"Training_ID"  INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
"Description"  TEXT NOT NULL
);

-- ----------------------------
-- Records of Trainings
-- ----------------------------
INSERT INTO "main"."Trainings" VALUES (1, 'General First Aid');
INSERT INTO "main"."Trainings" VALUES (2, 'First Responder');
INSERT INTO "main"."Trainings" VALUES (3, 'Medicine');

-- ----------------------------
-- Table structure for Volunteers
-- ----------------------------
DROP TABLE IF EXISTS "main"."Volunteers";
CREATE TABLE "Volunteers" (
"Volunteer_ID"  INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
"Name"  TEXT NOT NULL,
"Training_Level"  INTEGER NOT NULL,
FOREIGN KEY ("Training_Level") REFERENCES "Trainings" ("Training_ID") ON DELETE CASCADE ON UPDATE CASCADE
);

-- ----------------------------
-- Records of Volunteers
-- ----------------------------
INSERT INTO "main"."Volunteers" VALUES (3, 'A', 11);
INSERT INTO "main"."Volunteers" VALUES (4, 'C', 10);
INSERT INTO "main"."Volunteers" VALUES (5, 'D', 11);
INSERT INTO "main"."Volunteers" VALUES (6, 'E', 11);
INSERT INTO "main"."Volunteers" VALUES (7, 'E', 10);
INSERT INTO "main"."Volunteers" VALUES (8, 'B', 11);
INSERT INTO "main"."Volunteers" VALUES (9, 'A', 11);
INSERT INTO "main"."Volunteers" VALUES (10, 'B', 11);
INSERT INTO "main"."Volunteers" VALUES (11, 'Aaa', 11);
INSERT INTO "main"."Volunteers" VALUES (12, 'C', 10);
INSERT INTO "main"."Volunteers" VALUES (13, 'D', 10);
