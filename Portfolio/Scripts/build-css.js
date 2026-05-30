const fs = require("fs");
const path = require("path");
const CleanCSS = require("clean-css");

const inputDir = path.join(__dirname, "../wwwroot/css");
const outputDir = path.join(__dirname, "../wwwroot/dist/css");

fs.mkdirSync(outputDir, { recursive: true });

function getAllCssFiles(dir) {
	const entries = fs.readdirSync(dir, { withFileTypes: true });

	let files = [];

	for (const entry of entries) {
		const fullPath = path.join(dir, entry.name);

		if (entry.isDirectory()) {
			files = files.concat(getAllCssFiles(fullPath));
		} else if (
			entry.isFile() &&
			entry.name.endsWith(".css") &&
			!entry.name.endsWith(".min.css")
		) {
			files.push(fullPath);
		}
	}

	return files;
}

const files = getAllCssFiles(inputDir);

for (const inputPath of files) {
	const relativePath = path.relative(inputDir, inputPath);

	const parsed = path.parse(relativePath);
	const outputFileName = `${parsed.name}.min.css`;

	const outputPath = path.join(
		outputDir,
		parsed.dir, // preserves subfolders
		outputFileName
	);

	fs.mkdirSync(path.dirname(outputPath), { recursive: true });

	const input = fs.readFileSync(inputPath, "utf8");

	const output = new CleanCSS({}).minify(input);

	if (output.errors.length) {
		console.error(inputPath, output.errors);
		continue;
	}

	fs.writeFileSync(outputPath, output.styles);
	console.log(`Minified: ${relativePath}\t-> ${path.join(parsed.dir, outputFileName)}`);
}
