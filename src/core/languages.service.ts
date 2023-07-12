import { Injectable } from '@nestjs/common';
import { InjectRepository } from '@nestjs/typeorm';
import { Repository } from 'typeorm';
import { Language } from './entities/language.entity';

@Injectable()
export class LanguagesService {
    constructor(
        @InjectRepository(Language)
        private languageRepository: Repository<Language>,
    ) {}

    findByIsoCode(ISO639_3Code = '') {
        return this.languageRepository.findOneBy({ ISO639_3Code });
    }
}
