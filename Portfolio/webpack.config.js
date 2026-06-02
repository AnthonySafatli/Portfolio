const path = require("path");
const glob = require("glob");

const baseDir = path.resolve(__dirname, "wwwroot/js");

const entries = Object.fromEntries(
	glob.sync("**/*.js", { cwd: baseDir }).map((file) => {
		const fullPath = path.join(baseDir, file);

		return [
			file.replace(/\.js$/, ""), 
			fullPath,
		];
	})
);

module.exports = {
	entry: entries,
	output: {
		filename: "[name].bundle.js",
		path: path.resolve(__dirname, "wwwroot", "dist", "js"),
	},
	resolve: {
		alias: {
			"@": path.resolve(__dirname, "src/"),
		},
	},
	module: {
		rules: [
			{
				test: /\.(glsl|vs|fs)$/,
				exclude: /node_modules/,
				use: "raw-loader",
			},
		],
	},
};
