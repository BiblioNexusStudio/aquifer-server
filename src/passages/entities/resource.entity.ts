import {
    Entity,
    Column,
    UpdateDateColumn,
    CreateDateColumn,
    PrimaryGeneratedColumn,
    JoinTable,
    ManyToMany,
} from 'typeorm';

@Entity({ name: 'Resources' })
export class Resource {
    @PrimaryGeneratedColumn({ name: 'Id' })
    id: number;

    @Column({ type: 'int', name: 'Type' })
    type: ResourceType;

    @ManyToMany(() => Resource)
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
    CCBT_ER = 0,
}
