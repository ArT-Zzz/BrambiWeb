--
-- Archivo generado con SQLiteStudio v3.4.4 el lun. nov. 13 12:12:18 2023
--
-- Codificaci�n de texto usada: System
--
PRAGMA foreign_keys = off;
BEGIN TRANSACTION;

-- Tabla: academies
CREATE TABLE IF NOT EXISTS academies (
    academyId INTEGER      PRIMARY KEY AUTOINCREMENT
                           NOT NULL,
    name      VARCHAR (40) 
);
INSERT INTO academies (academyId, name) VALUES (1, 'Computacion');
INSERT INTO academies (academyId, name) VALUES (2, 'Informatica');
INSERT INTO academies (academyId, name) VALUES (3, 'Sistemas electronicos');
INSERT INTO academies (academyId, name) VALUES (4, 'Sistemas digitales');
INSERT INTO academies (academyId, name) VALUES (5, 'Infraestructura y tecnologias de la informacion');

-- Tabla: areas
CREATE TABLE IF NOT EXISTS areas (
    areaId INTEGER      PRIMARY KEY AUTOINCREMENT,
    name   VARCHAR (40) 
);
INSERT INTO areas (areaId, name) VALUES (1, 'Osciloscopios');
INSERT INTO areas (areaId, name) VALUES (2, 'Fuentes de alimentacion');
INSERT INTO areas (areaId, name) VALUES (3, 'Multimetros');
INSERT INTO areas (areaId, name) VALUES (4, 'Generadores');
INSERT INTO areas (areaId, name) VALUES (5, 'Frecuencimetros');
INSERT INTO areas (areaId, name) VALUES (6, 'Programadores');
INSERT INTO areas (areaId, name) VALUES (7, 'Kit de fibra optica');
INSERT INTO areas (areaId, name) VALUES (8, 'Herramientas para redes');
INSERT INTO areas (areaId, name) VALUES (9, 'Motores');
INSERT INTO areas (areaId, name) VALUES (10, 'Tarjetas de desarrollo');
INSERT INTO areas (areaId, name) VALUES (11, 'Brazos robot');
INSERT INTO areas (areaId, name) VALUES (12, 'Herramientas para cableado');
INSERT INTO areas (areaId, name) VALUES (13, 'Herramientas para soldar');

-- Tabla: classrooms
CREATE TABLE IF NOT EXISTS classrooms (
    classroomId INTEGER      PRIMARY KEY AUTOINCREMENT,
    name        VARCHAR (40),
    clave       VARCHAR (8),
    divisionId  INTEGER      REFERENCES divisions (divisionId),
    FOREIGN KEY (
        divisionId
    )
    REFERENCES divisions (divisionId) 
);
INSERT INTO classrooms (classroomId, name, clave, divisionId) VALUES (1, 'Laboratorio de computo E', 'F:LABE', 1);
INSERT INTO classrooms (classroomId, name, clave, divisionId) VALUES (2, 'Laboratorio de computo D', 'F:LABD', 1);
INSERT INTO classrooms (classroomId, name, clave, divisionId) VALUES (3, 'Laboratorio de computo C', 'F:LABC', 1);
INSERT INTO classrooms (classroomId, name, clave, divisionId) VALUES (4, 'Laboratorio de computo B', 'F:LABB', 1);
INSERT INTO classrooms (classroomId, name, clave, divisionId) VALUES (5, 'Laboratorio de computo A', 'F:LABA', 1);
INSERT INTO classrooms (classroomId, name, clave, divisionId) VALUES (6, 'Aula F-215 Interactiva', 'F:I215', 1);
INSERT INTO classrooms (classroomId, name, clave, divisionId) VALUES (7, 'Laboratorio de software 1', 'F:SL1', 1);
INSERT INTO classrooms (classroomId, name, clave, divisionId) VALUES (8, 'Laboratorio de software libre 3', 'F:SL3', 1);
INSERT INTO classrooms (classroomId, name, clave, divisionId) VALUES (9, 'Laboratorio de software libre 2', 'F:SL2', 1);
INSERT INTO classrooms (classroomId, name, clave, divisionId) VALUES (10, 'Taller de electronica B', 'F-ELECB', 1);
INSERT INTO classrooms (classroomId, name, clave, divisionId) VALUES (11, 'Taller de electronica C', 'F-ELECC', 1);
INSERT INTO classrooms (classroomId, name, clave, divisionId) VALUES (12, 'Taller de electronica A', 'F-ELECA', 1);
INSERT INTO classrooms (classroomId, name, clave, divisionId) VALUES (13, 'Taller de sistemas digitales II', 'F-LSDIG2', 1);
INSERT INTO classrooms (classroomId, name, clave, divisionId) VALUES (14, 'Taller de sistemas digitales I', 'F-LSDIG1', 1);
INSERT INTO classrooms (classroomId, name, clave, divisionId) VALUES (15, 'Laboratorio de Redes I', 'F:LRED1', 1);
INSERT INTO classrooms (classroomId, name, clave, divisionId) VALUES (16, 'Laboratorio de Redes II', 'F:LRED2', 1);
INSERT INTO classrooms (classroomId, name, clave, divisionId) VALUES (17, 'Aula', 'F-204A', 1);
INSERT INTO classrooms (classroomId, name, clave, divisionId) VALUES (18, 'Aula', 'F-204B', 1);
INSERT INTO classrooms (classroomId, name, clave, divisionId) VALUES (19, 'Aula', 'F-203A', 1);
INSERT INTO classrooms (classroomId, name, clave, divisionId) VALUES (20, 'Aula 203B', 'F-203B', 1);
INSERT INTO classrooms (classroomId, name, clave, divisionId) VALUES (21, 'Aula 202', 'F-202', 1);
INSERT INTO classrooms (classroomId, name, clave, divisionId) VALUES (22, 'Taller de mantenimiento', 'F-MTO', 1);

-- Tabla: coordinators
CREATE TABLE IF NOT EXISTS coordinators (
    coordinatorId CHAR (10)    PRIMARY KEY,
    name          VARCHAR (30),
    lastNameP     VARCHAR (30),
    lastNameM     VARCHAR (30),
    password      VARCHAR (50) 
);
INSERT INTO coordinators (coordinatorId, name, lastNameP, lastNameM, password) VALUES ('ZzfV8bJ4zIFA9VKJuivNXg==', 'Andres', 'Figueroa', 'Flores', 'F8t128gjJIaegDAGPG//LA==');

-- Tabla: divisions
CREATE TABLE IF NOT EXISTS divisions (
    divisionId INTEGER      PRIMARY KEY AUTOINCREMENT,
    name       VARCHAR (40) 
);
INSERT INTO divisions (divisionId, name) VALUES (1, 'Desarrollo de software');

-- Tabla: DyLEquipments
CREATE TABLE IF NOT EXISTS DyLEquipments (
    DyLEquipmentId INTEGER       PRIMARY KEY AUTOINCREMENT,
    statusId       INTEGER,
    equipmentId    VARCHAR (15)  REFERENCES equipments (equipmentId),
    description    VARCHAR (200),
    dateOfEvent    DATE,
    dateOfReturn   DATE,
    objectReturn   VARCHAR(100),
    studentId      CHAR (8)      REFERENCES students (studentId),
    coordinatorId  CHAR (10)      REFERENCES coordinators (coordinatorId),
    FOREIGN KEY (
        statusId
    )
    REFERENCES status (statusId),
    FOREIGN KEY (
        equipmentId
    )
    REFERENCES equipments (equipmentId),
    FOREIGN KEY (
        studentId
    )
    REFERENCES students (studentId),
    FOREIGN KEY (
        coordinatorId
    )
    REFERENCES coordinators (coordinatorId) 
);

