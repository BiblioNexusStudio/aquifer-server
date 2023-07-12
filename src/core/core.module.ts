import { Module } from '@nestjs/common';
import { TypeOrmModule } from '@nestjs/typeorm';
import { Language } from './entities/language.entity';
import { LanguagesService } from './languages.service';

@Module({
    imports: [TypeOrmModule.forFeature([Language])],
    controllers: [],
    providers: [LanguagesService],
    exports: [LanguagesService],
})
export class CoreModule {}
