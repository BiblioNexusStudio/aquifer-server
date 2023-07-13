import { Injectable } from '@nestjs/common';
import { InjectRepository } from '@nestjs/typeorm';
import { bookChapterVerseToBnVerse } from '../utils/bn-verse-mapper';
import { parsePassageReference } from '../utils/bn-verse-parser';
import { upsertEntityUsingRepository } from '../utils/repository-upsert';
import { Repository } from 'typeorm';
import { CreatePassageDto } from './dto/create-passage.dto';
import { Passage, PassageTypeInt } from './entities/passage.entity';
import { Resource, ResourceType, ResourceTypeInt } from './entities/resource.entity';

@Injectable()
export class PassagesService {
    constructor(
        @InjectRepository(Passage)
        private passagesRepository: Repository<Passage>,
    ) {}

    async upsert(dto: CreatePassageDto) {
        const startAndEnd = parsePassageReference(dto.passageReference);
        if (startAndEnd) {
            const [startBookChapterVerse, endBookChapterVerse] = startAndEnd;
            const startBnVerse = bookChapterVerseToBnVerse(startBookChapterVerse);
            const endBnVerse = bookChapterVerseToBnVerse(endBookChapterVerse);

            const passage = this.passagesRepository.create({
                type: PassageTypeInt[dto.type],
                startBnVerse,
                endBnVerse,
            });

            return await upsertEntityUsingRepository(
                passage,
                ['type', 'startBnVerse', 'endBnVerse'],
                this.passagesRepository,
            );
        } else {
            throw new Error('Invalid passage reference');
        }
    }

    async linkPassageToResource(passage: Passage, resource: Resource) {
        await passage.loadRelation('resources');
        if (passage.resources.every((r) => r.id !== resource.id)) {
            passage.resources.push(resource);
            return await this.passagesRepository.save(passage);
        } else {
            return passage;
        }
    }

    async findAllWithResourcesInLanguage(
        resourceTypes: ResourceType[],
        languageCode = '',
    ): Promise<Passage[]> {
        if (!resourceTypes.length) {
            return [];
        } else {
            return this.passagesRepository
                .createQueryBuilder('passage')
                .innerJoinAndSelect('passage.resources', 'resource')
                .innerJoinAndSelect('resource.resourceContents', 'resourceContent')
                .innerJoin('resourceContent.language', 'language')
                .where(
                    'language.ISO639_3Code = :languageCode AND resource.type IN (:...resourceType)',
                    {
                        resourceType: resourceTypes.map((t) => ResourceTypeInt[t]),
                        languageCode,
                    },
                )
                .getMany();
        }
    }

    findOne(id: number) {
        return `This action returns a #${id} passage`;
    }
}
