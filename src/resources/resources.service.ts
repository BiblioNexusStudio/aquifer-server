import { Injectable } from '@nestjs/common';
import { InjectRepository } from '@nestjs/typeorm';
import { PassageType } from './entities/passage.entity';
import { PassagesService } from './passages.service';
import { Repository } from 'typeorm';
import { CreateResourceDto } from './dto/create-resource.dto';
import { Resource, ResourceType, ResourceTypeInt } from './entities/resource.entity';
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
        let resource = this.resourcesRepository.create({
            type: ResourceTypeInt[dto.type],
            englishLabel: tag,
            tag,
        });

        resource = await upsertEntityUsingRepository(
            resource,
            ['type', 'tag'],
            this.resourcesRepository,
        );

        await this.passagesService.linkPassageToResource(passage, resource);

        return resource;
    }

    findAll() {
        return this.resourcesRepository.find();
    }

    findOne(id: number) {
        return this.resourcesRepository.findOneBy({ id });
    }

    private resourceTypeToPassageType(resourceType: ResourceType): PassageType {
        switch (resourceType) {
            case 'CBBT_ER':
                return 'CBBT_ER';
        }
    }
}
