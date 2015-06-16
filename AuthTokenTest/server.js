// from https://scotch.io/tutorials/authenticate-a-node-js-api-with-json-web-tokens

// =======================
// get the packages we need ============
// =======================
var express = require('express');
var app = express();
var bodyParser = require('body-parser');
var morgan = require('morgan');
// var mongoose = require('mongoose');

var bcrypt = require('bcrypt-nodejs');
var jwt = require('jsonwebtoken'); // used to create, sign, and verify tokens
var config = require('./config'); // get our config file
// var User = require('./models/user'); // get our mongoose model
var login = require('./login');

// =======================
// configuration =========
// =======================
var port = process.env.PORT || 8080; // used to create, sign, and verify tokens
// mongoose.connect(config.database); // connect to database
app.set('superSecret', config.secret); // secret variable

// use body parser so we can get info from POST and/or URL parameters
app.use(bodyParser.urlencoded({
	extended: false
}));
app.use(bodyParser.json());

// use morgan to log requests to the console
app.use(morgan('dev'));

// =======================
// routes ================
// =======================

// API ROUTES -------------------
// we'll get to these in a second
app.get('/setup', function(req, res) {

	// create a sample user
	var nick = new User({
		name: 'Alan Liang',
		password: 'password',
		admin: true
	});

	// save the sample user
	nick.save(function(err) {
		if (err) throw err;

		console.log('User saved successfully');
		res.json({
			success: true
		});
	});
});
// get an instance of the router for api routes
var apiRoutes = express.Router();

// route to authenticate a user (POST http://localhost:8080/api/authenticate)
apiRoutes.post('/authenticate', function(req, res) {
	console.log(req.body.name);
	// find the user
	login.findUser(req.body.name, function(err, user) {
		if (err) {
			console.log(err);
			throw err;
		}
		if (!user) {
			res.json({
				success: false,
				message: 'Authentication failed. User not found.'
			});
		} else if (user) {
			console.log(req.body.password);
			console.log(user.password);
			console.log(user);
			if (!bcrypt.compareSync(req.body.password, user.password)) {
				res.json({
					success: false,
					message: 'Authentication failed. Wrong password.'
				});
			} else {
				var token = jwt.sign(user, app.get('superSecret'), {
					expiresInMinutes: 1440 // expires in 24 hours
				});
				res.json({
					success: true,
					message: 'Enjoy your token!',
					token: token,
				});
			}
		}
	});
});

// route middleware to verify a token
apiRoutes.use(function(req, res, next) {

	// check header or url parameters or post parameters for token
	var token = req.body.token || req.query.token || req.headers['x-access-token'];

	// decode token
	if (token) {

		// verifies secret and checks exp
		jwt.verify(token, app.get('superSecret'), function(err, decoded) {
			if (err) {
				return res.json({
					success: false,
					message: 'Failed to authenticate token.'
				});
			} else {
				// if everything is good, save to request for use in other routes
				req.decoded = decoded;
				console.log("Decoded: " + JSON.stringify(req.decoded));
				next();
			}
		});

	} else {

		// if there is no token
		// return an error
		return res.status(403).send({
			success: false,
			message: 'No token provided.'
		});

	}
});

// route to show a random message (GET http://localhost:8080/api/)
apiRoutes.get('/', function(req, res) {
	res.json({
		message: 'Welcome to the coolest API on earth!'
	});
});

// route to return all users (GET http://localhost:8080/api/users)
// need to set 
apiRoutes.get('/users', function(req, res) {
	login.getAllUsers(function(err, users) {
		res.json(users);
	})
});



// apply the routes to our application with the prefix /api
app.use('/api', apiRoutes);

// =======================
// start the server ======
// =======================
app.listen(port);
console.log('Magic happens at http://localhost:' + port);