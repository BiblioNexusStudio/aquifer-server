import { repl } from '@nestjs/core';
import { AppModule } from './app.module';
import { ResourceTypeInt } from './resources/entities/resource.entity';
import './utils/group-by';
import { parsePassageReference } from './utils/bn-verse-parser';

async function bootstrap() {
    const server = await repl(AppModule);
    server.setupHistory('.repl-history', () => {
        // no-op
    });
    server.context.ResourceType = ResourceTypeInt;
    server.context.parsePassageReference = parsePassageReference;
}
// eslint-disable-next-line
bootstrap();
