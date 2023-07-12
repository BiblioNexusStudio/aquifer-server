import { Repository } from 'typeorm';

type NonEmptyArray<T> = [T, ...T[]];

// this function is meant to emulate an upsert, which doesn't work with
// TypeORM's MSSQL adapter. this version is not atomic, since it's possible for
// the queried data to be stale at the time of `save`, but given the low write
// volume Aquifer expects, this should suffice.
export async function upsertEntityUsingRepository<T>(
    entity: T,
    uniqueColumns: NonEmptyArray<keyof T>,
    repository: Repository<T>,
): Promise<T> {
    if (uniqueColumns.length === 0) {
        throw new Error(
            'upsertEntityUsingRepository must be called with at least one unique column or you could inadvertently update random rows',
        );
    }

    const uniqueSubset = uniqueColumns.reduce((acc, columnName) => {
        const value = entity[columnName];
        if (isObject(value) && 'id' in (value as object) && Object.keys(value).length > 1) {
            throw new Error(
                'When upserting an entity with relations, make sure to only put `id` on the relation to prevent unexpected behavior',
            );
        }
        return { ...acc, [columnName]: value };
    }, {});

    const existing = await repository.findOneBy(uniqueSubset);

    if (existing) {
        const columns = columnNames(repository);

        columns.forEach((column) => {
            if (column !== 'id') {
                existing[column] = entity[column];
            }
        });
        return await repository.save(existing);
    } else {
        return await repository.save(entity);
    }
}

function columnNames<T>(repository: Repository<T>): string[] {
    return repository.metadata.columns
        .filter(
            ({ isCreateDate, isUpdateDate, isVersion }) =>
                !isCreateDate && !isUpdateDate && !isVersion,
        )
        .map(({ propertyName }) => propertyName);
}

function isObject(maybeObject: any): boolean {
    return typeof maybeObject === 'object' && !Array.isArray(maybeObject) && maybeObject !== null;
}