-- Tabla: equipments
CREATE TABLE IF NOT EXISTS equipments (
    equipmentId   VARCHAR (15)  PRIMARY KEY,
    name          VARCHAR (40),
    areaId        INTEGER,
    description   VARCHAR (200),
    year          INTEGER,
    statusId      INTEGER,
    controlNumber VARCHAR (20),
    coordinatorId CHAR (10),
    FOREIGN KEY (
        areaId
    )
    REFERENCES areas (areaId),
    FOREIGN KEY (
        statusId
    )
    REFERENCES status (statusId),
    FOREIGN KEY (
        coordinatorId
    )
    REFERENCES coordinators (coordinatorId) 
);
INSERT INTO equipments (equipmentId, name, areaId, description, year, statusId, controlNumber, coordinatorId) VALUES ('OSC101', 'Digital Oscilloscope Tektronix TBS1000C ', 1, 'Digital oscilloscope with 2 channels and 100MHz bandwidth', 2015, 1, '123456789012345', 'CokUy+/bPTOOfNMUx77mMg==');
INSERT INTO equipments (equipmentId, name, areaId, description, year, statusId, controlNumber, coordinatorId) VALUES ('OSC102', 'Digital Oscilloscope Rigol DS1054Z ', 1, '4-channel digital oscilloscope with 50MHz bandwidth', 2018, 4, '234567890123456', 'CokUy+/bPTOOfNMUx77mMg==');
INSERT INTO equipments (equipmentId, name, areaId, description, year, statusId, controlNumber, coordinatorId) VALUES ('OSC103', 'Oscilloscope Keysight DSOX1102G ', 1, 'Mixed-signal oscilloscope with 100MHz bandwidth', 2019, 1, '3456789012345678', 'CokUy+/bPTOOfNMUx77mMg==');
INSERT INTO equipments (equipmentId, name, areaId, description, year, statusId, controlNumber, coordinatorId) VALUES ('OSC104', 'Oscilloscope Siglent SDS1104X-E ', 1, '4-channel oscilloscope with 100MHz bandwidth', 2017, 1, '45678901234567890', 'CokUy+/bPTOOfNMUx77mMg==');
INSERT INTO equipments (equipmentId, name, areaId, description, year, statusId, controlNumber, coordinatorId) VALUES ('OSC105', 'Digital Storage Oscilloscope GW Instek GDS-1054B ', 1, 'Digital storage oscilloscope with 5.7" display', 2016, 1, '5678901234567890123', 'CokUy+/bPTOOfNMUx77mMg==');
INSERT INTO equipments (equipmentId, name, areaId, description, year, statusId, controlNumber, coordinatorId) VALUES ('FNT201', 'Triple Output DC Power Supply Rigol DP832 ', 2, 'Triple output DC power supply with programmable features', 2019, 1, '67890123456789012345', 'CokUy+/bPTOOfNMUx77mMg==');
INSERT INTO equipments (equipmentId, name, areaId, description, year, statusId, controlNumber, coordinatorId) VALUES ('FNT202', 'DC Power Supply Keysight E36312A ', 2, 'High-performance DC power supply with three outputs', 2016, 1, '78901234567890123456', 'CokUy+/bPTOOfNMUx77mMg==');
INSERT INTO equipments (equipmentId, name, areaId, description, year, statusId, controlNumber, coordinatorId) VALUES ('FNT203', 'Power Supply Programmable Tektronix PWS4305 ', 2, 'Programmable power supply with 30V and 5A outputs', 2018, 1, '89012345678901234567', 'CokUy+/bPTOOfNMUx77mMg==');
INSERT INTO equipments (equipmentId, name, areaId, description, year, statusId, controlNumber, coordinatorId) VALUES ('FNT204', 'Power Supply DC Siglent SPD3303X-E ', 2, 'Programmable linear DC power supply with three outputs', 2017, 1, '90123456789012345678', 'CokUy+/bPTOOfNMUx77mMg==');
INSERT INTO equipments (equipmentId, name, areaId, description, year, statusId, controlNumber, coordinatorId) VALUES ('FNT205', 'Triple Output Power Supply GW Instek GPS-3303 ', 2, 'Triple output linear DC power supply with low ripple', 2020, 1, '12345678901234567890', 'CokUy+/bPTOOfNMUx77mMg==');
INSERT INTO equipments (equipmentId, name, areaId, description, year, statusId, controlNumber, coordinatorId) VALUES ('MMT301', 'Multimeter Fluke 87V Industrial ', 3, 'True RMS multimeter with industrial-grade features', 2022, 1, '23456789012345678901', 'CokUy+/bPTOOfNMUx77mMg==');
INSERT INTO equipments (equipmentId, name, areaId, description, year, statusId, controlNumber, coordinatorId) VALUES ('MMT302', 'Multimeter Agilent U1272A True RMS ', 3, 'Precision multimeter with True RMS capabilities', 2017, 1, '34567890123456789012', 'CokUy+/bPTOOfNMUx77mMg==');
INSERT INTO equipments (equipmentId, name, areaId, description, year, statusId, controlNumber, coordinatorId) VALUES ('MMT303', 'Multimeter Auto-Ranging Klein Tools MM700 ', 3, 'Auto-ranging multimeter with backlit display', 2019, 1, '45678901234567890123', 'CokUy+/bPTOOfNMUx77mMg==');
INSERT INTO equipments (equipmentId, name, areaId, description, year, statusId, controlNumber, coordinatorId) VALUES ('MMT304', 'Mini Multimeter Autoranging Extech EX330  ', 3, 'Compact autoranging multimeter with built-in stand', 2018, 1, '56789012345678901234', 'CokUy+/bPTOOfNMUx77mMg==');
INSERT INTO equipments (equipmentId, name, areaId, description, year, statusId, controlNumber, coordinatorId) VALUES ('MMT305', 'Digital Multimeter UNI-T UT61E ', 3, 'Digital multimeter with 22000-count display', 2016, 1, '67890123456789012345', 'CokUy+/bPTOOfNMUx77mMg==');
INSERT INTO equipments (equipmentId, name, areaId, description, year, statusId, controlNumber, coordinatorId) VALUES ('GEN401', 'Waveform Generator Function/Arbitrary Rigol DG1022Z  ', 4, 'Dual-channel function/arbitrary waveform generator', 2018, 2, '78901234567890123456', 'CokUy+/bPTOOfNMUx77mMg==');
INSERT INTO equipments (equipmentId, name, areaId, description, year, statusId, controlNumber, coordinatorId) VALUES ('GEN402', 'Waveform Generator Function/Arbitrary Keysight 33522B Dual Channel  ', 4, 'Dual-channel 30 MHz arbitrary waveform generator', 2019, 2, '89012345678901234567', 'CokUy+/bPTOOfNMUx77mMg==');
INSERT INTO equipments (equipmentId, name, areaId, description, year, statusId, controlNumber, coordinatorId) VALUES ('GEN403', 'Waveform Generator Function/Arbitrary Siglent SDG1032X ', 4, '2-channel arbitrary waveform generator with 30MHz bandwidth', 2020, 2, '90123456789012345678', 'CokUy+/bPTOOfNMUx77mMg==');
INSERT INTO equipments (equipmentId, name, areaId, description, year, statusId, controlNumber, coordinatorId) VALUES ('GEN404', 'Function Generator B&K Precision 4040A Sweep ', 4, 'Function generator with linear and logarithmic sweep', 2017, 2, '12345678901234567890', 'CokUy+/bPTOOfNMUx77mMg==');
INSERT INTO equipments (equipmentId, name, areaId, description, year, statusId, controlNumber, coordinatorId) VALUES ('GEN405', 'Function Generator Hantek DDS-3005 ', 4, '5MHz DDS function generator with 200MSa/s sample rate', 2016, 1, '23456789012345678901', 'CokUy+/bPTOOfNMUx77mMg==');
INSERT INTO equipments (equipmentId, name, areaId, description, year, statusId, controlNumber, coordinatorId) VALUES ('FREQ501', 'Counter/Timer Fluke 1912A Universal ', 5, 'Universal counter/timer with 12 digits resolution', 2019, 1, '34567890123456789012', 'CokUy+/bPTOOfNMUx77mMg==');
INSERT INTO equipments (equipmentId, name, areaId, description, year, statusId, controlNumber, coordinatorId) VALUES ('FREQ502', 'Frequency Counter/Timer Keysight 53230A Universal ', 5, 'Universal frequency counter/timer with 350 MHz', 2018, 2, '45678901234567890123', 'CokUy+/bPTOOfNMUx77mMg==');
INSERT INTO equipments (equipmentId, name, areaId, description, year, statusId, controlNumber, coordinatorId) VALUES ('FREQ503', 'Counter Agilent 53132A 225 MHz Universal ', 5, 'Universal counter with two input channels', 2017, 1, '56789012345678901234', 'CokUy+/bPTOOfNMUx77mMg==');
INSERT INTO equipments (equipmentId, name, areaId, description, year, statusId, controlNumber, coordinatorId) VALUES ('FREQ504', 'Frequency Counter B&K Precision 1856C ', 5, 'Frequency counter with 2.4 GHz range', 2020, 1, '67890123456789012345', 'CokUy+/bPTOOfNMUx77mMg==');
INSERT INTO equipments (equipmentId, name, areaId, description, year, statusId, controlNumber, coordinatorId) VALUES ('FREQ505', 'Frequency Counter GW Instek GFC-8010H ', 5, 'High-frequency counter with 1.3 GHz range', 2016, 1, '78901234567890123456', 'CokUy+/bPTOOfNMUx77mMg==');
INSERT INTO equipments (equipmentId, name, areaId, description, year, statusId, controlNumber, coordinatorId) VALUES ('PROG601', 'Universal Programmer Xeltek SuperPro 610P ', 6, 'Universal programmer with support for a wide range of devices', 2018, 1, '90123456789012345678', 'CokUy+/bPTOOfNMUx77mMg==');
INSERT INTO equipments (equipmentId, name, areaId, description, year, statusId, controlNumber, coordinatorId) VALUES ('PROG602', 'Universal Programmer Wellon VP-496 ', 6, 'High-speed universal programmer with USB interface', 2019, 1, '12345678901234567890', 'CokUy+/bPTOOfNMUx77mMg==');
INSERT INTO equipments (equipmentId, name, areaId, description, year, statusId, controlNumber, coordinatorId) VALUES ('PROG603', 'Universal Programmer Data I/O PS388 ', 6, 'Compact universal programmer with easy-to-use interface', 2017, 1, '23456789012345678901', 'CokUy+/bPTOOfNMUx77mMg==');
INSERT INTO equipments (equipmentId, name, areaId, description, year, statusId, controlNumber, coordinatorId) VALUES ('PROG604', 'USB Programmer TOP853 Universal ', 6, 'Universal programmer with USB interface and LCD display', 2020, 1, '34567890123456789012', 'CokUy+/bPTOOfNMUx77mMg==');
INSERT INTO equipments (equipmentId, name, areaId, description, year, statusId, controlNumber, coordinatorId) VALUES ('PROG605', 'Universal Programmer BeeProg+ ', 6, 'Universal programmer with 48-pin ZIF socket', 2016, 1, '45678901234567890123', 'CokUy+/bPTOOfNMUx77mMg==');
INSERT INTO equipments (equipmentId, name, areaId, description, year, statusId, controlNumber, coordinatorId) VALUES ('FIBO701', 'FiberInspector Mini Fluke Networks FT500 ', 7, 'Portable fiber inspection scope for inspecting connectors', 2019, 2, '56789012345678901234', 'CokUy+/bPTOOfNMUx77mMg==');
INSERT INTO equipments (equipmentId, name, areaId, description, year, statusId, controlNumber, coordinatorId) VALUES ('FIBO702', 'VIAVI FFL-050 Visual Fault Locator', 7, 'Visual fault locator for fiber optic cable testing', 2018, 1, '67890123456789012345', 'CokUy+/bPTOOfNMUx77mMg==');
INSERT INTO equipments (equipmentId, name, areaId, description, year, statusId, controlNumber, coordinatorId) VALUES ('NETW801', 'Fluke Networks Pro3000 Tone and Probe Kit', 8, 'Tone and probe kit for network cable tracing', 2019, 1, '12345678901234567890', 'CokUy+/bPTOOfNMUx77mMg==');
INSERT INTO equipments (equipmentId, name, areaId, description, year, statusId, controlNumber, coordinatorId) VALUES ('NETW802', 'Klein Tools VDV501-823 Scout Pro 2 Tester Kit', 8, 'Network tester kit with cable length measurement', 2018, 1, '23456789012345678901', 'CokUy+/bPTOOfNMUx77mMg==');
INSERT INTO equipments (equipmentId, name, areaId, description, year, statusId, controlNumber, coordinatorId) VALUES ('MOTR901', 'Maxon RE35 DC Motor', 9, 'High-performance DC motor for various applications', 2018, 1, '67890123456789012345', 'CokUy+/bPTOOfNMUx77mMg==');
INSERT INTO equipments (equipmentId, name, areaId, description, year, statusId, controlNumber, coordinatorId) VALUES ('MOTR902', 'Faulhaber 2232U012S DC Motor', 9, 'Micro DC motor with high torque and compact design', 2019, 1, '78901234567890123456', 'CokUy+/bPTOOfNMUx77mMg==');
INSERT INTO equipments (equipmentId, name, areaId, description, year, statusId, controlNumber, coordinatorId) VALUES ('DEVL1001', 'Arduino Uno R3', 10, 'Open-source electronics platform with easy-to-use hardware and software', 2017, 1, '23456789012345678901', 'CokUy+/bPTOOfNMUx77mMg==');
INSERT INTO equipments (equipmentId, name, areaId, description, year, statusId, controlNumber, coordinatorId) VALUES ('DEVL1002', 'Raspberry Pi 4 Model B', 10, 'Single-board computer with powerful features for various applications', 2019, 1, '34567890123456789012', 'CokUy+/bPTOOfNMUx77mMg==');
INSERT INTO equipments (equipmentId, name, areaId, description, year, statusId, controlNumber, coordinatorId) VALUES ('CABL1201', 'Greenlee PA901053 ProGrip Punchdown Tool', 12, 'ProGrip punchdown tool for terminating and cutting wires', 2019, 1, '67890123456789012345', 'CokUy+/bPTOOfNMUx77mMg==');
INSERT INTO equipments (equipmentId, name, areaId, description, year, statusId, controlNumber, coordinatorId) VALUES ('SOLD1301', 'Weller WE1010NA Digital Soldering Station', 13, 'Digital soldering station with adjustable temperature control', 2018, 1, '78901234567890123456', 'CokUy+/bPTOOfNMUx77mMg==');

