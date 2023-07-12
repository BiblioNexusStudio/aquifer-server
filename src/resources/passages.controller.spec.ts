import { Test, TestingModule } from '@nestjs/testing';
import { getRepositoryToken } from '@nestjs/typeorm';
import { Passage } from './entities/passage.entity';
import { PassagesController } from './passages.controller';
import { PassagesService } from './passages.service';

describe('PassagesController', () => {
    let controller: PassagesController;
    const mockPassagesRepository = jest.fn();

    beforeEach(async () => {
        const module: TestingModule = await Test.createTestingModule({
            controllers: [PassagesController],
            providers: [
                PassagesService,
                {
                    provide: getRepositoryToken(Passage),
                    useValue: mockPassagesRepository,
                },
            ],
        }).compile();

        controller = module.get<PassagesController>(PassagesController);
    });

    it('should be defined', () => {
        expect(controller).toBeDefined();
    });
});
