// get an instance of mongoose and mongoose.Scehma
var mongoose = require('mongoose');
var Schema = mongoose.Schema;

// set up a mongoose model and pass it using module.exports
module.exports = mongoose.model('User', new Schema({
	name: String,
	password: String,
	admin: Boolean
}));