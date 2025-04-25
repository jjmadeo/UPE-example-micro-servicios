
CREATE DATABASE IF NOT EXISTS wallet;

USE wallet;

CREATE TABLE IF NOT EXISTS account (
    account_id INT PRIMARY KEY AUTO_INCREMENT,
    user_id INT,
    alias VARCHAR(50),
    cvu VARCHAR(22),
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);
-- Insertar 20 wallets
INSERT INTO account (user_id, alias, cvu) VALUES
(1, 'juan.wallet', '00000031000000000001'),
(2, 'ana.wallet', '00000031000000000002'),
(3, 'luis.wallet', '00000031000000000003'),
(4, 'carla.wallet', '00000031000000000004'),
(5, 'mario.wallet', '00000031000000000005'),
(6, 'sofia.wallet', '00000031000000000006'),
(7, 'diego.wallet', '00000031000000000007'),
(8, 'lucia.wallet', '00000031000000000008'),
(9, 'pedro.wallet', '00000031000000000009'),
(10, 'flor.wallet', '00000031000000000010'),
(11, 'gustavo.wallet', '00000031000000000011'),
(12, 'martina.wallet', '00000031000000000012'),
(13, 'fede.wallet', '00000031000000000013'),
(14, 'julieta.wallet', '00000031000000000014'),
(15, 'nicolas.wallet', '00000031000000000015'),
(16, 'paula.wallet', '00000031000000000016'),
(17, 'andres.wallet', '00000031000000000017'),
(18, 'maria.wallet', '00000031000000000018'),
(19, 'ricardo.wallet', '00000031000000000019'),
(20, 'camila.wallet', '00000031000000000020');
