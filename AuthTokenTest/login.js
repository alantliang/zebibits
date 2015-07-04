var db = require('./db');
var bcrypt = require('bcrypt-nodejs');

function findUser(username, callback) {
	db.getConnection(function(err, connection) {
		connection.query("SELECT * FROM users WHERE username = ?", [username], function(err, rows) {
			if (err) {
				console.log("There was an error with the SQL query: ");
				callback(err);
			}
			if (rows.length == 0) {
				// no results found, so pass null user
				callback(err, null);
			} else if (rows.length == 1) {
				callback(err, rows[0]);
			} else {
				console.log("Too many users!");
			}  // we might want to handle if multiple rows are returned
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

function getUser(username, password, callback) {
	db.getConnection(function(err, connection) {
		console.log('Im here');
		var query = 'SELECT * FROM users U JOIN player P JOIN owner O JOIN pet ON ' + 
			'P.user_id = U.id AND O.user_id = U.id AND pet.pet_id = O.pet_id ' + 
			'WHERE U.username = (?) AND U.password = (?)';
		console.log(query);
		connection.query(query, [username, password], function(err, rows) {
			if (err) {
				console.log("There was an error with the SQL query: " + err);
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
exports.getUser = getUser;
// exports.getAllUsers = getAllUsers;