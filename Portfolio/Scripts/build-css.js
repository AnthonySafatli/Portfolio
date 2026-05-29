const fs = require("fs");
const path = require("path");
const CleanCSS = require("clean-css");

const inputDir = path.join(__dirname, "../wwwroot/css");
const outputDir = path.join(__dirname, "../wwwroot/dist/css");

fs.mkdirSync(outputDir, { recursive: true });

const files = fs.readdirSync(inputDir).filter((f) => f.endsWith(".css"));

for (const file of files) {
	const inputPath = path.join(inputDir, file);
	const outputPath = path.join(outputDir, file);

	const input = fs.readFileSync(inputPath, "utf8");

	const output = new CleanCSS({}).minify(input);

	if (output.errors.length) {
		console.error(file, output.errors);
		continue;
	}

	fs.writeFileSync(outputPath, output.styles);
	console.log(`Minified: ${file}`);
}
