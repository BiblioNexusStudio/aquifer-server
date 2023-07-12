import { repl } from '@nestjs/core';
import { AppModule } from './app.module';

async function bootstrap() {
    const server = await repl(AppModule);
    server.setupHistory('.repl-history', () => {
        // no-op
    });
}
bootstrap();
