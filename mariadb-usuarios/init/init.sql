
CREATE DATABASE IF NOT EXISTS personas;

-- Cambiar al esquema usuarios y crear tabla
USE personas;

CREATE TABLE IF NOT EXISTS usuarios (
    user_id INT PRIMARY KEY AUTO_INCREMENT,
    nombre VARCHAR(100),
    apellido VARCHAR(100),
    cuil VARCHAR(20),
    email VARCHAR(100),
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Insertar 20 usuarios
INSERT INTO usuarios (nombre, apellido, cuil, email) VALUES
('Juan', 'Pérez', '20-12345678-1', 'juan.perez@mail.com'),
('Ana', 'García', '27-23456789-2', 'ana.garcia@mail.com'),
('Luis', 'Martínez', '23-34567890-3', 'luis.martinez@mail.com'),
('Carla', 'Rodríguez', '24-45678901-4', 'carla.rodriguez@mail.com'),
('Mario', 'López', '21-56789012-5', 'mario.lopez@mail.com'),
('Sofía', 'Díaz', '28-67890123-6', 'sofia.diaz@mail.com'),
('Diego', 'Fernández', '25-78901234-7', 'diego.fernandez@mail.com'),
('Lucía', 'Gómez', '26-89012345-8', 'lucia.gomez@mail.com'),
('Pedro', 'Ruiz', '20-90123456-9', 'pedro.ruiz@mail.com'),
('Florencia', 'Alvarez', '23-11223344-0', 'florencia.alvarez@mail.com'),
('Gustavo', 'Benítez', '27-22334455-1', 'gustavo.benitez@mail.com'),
('Martina', 'Castro', '24-33445566-2', 'martina.castro@mail.com'),
('Federico', 'Domínguez', '21-44556677-3', 'federico.dominguez@mail.com'),
('Julieta', 'Escobar', '22-55667788-4', 'julieta.escobar@mail.com'),
('Nicolás', 'Flores', '26-66778899-5', 'nicolas.flores@mail.com'),
('Paula', 'Herrera', '23-77889900-6', 'paula.herrera@mail.com'),
('Andrés', 'Ibarra', '22-88990011-7', 'andres.ibarra@mail.com'),
('María', 'Juárez', '25-99001122-8', 'maria.juarez@mail.com'),
('Ricardo', 'Katz', '20-10111213-9', 'ricardo.katz@mail.com'),
('Camila', 'Luna', '21-12131415-0', 'camila.luna@mail.com');
