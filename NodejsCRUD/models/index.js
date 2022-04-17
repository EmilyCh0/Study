const mongoose = require('mongoose');

const config = require('../config/key');

const connect = () => {

    mongoose.connect(config.mongoURI, {
        dbName: config.dbName
    }, (error) => {
        if(error){
            console.log('MongoDB connection error', error);
        }else{
            console.log('MongoDB connected . . .');
        }
    });

}

mongoose.connection.on('error', (error) => {
    console.error('MongoDB connection error', error);
});

module.exports = connect;