-- Tabla: groups
CREATE TABLE IF NOT EXISTS groups (
    groupId INTEGER  PRIMARY KEY AUTOINCREMENT,
    name    CHAR (3) 
);
INSERT INTO groups (groupId, name) VALUES (1, '7F1');
INSERT INTO groups (groupId, name) VALUES (2, '1F1');
INSERT INTO groups (groupId, name) VALUES (3, '1K1');
INSERT INTO groups (groupId, name) VALUES (4, '8A1');
INSERT INTO groups (groupId, name) VALUES (5, '7B1');
INSERT INTO groups (groupId, name) VALUES (6, '5E1');
INSERT INTO groups (groupId, name) VALUES (7, '5C1');
INSERT INTO groups (groupId, name) VALUES (8, '5D1');
INSERT INTO groups (groupId, name) VALUES (9, '6D1');
INSERT INTO groups (groupId, name) VALUES (10, '6C1');
INSERT INTO groups (groupId, name) VALUES (11, '6B1');
INSERT INTO groups (groupId, name) VALUES (12, '2H1');
INSERT INTO groups (groupId, name) VALUES (13, '2G1');
INSERT INTO groups (groupId, name) VALUES (14, '3B1');
INSERT INTO groups (groupId, name) VALUES (15, '3A1');
INSERT INTO groups (groupId, name) VALUES (16, '3F1');
INSERT INTO groups (groupId, name) VALUES (17, '4D1');
INSERT INTO groups (groupId, name) VALUES (18, '4E1');
INSERT INTO groups (groupId, name) VALUES (19, '4F1');
INSERT INTO groups (groupId, name) VALUES (20, '4G1');
INSERT INTO groups (groupId, name) VALUES (21, '1O1');
INSERT INTO groups (groupId, name) VALUES (22, '8C1');

