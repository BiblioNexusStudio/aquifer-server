import { MigrationInterface, QueryRunner } from 'typeorm';

export class CreateResourcesTable1689023573563 implements MigrationInterface {
    name = 'CreateResourcesTable1689023573563';

    public async up(queryRunner: QueryRunner): Promise<void> {
        await queryRunner.query(`
            CREATE TABLE "Resources" (
                "Id" int NOT NULL IDENTITY(1,1),
                "Type" int NOT NULL,
                "EnglishLabel" nvarchar(255) NOT NULL,
                "Tag" nvarchar(255),
                "CreateDate" datetime2 NOT NULL CONSTRAINT "DF_c08ed274b73b83bca9ecbaa3b1f" DEFAULT getdate(),
                "UpdateDate" datetime2 NOT NULL CONSTRAINT "DF_7ea2108544534cc91640d012995" DEFAULT getdate(),
                CONSTRAINT "PK_c1c3a9094a400a1206b18666e93" PRIMARY KEY ("Id")
            )
        `);
    }

    public async down(queryRunner: QueryRunner): Promise<void> {
        await queryRunner.query(`DROP TABLE "Resources"`);
    }
}
