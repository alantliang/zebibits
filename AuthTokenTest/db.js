var mysql = require('mysql');

var myDb = {
	'connection': {
		'host': '127.0.0.1', // don't need port number here
		'user': 'root',
		'password': 'hbFs!er225'
	},
	'database': 'zebibits',
	'users_table': 'users' // this must be the table name (currently) 
};


var pool = mysql.createPool({
	host: myDb.connection.host,
	user: myDb.connection.user,
	password: myDb.connection.password,
	database: myDb.database,
});

var getConnection = function(callback) {
	pool.getConnection(function(err, connection) {
		if (err) {
			console.log("There was an error with the SQL connection: ");
			console.log(err);
		} else {
			callback(err, connection);
		}
	});
};

exports.getConnection = getConnection;