-- Tabla: maintain
CREATE TABLE IF NOT EXISTS maintain (
    maintainId    INTEGER      PRIMARY KEY AUTOINCREMENT,
    maintenanceId INTEGER      REFERENCES maintenanceRegister (maintenanceId),
    equipmentId   VARCHAR (15) REFERENCES equipments (equipmentId) 
);

-- Tabla: maintenanceRegister
CREATE TABLE IF NOT EXISTS maintenanceRegister (
    maintenanceId                   INTEGER       PRIMARY KEY AUTOINCREMENT,
    maintenanceTypeId               INTEGER,
    maintenanceInstructions         VARCHAR (255),
    programmedDate                  DATE,
    exitDate                        DATE,
    maintenanceDescription          VARCHAR (255),
    storerId                        CHAR (10),
    maintenanceMaterialsDescription VARCHAR (100),
    FOREIGN KEY (
        maintenanceTypeId
    )
    REFERENCES maintenanceTypes (maintenanceTypeId),
    FOREIGN KEY (
        storerId
    )
    REFERENCES storers (storerId) 
);

-- Tabla: maintenanceTypes
CREATE TABLE IF NOT EXISTS maintenanceTypes (
    maintenanceTypeId INTEGER      PRIMARY KEY AUTOINCREMENT,
    name              VARCHAR (10) 
);
INSERT INTO maintenanceTypes (maintenanceTypeId, name) VALUES (1, 'Preventivo');
INSERT INTO maintenanceTypes (maintenanceTypeId, name) VALUES (2, 'Correctivo');
INSERT INTO maintenanceTypes (maintenanceTypeId, name) VALUES (3, 'Predictivo');

-- Tabla: petitionDetails
CREATE TABLE IF NOT EXISTS petitionDetails (
    petitionDetailsId INTEGER PRIMARY KEY AUTOINCREMENT, 
    petitionId INTEGER REFERENCES petitions (petitionId), 
    equipmentId VARCHAR (15) REFERENCES equipments (equipmentId), 
    statusId INTEGER REFERENCES status (statusId), 
    dispatchTime TIME, 
    returnTime TIME, 
    requestedDate DATE, 
    currentDate DATE
    );
INSERT INTO petitionDetails (petitionDetailsId, petitionId, equipmentId, statusId, dispatchTime, returnTime, requestedDate, currentDate) VALUES (1, 1, 'OSC103', 1, '10:20', '12:00', '2023/11/18', '2023/11/13');

-- Tabla: petitions
CREATE TABLE IF NOT EXISTS petitions (
    petitionId INTEGER PRIMARY KEY AUTOINCREMENT, 
    classroomId INTEGER REFERENCES classrooms (classroomId), 
    professorId CHAR (10) REFERENCES professors (professorId), 
    storerId CHAR (10) REFERENCES storers (storerId), 
    subjectId VARCHAR (13) REFERENCES subjects (subjectId));
    
INSERT INTO petitions (petitionId, classroomId, professorId, storerId, subjectId) VALUES (1, 5, 's+d1CVba7aAcZHO4z0LPaw==', 'kT556oT3ig6WU155xbGHTw==', '18MPEDS0620');

-- Tabla: professors
CREATE TABLE IF NOT EXISTS professors (
    professorId CHAR (10)    PRIMARY KEY,
    name        VARCHAR (30),
    lastNameP   VARCHAR (30),
    lastNameM   VARCHAR (30),
    nip         VARCHAR (50),
    password    VARCHAR (50) 
);
INSERT INTO professors (professorId, name, lastNameP, lastNameM, nip, password) VALUES ('owTkVgr0sk6DHaUug+7/SA==', 'Carlos', 'Molina', 'Martinez', '1000', 'F8t128gjJIaegDAGPG//LA==');
INSERT INTO professors (professorId, name, lastNameP, lastNameM, nip, password) VALUES ('s+d1CVba7aAcZHO4z0LPaw==', 'Nancy del Carmen', 'Benavides', 'Medina', '2000', 'F8t128gjJIaegDAGPG//LA==');
INSERT INTO professors (professorId, name, lastNameP, lastNameM, nip, password) VALUES ('AEd+FHe4V36Wpe7FXKWZtg==', 'Diana Marisol', 'Figueroa', 'Flores', '3000', 'F8t128gjJIaegDAGPG//LA==');
INSERT INTO professors (professorId, name, lastNameP, lastNameM, nip, password) VALUES ('WlFpmcuL+BVJ8ArjUZQrJg==', 'Clara Gabriela', 'Garcia', 'Duran', '4000', 'F8t128gjJIaegDAGPG//LA=='); 
INSERT INTO professors (professorId, name, lastNameP, lastNameM, nip, password) VALUES ('tOg3M5y+JKu0JgQGDtUn2Q==', 'Carlos Alberto', 'Ramirez', 'Garcia', '5000', 'F8t128gjJIaegDAGPG//LA==');

