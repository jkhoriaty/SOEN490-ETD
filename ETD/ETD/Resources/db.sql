/*
Navicat SQLite Data Transfer

Source Server         : EDT
Source Server Version : 30808
Source Host           : :0

Target Server Type    : SQLite
Target Server Version : 30808
File Encoding         : 65001

Date: 2015-03-18 00:38:04
*/

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
"Available_ID"  INTEGER NOT NULL,
"Team_ID"  INTEGER NOT NULL,
"Assigned_Time"  DATETIME,
"Removed_Time"  DATETIME,
PRIMARY KEY ("Available_ID" ASC, "Team_ID" ASC),
CONSTRAINT "fkey0" FOREIGN KEY ("Available_ID") REFERENCES "Available_Equipments" ("Available_ID") ON DELETE CASCADE ON UPDATE CASCADE,
CONSTRAINT "fkey1" FOREIGN KEY ("Team_ID") REFERENCES "Teams" ("Team_ID") ON DELETE CASCADE ON UPDATE CASCADE
);

-- ----------------------------
-- Records of Assigned_Equipment
-- ----------------------------

-- ----------------------------
-- Table structure for Available_Equipments
-- ----------------------------
DROP TABLE IF EXISTS "main"."Available_Equipments";
CREATE TABLE "Available_Equipments" (
"Available_ID"  INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
"Operation_ID"  INTEGER NOT NULL,
"Type_ID"  INTEGER NOT NULL,
FOREIGN KEY ("Operation_ID") REFERENCES "Operations" ("Operation_ID") ON DELETE CASCADE ON UPDATE CASCADE,
FOREIGN KEY ("Type_ID") REFERENCES "Equipments" ("Equipment_ID") ON DELETE CASCADE ON UPDATE CASCADE
);

-- ----------------------------
-- Records of Available_Equipments
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
"Description"  TEXT NOT NULL
);

-- ----------------------------
-- Records of Equipments
-- ----------------------------
INSERT INTO "main"."Equipments" VALUES (1, 'Ambulance Cart');
INSERT INTO "main"."Equipments" VALUES (2, 'Sitting Cart');
INSERT INTO "main"."Equipments" VALUES (3, 'Epipen');
INSERT INTO "main"."Equipments" VALUES (4, 'Transport Stretcher');
INSERT INTO "main"."Equipments" VALUES (5, 'Mounted Stretcher');
INSERT INTO "main"."Equipments" VALUES (6, 'Wheelchair');

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
"Nature_Of_Call"  INTEGER,
"Code"  INTEGER,
"Gender"  TEXT,
"Age"  INTEGER,
"Chief_Complaint"  TEXT,
"Other_Chief_Complaint"  TEXT,
"Conclusion"  INTEGER,
CONSTRAINT "fkey0" FOREIGN KEY ("Operation_ID") REFERENCES "Operations" ("Operation_ID") ON DELETE CASCADE ON UPDATE CASCADE,
CONSTRAINT "fkey1" FOREIGN KEY ("Nature_Of_Call") REFERENCES "Intervention_Types" ("Type_ID"),
CONSTRAINT "fkey2" FOREIGN KEY ("Conclusion") REFERENCES "Ending_Codes" ("Ending_Code")
);

-- ----------------------------
-- Records of Interventions
-- ----------------------------

-- ----------------------------
-- Table structure for Intervention_Types
-- ----------------------------
DROP TABLE IF EXISTS "main"."Intervention_Types";
CREATE TABLE [Intervention_Types] (
[Type_ID] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
[Description] TEXT  NOT NULL
);

-- ----------------------------
-- Records of Intervention_Types
-- ----------------------------
INSERT INTO "main"."Intervention_Types" VALUES (1, 'Abdominal pain');
INSERT INTO "main"."Intervention_Types" VALUES (2, 'Allergy');
INSERT INTO "main"."Intervention_Types" VALUES (3, 'Anaphylaxy');
INSERT INTO "main"."Intervention_Types" VALUES (4, 'Behavioral');
INSERT INTO "main"."Intervention_Types" VALUES (5, 'Burn');
INSERT INTO "main"."Intervention_Types" VALUES (6, 'Cardiac Arrest');
INSERT INTO "main"."Intervention_Types" VALUES (7, 'Convulsion');
INSERT INTO "main"."Intervention_Types" VALUES (8, 'Environmental related disorder');
INSERT INTO "main"."Intervention_Types" VALUES (9, 'Head / Spinal injury');
INSERT INTO "main"."Intervention_Types" VALUES (10, 'Headache');
INSERT INTO "main"."Intervention_Types" VALUES (11, 'Hemorrhage');
INSERT INTO "main"."Intervention_Types" VALUES (12, 'Insect bite');
INSERT INTO "main"."Intervention_Types" VALUES (13, 'Intoxication');
INSERT INTO "main"."Intervention_Types" VALUES (14, 'Musculoskeletal');
INSERT INTO "main"."Intervention_Types" VALUES (15, 'Nosebleed');
INSERT INTO "main"."Intervention_Types" VALUES (16, 'Ocular');
INSERT INTO "main"."Intervention_Types" VALUES (17, 'Oral');
INSERT INTO "main"."Intervention_Types" VALUES (18, 'Respiratory');
INSERT INTO "main"."Intervention_Types" VALUES (19, 'Respiratory arrest');
INSERT INTO "main"."Intervention_Types" VALUES (20, 'Soft tissue injury');
INSERT INTO "main"."Intervention_Types" VALUES (21, 'Suspected stroke');
INSERT INTO "main"."Intervention_Types" VALUES (22, 'Thoracic pain');
INSERT INTO "main"."Intervention_Types" VALUES (23, 'Unconscious');
INSERT INTO "main"."Intervention_Types" VALUES (24, 'Weakness / Fainting');
INSERT INTO "main"."Intervention_Types" VALUES (25, 'Other');

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
"Dispatcher"  TEXT NOT NULL
);

-- ----------------------------
-- Records of Operations
-- ----------------------------

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

-- ----------------------------
-- Table structure for sqlite_sequence
-- ----------------------------
DROP TABLE IF EXISTS "main"."sqlite_sequence";
CREATE TABLE sqlite_sequence(name,seq);

-- ----------------------------
-- Records of sqlite_sequence
-- ----------------------------
INSERT INTO "main"."sqlite_sequence" VALUES ('Equipments', 6);
INSERT INTO "main"."sqlite_sequence" VALUES ('Statuses', 4);
INSERT INTO "main"."sqlite_sequence" VALUES ('Trainings', 3);
INSERT INTO "main"."sqlite_sequence" VALUES ('Ending_Codes', 9);
INSERT INTO "main"."sqlite_sequence" VALUES ('Intervention_Types', 25);
INSERT INTO "main"."sqlite_sequence" VALUES ('Volunteers', 2);
INSERT INTO "main"."sqlite_sequence" VALUES ('ABCs', 0);
INSERT INTO "main"."sqlite_sequence" VALUES ('Interventions', 0);
INSERT INTO "main"."sqlite_sequence" VALUES ('Operations', 0);
INSERT INTO "main"."sqlite_sequence" VALUES ('Resources', 0);

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

PRAGMA foreign_keys = ON;