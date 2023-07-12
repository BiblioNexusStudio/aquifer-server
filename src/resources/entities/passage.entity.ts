import { bnVerseToBookChapterVerse } from '../../utils/bn-verse-mapper';
import {
    Entity,
    Column,
    UpdateDateColumn,
    CreateDateColumn,
    PrimaryGeneratedColumn,
    Index,
} from 'typeorm';

@Entity({ name: 'Passages' })
@Index(['type', 'startBnVerse', 'endBnVerse'], { unique: true })
export class Passage {
    @PrimaryGeneratedColumn({ name: 'Id' })
    id: number;

    @Column({ type: 'int', name: 'Type' })
    type: PassageType;

    @Column({ name: 'StartBnVerse' })
    startBnVerse: number;

    @Column({ name: 'EndBnVerse' })
    endBnVerse: number;

    @CreateDateColumn({ name: 'CreateDate' })
    createDate: Date;

    @UpdateDateColumn({ name: 'UpdateDate' })
    updateDate: Date;

    toString(): string {
        const start = bnVerseToBookChapterVerse(this.startBnVerse);
        const end = bnVerseToBookChapterVerse(this.endBnVerse);
        return `${start}-${end}`;
    }
}

export enum PassageType {
    CBBT_ER = 0,
}
