// config/game.js

var mysql = require('mysql');
var dbconfig = require('./gameDatabase');
var connection = mysql.createConnection(dbconfig.connection);

connection.query('USE ' + dbconfig.database);

function getMoney(username, done) {
    connection.query("SELECT `money` FROM users WHERE `name` = ? ", [username], function(err, rows) {
        done(err, rows[0]);
    });
}

function addMoney(username, value, done) {
    // currently username is not used. We should find the primary id of the user?
    connection.query("UPDATE users SET `money` = `money` + ? WHERE `id` = ? ", 
        [value, 1], function(err) {
            done(err);
        });
}

function getUsers(done) {
    connection.query("SELECT * FROM users ", function(err, rows) {
        done(err, rows[0]);
    });
}


function testAddMoney() {
    addMoney("liang", 20, function(err) {
        if (err) {
            console.log("There was an error:");
            console.log(err);
            console.log(err.errors);
        }
    });
}

function testGetUsers() {
    getUsers(function(err, data) {
        if (err) {
            console.log(err);
        } else {
            console.log(data);
        }
    });
}

// testAddMoney();
// testGetUsers();

exports.getMoney = getMoney;
exports.addMoney = addMoney;
exports.getUsers = getUsers;