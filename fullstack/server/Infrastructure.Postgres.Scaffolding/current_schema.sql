CREATE TABLE roles (
                       id TEXT PRIMARY KEY,
                       name VARCHAR(30) NOT NULL
);

CREATE TABLE users (
                       id TEXT PRIMARY KEY,
                       first_name VARCHAR(100) not null ,
                       last_name VARCHAR(100) not null ,
                       rfid VARCHAR(100),
                       hashed_password TEXT not null ,
                       salt TEXT not null ,
                       email VARCHAR(255) not null ,
                       role_id TEXT REFERENCES roles(id) not null,
                       confirmed_email BOOLEAN default false,
                       created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                       updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE services (
                          id TEXT PRIMARY KEY,
                          name VARCHAR(100) NOT NULL
);

CREATE TABLE booking_status (
                                id TEXT PRIMARY KEY,
                                name VARCHAR(100) NOT NULL
);


CREATE TABLE weekdays (
                          id INT PRIMARY KEY CHECK(id BETWEEN 1 AND 7),
                          name VARCHAR(30) NOT NULL
);

CREATE TABLE service_time_slots (
                                    id TEXT PRIMARY KEY,
                                    service_id TEXT NOT NULL REFERENCES services(id),
                                    day_of_week INT NOT NULL REFERENCES weekdays(id),
                                    start_time TIME NOT NULL,
                                    end_time TIME NOT NULL,
                                    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE bookings (
                          id TEXT PRIMARY KEY,
                          user_id TEXT NOT NULL REFERENCES users(id),
                          service_id TEXT NOT NULL REFERENCES services(id),
                          status_id TEXT REFERENCES booking_status(id),
                          slot_id TEXT NOT NULL REFERENCES service_time_slots(id),
                          booking_date DATE NOT NULL,
                          created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                          updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                          UNIQUE(slot_id, booking_date)
);

insert into weekdays (id, name) values
(1, 'Monday'),
(2, 'Tuesday'),
(3, 'Wednesday'),
(4, 'Thursday'),
(5, 'Friday'),
(6, 'Saturday'),
(7, 'Sunday');

insert into roles (id, name) values 
(1, 'User'),
(2, 'Admin');
