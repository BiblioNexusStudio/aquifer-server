import { ConfigService } from '@nestjs/config';
import { NestFactory } from '@nestjs/core';
import { AppModule } from './app.module';
import * as compression from 'compression';
import './utils/group-by';

async function bootstrap() {
    const app = await NestFactory.create(AppModule);
    app.enableCors();
    const config = app.get(ConfigService);
    if (config.get('useCompression')) {
        app.use(compression());
    }
    await app.listen(config.get('port'));
}
// eslint-disable-next-line
bootstrap();
