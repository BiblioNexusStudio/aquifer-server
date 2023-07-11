import {
    Entity,
    Column,
    UpdateDateColumn,
    CreateDateColumn,
    PrimaryGeneratedColumn,
    ManyToOne,
    JoinColumn,
} from 'typeorm';
import { Language } from './language.entity';
import { Resource } from './resource.entity';

@Entity({ name: 'ResourceContent' })
export class ResourceContent {
    @PrimaryGeneratedColumn({ name: 'Id' })
    id: number;

    @ManyToOne(() => Resource)
    @JoinColumn({ name: 'ResourceId' })
    resource: Resource;

    @ManyToOne(() => Language)
    @JoinColumn({ name: 'LanguageId' })
    language: Language;

    @Column({ name: 'DisplayName' })
    displayName: string;

    @Column({ name: 'Summary', nullable: true })
    summary: string | undefined;

    @Column({ name: 'CurrentVersion' })
    currentVersion: number;

    @Column({ name: 'IsComplete' })
    isComplete: boolean;

    @Column({ name: 'IsTrusted' })
    isTrusted: boolean;

    @Column({ name: 'Content', type: 'nvarchar', length: 'max' })
    content: string;

    @CreateDateColumn({ name: 'CreateDate' })
    createDate: Date;

    @UpdateDateColumn({ name: 'UpdateDate' })
    updateDate: Date;
}
