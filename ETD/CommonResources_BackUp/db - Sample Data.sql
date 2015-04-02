/*
Navicat SQLite Data Transfer

Source Server         : ETD-Debug
Source Server Version : 30808
Source Host           : :0

Target Server Type    : SQLite
Target Server Version : 30808
File Encoding         : 65001

Date: 2015-04-01 21:35:25
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
INSERT INTO "main"."ABCs" VALUES (1, 0, null, 'False', null, null, null, null, null);
INSERT INTO "main"."ABCs" VALUES (2, 0, null, 'False', null, null, null, null, null);

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
PRIMARY KEY ("Equipment_ID" ASC, "Team_ID" ASC),
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
INSERT INTO "main"."Calls" VALUES (1, 2, '2015-3-28 10:58:21.0', '', null, 'Random', '', '2015-3-28 10:58:42.0', 'Random', '');

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
"Intervention_ID"  INTEGER NOT NULL,
"Team_ID"  INTEGER NOT NULL,
"Started_Intervening"  DATETIME,
"Stopped_Intervening"  DATETIME,
PRIMARY KEY ("Intervention_ID" ASC, "Team_ID" ASC),
FOREIGN KEY ("Intervention_ID") REFERENCES "Interventions" ("Intervention_ID") ON DELETE CASCADE ON UPDATE CASCADE,
FOREIGN KEY ("Team_ID") REFERENCES "Teams" ("Team_ID") ON DELETE CASCADE ON UPDATE CASCADE
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
INSERT INTO "main"."Interventions" VALUES (1, 4, 1, '2015-3-28 10:56:47.813', 'John', 'Street', 'Headache', 1, 'M', 15, 'get_ComboBoxItem_Complaint_Allergy', null, 'get_ComboBoxItem_Conclusion_ReturnHome', null, '2015-3-28 10:57:44.0');
INSERT INTO "main"."Interventions" VALUES (2, 4, 2, '2015-3-28 10:57:46.554', 'Kid', 'Building', 'Headache', 1, 'M', 12, 'get_ComboBoxItem_Complaint_Allergy', null, 'get_ComboBoxItem_Conclusion_911', 'Hospital', '2015-3-28 10:58:0.0');
INSERT INTO "main"."Interventions" VALUES (3, 4, 3, '2015-3-28 10:59:5.491', 'AdultNoAmb', 'Notsure', 'Random', 1, 'M', 20, 'get_ComboBoxItem_Complaint_Allergy', null, 'get_ComboBoxItem_Conclusion_ReturnHome', null, '2015-3-28 10:59:43.0');
INSERT INTO "main"."Interventions" VALUES (4, 4, 4, '2015-3-28 10:59:46.46', 'AdultAmb', 'Street', 'Seizures', 1, 'F', 20, 'get_ComboBoxItem_Complaint_Allergy', null, 'get_ComboBoxItem_Conclusion_911', 'Hospital', '2015-4-28 10:59:43.0');
INSERT INTO "main"."Interventions" VALUES (5, 4, 1, '2015-3-28 11:58:38.907', null, null, null, null, null, 12, 'get_ComboBoxItem_Complaint_ChestPain', null, 'get_ComboBoxItem_Conclusion_ReturnHome', null, '2015-3-28 11:59:1.0');
INSERT INTO "main"."Interventions" VALUES (6, 4, 2, '2015-3-28 11:58:48.816', null, null, null, null, null, 18, 'get_ComboBoxItem_Complaint_Behavioural', null, 'get_ComboBoxItem_Conclusion_ReturnHome', null, '2015-3-28 11:59:5.0');
INSERT INTO "main"."Interventions" VALUES (7, 4, 3, '2015-3-28 11:59:6.825', null, null, null, null, null, 19, 'get_ComboBoxItem_Complaint_Anaphylaxis', null, 'get_ComboBoxItem_Conclusion_ReturnHome', null, '2015-3-28 11:59:17.0');
INSERT INTO "main"."Interventions" VALUES (8, 4, 4, '2015-3-28 11:59:18.737', null, null, null, null, null, 17, 'get_ComboBoxItem_Complaint_Environmental', null, 'get_ComboBoxItem_Conclusion_ReturnHome', null, '2015-3-28 11:59:35.0');
INSERT INTO "main"."Interventions" VALUES (9, 5, 1, '2015-3-28 12:2:37.312', null, null, null, null, null, 16, 'get_ComboBoxItem_Complaint_Environmental', null, 'get_ComboBoxItem_Conclusion_ReturnHome', null, '2015-3-28 12:2:46.0');
INSERT INTO "main"."Interventions" VALUES (10, 5, 2, '2015-3-28 12:2:48.174', null, null, null, null, null, 15, 'get_ComboBoxItem_Complaint_SoftTissueInjury', null, 'get_ComboBoxItem_Conclusion_ReturnHome', null, '2015-3-28 12:2:56.0');
INSERT INTO "main"."Interventions" VALUES (11, 5, 3, '2015-3-28 12:2:58.605', null, null, null, null, null, 22, 'get_ComboBoxItem_Complaint_HeadSpine', null, 'get_ComboBoxItem_Conclusion_ReturnHome', null, '2015-3-28 12:3:5.0');
INSERT INTO "main"."Interventions" VALUES (12, 5, 1, '2015-3-28 12:6:15.687', null, null, null, null, null, 23, 'get_ComboBoxItem_Complaint_HeadSpine', null, 'get_ComboBoxItem_Conclusion_911', null, null);
INSERT INTO "main"."Interventions" VALUES (13, 6, 1, '2015-3-28 12:26:0.712', null, null, null, null, null, 15, 'get_ComboBoxItem_Complaint_Headache', null, 'get_ComboBoxItem_Conclusion_ReturnHome', null, '2015-3-28 12:26:20.0');
INSERT INTO "main"."Interventions" VALUES (14, 5, 1, '2015-3-28 12:6:15.0', null, null, null, null, null, 11, 'get_ComboBoxItem_Complaint_Respiratory', null, 'get_ComboBoxItem_Conclusion_911', null, null);
INSERT INTO "main"."Interventions" VALUES (15, 9, 1, '2015-3-31 15:9:38.749', 'John', 'A', 'A', 2, 'M', 18, 'get_ComboBoxItem_Complaint_Headache', null, 'get_ComboBoxItem_Conclusion_ReturnHome', null, null);
INSERT INTO "main"."Interventions" VALUES (16, 9, 2, '2015-3-31 15:9:58.112', 'Lp', 'Home', 'Headache', null, null, 29, 'get_ComboBoxItem_Complaint_InsectBite', null, 'get_ComboBoxItem_Conclusion_ReturnHome', null, null);
INSERT INTO "main"."Interventions" VALUES (17, 9, 3, '2015-3-31 15:10:16.54', null, null, null, null, null, 12, 'get_ComboBoxItem_Complaint_Ocular', null, 'get_ComboBoxItem_Conclusion_ReturnHome', null, null);
INSERT INTO "main"."Interventions" VALUES (18, 10, 1, '2015-3-31 15:46:54.632', null, null, null, null, null, 21, 'get_ComboBoxItem_Complaint_Hemorrhage', null, 'get_ComboBoxItem_Conclusion_911', 'Hospital', '2015-3-31 15:47:11.0');

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
INSERT INTO "main"."Operations" VALUES (1, 'Festival de Jazz de Montreal', 'FJM', '2014-07-01 11:00:00.0', '2014-07-02 00:30.00.0', 'Ivana', null, null, null, null, null, null);
INSERT INTO "main"."Operations" VALUES (2, 'Just For Laughs', 'JFL', '2014-07-22 12:00:00.0', '2014-07-22 22:30:00.0', 'Zachary', null, null, null, null, null, null);
INSERT INTO "main"."Operations" VALUES (3, 'Osheaga Festival Music et Arts', 'OMA', '2014-08-01 13:00:00.0', '2014-08-01 23:00:00.0', 'Quinn', null, null, null, null, null, null);
INSERT INTO "main"."Operations" VALUES (4, 'A', 'A', '2015-3-12 12:12:0.0', '2015-3-13 12:12:0.0', 'Bob', 'AADFSD', 'SDFS', 'SDF', 'SDF', 'SDF', '');
INSERT INTO "main"."Operations" VALUES (5, 'A', 'A', '2015-2-25 12:12:0.0', '2015-3-27 12:12:0.0', 'Bob', null, null, null, null, null, null);
INSERT INTO "main"."Operations" VALUES (6, 'A', 'A', '2015-3-21 12:12:0.0', '2015-3-28 12:12:0.0', 'John', null, null, null, null, null, null);
INSERT INTO "main"."Operations" VALUES (7, 'A', 'A', '2015-3-6 12:12:0.0', '2015-3-12 12:12:0.0', 'Bob', null, null, null, null, null, null);
INSERT INTO "main"."Operations" VALUES (8, 'A', 'A', '2015-3-20 12:12:0.0', '2015-3-21 12:12:0.0', 'John', null, null, null, null, null, null);
INSERT INTO "main"."Operations" VALUES (9, 'Jill', 'JL', '2015-3-20 12:12:0.0', '2015-3-21 21:12:0.0', 'John', null, null, null, null, null, null);
INSERT INTO "main"."Operations" VALUES (10, 'Tom', 'TO', '2015-3-21 12:12:0.0', '2015-3-22 12:12:0.0', 'Bob', null, null, null, null, null, null);
INSERT INTO "main"."Operations" VALUES (11, 'Ralph', 'RH', '2015-3-14 12:0:0.0', '2015-3-16 12:0:0.0', 'Bob', null, null, null, null, null, null);
INSERT INTO "main"."Operations" VALUES (12, 'A', 'A', '2015-2-2 12:0:0.0', '2015-2-3 12:0:0.0', 'John', 'None', 'Blahh', 'Blah', 'Blah', 'Blah', 'Nothing');
INSERT INTO "main"."Operations" VALUES (13, 'B', 'B', '2014-3-15 12:12:12.0', '2014-3-16 12:11:11.0', 'Bob', 'Nothing', 'Bleh', 'Blah', 'Blah', 'Blah', 'Blah');
INSERT INTO "main"."Operations" VALUES (14, 'C', 'C', '2015-4-12 12:10:10.0', '2015-3-15 10:0:0.0', 'George', 'Nothing at all', null, null, null, null, 'Blalskj');

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
INSERT INTO "main"."sqlite_sequence" VALUES ('Volunteers', 26);
INSERT INTO "main"."sqlite_sequence" VALUES ('Resources', 0);
INSERT INTO "main"."sqlite_sequence" VALUES ('Equipments', 0);
INSERT INTO "main"."sqlite_sequence" VALUES ('Operations', 14);
INSERT INTO "main"."sqlite_sequence" VALUES ('Teams', 2);
INSERT INTO "main"."sqlite_sequence" VALUES ('Interventions', 18);
INSERT INTO "main"."sqlite_sequence" VALUES ('ABCs', 2);

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
INSERT INTO "main"."Volunteers" VALUES (1, 'Annie', 0);
INSERT INTO "main"."Volunteers" VALUES (2, 'Bob', 2);
INSERT INTO "main"."Volunteers" VALUES (3, 'Carla', 1);
INSERT INTO "main"."Volunteers" VALUES (4, 'David', 0);
INSERT INTO "main"."Volunteers" VALUES (5, 'Ellen', 1);
INSERT INTO "main"."Volunteers" VALUES (6, 'Frank', 0);
INSERT INTO "main"."Volunteers" VALUES (7, 'Genvieve', 2);
INSERT INTO "main"."Volunteers" VALUES (8, 'House', 2);
INSERT INTO "main"."Volunteers" VALUES (9, 'Ivana', 0);
INSERT INTO "main"."Volunteers" VALUES (10, 'Jean', 1);
INSERT INTO "main"."Volunteers" VALUES (11, 'Katie', 2);
INSERT INTO "main"."Volunteers" VALUES (12, 'Lou', 1);
INSERT INTO "main"."Volunteers" VALUES (13, 'Melissa', 2);
INSERT INTO "main"."Volunteers" VALUES (14, 'Norman', 0);
INSERT INTO "main"."Volunteers" VALUES (15, 'Olivia', 2);
INSERT INTO "main"."Volunteers" VALUES (16, 'Paul', 1);
INSERT INTO "main"."Volunteers" VALUES (17, 'Quinn', 2);
INSERT INTO "main"."Volunteers" VALUES (18, 'Roger', 1);
INSERT INTO "main"."Volunteers" VALUES (19, 'Stephanie', 0);
INSERT INTO "main"."Volunteers" VALUES (20, 'Todd', 2);
INSERT INTO "main"."Volunteers" VALUES (21, 'Ursula', 0);
INSERT INTO "main"."Volunteers" VALUES (22, 'Victor', 2);
INSERT INTO "main"."Volunteers" VALUES (23, 'Wendy', 1);
INSERT INTO "main"."Volunteers" VALUES (24, 'Xavier', 0);
INSERT INTO "main"."Volunteers" VALUES (25, 'Yvette', 2);
INSERT INTO "main"."Volunteers" VALUES (26, 'Zachary', 1);
