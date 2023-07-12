import { Injectable } from '@nestjs/common';
import { InjectRepository } from '@nestjs/typeorm';
import { bookChapterVerseToBnVerse } from '../utils/bn-verse-mapper';
import { parsePassageReference } from '../utils/bn-verse-parser';
import { upsertEntityUsingRepository } from '../utils/repository-upsert';
import { Repository } from 'typeorm';
import { CreatePassageDto } from './dto/create-passage.dto';
import { Passage } from './entities/passage.entity';

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
                type: dto.type,
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

    findAll() {
        return `This action returns all passages`;
    }

    findOne(id: number) {
        return `This action returns a #${id} passage`;
    }
}
