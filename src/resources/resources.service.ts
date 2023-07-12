import { Injectable } from '@nestjs/common';
import { InjectRepository } from '@nestjs/typeorm';
import { PassageType } from './entities/passage.entity';
import { PassagesService } from './passages.service';
import { Repository } from 'typeorm';
import { CreateResourceDto } from './dto/create-resource.dto';
import { Resource, ResourceType } from './entities/resource.entity';
import { upsertEntityUsingRepository } from '../utils/repository-upsert';

@Injectable()
export class ResourcesService {
    constructor(
        @InjectRepository(Resource)
        private resourcesRepository: Repository<Resource>,
        private passagesService: PassagesService,
    ) {}

    async upsert(dto: CreateResourceDto) {
        const passage = await this.passagesService.upsert({
            passageReference: dto.passageReference,
            type: this.resourceTypeToPassageType(dto.type),
        });

        const tag = passage.toString();
        const resource = this.resourcesRepository.create({
            type: dto.type,
            englishLabel: tag,
            tag,
        });

        // TODO: create passage-resource link

        return await upsertEntityUsingRepository(
            resource,
            ['type', 'tag'],
            this.resourcesRepository,
        );
    }

    findAll() {
        return this.resourcesRepository.find();
    }

    findOne(id: number) {
        return this.resourcesRepository.findOneBy({ id });
    }

    private resourceTypeToPassageType(resourceType: ResourceType): PassageType {
        switch (resourceType) {
            case ResourceType.CBBT_ER:
                return PassageType.CBBT_ER;
        }
    }
}
