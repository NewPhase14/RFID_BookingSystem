-- This schema is generated based on the current DBContext. Please check the class Seeder to see.
CREATE TABLE booking_status (
    id text NOT NULL,
    name character varying(50) NOT NULL,
    CONSTRAINT booking_status_pkey PRIMARY KEY (id)
);


CREATE TABLE roles (
    id text NOT NULL,
    name character varying(100) NOT NULL,
    CONSTRAINT roles_pkey PRIMARY KEY (id)
);


CREATE TABLE services (
    id text NOT NULL,
    name character varying(100) NOT NULL,
    CONSTRAINT services_pkey PRIMARY KEY (id)
);


CREATE TABLE users (
    id text NOT NULL,
    first_name character varying(100) NOT NULL,
    last_name character varying(100) NOT NULL,
    rfid character varying(50),
    hashed_password text NOT NULL,
    email character varying(255) NOT NULL,
    role_id text NOT NULL,
    created_at timestamp without time zone DEFAULT (CURRENT_TIMESTAMP),
    updated_at timestamp without time zone DEFAULT (CURRENT_TIMESTAMP),
    confirmed_email boolean DEFAULT FALSE,
    CONSTRAINT users_pkey PRIMARY KEY (id),
    CONSTRAINT users_role_id_fkey FOREIGN KEY (role_id) REFERENCES roles (id) ON DELETE RESTRICT
);


CREATE TABLE bookings (
    id text NOT NULL,
    user_id text NOT NULL,
    service_id text NOT NULL,
    status_id text NOT NULL,
    created_at timestamp without time zone DEFAULT (CURRENT_TIMESTAMP),
    start_time timestamp without time zone NOT NULL,
    end_time timestamp without time zone NOT NULL,
    CONSTRAINT bookings_pkey PRIMARY KEY (id),
    CONSTRAINT bookings_service_id_fkey FOREIGN KEY (service_id) REFERENCES services (id) ON DELETE CASCADE,
    CONSTRAINT bookings_status_id_fkey FOREIGN KEY (status_id) REFERENCES booking_status (id),
    CONSTRAINT bookings_user_id_fkey FOREIGN KEY (user_id) REFERENCES users (id) ON DELETE CASCADE
);


CREATE UNIQUE INDEX booking_status_name_key ON booking_status (name);


CREATE INDEX idx_bookings_service_id ON bookings (service_id);


CREATE INDEX idx_bookings_status_id ON bookings (status_id);


CREATE INDEX idx_bookings_user_id ON bookings (user_id);


CREATE UNIQUE INDEX roles_name_key ON roles (name);


CREATE UNIQUE INDEX services_name_key ON services (name);


CREATE INDEX idx_users_role_id ON users (role_id);


CREATE UNIQUE INDEX users_email_key ON users (email);


