/* eslint-disable no-console */
const fs = require("node:fs/promises");
const path = require("node:path");

const sharp = require("sharp");
const toIco = require("to-ico");

const ROOT = path.resolve(__dirname, "..");
const WWWROOT = path.join(ROOT, "wwwroot");

const SRC_SVG = path.join(WWWROOT, "favicon.svg");
const OUT_ICO = path.join(WWWROOT, "favicon.ico");
const OUT_192 = path.join(WWWROOT, "favicon-192.png");
const OUT_512 = path.join(WWWROOT, "favicon-512.png");

async function ensureSource() {
  await fs.access(SRC_SVG);
}

async function rasterizePng(size) {
  return sharp(SRC_SVG, { density: 384 })
    .resize(size, size, { fit: "cover" })
    .png({ compressionLevel: 9 })
    .toBuffer();
}

async function run() {
  await ensureSource();

  // PWA icons
  const png192 = await rasterizePng(192);
  const png512 = await rasterizePng(512);
  await fs.writeFile(OUT_192, png192);
  await fs.writeFile(OUT_512, png512);

  // Multi-size favicon.ico (common sizes)
  const icoPngs = await Promise.all([16, 24, 32, 48, 64, 128, 256].map(rasterizePng));
  const ico = await toIco(icoPngs);
  await fs.writeFile(OUT_ICO, ico);

  console.log("Generated favicons:");
  console.log("-", path.relative(ROOT, OUT_ICO));
  console.log("-", path.relative(ROOT, OUT_192));
  console.log("-", path.relative(ROOT, OUT_512));
}

run().catch((err) => {
  console.error(err);
  process.exitCode = 1;
});

