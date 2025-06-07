CREATE TABLE "user"(
    "user_id" BIGINT NOT NULL,
    "email" VARCHAR(255) NOT NULL,
    "phone_num" VARCHAR(255) NOT NULL,
    "password" VARCHAR(255) NOT NULL,
    "first_name" VARCHAR(255) NOT NULL,
    "last_name" VARCHAR(255) NOT NULL,
    "is_admin" BIT NOT NULL
);
ALTER TABLE
    "user" ADD CONSTRAINT "user_user_id_primary" PRIMARY KEY("user_id");
CREATE TABLE "park"(
    "park_id" TINYINT NOT NULL,
    "park_name" VARCHAR(50) NOT NULL,
    "geolocation" VARCHAR(255) NULL,
    "street_address" VARCHAR(50) NOT NULL,
    "city" VARCHAR(50) NOT NULL,
    "state" VARCHAR(20) NOT NULL,
    "zip" VARCHAR(10) NOT NULL,
    "phone_num" VARCHAR(50) NOT NULL,
    "num_of_pavillions" TINYINT NOT NULL,
    "acres" SMALLINT NOT NULL,
    "play_structs" TINYINT NOT NULL,
    "trails" TINYINT NULL,
    "baseball_fields" TINYINT NOT NULL,
    "disc_golf_courses" TINYINT NULL,
    "volley_ball_courts" TINYINT NULL,
    "fishing_spots" TINYINT NULL,
    "soccer_fields" TINYINT NOT NULL
);
ALTER TABLE
    "park" ADD CONSTRAINT "park_park_id_primary" PRIMARY KEY("park_id");
CREATE TABLE "registration_inquiry"(
    "registration_id" BIGINT NOT NULL,
    "user_id" BIGINT NOT NULL,
    "park_id" TINYINT NOT NULL,
    "event_id" BIGINT NULL,
    "pavillion" TINYINT NOT NULL,
    "date" DATE NOT NULL,
    "start_time" TIME NOT NULL,
    "end_time" TIME NOT NULL,
    "is_approved" BIT NOT NULL,
    "waitlist" SMALLINT NOT NULL
);
ALTER TABLE
    "registration_inquiry" ADD CONSTRAINT "registration_inquiry_registration_id_primary" PRIMARY KEY("registration_id");
CREATE TABLE "event"(
    "event_id" BIGINT NOT NULL,
    "event_name" VARCHAR(255) NOT NULL,
    "event_desc" TEXT NOT NULL,
    "event_start_date" DATE NOT NULL,
    "event_end_date" DATE NOT NULL,
    "event_start_time" TIME NOT NULL,
    "event_end_time" TIME NOT NULL
);
ALTER TABLE
    "event" ADD CONSTRAINT "event_event_id_primary" PRIMARY KEY("event_id");
ALTER TABLE
    "registration_inquiry" ADD CONSTRAINT "registration_inquiry_user_id_foreign" FOREIGN KEY("user_id") REFERENCES "user"("user_id");
ALTER TABLE
    "registration_inquiry" ADD CONSTRAINT "registration_inquiry_event_id_foreign" FOREIGN KEY("event_id") REFERENCES "event"("event_id");
ALTER TABLE
    "registration_inquiry" ADD CONSTRAINT "registration_inquiry_park_id_foreign" FOREIGN KEY("park_id") REFERENCES "park"("park_id");