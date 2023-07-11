import { Test, TestingModule } from '@nestjs/testing';
import { PassagesService } from './passages.service';

describe('PassagesService', () => {
    let service: PassagesService;

    beforeEach(async () => {
        const module: TestingModule = await Test.createTestingModule({
            providers: [PassagesService],
        }).compile();

        service = module.get<PassagesService>(PassagesService);
    });

    it('should be defined', () => {
        expect(service).toBeDefined();
    });
});
