import { Module } from '@nestjs/common';
import { TypeOrmModule } from '@nestjs/typeorm';
import { Resource } from './entities/resource.entity';
import { ResourcesService } from './resources.service';
import { ResourceContent } from './entities/resource-content.entity';
import { CoreModule } from '../core/core.module';
import { PassagesService } from './passages.service';
import { Passage } from './entities/passage.entity';
import { ResourceContentService } from './resource-content.service';
import { PassagesController } from './passages.controller';
import { ResourceContentController } from './resource-content.controller';

@Module({
    imports: [TypeOrmModule.forFeature([Passage, Resource, ResourceContent]), CoreModule],
    controllers: [PassagesController, ResourceContentController],
    providers: [PassagesService, ResourcesService, ResourceContentService],
})
export class ResourcesModule {}
