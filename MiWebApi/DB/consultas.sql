INSERT INTO usuario (nombre_de_usuario)
VALUES ("Lola");

INSERT INTO Tablero (id_usuario_propietario, nombre, descripcion) 
VALUES (4, 'tablero3', 'realizo tarea');

INSERT INTO Tarea (id_tablero, nombre, estado, descripcion, color, id_usuario_asignado) 
VALUES (3, 'tarea4', 2, 'tarea4', 'violeta', 2);

UPDATE tarea set nombre= "tarea modificada" where id=1;

UPDATE usuario set nombre_de_usuario= "Pablo" where id=3;

UPDATE tablero set nombre= "tablero2" where id=2;

DELETE from tarea where id=2;

select count(*) from tarea where estado=1;

select * from tarea where id_usuario_asignado=2;

select * from tablero;
