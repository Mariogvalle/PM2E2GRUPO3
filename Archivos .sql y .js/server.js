const express = require('express');
const bodyParser = require('body-parser');
const mysql = require('mysql');

const cors = require('cors');
const port = 5000;
const app = express();

app.use(bodyParser.json({ limit: '20mb' }));
app.use(bodyParser.urlencoded({ limit: '20mb', extended: true }));
app.use(bodyParser.text({ limit: '20mb' }));

const db = mysql.createConnection({
    host: 'localhost',
    user: 'root',
    password: '',
    database: 'dbpm02'
});

db.connect(err => {
    if (err) throw err;
    console.log('MySQL conectado...');
});

//Operaciones de CRUD
//Para registro de ubiaciones
app.post('/api/ubicaciones', (req, res) => {
    const { descripcion, latitude, longitude, video, audio } = req.body;
    const query = 'INSERT INTO ubicaciones (descripcion, latitude, longitude, video, audio) VALUES (?, ?, ?, ?, ?)';
    db.query(query, [descripcion, latitude, longitude, video, audio], (err, result) => {
        if (err) throw err;
        res.send('Ubicación agregada...');
    });
});

//Para obtener ubicaciones
app.get('/api/ubicaciones', (req, res) => {
    db.query('SELECT * FROM ubicaciones', (err, results) => {
        if (err) throw err;
        res.json(results);
    });
});

//update by id
app.put('/api/ubicaciones/:id', (req, res) => 
    {
        const { id } = req.params;
        const { descripcion } = req.body;
        const consulta = 'UPDATE ubicaciones set descripcion = ? WHERE id = ?';
        db.query(consulta, [descripcion], (err, result) => {
            if (err) throw err;
            res.send('Eliminado exitosamente.');
        });
    });
    
// Ruta para eliminar una ubicación por ID
app.delete('/api/ubicaciones/:id', (req, res) => {
    const { id } = req.params;
    db.query('DELETE FROM ubicaciones WHERE id = ?', [id], (err, result) => {
        if (err) throw err;
        res.send('Eliminado exitosamente.');
    });
});

app.listen(port, () => {
    console.log(`Servidor escuchando en el puerto ${port}`);
});
