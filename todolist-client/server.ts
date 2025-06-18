import fs from 'fs';
import next from 'next';
import { parse } from 'url';
import { createServer } from 'https';
import 'dotenv/config';

const port: number = parseInt(process.env.NEXTAPP_HTTPS_PORT ?? '4000');
const dev: boolean = process.env.NODE_ENV !== 'production';
const httpsOptions = {
  pfx: fs.readFileSync('certs/https/development.pfx'),
  passphrase: (() => {
    if (!process.env.HTTPS_DEVCERT_PASSWORD) {
      throw new Error("Environment variable HTTPS_DEVCERT_PASSWORD is required but not set.");
    }

    return process.env.HTTPS_DEVCERT_PASSWORD;
  })(),
};

const app = next({ dev });
const handle = app.getRequestHandler();

startServer();

async function startServer(): Promise<void> {
  await app.prepare();

  createServer(httpsOptions, (req, res) => {
    handle(req, res, parse(req.url!, true));
  }).listen(port, () => console.info(`> 🔐 HTTPS server is ready at https://localhost:${port}`));
}
