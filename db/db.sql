CREATE DATABASE Desastres;

use Desastres;

CREATE TABLE desastres (
	id_desastre VARCHAR(20) NOT NULL,
	nombre VARCHAR (100) NOT NULL,
	tipo VARCHAR (20) NOT NULL,
	fecha_inicio date,
	fecha_fin date,
	id_region VARCHAR(20),
CONSTRAINT pk_desastres PRIMARY KEY(id_desastre),
CONSTRAINT fk_desastre_region FOREIGN KEY(id_region) REFERENCES regiones(id)
)ENGINE=Innodb;

CREATE TABLE regiones(
	id_region VARCHAR(20) NOT NULL,
	nombre VARCHAR(100) NOT NULL,
	CONSTRAINT pk_regiones PRIMARY KEY(id_region)
)ENGINE=Innodb;