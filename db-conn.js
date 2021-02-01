var mysql = require('mysql2');

//TODO: db pool cluster with master -> slave env settings
// Part of optimization

console.log("Creating mysql pool")
var db = mysql.createPool({
    host: process.env.DB_HOST,
    port: process.env.DB_PORT,
    user: process.env.DB_USER,
    password: process.env.DB_SECRET,
    database: process.env.DB_NAME,
    insecureAuth: true
});

var dbConnectionAttempts = 0;
const DB_MAX_ATTEMPTS = 5;

function result(err) {
    if (err) {
        dbConnectionAttempts++;
        console.log('attempt ' + dbConnectionAttempts + ' sql connection error: ' + err);
        
        if (dbConnectionAttempts < DB_MAX_ATTEMPTS) {
            setTimeout(() => { db.connect(result); }, 5000) 
        }

        return;
    }
    
    console.log('connected to database server');

    if (db.onload) db.onload();
}

module.exports = db;