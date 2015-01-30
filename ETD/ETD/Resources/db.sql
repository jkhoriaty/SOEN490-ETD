/*
Navicat SQLite Data Transfer

Source Server         : ETD
Source Server Version : 30808
Source Host           : :0

Target Server Type    : SQLite
Target Server Version : 30808
File Encoding         : 65001

Date: 2015-01-29 19:01:24
*/

PRAGMA foreign_keys = OFF;

-- ----------------------------
-- Table structure for Ambulance_Calls
-- ----------------------------
DROP TABLE IF EXISTS "main"."Ambulance_Calls";
CREATE TABLE [Ambulance_Calls] (
[Call_ID] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
[Intervention_ID] INTEGER  NOT NULL,
[Responder_Arrival] TIME,
[Ambulance_Arrival] TIME,
FOREIGN KEY ([Intervention_ID]) REFERENCES [Interventions]([Intervention_ID])
);

-- ----------------------------
-- Records of Ambulance_Calls
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
-- Table structure for Events
-- ----------------------------
DROP TABLE IF EXISTS "main"."Events";
CREATE TABLE "Events" (
"Event_ID"  VARCHAR(20) NOT NULL,
"Name"  TEXT NOT NULL,
"Start_Date"  DATE NOT NULL,
"End_Date"  DATE NOT NULL DEFAULT CURRENT_DATE,
"Location"  TEXT NOT NULL,
PRIMARY KEY ("Event_ID" ASC)
);

-- ----------------------------
-- Records of Events
-- ----------------------------

-- ----------------------------
-- Table structure for Interventions
-- ----------------------------
DROP TABLE IF EXISTS "main"."Interventions";
CREATE TABLE "Interventions" (
"Intervention_ID"  INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
"Event_ID"  VARCHAR(20) NOT NULL,
"Start_Time"  TIME NOT NULL,
"End_Time"  TIME,
"Ending_Code"  INTEGER,
"Other_Description"  TEXT,
"Type"  INTEGER,
CONSTRAINT "fkey0" FOREIGN KEY ("Event_ID") REFERENCES "Events" ("Event_ID"),
CONSTRAINT "fkey1" FOREIGN KEY ("Ending_Code") REFERENCES "Ending_Codes" ("Ending_Code"),
CONSTRAINT "fkey2" FOREIGN KEY ("Type") REFERENCES "Intervention_Types" ("Type_ID")
);

-- ----------------------------
-- Records of Interventions
-- ----------------------------

-- ----------------------------
-- Table structure for Intervention_Details
-- ----------------------------
DROP TABLE IF EXISTS "main"."Intervention_Details";
CREATE TABLE [Intervention_Details] (
[Detail_ID] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
[Intervention_ID] INTEGER  NOT NULL,
[Description] TEXT NOT NULL,
[Timestamp] TIME NOT NULL,
[Volunteer_ID] INTEGER,
FOREIGN KEY ([Intervention_ID]) REFERENCES [Interventions]([Intervention_ID])
FOREIGN KEY ([Volunteer_ID]) REFERENCES [Volunteers]([Volunteer_ID])
);

-- ----------------------------
-- Records of Intervention_Details
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
-- Table structure for sqlite_sequence
-- ----------------------------
DROP TABLE IF EXISTS "main"."sqlite_sequence";
CREATE TABLE sqlite_sequence(name,seq);

-- ----------------------------
-- Records of sqlite_sequence
-- ----------------------------
INSERT INTO "main"."sqlite_sequence" VALUES ('Ending_Codes', 9);
INSERT INTO "main"."sqlite_sequence" VALUES ('Intervention_Types', 25);
INSERT INTO "main"."sqlite_sequence" VALUES ('Training_Levels', 3);
INSERT INTO "main"."sqlite_sequence" VALUES ('Interventions', 0);

-- ----------------------------
-- Table structure for Training_Levels
-- ----------------------------
DROP TABLE IF EXISTS "main"."Training_Levels";
CREATE TABLE [Training_Levels] (
[Training_ID] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
[Description] TEXT  NOT NULL
);

-- ----------------------------
-- Records of Training_Levels
-- ----------------------------
INSERT INTO "main"."Training_Levels" VALUES (1, 'General First Aid');
INSERT INTO "main"."Training_Levels" VALUES (2, 'First Responder');
INSERT INTO "main"."Training_Levels" VALUES (3, 'Medicine');

-- ----------------------------
-- Table structure for Volunteers
-- ----------------------------
DROP TABLE IF EXISTS "main"."Volunteers";
CREATE TABLE [Volunteers] (
[Volunteer_ID] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
[Name] VARCHAR(100) NOT NULL,
[Training_Level] INTEGER NOT NULL,
[Hours_Worked] FLOAT DEFAULT 0,
[Interventions_Worked] INTEGER DEFAULT 0,
FOREIGN KEY ([Training_Level]) REFERENCES [Training_Levels]([Training_ID])
);

-- ----------------------------
-- Records of Volunteers
-- ----------------------------

-- ----------------------------
-- Table structure for Volunteer_Comments
-- ----------------------------
DROP TABLE IF EXISTS "main"."Volunteer_Comments";
CREATE TABLE [Volunteer_Comments] (
[Comment_ID] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
[Volunteer_ID] INTEGER NOT NULL,
[Author] VARCHAR(100) NOT NULL,
[Comment] TEXT NOT NULL,
FOREIGN KEY ([Volunteer_ID]) REFERENCES [Volunteers]([Volunteer_ID])
);

-- ----------------------------
-- Records of Volunteer_Comments
-- ----------------------------
