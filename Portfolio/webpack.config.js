const path = require('path');

module.exports = {
    entry: {
        timeline: './wwwroot/js/timeline.js',
        site: './wwwroot/js/site.js'
    },
    output: {
        filename: '[name].bundle.js',
        path: path.resolve(__dirname, 'wwwroot', 'dist'),
    },
    resolve: {
        alias: {
            '@': path.resolve(__dirname, 'src/')
        }
    },
};