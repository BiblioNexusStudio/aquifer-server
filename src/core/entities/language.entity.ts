import {
    Entity,
    Column,
    UpdateDateColumn,
    CreateDateColumn,
    PrimaryGeneratedColumn,
} from 'typeorm';

@Entity({ name: 'Languages' })
export class Language {
    @PrimaryGeneratedColumn({ name: 'Id' })
    id: number;

    @Column({ name: 'EnglishDisplay' })
    englishDisplay: string;

    @Column({ type: 'char', length: 3, name: 'ISO639_3Code' })
    ISO639_3Code: string;

    @Column({ name: 'GeoLocation', nullable: true })
    geoLocation: string | undefined;

    @Column({ name: 'IsPriority', default: false })
    isPriority: boolean;

    @CreateDateColumn({ name: 'CreateDate' })
    createDate: Date;

    @UpdateDateColumn({ name: 'UpdateDate' })
    updateDate: Date;
}
