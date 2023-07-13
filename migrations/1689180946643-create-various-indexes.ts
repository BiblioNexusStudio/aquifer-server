import { MigrationInterface, QueryRunner } from 'typeorm';

export class CreateVariousIndexes1689180946643 implements MigrationInterface {
    name = 'CreateVariousIndexes1689180946643';

    public async up(queryRunner: QueryRunner): Promise<void> {
        await queryRunner.query(
            `ALTER TABLE "SupportingResources" DROP CONSTRAINT "FK_2d393c7e2413914939bf38cdaf7"`,
        );
        await queryRunner.query(
            `ALTER TABLE "ResourceContent" DROP CONSTRAINT "FK_36f66009a0366b136878db54cfe"`,
        );
        await queryRunner.query(
            `ALTER TABLE "ResourceContent" DROP CONSTRAINT "FK_98cad76605f34b0b08940e4fcd2"`,
        );
        await queryRunner.query(
            `ALTER TABLE "ResourceContent" ALTER COLUMN "ResourceId" int NOT NULL`,
        );
        await queryRunner.query(
            `ALTER TABLE "ResourceContent" ALTER COLUMN "LanguageId" int NOT NULL`,
        );
        await queryRunner.query(
            `CREATE UNIQUE INDEX "IDX_75e32d07ba8d912ceb550eae0f" ON "Resources" ("Type", "Tag") `,
        );
        await queryRunner.query(
            `CREATE UNIQUE INDEX "IDX_c96f0d3bf29f408604314b3a87" ON "ResourceContent" ("ResourceId", "LanguageId") `,
        );
        await queryRunner.query(
            `CREATE UNIQUE INDEX "IDX_735d195df9be041ea39a62ab94" ON "Passages" ("Type", "StartBnVerse", "EndBnVerse") `,
        );
        await queryRunner.query(
            `ALTER TABLE "ResourceContent" ADD CONSTRAINT "FK_36f66009a0366b136878db54cfe" FOREIGN KEY ("ResourceId") REFERENCES "Resources"("Id") ON DELETE NO ACTION ON UPDATE NO ACTION`,
        );
        await queryRunner.query(
            `ALTER TABLE "ResourceContent" ADD CONSTRAINT "FK_98cad76605f34b0b08940e4fcd2" FOREIGN KEY ("LanguageId") REFERENCES "Languages"("Id") ON DELETE NO ACTION ON UPDATE NO ACTION`,
        );
        await queryRunner.query(
            `ALTER TABLE "SupportingResources" ADD CONSTRAINT "FK_2d393c7e2413914939bf38cdaf7" FOREIGN KEY ("ResourceId") REFERENCES "Resources"("Id") ON DELETE CASCADE ON UPDATE CASCADE`,
        );
    }

    public async down(queryRunner: QueryRunner): Promise<void> {
        await queryRunner.query(
            `ALTER TABLE "SupportingResources" DROP CONSTRAINT "FK_2d393c7e2413914939bf38cdaf7"`,
        );
        await queryRunner.query(
            `ALTER TABLE "ResourceContent" DROP CONSTRAINT "FK_98cad76605f34b0b08940e4fcd2"`,
        );
        await queryRunner.query(
            `ALTER TABLE "ResourceContent" DROP CONSTRAINT "FK_36f66009a0366b136878db54cfe"`,
        );
        await queryRunner.query(`DROP INDEX "IDX_735d195df9be041ea39a62ab94" ON "Passages"`);
        await queryRunner.query(`DROP INDEX "IDX_c96f0d3bf29f408604314b3a87" ON "ResourceContent"`);
        await queryRunner.query(`DROP INDEX "IDX_75e32d07ba8d912ceb550eae0f" ON "Resources"`);
        await queryRunner.query(`ALTER TABLE "ResourceContent" ALTER COLUMN "LanguageId" int`);
        await queryRunner.query(`ALTER TABLE "ResourceContent" ALTER COLUMN "ResourceId" int`);
        await queryRunner.query(
            `ALTER TABLE "ResourceContent" ADD CONSTRAINT "FK_98cad76605f34b0b08940e4fcd2" FOREIGN KEY ("LanguageId") REFERENCES "Languages"("Id") ON DELETE NO ACTION ON UPDATE NO ACTION`,
        );
        await queryRunner.query(
            `ALTER TABLE "ResourceContent" ADD CONSTRAINT "FK_36f66009a0366b136878db54cfe" FOREIGN KEY ("ResourceId") REFERENCES "Resources"("Id") ON DELETE NO ACTION ON UPDATE NO ACTION`,
        );
        await queryRunner.query(
            `ALTER TABLE "SupportingResources" ADD CONSTRAINT "FK_2d393c7e2413914939bf38cdaf7" FOREIGN KEY ("ResourceId") REFERENCES "Resources"("Id") ON DELETE NO ACTION ON UPDATE NO ACTION`,
        );
    }
}
