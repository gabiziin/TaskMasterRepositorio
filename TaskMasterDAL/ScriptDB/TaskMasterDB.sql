CREATE DATABASE TaskMasterDB
USE TaskMasterDB

CREATE TABLE TipoUsuario(
IdTipoUsuario INT PRIMARY KEY IDENTITY (1,1),
DescricaoTipoUsuario VARCHAR(150) NOT NULL
);

INSERT INTO TipoUsuario (DescricaoTipoUsuario)
VALUES ('Administrador'),('Comum');

SELECT * FROM TipoUsuario;
UPDATE TipoUsuario SET DescricaoTipoUsuario = 'Administrador'
WHERE IdTipoUsuario = 1;

CREATE TABLE GeneroUsuario(
IdGeneroUsuario INT PRIMARY KEY IDENTITY (1,1),
DescricaoGeneroUsuario VARCHAR(150) NOT NULL
);

INSERT INTO GeneroUsuario (DescricaoGeneroUsuario)
VALUES ('Masculino'),('Feminino'),('Outro');
-- Será adicionado muito mais

SELECT * FROM GeneroUsuario;

/* Tabela de Usuários */
CREATE TABLE Usuario (
    IdUsuario INT PRIMARY KEY IDENTITY(1,1),
    NomeUsuario VARCHAR(150) NOT NULL,
    UsuarioGenero INT NOT NULL,
    EmailUsuario VARCHAR(150) UNIQUE NOT NULL,
    SenhaUsuario CHAR(6) NOT NULL,
    UsuarioTp INT NOT NULL,

    FOREIGN KEY (UsuarioGenero) REFERENCES GeneroUsuario (IdGeneroUsuario),
    FOREIGN KEY (UsuarioTp) REFERENCES TipoUsuario (IdTipoUsuario)
);

/* Inserção de Usuários */
INSERT INTO Usuario (NomeUsuario, UsuarioGenero, EmailUsuario, SenhaUsuario, UsuarioTp) 
VALUES 
('admin', 3, 'admin@email.com', 'admin', 1),
('comum', 3, 'comum@email.com', 'comum', 2),
('Jahji', 1, 'jahji@email.com', 'jah123', 2),
('Fernanda', 2, 'doidinha@email.com', 'fer123', 2),
('Wil', 1, 'COMFORCA@email.com', 'WIL123', 1);

/* Consulta de Usuários */
SELECT * FROM Usuario
INNER JOIN TipoUsuario ON UsuarioTp = IdTipoUsuario;

------------------------------------------------------------

/* Tabela de Tarefas */
DELETE TABLE Tarefas;
CREATE TABLE Tarefas (
    IdTarefa INT PRIMARY KEY IDENTITY(1,1),
    TituloTarefa VARCHAR(200) NOT NULL,
    DescricaoTarefa VARCHAR(200),
    DataCriacao DATETIME DEFAULT GETDATE(),
    DataConclusao DATETIME,
    StatusTarefa VARCHAR(50) DEFAULT 'Pendente',
    UsuarioId INT NOT NULL,

    FOREIGN KEY (UsuarioId) REFERENCES Usuario(IdUsuario) ON DELETE CASCADE
);

/* Inserção de Tarefas */
INSERT INTO Tarefas (TituloTarefa, DescricaoTarefa, UsuarioId)
VALUES 
('Estudar SQL', 'Focar em JOINs e subconsultas.', 1),
('Revisar código', 'Corrigir bugs do projeto C#.', 2),
('Treinar lógica', 'Resolver desafios de algoritmos.', 3),
('Organizar tarefas', 'Limpar tarefas antigas.', 1);

/* Consulta de Tarefas por Usuário */
SELECT 
    U.NomeUsuario,
    T.TituloTarefa,
    T.DescricaoTarefa,
    T.StatusTarefa,
    T.DataCriacao,
    T.DataConclusao
FROM Tarefas T
INNER JOIN Usuario U ON T.UsuarioId = U.IdUsuario;

------------------------------------------------------------

/* CRUD Tarefas */

/* CREATE */
INSERT INTO Tarefas (TituloTarefa, DescricaoTarefa, UsuarioId)
VALUES ('Nova Tarefa', 'Descrição da nova tarefa', 4);

/* READ */
SELECT * FROM Tarefas WHERE UsuarioId = 4;

/* UPDATE */
UPDATE Tarefas
SET TituloTarefa = 'Tarefa Atualizada', StatusTarefa = 'Concluído', DataConclusao = GETDATE()
WHERE IdTarefa = 1;

/* DELETE */
DELETE FROM Tarefas WHERE IdTarefa = 2;