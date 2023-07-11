import { Test, TestingModule } from '@nestjs/testing';
import { AppModule } from './app.module';

describe('AppModule', () => {
    let appModule: AppModule;

    beforeEach(async () => {
        const testModule: TestingModule = await Test.createTestingModule({
            providers: [AppModule],
        }).compile();

        appModule = testModule.get<AppModule>(AppModule);
    });

    it('should be defined', () => {
        expect(appModule).toBeDefined();
    });
});
