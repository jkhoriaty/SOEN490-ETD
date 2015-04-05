/*
Navicat SQLite Data Transfer

Source Server         : EDT
Source Server Version : 30808
Source Host           : :0

Target Server Type    : SQLite
Target Server Version : 30808
File Encoding         : 65001

Date: 2015-04-04 21:47:39
*/

PRAGMA foreign_keys = OFF;

-- ----------------------------
-- Table structure for ABCs
-- ----------------------------
DROP TABLE IF EXISTS "main"."ABCs";
CREATE TABLE "ABCs" (
"ABC_ID"  INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
"Intervention_ID"  INTEGER NOT NULL,
"Consciousness"  TEXT,
"Disoriented"  BOOLEAN DEFAULT False,
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
"Assignment_ID"  INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
"Equipment_ID"  INTEGER NOT NULL,
"Team_ID"  INTEGER NOT NULL,
"Assigned_Time"  DATETIME,
"Removed_Time"  DATETIME,
CONSTRAINT "fkey0" FOREIGN KEY ("Equipment_ID") REFERENCES "Equipments" ("Equipment_ID") ON DELETE CASCADE ON UPDATE CASCADE,
CONSTRAINT "fkey1" FOREIGN KEY ("Team_ID") REFERENCES "Teams" ("Team_ID")
);

-- ----------------------------
-- Records of Assigned_Equipment
-- ----------------------------

-- ----------------------------
-- Table structure for Calls
-- ----------------------------
DROP TABLE IF EXISTS "main"."Calls";
CREATE TABLE "Calls" (
"Call_ID"  INTEGER NOT NULL,
"Intervention_ID"  INTEGER,
"Call_Time"  DATETIME,
"Meeting_Point"  TEXT,
"First_Responder_Time"  DATETIME,
"First_Responder_Company"  TEXT,
"First_Responder_Vehicle"  TEXT,
"Ambulance_Time"  DATETIME,
"Ambulance_Company"  TEXT,
"Ambulance_Vehicle"  TEXT,
PRIMARY KEY ("Call_ID" ASC),
CONSTRAINT "fkey0" FOREIGN KEY ("Intervention_ID") REFERENCES "Interventions" ("Intervention_ID") ON DELETE CASCADE ON UPDATE CASCADE
);

-- ----------------------------
-- Records of Calls
-- ----------------------------

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
-- Table structure for Intervening_Teams
-- ----------------------------
DROP TABLE IF EXISTS "main"."Intervening_Teams";
CREATE TABLE "Intervening_Teams" (
"Intervening_ID"  INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
"Intervention_ID"  INTEGER NOT NULL,
"Team_ID"  INTEGER NOT NULL,
"Started_Intervening"  DATETIME,
"Stopped_Intervening"  DATETIME,
CONSTRAINT "fkey0" FOREIGN KEY ("Intervention_ID") REFERENCES "Interventions" ("Intervention_ID") ON DELETE CASCADE ON UPDATE CASCADE,
CONSTRAINT "fkey1" FOREIGN KEY ("Team_ID") REFERENCES "Teams" ("Team_ID") ON DELETE CASCADE ON UPDATE CASCADE
);

-- ----------------------------
-- Records of Intervening_Teams
-- ----------------------------

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
"Conclusion"  TEXT,
"Conclusion_Info"  TEXT,
"Conclusion_Time"  DATETIME,
CONSTRAINT "fkey0" FOREIGN KEY ("Operation_ID") REFERENCES "Operations" ("Operation_ID") ON DELETE CASCADE ON UPDATE CASCADE
);

-- ----------------------------
-- Records of Interventions
-- ----------------------------

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

-- ----------------------------
-- Table structure for Requests
-- ----------------------------
DROP TABLE IF EXISTS "main"."Requests";
CREATE TABLE "Requests" (
"Request_ID"  INTEGER NOT NULL,
"Operation_ID"  INTEGER,
"Client"  TEXT,
"Request"  TEXT,
"Handled_By"  TEXT,
"Recipient"  TEXT,
"Time"  DATETIME,
"FollowUp_Time"  DATETIME,
"Completion_Time"  DATETIME,
PRIMARY KEY ("Request_ID" ASC),
FOREIGN KEY ("Operation_ID") REFERENCES "Operations" ("Operation_ID") ON DELETE CASCADE ON UPDATE CASCADE
);

-- ----------------------------
-- Records of Requests
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
INSERT INTO "main"."sqlite_sequence" VALUES ('Equipment_Types', 6);
INSERT INTO "main"."sqlite_sequence" VALUES ('Statuses', 4);
INSERT INTO "main"."sqlite_sequence" VALUES ('Trainings', 3);
INSERT INTO "main"."sqlite_sequence" VALUES ('Volunteers', 0);
INSERT INTO "main"."sqlite_sequence" VALUES ('Resources', 0);
INSERT INTO "main"."sqlite_sequence" VALUES ('Equipments', 0);
INSERT INTO "main"."sqlite_sequence" VALUES ('Operations', 0);
INSERT INTO "main"."sqlite_sequence" VALUES ('Teams', 0);
INSERT INTO "main"."sqlite_sequence" VALUES ('Interventions', 0);
INSERT INTO "main"."sqlite_sequence" VALUES ('ABCs', 0);
INSERT INTO "main"."sqlite_sequence" VALUES ('Intervening_Teams', 0);
INSERT INTO "main"."sqlite_sequence" VALUES ('Assigned_Equipment', 0);

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
INSERT INTO "main"."Statuses" VALUES (0, 'Available');
INSERT INTO "main"."Statuses" VALUES (1, 'Moving');
INSERT INTO "main"."Statuses" VALUES (2, 'Intervening');
INSERT INTO "main"."Statuses" VALUES (3, 'Unavailable');

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
INSERT INTO "main"."Trainings" VALUES (0, 'General First Aid');
INSERT INTO "main"."Trainings" VALUES (1, 'First Responder');
INSERT INTO "main"."Trainings" VALUES (2, 'Medicine');

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
