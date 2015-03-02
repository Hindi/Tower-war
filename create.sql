DROP TABLE tw_users;

CREATE TABLE tw_users
(
	id INT PRIMARY KEY NOT NULL,
    login VARCHAR(100),
    password VARCHAR(100)
);