-- Tabla: requestDetails
CREATE TABLE IF NOT EXISTS requestDetails (
    requestDetailsId INTEGER      PRIMARY KEY AUTOINCREMENT,
    requestId        INTEGER      REFERENCES requests (requestId),
    equipmentId      VARCHAR (15) REFERENCES equipments (equipmentId),
    statusId         INTEGER      REFERENCES status (statusId),
    professorNIP     INTEGER,
    dispatchTime     TIME,
    returnTime       TIME,
    requestedDate    DATE,
    currentDate      DATE
);
INSERT INTO requestDetails (requestDetailsId, requestId, equipmentId, statusId, professorNIP, dispatchTime, returnTime, requestedDate, currentDate) VALUES (1, 1, 'OSC101', 1, 1, '07:00', '08:40', '2023-11-10', '2023-11-09');
INSERT INTO requestDetails (requestDetailsId, requestId, equipmentId, statusId, professorNIP, dispatchTime, returnTime, requestedDate, currentDate) VALUES (2, 2, 'DEVL1001', 1, 2, '08:40', '09:30', '2023-11-15', '2023-11-14');
INSERT INTO requestDetails (requestDetailsId, requestId, equipmentId, statusId, professorNIP, dispatchTime, returnTime, requestedDate, currentDate) VALUES (3, 3, 'MOTR901', 1, 0, '09:30', '10:20', '2023-11-18', '2023-11-17');
INSERT INTO requestDetails (requestDetailsId, requestId, equipmentId, statusId, professorNIP, dispatchTime, returnTime, requestedDate, currentDate) VALUES (4, 4, 'FIBO701', 2, 1, '10:20', '11:10', '2023-10-28', '2023-10-27');
INSERT INTO requestDetails (requestDetailsId, requestId, equipmentId, statusId, professorNIP, dispatchTime, returnTime, requestedDate, currentDate) VALUES (5, 5, 'NETW801', 2, 1, '11:10', '12:00', '2023-11-01', '2023-10-31');
INSERT INTO requestDetails (requestDetailsId, requestId, equipmentId, statusId, professorNIP, dispatchTime, returnTime, requestedDate, currentDate) VALUES (6, 6, 'OSC102', 1, 2, '08:40', '09:30', '2023-11-05', '2023-11-04');
INSERT INTO requestDetails (requestDetailsId, requestId, equipmentId, statusId, professorNIP, dispatchTime, returnTime, requestedDate, currentDate) VALUES (7, 6, 'OSC103', 1, 2, '08:40', '09:30', '2023-11-05', '2023-11-04');
INSERT INTO requestDetails (requestDetailsId, requestId, equipmentId, statusId, professorNIP, dispatchTime, returnTime, requestedDate, currentDate) VALUES (8, 7, 'NETW802', 2, 1, '12:00', '12:50', '2023-10-31', '2023-10-30');
INSERT INTO requestDetails (requestDetailsId, requestId, equipmentId, statusId, professorNIP, dispatchTime, returnTime, requestedDate, currentDate) VALUES (10, 8, 'FIBO702', 1, 0, '07:50', '08:40', '2023-11-12', '2023-11-11');
INSERT INTO requestDetails (requestDetailsId, requestId, equipmentId, statusId, professorNIP, dispatchTime, returnTime, requestedDate, currentDate) VALUES (12, 9, 'MOTR902', 2, 1, '08:40', '09:30', '2023-10-30', '2023-10-29');
INSERT INTO requestDetails (requestDetailsId, requestId, equipmentId, statusId, professorNIP, dispatchTime, returnTime, requestedDate, currentDate) VALUES (13, 9, 'MOTR901', 2, 1, '08:40', '09:30', '2023-10-30', '2023-10-29');
INSERT INTO requestDetails (requestDetailsId, requestId, equipmentId, statusId, professorNIP, dispatchTime, returnTime, requestedDate, currentDate) VALUES (14, 10, 'FREQ505', 1, 2, '10:20', '11:10', '2023-11-01', '2023-10-31');
INSERT INTO requestDetails (requestDetailsId, requestId, equipmentId, statusId, professorNIP, dispatchTime, returnTime, requestedDate, currentDate) VALUES (16, 11, 'SOLD1301', 2, 1, '11:10', '12:00', '2023-11-02', '2023-11-01');
INSERT INTO requestDetails (requestDetailsId, requestId, equipmentId, statusId, professorNIP, dispatchTime, returnTime, requestedDate, currentDate) VALUES (18, 12, 'NETW801', 1, 0, '12:50', '13:40', '2023-11-18', '2023-11-17');
INSERT INTO requestDetails (requestDetailsId, requestId, equipmentId, statusId, professorNIP, dispatchTime, returnTime, requestedDate, currentDate) VALUES (20, 13, 'FIBO701', 2, 1, '13:40', '14:30', '2023-11-02', '2023-11-01');
INSERT INTO requestDetails (requestDetailsId, requestId, equipmentId, statusId, professorNIP, dispatchTime, returnTime, requestedDate, currentDate) VALUES (21, 13, 'CABL1201', 2, 1, '13:40', '14:30', '2023-11-02', '2023-11-01');
INSERT INTO requestDetails (requestDetailsId, requestId, equipmentId, statusId, professorNIP, dispatchTime, returnTime, requestedDate, currentDate) VALUES (23, 14, 'MOTR901', 1, 0, '14:30', '07:00', '2023-11-15', '2023-11-14');
INSERT INTO requestDetails (requestDetailsId, requestId, equipmentId, statusId, professorNIP, dispatchTime, returnTime, requestedDate, currentDate) VALUES (24, 15, 'FREQ502', 2, 1, '07:00', '07:50', '2023-11-01', '2023-10-31');
INSERT INTO requestDetails (requestDetailsId, requestId, equipmentId, statusId, professorNIP, dispatchTime, returnTime, requestedDate, currentDate) VALUES (26, 16, 'NETW802', 1, 1, '08:40', '09:30', '2023-11-12', '2023-11-11');
INSERT INTO requestDetails (requestDetailsId, requestId, equipmentId, statusId, professorNIP, dispatchTime, returnTime, requestedDate, currentDate) VALUES (28, 17, 'MMT301', 1, 1, '09:30', '10:20', '2023-11-16', '2023-11-10');
INSERT INTO requestDetails (requestDetailsId, requestId, equipmentId, statusId, professorNIP, dispatchTime, returnTime, requestedDate, currentDate) VALUES (29, 17, 'FIBO705', 1, 1, '09:30', '10:20', '2023-11-17', '2023-11-10');
INSERT INTO requestDetails (requestDetailsId, requestId, equipmentId, statusId, professorNIP, dispatchTime, returnTime, requestedDate, currentDate) VALUES (30, 18, 'FNT205', 1, 1, '07:00', '08:40', '2023-11-10', '2023-11-09');
INSERT INTO requestDetails (requestDetailsId, requestId, equipmentId, statusId, professorNIP, dispatchTime, returnTime, requestedDate, currentDate) VALUES (31, 19, 'PROG604', 1, 1, '07:00', '08:40', '2023-11-10', '2023-11-09');
INSERT INTO requestDetails (requestDetailsId, requestId, equipmentId, statusId, professorNIP, dispatchTime, returnTime, requestedDate, currentDate) VALUES (32, 20, 'FREQ501', 1, 1, '07:00', '08:40', '2023-11-10', '2023-11-09');

-- Tabla: requests
CREATE TABLE IF NOT EXISTS requests (
    requestId   INTEGER   PRIMARY KEY AUTOINCREMENT,
    classroomId INTEGER   REFERENCES classrooms (classroomId),
    professorId CHAR (10) REFERENCES professors (professorId),
    studentId   CHAR (8)  REFERENCES students (studentId),
    storerId    CHAR (10) REFERENCES storers (storerId),
    subjectId   VARCHAR (13)   REFERENCES subjects (subjectId) 
);
INSERT INTO requests (requestId, classroomId, professorId, studentId, storerId, subjectId) VALUES (1, 3, 'owTkVgr0sk6DHaUug+7/SA==', '20300684', 'owTkVgr0sk6DHaUug+7/SA==', '18MPBDS0202');
INSERT INTO requests (requestId, classroomId, professorId, studentId, storerId, subjectId) VALUES (2, 7, 's+d1CVba7aAcZHO4z0LPaw==', '20300679', 'owTkVgr0sk6DHaUug+7/SA==', '18MPBDS0203');
INSERT INTO requests (requestId, classroomId, professorId, studentId, storerId, subjectId) VALUES (3, 12, 'AEd+FHe4V36Wpe7FXKWZtg==', '20300826', 'owTkVgr0sk6DHaUug+7/SA==', '18MPBDS0305');
INSERT INTO requests (requestId, classroomId, professorId, studentId, storerId, subjectId) VALUES (4, 15, 'i7T8ZRB4ZgI/GHgQY3B6IQ==', '20300698', 'owTkVgr0sk6DHaUug+7/SA==', '18MPBDS0307');
INSERT INTO requests (requestId, classroomId, professorId, studentId, storerId, subjectId) VALUES (5, 20, 's+d1CVba7aAcZHO4z0LPaw==', '22110211', 'owTkVgr0sk6DHaUug+7/SA==', '18MPBDS0308');
INSERT INTO requests (requestId, classroomId, professorId, studentId, storerId, subjectId) VALUES (6, 4, 'AEd+FHe4V36Wpe7FXKWZtg==', '23301024', 'owTkVgr0sk6DHaUug+7/SA==', '18MPBDS0409');
INSERT INTO requests (requestId, classroomId, professorId, studentId, storerId, subjectId) VALUES (7, 10, 'i7T8ZRB4ZgI/GHgQY3B6IQ==', '18110026', 'owTkVgr0sk6DHaUug+7/SA==', '18MPBDS0411');
INSERT INTO requests (requestId, classroomId, professorId, studentId, storerId, subjectId) VALUES (8, 18, 's+d1CVba7aAcZHO4z0LPaw==', '17300775', 'owTkVgr0sk6DHaUug+7/SA==', '18MPBDS0413');
INSERT INTO requests (requestId, classroomId, professorId, studentId, storerId, subjectId) VALUES (9, 8, 'AEd+FHe4V36Wpe7FXKWZtg==', '21310259', 'owTkVgr0sk6DHaUug+7/SA==', '18MPBDS0514');
INSERT INTO requests (requestId, classroomId, professorId, studentId, storerId, subjectId) VALUES (10, 14, 'i7T8ZRB4ZgI/GHgQY3B6IQ==', '23310378', 'owTkVgr0sk6DHaUug+7/SA==', '18MPBDS0515');
INSERT INTO requests (requestId, classroomId, professorId, studentId, storerId, subjectId) VALUES (11, 5, 's+d1CVba7aAcZHO4z0LPaw==', '19300062', 'owTkVgr0sk6DHaUug+7/SA==', '18MPBDS0517');
INSERT INTO requests (requestId, classroomId, professorId, studentId, storerId, subjectId) VALUES (12, 19, 'i7T8ZRB4ZgI/GHgQY3B6IQ==', '17300616', 'owTkVgr0sk6DHaUug+7/SA==', '18MPBDS0518');
INSERT INTO requests (requestId, classroomId, professorId, studentId, storerId, subjectId) VALUES (13, 2, 's+d1CVba7aAcZHO4z0LPaw==', '21310498', 'owTkVgr0sk6DHaUug+7/SA==', '18MPEDS0620');
INSERT INTO requests (requestId, classroomId, professorId, studentId, storerId, subjectId) VALUES (14, 9, 'AEd+FHe4V36Wpe7FXKWZtg==', '18110251', 'owTkVgr0sk6DHaUug+7/SA==', '18MPEDS0621');
INSERT INTO requests (requestId, classroomId, professorId, studentId, storerId, subjectId) VALUES (15, 17, 'i7T8ZRB4ZgI/GHgQY3B6IQ==', '18110226', 'owTkVgr0sk6DHaUug+7/SA==', '18MPEDS0622');
INSERT INTO requests (requestId, classroomId, professorId, studentId, storerId, subjectId) VALUES (16, 6, 's+d1CVba7aAcZHO4z0LPaw==', '19110444', 'owTkVgr0sk6DHaUug+7/SA==', '18MPEDS0623');
INSERT INTO requests (requestId, classroomId, professorId, studentId, storerId, subjectId) VALUES (17, 13, 'AEd+FHe4V36Wpe7FXKWZtg==', '18310372', 'owTkVgr0sk6DHaUug+7/SA==', '18MPEDS0624');
INSERT INTO requests (requestId, classroomId, professorId, studentId, storerId, subjectId) VALUES (18, 1, 'i7T8ZRB4ZgI/GHgQY3B6IQ==', '21100295', 'owTkVgr0sk6DHaUug+7/SA==', '18MPEDS0729');
INSERT INTO requests (requestId, classroomId, professorId, studentId, storerId, subjectId) VALUES (19, 11, 's+d1CVba7aAcZHO4z0LPaw==', '21110259', 'owTkVgr0sk6DHaUug+7/SA==', '18MPEDS0730');
INSERT INTO requests (requestId, classroomId, professorId, studentId, storerId, subjectId) VALUES (20, 16, 'AEd+FHe4V36Wpe7FXKWZtg==', '21300182', 'owTkVgr0sk6DHaUug+7/SA==', '18MPEDS0836');

