import { MigrationInterface, QueryRunner } from 'typeorm';

export class CreatePassagesTable1689109348382 implements MigrationInterface {
    name = 'CreatePassagesTable1689109348382';

    public async up(queryRunner: QueryRunner): Promise<void> {
        await queryRunner.query(`
            CREATE TABLE "Passages" (
                "Id" int NOT NULL IDENTITY(1,1),
                "Type" int NOT NULL,
                "StartBnVerse" int NOT NULL,
                "EndBnVerse" int NOT NULL,
                "CreateDate" datetime2 NOT NULL CONSTRAINT "DF_5e72eb2f6a7dc3386d43e065698" DEFAULT getdate(),
                "UpdateDate" datetime2 NOT NULL CONSTRAINT "DF_4b2fe2cdf9c4debe5e7b82c2622" DEFAULT getdate(),
                CONSTRAINT "PK_0ae622814714c4913b95cc37310" PRIMARY KEY ("Id")
            )
        `);
    }

    public async down(queryRunner: QueryRunner): Promise<void> {
        await queryRunner.query(`DROP TABLE "Passages"`);
    }
}
