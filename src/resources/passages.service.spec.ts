import { Test, TestingModule } from '@nestjs/testing';
import { getRepositoryToken } from '@nestjs/typeorm';
import { Passage } from './entities/passage.entity';
import { PassagesService } from './passages.service';

describe('PassagesService', () => {
    let service: PassagesService;
    const mockPassagesRepository = jest.fn();

    beforeEach(async () => {
        const module: TestingModule = await Test.createTestingModule({
            providers: [
                PassagesService,
                {
                    provide: getRepositoryToken(Passage),
                    useValue: mockPassagesRepository,
                },
            ],
        }).compile();

        service = module.get<PassagesService>(PassagesService);
    });

    it('should be defined', () => {
        expect(service).toBeDefined();
    });
});