-- Tabla: schedules
CREATE TABLE IF NOT EXISTS schedules (
    scheduleId INTEGER     PRIMARY KEY AUTOINCREMENT,
    initTime   TIME,
    weekDay    VARCHAR (9) 
);
INSERT INTO schedules (scheduleId, initTime, weekDay) VALUES (1, '7:00', 'Monday');
INSERT INTO schedules (scheduleId, initTime, weekDay) VALUES (2, '7:50', 'Monday');
INSERT INTO schedules (scheduleId, initTime, weekDay) VALUES (3, '8:40', 'Monday');
INSERT INTO schedules (scheduleId, initTime, weekDay) VALUES (4, '9:30', 'Monday');
INSERT INTO schedules (scheduleId, initTime, weekDay) VALUES (5, '10:20', 'Monday');
INSERT INTO schedules (scheduleId, initTime, weekDay) VALUES (6, '11:10', 'Monday');
INSERT INTO schedules (scheduleId, initTime, weekDay) VALUES (7, '12:00', 'Monday');
INSERT INTO schedules (scheduleId, initTime, weekDay) VALUES (8, '12:50', 'Monday');
INSERT INTO schedules (scheduleId, initTime, weekDay) VALUES (9, '13:40', 'Monday');
INSERT INTO schedules (scheduleId, initTime, weekDay) VALUES (10, '14:30', 'Monday');
INSERT INTO schedules (scheduleId, initTime, weekDay) VALUES (11, '7:00', 'Tuesday');
INSERT INTO schedules (scheduleId, initTime, weekDay) VALUES (12, '7:50', 'Tuesday');
INSERT INTO schedules (scheduleId, initTime, weekDay) VALUES (13, '8:40', 'Tuesday');
INSERT INTO schedules (scheduleId, initTime, weekDay) VALUES (14, '9:30', 'Tuesday');
INSERT INTO schedules (scheduleId, initTime, weekDay) VALUES (15, '10:20', 'Tuesday');
INSERT INTO schedules (scheduleId, initTime, weekDay) VALUES (16, '11:10', 'Tuesday');
INSERT INTO schedules (scheduleId, initTime, weekDay) VALUES (17, '12:00', 'Tuesday');
INSERT INTO schedules (scheduleId, initTime, weekDay) VALUES (18, '12:50', 'Tuesday');
INSERT INTO schedules (scheduleId, initTime, weekDay) VALUES (19, '13:40', 'Tuesday');
INSERT INTO schedules (scheduleId, initTime, weekDay) VALUES (20, '14:30', 'Tuesday');
INSERT INTO schedules (scheduleId, initTime, weekDay) VALUES (21, '7:00', 'Wednesday');
INSERT INTO schedules (scheduleId, initTime, weekDay) VALUES (22, '7:50', 'Wednesday');
INSERT INTO schedules (scheduleId, initTime, weekDay) VALUES (23, '8:40', 'Wednesday');
INSERT INTO schedules (scheduleId, initTime, weekDay) VALUES (24, '9:30', 'Wednesday');
INSERT INTO schedules (scheduleId, initTime, weekDay) VALUES (25, '10:20', 'Wednesday');
INSERT INTO schedules (scheduleId, initTime, weekDay) VALUES (26, '11:10', 'Wednesday');
INSERT INTO schedules (scheduleId, initTime, weekDay) VALUES (27, '12:00', 'Wednesday');
INSERT INTO schedules (scheduleId, initTime, weekDay) VALUES (28, '12:50', 'Wednesday');
INSERT INTO schedules (scheduleId, initTime, weekDay) VALUES (29, '13:40', 'Wednesday');
INSERT INTO schedules (scheduleId, initTime, weekDay) VALUES (30, '14:30', 'Wednesday');
INSERT INTO schedules (scheduleId, initTime, weekDay) VALUES (31, '7:00', 'Thursday');
INSERT INTO schedules (scheduleId, initTime, weekDay) VALUES (32, '7:50', 'Thursday');
INSERT INTO schedules (scheduleId, initTime, weekDay) VALUES (33, '8:40', 'Thursday');
INSERT INTO schedules (scheduleId, initTime, weekDay) VALUES (34, '9:30', 'Thursday');
INSERT INTO schedules (scheduleId, initTime, weekDay) VALUES (35, '10:20', 'Thursday');
INSERT INTO schedules (scheduleId, initTime, weekDay) VALUES (36, '11:10', 'Thursday');
INSERT INTO schedules (scheduleId, initTime, weekDay) VALUES (37, '12:00', 'Thursday');
INSERT INTO schedules (scheduleId, initTime, weekDay) VALUES (38, '12:50', 'Thursday');
INSERT INTO schedules (scheduleId, initTime, weekDay) VALUES (39, '13:40', 'Thursday');
INSERT INTO schedules (scheduleId, initTime, weekDay) VALUES (40, '14:30', 'Thursday');
INSERT INTO schedules (scheduleId, initTime, weekDay) VALUES (41, '7:00', 'Friday');
INSERT INTO schedules (scheduleId, initTime, weekDay) VALUES (42, '7:50', 'Friday');
INSERT INTO schedules (scheduleId, initTime, weekDay) VALUES (43, '8:40', 'Friday');
INSERT INTO schedules (scheduleId, initTime, weekDay) VALUES (44, '9:30', 'Friday');
INSERT INTO schedules (scheduleId, initTime, weekDay) VALUES (45, '10:20', 'Friday');
INSERT INTO schedules (scheduleId, initTime, weekDay) VALUES (46, '11:10', 'Friday');
INSERT INTO schedules (scheduleId, initTime, weekDay) VALUES (47, '12:00', 'Friday');
INSERT INTO schedules (scheduleId, initTime, weekDay) VALUES (48, '12:50', 'Friday');
INSERT INTO schedules (scheduleId, initTime, weekDay) VALUES (49, '13:40', 'Friday');
INSERT INTO schedules (scheduleId, initTime, weekDay) VALUES (50, '14:30', 'Friday');

