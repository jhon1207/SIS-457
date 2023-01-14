CREATE TABLE Serie (
  id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
  titulo VARCHAR(250) NOT NULL,
  sinopsis VARCHAR(5000) NOT NULL,
  director VARCHAR(100) NOT NULL,
  duracion INT,
);

CREATE TABLE Usuario (
  id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
  usuario VARCHAR(12) NOT NULL,
  clave VARCHAR(250) NOT NULL,
  rol VARCHAR(20) NOT NULL,
);

ALTER TABLE Serie ADD usuarioRegistro VARCHAR(12) NULL DEFAULT SUSER_SNAME();
ALTER TABLE Serie ADD fechaEstreno DATE NULL DEFAULT GETDATE();
ALTER TABLE Serie ADD registroActivo BIT NULL DEFAULT 1;

ALTER TABLE Usuario ADD registroActivo BIT NULL DEFAULT 1;

CREATE PROC paSerieListar @parametro VARCHAR(50)
AS
  SELECT id, titulo, sinopsis, director, duracion, usuarioRegistro
  FROM Serie
  WHERE registroActivo=1 AND titulo+director LIKE '%'+@parametro+'%'

EXEC paUsuarioListar ''

CREATE PROC paUsuarioListar @parametro VARCHAR(50)
AS
  SELECT id, usuario, clave, rol, registroActivo
  FROM Usuario
  WHERE registroActivo=1 AND usuario+rol LIKE '%'+@parametro+'%'
