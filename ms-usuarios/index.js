const express = require('express');
const amqp = require('amqplib');
const pool = require('./db');
require('dotenv').config();

const app = express();
app.use(express.json());

let channel;

async function connectRabbitMQ() {
  const conn = await amqp.connect(process.env.RABBITMQ_URL);
  channel = await conn.createChannel();
  await channel.assertExchange('eventos', 'fanout', { durable: true });
}

app.post('/usuario', async (req, res) => {
  const { nombre, apellido, cuil, email } = req.body;

  try {
    const [result] = await pool.query(
      'INSERT INTO usuarios (nombre, apellido, cuil, email) VALUES (?, ?, ?, ?)',
      [nombre, apellido, cuil, email]
    );

    const evento = {
      tipo: 'usuario_creado',
      data: {
        user_id: result.insertId,
        nombre,
        apellido,
        email
      }
    };

    channel.publish('eventos', '', Buffer.from(JSON.stringify(evento)));
    res.status(201).json({ id: result.insertId, mensaje: 'Usuario creado y evento publicado' });
  } catch (error) {
    console.error('Error:', error);
    res.status(500).json({ error: 'Error al crear usuario' });
  }
});

app.get('/usuario', async (req, res) => {
  try {
    const [rows] = await pool.query('SELECT * FROM usuarios');
    res.status(200).json(rows);
  } catch (error) {
    console.error('Error al obtener usuarios:', error);
    res.status(500).json({ error: 'Error al obtener usuarios' });
  }
});

const PORT = 3000;
app.listen(PORT, async () => {
  await connectRabbitMQ();
  console.log(`Servidor escuchando en puerto ${PORT}`);
});