-- Tabla: status
CREATE TABLE IF NOT EXISTS status (
    statusId INTEGER      PRIMARY KEY AUTOINCREMENT,
    value    VARCHAR (15) 
);
INSERT INTO status (statusId, value) VALUES (1, 'Available');
INSERT INTO status (statusId, value) VALUES (2, 'In use');
INSERT INTO status (statusId, value) VALUES (3, 'Lost');
INSERT INTO status (statusId, value) VALUES (4, 'Damaged');
INSERT INTO status (statusId, value) VALUES (5, 'Under Maintenance');
INSERT INTO status (statusId, value) VALUES (6, 'Delivered');

-- Tabla: storers
CREATE TABLE IF NOT EXISTS storers (
    storerId  CHAR (10)    PRIMARY KEY,
    name      VARCHAR (30),
    lastNameP VARCHAR (30),
    lastNameM VARCHAR (30),
    password  VARCHAR (50) 
);
INSERT INTO storers (storerId, name, lastNameP, lastNameM, password) VALUES ('kT556oT3ig6WU155xbGHTw==', 'Annel', 'Marin', 'Gutierrez', 'F8t128gjJIaegDAGPG//LA==');

-- Tabla: students
CREATE TABLE IF NOT EXISTS students (
    studentId CHAR (8)     PRIMARY KEY,
    name      VARCHAR (30),
    lastNameP VARCHAR (30),
    lastNameM VARCHAR (30),
    password  VARCHAR (50),
    groupId   INTEGER,
    FOREIGN KEY (
        groupId
    )
    REFERENCES groups (groupId) 
);
INSERT INTO students (studentId, name, lastNameP, lastNameM, password, groupId) VALUES ('20300684', 'Samantha', 'Briseno', 'Valadez', 'F8t128gjJIaegDAGPG//LA==', 1);
INSERT INTO students (studentId, name, lastNameP, lastNameM, password, groupId) VALUES ('20300679', 'Valeria', 'Gallegos', 'Goncalves', 'F8t128gjJIaegDAGPG//LA==', 1);
INSERT INTO students (studentId, name, lastNameP, lastNameM, password, groupId) VALUES ('20300826', 'Angelica', 'Prian', 'Sanchez', 'F8t128gjJIaegDAGPG//LA==', 1);
INSERT INTO students (studentId, name, lastNameP, lastNameM, password, groupId) VALUES ('20300698', 'Arturo', 'Garza', 'Sanchez', 'F8t128gjJIaegDAGPG//LA==', 1);
INSERT INTO students (studentId, name, lastNameP, lastNameM, password, groupId) VALUES ('22110211', 'Adonai', 'Mendoza', 'Duarte', 'F8t128gjJIaegDAGPG//LA==', 6);
INSERT INTO students (studentId, name, lastNameP, lastNameM, password, groupId) VALUES ('23301024', 'Sofia', 'Vicente', 'Gonzales', 'F8t128gjJIaegDAGPG//LA==', 9);
INSERT INTO students (studentId, name, lastNameP, lastNameM, password, groupId) VALUES ('18110026', 'Abigail', 'Ramirez', 'Corona', 'F8t128gjJIaegDAGPG//LA==', 4);
INSERT INTO students (studentId, name, lastNameP, lastNameM, password, groupId) VALUES ('17300775', 'Ana', 'Garcia', 'Fonseca', 'F8t128gjJIaegDAGPG//LA==', 9);
INSERT INTO students (studentId, name, lastNameP, lastNameM, password, groupId) VALUES ('21310259', 'Carlos', 'Cardenas', 'Carrillo', 'F8t128gjJIaegDAGPG//LA==', 6);
INSERT INTO students (studentId, name, lastNameP, lastNameM, password, groupId) VALUES ('23310378', 'Alex', 'Huerta', 'Jimenez', 'F8t128gjJIaegDAGPG//LA==', 2);
INSERT INTO students (studentId, name, lastNameP, lastNameM, password, groupId) VALUES ('19300062', 'David', 'Sandoval', 'Alejo', 'F8t128gjJIaegDAGPG//LA==', 3);
INSERT INTO students (studentId, name, lastNameP, lastNameM, password, groupId) VALUES ('17300616', 'Alexis', 'Alef', 'Moreno', 'F8t128gjJIaegDAGPG//LA==', 3);
INSERT INTO students (studentId, name, lastNameP, lastNameM, password, groupId) VALUES ('21310498', 'Hannia', 'Garcia', 'Ibanes', 'F8t128gjJIaegDAGPG//LA==', 8);
INSERT INTO students (studentId, name, lastNameP, lastNameM, password, groupId) VALUES ('18110251', 'Jacob', 'Medina', 'Larios', 'F8t128gjJIaegDAGPG//LA==', 7);
INSERT INTO students (studentId, name, lastNameP, lastNameM, password, groupId) VALUES ('18110226', 'Jonathan', 'Cornejo', 'Vargas', 'F8t128gjJIaegDAGPG//LA==', 8);
INSERT INTO students (studentId, name, lastNameP, lastNameM, password, groupId) VALUES ('19110444', 'Hugo', 'Gonzales', 'Ornelas', 'F8t128gjJIaegDAGPG//LA==', 6);
INSERT INTO students (studentId, name, lastNameP, lastNameM, password, groupId) VALUES ('18310372', 'Alan', 'Garcia', 'Godinez', 'F8t128gjJIaegDAGPG//LA==', 7);
INSERT INTO students (studentId, name, lastNameP, lastNameM, password, groupId) VALUES ('21100295', 'Rodrigo', 'Ruiz', 'Gutierrez', 'F8t128gjJIaegDAGPG//LA==', 4);
INSERT INTO students (studentId, name, lastNameP, lastNameM, password, groupId) VALUES ('21110259', 'Janet', 'Sandoval', 'Hernandez', 'F8t128gjJIaegDAGPG//LA==', 10);
INSERT INTO students (studentId, name, lastNameP, lastNameM, password, groupId) VALUES ('21300182', 'Aliyah', 'Garcia', 'Flores', 'F8t128gjJIaegDAGPG//LA==', 11);
INSERT INTO students (studentId, name, lastNameP, lastNameM, password, groupId) VALUES ('22300502', 'Aron', 'Galindo', 'Mendez', 'F8t128gjJIaegDAGPG//LA==', 13);
INSERT INTO students (studentId, name, lastNameP, lastNameM, password, groupId) VALUES ('22100135', 'Aron', 'Ramirez', 'Martinez', 'F8t128gjJIaegDAGPG//LA==', 8);
INSERT INTO students (studentId, name, lastNameP, lastNameM, password, groupId) VALUES ('22300624', 'Abdiel', 'Nunes', 'Gutierrez', 'F8t128gjJIaegDAGPG//LA==', 15);
INSERT INTO students (studentId, name, lastNameP, lastNameM, password, groupId) VALUES ('22110052', 'Raul', 'Gutierrez', 'Leal', 'F8t128gjJIaegDAGPG//LA==', 10);

