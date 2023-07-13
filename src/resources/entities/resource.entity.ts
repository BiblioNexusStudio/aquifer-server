import {
    Entity,
    Column,
    UpdateDateColumn,
    CreateDateColumn,
    PrimaryGeneratedColumn,
    JoinTable,
    ManyToMany,
    Index,
    OneToMany,
} from 'typeorm';
import { BNBaseEntity } from '../../utils/bn-base-entity';
import { ResourceContent } from './resource-content.entity';

@Entity({ name: 'Resources' })
@Index(['type', 'tag'], { unique: true })
export class Resource extends BNBaseEntity {
    @PrimaryGeneratedColumn({ name: 'Id' })
    id: number;

    @Column({ type: 'int', name: 'Type' })
    type: ResourceTypeInt;

    @ManyToMany(() => Resource, { onDelete: 'NO ACTION', onUpdate: 'NO ACTION' })
    @JoinTable({
        name: 'SupportingResources',
        joinColumn: { name: 'ParentResourceId' },
        inverseJoinColumn: { name: 'ResourceId' },
    })
    supportingResources: Resource[];

    @OneToMany(() => ResourceContent, (resourceContent) => resourceContent.resource)
    resourceContents: ResourceContent[];

    @Column({ name: 'EnglishLabel' })
    englishLabel: string;

    @Column({ name: 'Tag', nullable: true })
    tag: string | undefined;

    @CreateDateColumn({ name: 'CreateDate' })
    createDate: Date;

    @UpdateDateColumn({ name: 'UpdateDate' })
    updateDate: Date;
}

export enum ResourceTypeInt {
    CBBT_ER = 0,
}

export type ResourceType = keyof typeof ResourceTypeInt;
