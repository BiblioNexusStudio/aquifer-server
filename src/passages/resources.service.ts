import { Injectable } from '@nestjs/common';
import { InjectRepository } from '@nestjs/typeorm';
import { Repository } from 'typeorm';
import { Resource } from './entities/resource.entity';

@Injectable()
export class ResourcesService {
    constructor(
        @InjectRepository(Resource)
        private resourcesRepository: Repository<Resource>,
    ) {}

    findAll() {
        return this.resourcesRepository.find();
    }

    findOne(id: number) {
        return this.resourcesRepository.findOneBy({ id });
    }
}
