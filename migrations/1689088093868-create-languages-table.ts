import { MigrationInterface, QueryRunner } from 'typeorm';

export class CreateLanguagesTable1689088093868 implements MigrationInterface {
    name = 'CreateLanguagesTable1689088093868';

    public async up(queryRunner: QueryRunner): Promise<void> {
        await queryRunner.query(`
            CREATE TABLE "Languages" (
                "Id" int NOT NULL IDENTITY(1,1),
                "ISO639_3Code" char(3) NOT NULL,
                "EnglishDisplay" nvarchar(255) NOT NULL,
                "GeoLocation" nvarchar(255),
                "IsPriority" bit NOT NULL CONSTRAINT "DF_589b4c29c8056747622d0eac1c2" DEFAULT 0,
                "CreateDate" datetime2 NOT NULL CONSTRAINT "DF_aca7d2462c98a986602286c0daa" DEFAULT getdate(),
                "UpdateDate" datetime2 NOT NULL CONSTRAINT "DF_bb3b80cf5b7410f3868eac9f62a" DEFAULT getdate(),
                CONSTRAINT "PK_401c136e67b10c2e96388bc0d84" PRIMARY KEY ("Id")

            )
        `);
    }

    public async down(queryRunner: QueryRunner): Promise<void> {
        await queryRunner.query(`DROP TABLE "Languages"`);
    }
}
