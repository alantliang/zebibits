var db = require('./db');
var bcrypt = require('bcrypt-nodejs');

function findUser(username, callback) {
	db.getConnection(function(err, connection) {
		connection.query("SELECT * FROM users WHERE username = ?", [username], function(err, rows) {
			if (err) {
				console.log("There was an error with the SQL query: ");
				callback(err);
			}
			if (rows.length) {
				callback(err, rows[0]);
				// console.log(rows);
			}
		});
	});
}

function createUser(username, password, callback) {
	db.getConnection(function(err, connection) {
		var newUserMysql = {};
		newUserMysql.username = username;
		newUserMysql.password = bcrypt.hashSync(password, null, null);
		console.log("Encrypted password: " + newUserMysql.password);
		var insertQuery = "INSERT INTO users (username, password) values (?, ?)";
		connection.query(insertQuery, [newUserMysql.username, newUserMysql.password], function(err, rows) {
			newUserMysql.id = rows.insertId;
			return callback(null, newUserMysql);
		});
	});
}

function getAllUsers(callback) {
	db.getConnection(function(err, connection) {
		connection.query("SELECT * FROM users", function(err, rows) {
			if (err) {
				console.log("There was an error with the SQL query: ");
				callback(err);
			}
			if (rows.length) {
				callback(err, rows);
			}
		});
	});
}


// createUser("liang", "taota0", function(err, newUserMysql) {
// 	console.log(JSON.stringify(newUserMysql));
// });

// findUser('xtraneus@gmail.com', 'taota0', function(err) {
// 	console.log(err);
// });

exports.findUser = findUser;
exports.getAllUsers = getAllUsers;