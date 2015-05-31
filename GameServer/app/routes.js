// app/routes.js

var game = require("../config/game");

module.exports = function(app, passport) {

    // =====================================
    // HOME PAGE (with login links) ========
    // =====================================
    app.get('/', function(req, res) {
        res.render('index.ejs'); // load the index.ejs file
    });

    // =====================================
    // LOGIN ===============================
    // =====================================
    // show the login form
    app.get('/login', function(req, res) {

        // render the page and pass in any flash data if it exists
        console.log("/login");
        res.render('login.ejs', { message: req.flash('loginMessage') });
    });

    // process the login form
    app.post('/login', passport.authenticate('local-login', {
            successRedirect : '/profile', // redirect to the secure profile section
            failureRedirect : '/login', // redirect back to the signup page if there is an error
            failureFlash : true // allow flash messages
        }),
        function(req, res) {
            console.log("here");
            if (req.body.remember) {
              req.session.cookie.maxAge = 1000 * 60 * 3;
            } else {
              req.session.cookie.expires = false;
           }
        res.redirect('/');
    });

    // =====================================
    // SIGNUP ==============================
    // =====================================
    // show the signup form
    app.get('/signup', function(req, res) {
        // render the page and pass in any flash data if it exists
        res.render('signup.ejs', { message: req.flash('signupMessage') });
    });

    // process the signup form
    app.post('/signup', passport.authenticate('local-signup', {
        successRedirect : '/profile', // redirect to the secure profile section
        failureRedirect : '/signup', // redirect back to the signup page if there is an error
        failureFlash : true // allow flash messages
    }));

    // =====================================
    // PROFILE SECTION =========================
    // =====================================
    // we will want this protected so you have to be logged in to visit
    // we will use route middleware to verify this (the isLoggedIn function)
    app.get('/profile', isLoggedIn, function(req, res) {
        res.render('profile.ejs', {
            user : req.user // get the user out of session and pass to template
        });
    });

    // =====================================
    // LOGOUT ==============================
    // =====================================
    app.get('/logout', function(req, res) {
        req.logout();
        res.redirect('/');
    });


    // Game routes. Can we add this somewhere else so routes doesn't get bloated?
    app.get('/money', function(req, res) {
        // TODO(liang): obviously unsafe way of doing database calls right now
        var username = req.query.id;
        console.log(username);
        console.log("get money");
        game.getMoney(username, function(err, data) {
            if (err) {
                res.json(err.errors);
            } else {
                res.json(data);
            }
        });
    });

    app.post('/addMoney', function(req, res) {
        var username = req.body.id;
        var value = req.body.value;
        console.log("Username: " + username);
        console.log("Value: " + value);
        console.log("add money");
        if (username && value) {
            game.addMoney(username, value, function(err) {
                if (err) {
                    console.log(err.errors);
                    res.end(err);
                } else {
                    res.end('{"success": "Updated Successfully", "status" : 200}');
                }
            });
        } else {
            console.log("Invalid arguments");
        }
    });

    app.get('/users', function(req, res) {
        var users = game.getUsers(function(err, data) {
            if (err) {
                res.json(err.errors);
            } else {
                res.json(data);
            }
        });
    });
}

// route middleware to make sure
function isLoggedIn(req, res, next) {

    // if user is authenticated in the session, carry on
    if (req.isAuthenticated()) {
        return next();
    }

    // if they aren't redirect them to the home page
    res.redirect('/');
}
