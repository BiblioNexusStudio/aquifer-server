import { Language } from '../../core/entities/language.entity';
import {
    Entity,
    Column,
    UpdateDateColumn,
    CreateDateColumn,
    PrimaryGeneratedColumn,
    ManyToOne,
    JoinColumn,
    Index,
    VersionColumn,
} from 'typeorm';
import { Resource } from './resource.entity';

@Entity({ name: 'ResourceContent' })
@Index(['resource', 'language'], { unique: true })
export class ResourceContent {
    @PrimaryGeneratedColumn({ name: 'Id' })
    id: number;

    @ManyToOne(() => Resource, { nullable: false })
    @JoinColumn({ name: 'ResourceId' })
    resource: Resource;

    @ManyToOne(() => Language, { nullable: false })
    @JoinColumn({ name: 'LanguageId' })
    language: Language;

    @Column({ name: 'DisplayName' })
    displayName: string;

    @Column({ name: 'Summary', nullable: true })
    summary: string | undefined;

    @VersionColumn({ name: 'CurrentVersion', default: 1 })
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
