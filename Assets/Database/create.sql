DROP TABLE tw_users;

CREATE TABLE tw_users
(
	id INT(6) AUTO_INCREMENT PRIMARY KEY,
	login VARCHAR(100) NOT NULL,
	pass VARCHAR(32) NOT NULL
);

DROP TABLE tw_levels;
CREATE TABLE tw_levels
(
	id INT(6) PRIMARY KEY,
	level INT(3) default '0'
);

DROP TABLE tw_user_relationships;
CREATE TABLE tw_user_relationships
(
	id INT(6) NOT NULL,
	userId INT(6) NOT NULL
);

DROP TABLE tw_relationships;
CREATE TABLE tw_relationships
(
	id INT(6) AUTO_INCREMENT PRIMARY KEY,
	date DATE
);

DROP TABLE tw_relationships_request;
CREATE TABLE tw_relationships_request
(
	id INT(6) PRIMARY KEY,
	askerId INT(6),
	targetId INT(6)
);