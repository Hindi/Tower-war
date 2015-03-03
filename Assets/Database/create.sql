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
	level INT(30) default '0'
);