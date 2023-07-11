import { Test, TestingModule } from '@nestjs/testing';
import { PassagesController } from './passages.controller';
import { PassagesService } from './passages.service';

describe('PassagesController', () => {
    let controller: PassagesController;

    beforeEach(async () => {
        const module: TestingModule = await Test.createTestingModule({
            controllers: [PassagesController],
            providers: [PassagesService],
        }).compile();

        controller = module.get<PassagesController>(PassagesController);
    });

    it('should be defined', () => {
        expect(controller).toBeDefined();
    });
});
