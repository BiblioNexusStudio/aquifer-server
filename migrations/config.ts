import configuration from '../src/config/configuration';
import { DataSource } from 'typeorm';
import 'dotenv/config';

export default new DataSource({
    ...configuration().database,
    migrations: ['migrations/!(config)*.ts'],
    entities: ['src/**/*.entity.ts'],
    migrationsTableName: 'Migrations',
});
