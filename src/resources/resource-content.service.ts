import { Injectable } from '@nestjs/common';
import { InjectRepository } from '@nestjs/typeorm';
import { LanguagesService } from '../core/languages.service';
import { Repository } from 'typeorm';
import { CreateResourceContentDto } from './dto/create-resource-content.dto';
import { ResourceContent } from './entities/resource-content.entity';
import { ResourcesService } from './resources.service';
import { upsertEntityUsingRepository } from '../utils/repository-upsert';

@Injectable()
export class ResourceContentService {
    constructor(
        @InjectRepository(ResourceContent)
        private resourceContentRepository: Repository<ResourceContent>,
        private resourcesService: ResourcesService,
        private languagesService: LanguagesService,
    ) {}

    async upsert(dto: CreateResourceContentDto) {
        const resource = await this.resourcesService.upsert({
            type: dto.type,
            passageReference: dto.passageReference,
        });

        const language = await this.languagesService.findByIsoCode(dto.languageCode);

        const resourceContent = this.resourceContentRepository.create({
            resource: { id: resource?.id },
            language: { id: language?.id },
            displayName: dto.displayName,
            content: JSON.stringify(dto.content),
            isComplete: true,
            isTrusted: false,
        });

        return await upsertEntityUsingRepository(
            resourceContent,
            ['resource', 'language'],
            this.resourceContentRepository,
        );
    }
}
