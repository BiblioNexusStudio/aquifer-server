import { MigrationInterface, QueryRunner } from 'typeorm';

export class CreateResourceContentTable1689088601549 implements MigrationInterface {
    name = 'CreateResourceContentTable1689088601549';

    public async up(queryRunner: QueryRunner): Promise<void> {
        await queryRunner.query(`
            CREATE TABLE "ResourceContent" (
                "Id" int NOT NULL IDENTITY(1,1),
                "DisplayName" nvarchar(255) NOT NULL,
                "Summary" nvarchar(255),
                "CurrentVersion" int NOT NULL,
                "IsComplete" bit NOT NULL,
                "IsTrusted" bit NOT NULL,
                "Content" nvarchar(max) NOT NULL,
                "CreateDate" datetime2 NOT NULL CONSTRAINT "DF_12e9a1c726421447fc18a2faa6a" DEFAULT getdate(),
                "UpdateDate" datetime2 NOT NULL CONSTRAINT "DF_304e581960da779bd60ee8f46fe" DEFAULT getdate(),
                "ResourceId" int,
                "LanguageId" int,
                CONSTRAINT "PK_846e6a2ad3d60e9b79e6f4bc82c" PRIMARY KEY ("Id")
            )
        `);
        await queryRunner.query(
            `ALTER TABLE "ResourceContent" ADD CONSTRAINT "FK_36f66009a0366b136878db54cfe" FOREIGN KEY ("ResourceId") REFERENCES "Resources"("Id") ON DELETE NO ACTION ON UPDATE NO ACTION`,
        );
        await queryRunner.query(
            `ALTER TABLE "ResourceContent" ADD CONSTRAINT "FK_98cad76605f34b0b08940e4fcd2" FOREIGN KEY ("LanguageId") REFERENCES "Languages"("Id") ON DELETE NO ACTION ON UPDATE NO ACTION`,
        );
    }

    public async down(queryRunner: QueryRunner): Promise<void> {
        await queryRunner.query(
            `ALTER TABLE "ResourceContent" DROP CONSTRAINT "FK_98cad76605f34b0b08940e4fcd2"`,
        );
        await queryRunner.query(
            `ALTER TABLE "ResourceContent" DROP CONSTRAINT "FK_36f66009a0366b136878db54cfe"`,
        );
        await queryRunner.query(`DROP TABLE "ResourceContent"`);
    }
}
