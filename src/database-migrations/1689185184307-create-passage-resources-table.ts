import { MigrationInterface, QueryRunner } from 'typeorm';

export class CreatePassageResourcesTable1689185184307 implements MigrationInterface {
    name = 'CreatePassageResourcesTable1689185184307';

    public async up(queryRunner: QueryRunner): Promise<void> {
        await queryRunner.query(
            `CREATE TABLE "PassageResources" ("PassageId" int NOT NULL, "ResourceId" int NOT NULL, CONSTRAINT "PK_486fc8bc62e88353139f9d01e94" PRIMARY KEY ("PassageId", "ResourceId"))`,
        );
        await queryRunner.query(
            `CREATE INDEX "IDX_83b6c64555d5730b0a90ebc10a" ON "PassageResources" ("PassageId") `,
        );
        await queryRunner.query(
            `CREATE INDEX "IDX_945fa914eb70df21d45828eba7" ON "PassageResources" ("ResourceId") `,
        );
        await queryRunner.query(
            `ALTER TABLE "ResourceContent" ADD CONSTRAINT "DF_5862f396d89e8bc15271b4d9ec7" DEFAULT 1 FOR "CurrentVersion"`,
        );
        await queryRunner.query(
            `ALTER TABLE "PassageResources" ADD CONSTRAINT "FK_83b6c64555d5730b0a90ebc10ad" FOREIGN KEY ("PassageId") REFERENCES "Passages"("Id") ON DELETE NO ACTION ON UPDATE NO ACTION`,
        );
        await queryRunner.query(
            `ALTER TABLE "PassageResources" ADD CONSTRAINT "FK_945fa914eb70df21d45828eba71" FOREIGN KEY ("ResourceId") REFERENCES "Resources"("Id") ON DELETE CASCADE ON UPDATE CASCADE`,
        );
    }

    public async down(queryRunner: QueryRunner): Promise<void> {
        await queryRunner.query(
            `ALTER TABLE "PassageResources" DROP CONSTRAINT "FK_945fa914eb70df21d45828eba71"`,
        );
        await queryRunner.query(
            `ALTER TABLE "PassageResources" DROP CONSTRAINT "FK_83b6c64555d5730b0a90ebc10ad"`,
        );
        await queryRunner.query(
            `ALTER TABLE "ResourceContent" DROP CONSTRAINT "DF_5862f396d89e8bc15271b4d9ec7"`,
        );
        await queryRunner.query(
            `DROP INDEX "IDX_945fa914eb70df21d45828eba7" ON "PassageResources"`,
        );
        await queryRunner.query(
            `DROP INDEX "IDX_83b6c64555d5730b0a90ebc10a" ON "PassageResources"`,
        );
        await queryRunner.query(`DROP TABLE "PassageResources"`);
    }
}
