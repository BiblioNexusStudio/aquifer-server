import { Module } from '@nestjs/common';
import { PassagesService } from './passages.service';
import { PassagesController } from './passages.controller';
import { TypeOrmModule } from '@nestjs/typeorm';
import { Resource } from './entities/resource.entity';
import { ResourcesService } from './resources.service';
import { ResourceContent } from './entities/resource-content.entity';
import { Language } from './entities/language.entity';

@Module({
    imports: [TypeOrmModule.forFeature([Language, Resource, ResourceContent])],
    controllers: [PassagesController],
    providers: [PassagesService, ResourcesService],
})
export class PassagesModule {}
