
CREATE DATABASE IF NOT EXISTS cards;

USE cards;
CREATE TABLE IF NOT EXISTS cards_emitidas (
    card_id INT PRIMARY KEY AUTO_INCREMENT,
    user_id INT,
    NombreCompleto VARCHAR(100),
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    cvv INT,
    card_number VARCHAR(20),
    mes_exp INT,
    año_exp INT
);


INSERT INTO cards_emitidas (NombreCompleto, cvv, card_number, mes_exp, año_exp) VALUES
('Juan Pérez', 123, '4111111111111111', 12, 2026),
('Ana García', 456, '4111111111111112', 5, 2025),
('Luis Martínez', 789, '4111111111111113', 7, 2027),
('Carla Rodríguez', 321, '4111111111111114', 10, 2024),
('Mario López', 654, '4111111111111115', 8, 2026),
('Sofía Díaz', 987, '4111111111111116', 3, 2025),
('Diego Fernández', 234, '4111111111111117', 9, 2024),
('Lucía Gómez', 567, '4111111111111118', 1, 2026),
('Pedro Ruiz', 890, '4111111111111119', 4, 2027),
('Florencia Alvarez', 112, '4111111111111120', 2, 2028),
('Gustavo Benítez', 223, '4111111111111121', 6, 2029),
('Martina Castro', 334, '4111111111111122', 11, 2026),
('Federico Domínguez', 445, '4111111111111123', 7, 2025),
('Julieta Escobar', 556, '4111111111111124', 12, 2024),
('Nicolás Flores', 667, '4111111111111125', 9, 2027),
('Paula Herrera', 778, '4111111111111126', 10, 2026),
('Andrés Ibarra', 889, '4111111111111127', 8, 2025),
('María Juárez', 990, '4111111111111128', 6, 2027),
('Ricardo Katz', 111, '4111111111111129', 4, 2024),
('Camila Luna', 222, '4111111111111130', 2, 2025);
