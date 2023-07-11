import { Module } from '@nestjs/common';
import { TypeOrmModule } from '@nestjs/typeorm';
import { PassagesModule } from './passages/passages.module';
import { ConfigModule, ConfigService } from '@nestjs/config';
import configuration from './config/configuration';

@Module({
    imports: [
        ConfigModule.forRoot({ load: [configuration] }),
        TypeOrmModule.forRootAsync({
            imports: [ConfigModule],
            useFactory: (configService: ConfigService) => ({
                ...configService.get('database'),
                autoLoadEntities: true,
            }),
            inject: [ConfigService],
        }),
        PassagesModule,
    ],
    controllers: [],
    providers: [],
})
export class AppModule {}
