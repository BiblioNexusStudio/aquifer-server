import { repl } from '@nestjs/core';
import { AppModule } from './app.module';
import { ResourceType } from './resources/entities/resource.entity';

async function bootstrap() {
    const server = await repl(AppModule);
    server.setupHistory('.repl-history', () => {
        // no-op
    });
    server.context.ResourceType = ResourceType;
}
bootstrap();
