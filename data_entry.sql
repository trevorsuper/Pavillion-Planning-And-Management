DROP TABLE IF EXISTS park;

CREATE TABLE "park"(
    "park_id" INT NOT NULL,
    "park_name" NVARCHAR(255) NOT NULL,
    "geolocation" NVARCHAR(255) NULL,
    "street_address" NVARCHAR(255) NOT NULL,
    "city" NVARCHAR(20) NOT NULL,
    "state" NVARCHAR(20) NOT NULL,
    "zipcode" NVARCHAR(10) NOT NULL,
    "number_of_pavillions" INT NOT NULL,
    "acres" INT NOT NULL,
    "play_structures" INT NOT NULL,
    "trails" INT NOT NULL,
    "baseball_fields" INT NOT NULL,
    "disc_golf_courses" INT NOT NULL,
    "volleyball_courts" INT NOT NULL,
    "fishing_spots" INT NOT NULL,
    "soccer_fields" INT NOT NULL
);
ALTER TABLE
    "park" ADD CONSTRAINT "park_park_id_primary" PRIMARY KEY("park_id");

INSERT INTO park VALUES(1, 'Boulan Park', NULL, '3671 Crooks Rd', 'Troy', 'MI', '48084', 9, 63, 2, 1, 3, 0, 2, 0, 5);
INSERT INTO park VALUES(2, 'Brinston Park', NULL, '2262 Brinston Dr', 'Troy', 'MI', '48083', 9, 18, 1, 0, 2, 0, 0, 0, 2);
INSERT INTO park VALUES(3, 'Firefighters Park', NULL, '1800 W Square Lake Rd', 'Troy', 'MI', '48098', 9, 96, 1, 0, 1, 18, 1, 1, 9);
INSERT INTO park VALUES(4, 'Donald J. Flynn Park', NULL, '1710 E South Blvd.', 'Troy', 'MI', '48085', 9, 25, 0, 4, 0, 0, 0, 0, 0);
INSERT INTO park VALUES(5, 'Jaycee Park', NULL, '1755 E Long Lake Rd', 'Troy', 'MI', '48085', 9, 45, 2, 0, 2, 0, 1, 0, 6);
INSERT INTO park VALUES(6, 'Raintree Park', NULL, '3755 John R Rd', 'Troy', 'MI', '48083', 1, 41, 2, 1, 1, 9, 1, 0, 0);
INSERT INTO park VALUES(7, 'Jeanne M Stine Community Park', NULL, '241 Town Center Dr', 'Troy', 'MI', '48084', 1, 6, 0, 1, 0, 0, 0, 0, 0);
INSERT INTO park VALUES(8, 'Milverton Park', NULL, '2384 E Maple Rd', 'Troy', 'MI', '48083', 1, 16, 0, 1, 0, 0, 0, 0, 0);
INSERT INTO park VALUES(9, 'Phillip J. Huber Park', NULL, '3500 Civic Center Dr', 'Troy', 'MI', '48084', 1, 20, 1, 0, 0, 0, 0, 0, 0);
INSERT INTO park VALUES(10, 'Sylvan Glen Lake Park', NULL, '5725 Rochester Rd', 'Troy', 'MI', '48085', 0, 16, 0, 0, 0, 0, 0, 48, 0);
INSERT INTO park VALUES(11, 'P. Terry & Barbara Knight Park', NULL, 'Daisy Knight Dog Park', 'Troy', 'MI', '48083', 0, 19, 0, 0, 0, 0, 0, 0, 0);

SELECT * FROM park;