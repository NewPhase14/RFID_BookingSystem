CREATE TABLE roles (
                       id TEXT PRIMARY KEY,
                       name VARCHAR(30) NOT NULL
);

CREATE TABLE users (
                       id TEXT PRIMARY KEY,
                       first_name VARCHAR(100) NOT NULL,
                       last_name VARCHAR(100) NOT NULL ,
                       rfid VARCHAR(100) NOT NULL,
                       hashed_password TEXT NOT NULL,
                       salt TEXT NOT NULL,
                       email VARCHAR(255) NOT NULL,
                       role_id TEXT REFERENCES roles(id) NOT NULL,
                       confirmed_email BOOLEAN DEFAULT FALSE,
                       created_at TIMESTAMP NOT NULL,
                       updated_at TIMESTAMP NOT NULL
);

CREATE TABLE services (
                          id TEXT PRIMARY KEY,
                          name VARCHAR(100) NOT NULL,
                          description TEXT NOT NULL,
                          image_url TEXT NOT NULL,
                          public_id TEXT NOT NULL,
                          created_at TIMESTAMP NOT NULL,
                          updated_at TIMESTAMP NOT NULL
);

CREATE TABLE service_availability (
                                      id TEXT PRIMARY KEY,
                                      service_id TEXT NOT NULL REFERENCES services(id),
                                      day_of_week INT NOT NULL,
                                      available_from TIME NOT NULL,
                                      available_to TIME NOT NULL,
                                      created_at TIMESTAMP NOT NULL,
                                      updated_at TIMESTAMP NOT NULL,
                                      UNIQUE(service_id, day_of_week)
);

CREATE TABLE bookings (
                          id TEXT PRIMARY KEY,
                          user_id TEXT NOT NULL REFERENCES users(id),
                          service_id TEXT NOT NULL REFERENCES services(id),
                          start_time TIMESTAMP NOT NULL,
                          end_time TIMESTAMP NOT NULL,
                          created_at TIMESTAMP NOT NULL,
                          updated_at TIMESTAMP NOT NULL,
                          CONSTRAINT time_valid CHECK ( start_time < end_time )

);


CREATE TABLE invite_token (
                                          id TEXT PRIMARY KEY,
                                          user_id TEXT NOT NULL REFERENCES users(id),
                                          created_at TIMESTAMP NOT NULL,
                                          expires_at TIMESTAMP NOT NULL
);


CREATE  TABLE password_reset_token (
                                       id TEXT PRIMARY KEY,
                                       user_id TEXT NOT NULL REFERENCES users(id),
                                       created_at TIMESTAMP NOT NULL,
                                       expires_at TIMESTAMP NOT NULL
);

CREATE TABLE activity_logs (
                              id TEXT PRIMARY KEY,
                              service_id TEXT NOT NULL REFERENCES services(id),
                              attempted_at TIMESTAMP NOT NULL,
                              user_id TEXT NOT NULL REFERENCES users(id),
                              status TEXT NOT NULL    
);

insert into roles (id, name) values
                                 (1, 'User'),
                                 (2, 'Admin');

