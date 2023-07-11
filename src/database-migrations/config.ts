import configuration from '../config/configuration';
import { DataSource } from 'typeorm';
import 'dotenv/config';

export default new DataSource({
    ...configuration().database,
    migrations: ['src/database-migrations/!(config)*.ts'],
    entities: ['src/**/*.entity.ts'],
    migrationsTableName: 'Migrations',
});
