import { MigrationInterface, QueryRunner } from 'typeorm';

export class CreateSupportingResourcesTable1689089042246 implements MigrationInterface {
    name = 'CreateSupportingResourcesTable1689089042246';

    public async up(queryRunner: QueryRunner): Promise<void> {
        await queryRunner.query(
            `CREATE TABLE "SupportingResources" (
                "ParentResourceId" int NOT NULL,
                "ResourceId" int NOT NULL,
                CONSTRAINT "PK_254b015c300d1daee92afb04caa" PRIMARY KEY ("ParentResourceId", "ResourceId")
            )`,
        );
        await queryRunner.query(
            `CREATE INDEX "IDX_83dd6dd48eb0e1bd5a7a8aef5c" ON "SupportingResources" ("ParentResourceId") `,
        );
        await queryRunner.query(
            `CREATE INDEX "IDX_2d393c7e2413914939bf38cdaf" ON "SupportingResources" ("ResourceId") `,
        );
        await queryRunner.query(
            `ALTER TABLE "SupportingResources" ADD CONSTRAINT "FK_83dd6dd48eb0e1bd5a7a8aef5cb" FOREIGN KEY ("ParentResourceId") REFERENCES "Resources"("Id") ON DELETE NO ACTION ON UPDATE NO ACTION`,
        );
        await queryRunner.query(
            `ALTER TABLE "SupportingResources" ADD CONSTRAINT "FK_2d393c7e2413914939bf38cdaf7" FOREIGN KEY ("ResourceId") REFERENCES "Resources"("Id") ON DELETE NO ACTION ON UPDATE NO ACTION`,
        );
    }

    public async down(queryRunner: QueryRunner): Promise<void> {
        await queryRunner.query(
            `ALTER TABLE "SupportingResources" DROP CONSTRAINT "FK_2d393c7e2413914939bf38cdaf7"`,
        );
        await queryRunner.query(
            `ALTER TABLE "SupportingResources" DROP CONSTRAINT "FK_83dd6dd48eb0e1bd5a7a8aef5cb"`,
        );
        await queryRunner.query(
            `DROP INDEX "IDX_2d393c7e2413914939bf38cdaf" ON "SupportingResources"`,
        );
        await queryRunner.query(
            `DROP INDEX "IDX_83dd6dd48eb0e1bd5a7a8aef5c" ON "SupportingResources"`,
        );
        await queryRunner.query(`DROP TABLE "SupportingResources"`);
    }
}