-- Tabla: subjects
CREATE TABLE IF NOT EXISTS subjects (
    subjectId VARCHAR (13) PRIMARY KEY,
    name      VARCHAR (55),
    academyId INTEGER,
    FOREIGN KEY (
        academyId
    )
    REFERENCES academies (academyId) 
);
INSERT INTO subjects (subjectId, name, academyId) VALUES ('18MPBDS0101', 'Fundamentos de Programacion ', 1);
INSERT INTO subjects (subjectId, name, academyId) VALUES ('18MPBDS0202', 'Fundamentos de Electronica I ', 3);
INSERT INTO subjects (subjectId, name, academyId) VALUES ('18MPBDS0203', 'Mantenimiento de Tecnologias de Informacion I ', 5);
INSERT INTO subjects (subjectId, name, academyId) VALUES ('18MPBDS0204', 'Programacion I ', 1);
INSERT INTO subjects (subjectId, name, academyId) VALUES ('18MPBDS0305', 'Mantenimiento de Tecnologias de Informacion II ', 5);
INSERT INTO subjects (subjectId, name, academyId) VALUES ('18MPBDS0306', 'Programacion II ', 1);
INSERT INTO subjects (subjectId, name, academyId) VALUES ('18MPBDS0307', 'Sistemas Digitales I ', 4);
INSERT INTO subjects (subjectId, name, academyId) VALUES ('18MPBDS0308', 'Fundamentos de Electronica II ', 3);
INSERT INTO subjects (subjectId, name, academyId) VALUES ('18MPBDS0409', 'Sistemas Digitales II ', 4);
INSERT INTO subjects (subjectId, name, academyId) VALUES ('18MPBDS0410', 'Analisis y Diseno de Sistemas ', 2);
INSERT INTO subjects (subjectId, name, academyId) VALUES ('18MPBDS0411', 'Infraestructura de Redes Locales ', 5);
INSERT INTO subjects (subjectId, name, academyId) VALUES ('18MPBDS0412', 'Programacion Orientada a Objetos ', 1);
INSERT INTO subjects (subjectId, name, academyId) VALUES ('18MPBDS0413', 'Temas de Electronica I ', 3);
INSERT INTO subjects (subjectId, name, academyId) VALUES ('18MPBDS0514', 'Arquitectura y Organizacion de Computadoras ', 4);
INSERT INTO subjects (subjectId, name, academyId) VALUES ('18MPBDS0515', 'Bases de Datos I ', 2);
INSERT INTO subjects (subjectId, name, academyId) VALUES ('18MPBDS0517', 'Temas de Electronica II ', 3);
INSERT INTO subjects (subjectId, name, academyId) VALUES ('18MPBDS0518', 'Estructuras de Datos  ', 1);
INSERT INTO subjects (subjectId, name, academyId) VALUES ('18MPEDS0516', 'Enrutamiento de Redes', 5);
INSERT INTO subjects (subjectId, name, academyId) VALUES ('18MPEDS0619', 'Bases de Datos II ', 2);
INSERT INTO subjects (subjectId, name, academyId) VALUES ('18MPEDS0620', 'Interfaces ', 3);
INSERT INTO subjects (subjectId, name, academyId) VALUES ('18MPEDS0621', 'Programacion Movil I ', 1);
INSERT INTO subjects (subjectId, name, academyId) VALUES ('18MPEDS0622', 'Sistemas Operativos ', 5);
INSERT INTO subjects (subjectId, name, academyId) VALUES ('18MPEDS0623', 'Sistemas Embebidos I ', 4);
INSERT INTO subjects (subjectId, name, academyId) VALUES ('18MPEDS0729', 'Sistemas Embebidos II ', 4);
INSERT INTO subjects (subjectId, name, academyId) VALUES ('18MPEDS0730', 'Sistemas de Medicion y Control ', 3);
INSERT INTO subjects (subjectId, name, academyId) VALUES ('18MPEDS0727', 'Proyecto Integrador de Desarrollo de Software I ', 2);
INSERT INTO subjects (subjectId, name, academyId) VALUES ('18MPEDS0724', 'Programacion Movil II ', 1);
INSERT INTO subjects (subjectId, name, academyId) VALUES ('18MPEDS0726', 'Programacion Web I ', 2);
INSERT INTO subjects (subjectId, name, academyId) VALUES ('18MPEDS0728', 'Servicios de Red y Computo Nube ', 5);
INSERT INTO subjects (subjectId, name, academyId) VALUES ('18MPEDS0725', 'Programacion Avanzada I ', 1);
INSERT INTO subjects (subjectId, name, academyId) VALUES ('18MPEDS0835', 'Seguridad en Infraestructura de Tecnologias de Informacion ', 5);
INSERT INTO subjects (subjectId, name, academyId) VALUES ('18MPEDS0836', 'Sistemas Inteligentes ', 4);
INSERT INTO subjects (subjectId, name, academyId) VALUES ('18MPEDSS063', 'Proyecto Integrador de Desarrollo de Software II ', 2);
INSERT INTO subjects (subjectId, name, academyId) VALUES ('18MPEDS833', 'Seguridad en Software ', 2);
INSERT INTO subjects (subjectId, name, academyId) VALUES ('18MPEDS0832', 'Programacion Web II ', 2);
INSERT INTO subjects (subjectId, name, academyId) VALUES ('18MPEDS0834', 'Sistemas Multimedia ', 2);
INSERT INTO subjects (subjectId, name, academyId) VALUES ('18MPEDS0831', 'Programacion Avanzada II ', 1);

-- Tabla: teaches
CREATE TABLE IF NOT EXISTS teaches (
    teachId     INTEGER      PRIMARY KEY AUTOINCREMENT,
    classroomId INTEGER          REFERENCES classrooms (classroomId),
    groupId     INTEGER     REFERENCES groups (groupId),
    professorId CHAR (10)    REFERENCES professors (professorId),
    subjectId   VARCHAR (13) REFERENCES subjects (subjectId),
    scheduleId  INTEGER          REFERENCES schedules (scheduleId) 
);
INSERT INTO teaches (teachId, classroomId, groupId, professorId, subjectId, scheduleId) VALUES (1, 18, 4, 'i7T8ZRB4ZgI/GHgQY3B6IQ==', '18MPEDS0836', 8);
INSERT INTO teaches (teachId, classroomId, groupId, professorId, subjectId, scheduleId) VALUES (2, 11, 7, 's+d1CVba7aAcZHO4z0LPaw==', '18MPBDS0517', 6);
INSERT INTO teaches (teachId, classroomId, groupId, professorId, subjectId, scheduleId) VALUES (3, 9, 22, 'i7T8ZRB4ZgI/GHgQY3B6IQ==', '18MPEDS0836', 15);
INSERT INTO teaches (teachId, classroomId, groupId, professorId, subjectId, scheduleId) VALUES (4, 22, 1, 'AEd+FHe4V36Wpe7FXKWZtg==', '18MPEDS0730', 36);
INSERT INTO teaches (teachId, classroomId, groupId, professorId, subjectId, scheduleId) VALUES (5, 19, 1, 'AEd+FHe4V36Wpe7FXKWZtg==', '18MPEDS0729', 46);
INSERT INTO teaches (teachId, classroomId, groupId, professorId, subjectId, scheduleId) VALUES (6, 3, 5, 's+d1CVba7aAcZHO4z0LPaw==', '18MPEDS0729', 22);
INSERT INTO teaches (teachId, classroomId, groupId, professorId, subjectId, scheduleId) VALUES (7, 9, 12, 'owTkVgr0sk6DHaUug+7/SA==', '18MPBDS0202', 19);
INSERT INTO teaches (teachId, classroomId, groupId, professorId, subjectId, scheduleId) VALUES (8, 20, 19, 's+d1CVba7aAcZHO4z0LPaw==', '18MPBDS0413', 38);
INSERT INTO teaches (teachId, classroomId, groupId, professorId, subjectId, scheduleId) VALUES (9, 10, 16, 'owTkVgr0sk6DHaUug+7/SA==', '18MPBDS0307', 46);
INSERT INTO teaches (teachId, classroomId, groupId, professorId, subjectId, scheduleId) VALUES (10, 12, 8, 'AEd+FHe4V36Wpe7FXKWZtg==', '18MPBDS0514', 4);

COMMIT TRANSACTION;
PRAGMA foreign_keys = on;
