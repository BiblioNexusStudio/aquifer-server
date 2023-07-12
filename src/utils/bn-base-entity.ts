import { BaseEntity as TypeOrmBaseEntity } from 'typeorm';

export class BNBaseEntity extends TypeOrmBaseEntity {
    async loadRelation<T>(relationKey: string, shouldLoadMany?: boolean): Promise<T> {
        if (relationKey.trim().length === 0) {
            throw new Error('Cannot load empty relation.');
        }

        shouldLoadMany = shouldLoadMany ?? relationKey.endsWith('s');

        const staticAccessor = this.constructor as any;
        const relationQuery = staticAccessor
            .createQueryBuilder()
            .relation(staticAccessor, relationKey)
            .of(this);
        const relationValue: T = shouldLoadMany
            ? await relationQuery.loadMany()
            : await relationQuery.loadOne();

        this[relationKey] = relationValue;

        return relationValue;
    }
}
