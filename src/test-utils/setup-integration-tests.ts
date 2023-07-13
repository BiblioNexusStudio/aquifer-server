import { Test, TestingModule } from '@nestjs/testing';
import { INestApplication } from '@nestjs/common';
import { AppModule } from '../app.module';
import { DataSource } from 'typeorm';
import TransactionalTestContext from './transactional-test-context';

export let app: INestApplication;

let transactionalTestContext: TransactionalTestContext | undefined;

beforeEach(async () => {
    try {
        const moduleFixture: TestingModule = await Test.createTestingModule({
            imports: [AppModule],
        }).compile();

        app = moduleFixture.createNestApplication();
    } catch (error) {
        if (error.name === 'ConnectionError') {
            throw new Error(
                "No database connection available. Make sure you're pointing to a database server and the correct database name 'biblionexus-test-db' is created.",
            );
        } else {
            throw error;
        }
    }

    await app.init();

    const dataSource = app.get(DataSource);
    transactionalTestContext = new TransactionalTestContext(dataSource);
    await transactionalTestContext.start();
});

afterEach(async () => {
    await transactionalTestContext?.finish();
    await app?.close();
});
