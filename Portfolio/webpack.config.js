const path = require('path');

module.exports = {
    entry: {
        timeline: './wwwroot/js/timeline.js',
        site: './wwwroot/js/site.js',
        background: './wwwroot/js/background.js'
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
    module: {
        rules: [
            {
                test: /\.(glsl|vs|fs)$/,
                exclude: /node_modules/,
                use: 'raw-loader'
            },
        ]
    }
};