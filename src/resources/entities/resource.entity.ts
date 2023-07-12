import {
    Entity,
    Column,
    UpdateDateColumn,
    CreateDateColumn,
    PrimaryGeneratedColumn,
    JoinTable,
    ManyToMany,
    Index,
} from 'typeorm';

@Entity({ name: 'Resources' })
@Index(['type', 'tag'], { unique: true })
export class Resource {
    @PrimaryGeneratedColumn({ name: 'Id' })
    id: number;

    @Column({ type: 'int', name: 'Type' })
    type: ResourceType;

    @ManyToMany(() => Resource, { onDelete: 'NO ACTION', onUpdate: 'NO ACTION' })
    @JoinTable({
        name: 'SupportingResources',
        joinColumn: { name: 'ParentResourceId' },
        inverseJoinColumn: { name: 'ResourceId' },
    })
    supportingResources: Resource[];

    @Column({ name: 'EnglishLabel' })
    englishLabel: string;

    @Column({ name: 'Tag', nullable: true })
    tag: string | undefined;

    @CreateDateColumn({ name: 'CreateDate' })
    createDate: Date;

    @UpdateDateColumn({ name: 'UpdateDate' })
    updateDate: Date;
}

export enum ResourceType {
    CBBT_ER = 0,
}
